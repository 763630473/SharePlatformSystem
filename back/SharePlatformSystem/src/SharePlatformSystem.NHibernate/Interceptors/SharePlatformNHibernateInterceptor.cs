using NHibernate;
using NHibernate.Type;
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Timing;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Events.Bus;
using SharePlatformSystem.Events.Bus.Entities;
using SharePlatformSystem.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem.NHibernate.Interceptors
{
    internal class SharePlatformNHibernateInterceptor : EmptyInterceptor
    {
        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

        private readonly IIocManager _iocManager;
        private readonly Lazy<ISharePlatformSession> _sharePlatformSession;
        private readonly Lazy<IGuidGenerator> _guidGenerator;
        private readonly Lazy<IEventBus> _eventBus;

        public SharePlatformNHibernateInterceptor(IIocManager iocManager)
        {
            _iocManager = iocManager;

            _sharePlatformSession =
                new Lazy<ISharePlatformSession>(
                    () => _iocManager.IsRegistered(typeof(ISharePlatformSession))
                        ? _iocManager.Resolve<ISharePlatformSession>()
                        : NullSharePlatformSession.Instance,
                    isThreadSafe: true
                    );
            _guidGenerator =
                new Lazy<IGuidGenerator>(
                    () => _iocManager.IsRegistered(typeof(IGuidGenerator))
                        ? _iocManager.Resolve<IGuidGenerator>()
                        : SequentialGuidGenerator.Instance,
                    isThreadSafe: true
                    );

            _eventBus =
                new Lazy<IEventBus>(
                    () => _iocManager.IsRegistered(typeof(IEventBus))
                        ? _iocManager.Resolve<IEventBus>()
                        : NullEventBus.Instance,
                    isThreadSafe: true
                );
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            //为guid设置ID
            if (entity is IEntity<Guid>)
            {
                var guidEntity = entity as IEntity<Guid>;
                if (guidEntity.IsTransient())
                {
                    guidEntity.Id = _guidGenerator.Value.Create();
                }
            }
            if (entity is IEntity<string>)
            {
                var guidEntity = entity as IEntity<string>;
                if (guidEntity.IsTransient())
                {
                    guidEntity.Id = _guidGenerator.Value.Create().ToString();
                }
            }
            //为新实体设置CreationTime
            if (entity is IHasCreationTime)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "CreationTime")
                    {
                        state[i] = (entity as IHasCreationTime).CreationTime = Clock.Now;
                    }
                }
            }

            //为新实体设置CreatorUserID
            if (entity is ICreationAudited<string> &&!string.IsNullOrWhiteSpace( _sharePlatformSession.Value.UserId))
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "CreatorUserId")
                    {
                        state[i] = (entity as ICreationAudited<string>).CreatorUserId = _sharePlatformSession.Value.UserId;
                    }
                }
            }

            EntityChangeEventHelper.TriggerEntityCreatingEvent(entity);
            EntityChangeEventHelper.TriggerEntityCreatedEventOnUowCompleted(entity);

            TriggerDomainEvents(entity);

            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            var updated = false;

            //修改审计
            if (entity is IHasModificationTime)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "LastModificationTime")
                    {
                        currentState[i] = (entity as IHasModificationTime).LastModificationTime = Clock.Now;
                        updated = true;
                    }
                }
            }

            if (entity is IModificationAudited<string> &&!_sharePlatformSession.Value.UserId.IsNullOrWhiteSpace())
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "LastModifierUserId")
                    {
                        currentState[i] = (entity as IModificationAudited<string>).LastModifierUserId = _sharePlatformSession.Value.UserId;
                        updated = true;
                    }
                }
            }

            if (entity is ISoftDelete && entity.As<ISoftDelete>().IsDeleted)
            {
                //以前删除过吗？通常，一个被删除的实体不应该稍后更新，但我更喜欢检查它。
                var previousIsDeleted = false;
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "IsDeleted")
                    {
                        previousIsDeleted = (bool)previousState[i];
                        break;
                    }
                }

                if (!previousIsDeleted)
                {
                    //设置删除时间
                    if (entity is IHasDeletionTime)
                    {
                        for (var i = 0; i < propertyNames.Length; i++)
                        {
                            if (propertyNames[i] == "DeletionTime")
                            {
                                currentState[i] = (entity as IHasDeletionTime).DeletionTime = Clock.Now;
                                updated = true;
                            }
                        }
                    }

                    //设置删除用户ID
                    if (entity is IDeletionAudited<string> && !_sharePlatformSession.Value.UserId.IsNullOrWhiteSpace())
                    {
                        for (var i = 0; i < propertyNames.Length; i++)
                        {
                            if (propertyNames[i] == "DeleterUserId")
                            {
                                currentState[i] = (entity as IDeletionAudited<string>).DeleterUserId = _sharePlatformSession.Value.UserId;
                                updated = true;
                            }
                        }
                    }

                    EntityChangeEventHelper.TriggerEntityDeletingEvent(entity);
                    EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entity);
                }
                else
                {
                    EntityChangeEventHelper.TriggerEntityUpdatingEvent(entity);
                    EntityChangeEventHelper.TriggerEntityUpdatedEventOnUowCompleted(entity);
                }
            }
            else
            {
                EntityChangeEventHelper.TriggerEntityUpdatingEvent(entity);
                EntityChangeEventHelper.TriggerEntityUpdatedEventOnUowCompleted(entity);
            }

            TriggerDomainEvents(entity);

            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types) || updated;
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            EntityChangeEventHelper.TriggerEntityDeletingEvent(entity);
            EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entity);

            TriggerDomainEvents(entity);

            base.OnDelete(entity, id, state, propertyNames, types);
        }

        protected virtual void TriggerDomainEvents(object entityAsObj)
        {
            var generatesDomainEventsEntity = entityAsObj as IGeneratesDomainEvents;
            if (generatesDomainEventsEntity == null)
            {
                return;
            }

            if (generatesDomainEventsEntity.DomainEvents.IsNullOrEmpty())
            {
                return;
            }

            var domainEvents = generatesDomainEventsEntity.DomainEvents.ToList();
            generatesDomainEventsEntity.DomainEvents.Clear();

            foreach (var domainEvent in domainEvents)
            {
                _eventBus.Value.Trigger(domainEvent.GetType(), entityAsObj, domainEvent);
            }
        }

        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            if (entity.GetType().IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
            {
                return true;
            }

            NormalizeDateTimePropertiesForEntity(entity, state, propertyNames, types);
            return true;
        }

        private static void NormalizeDateTimePropertiesForEntity(object entity, object[] state, string[] propertyNames, IList<IType> types)
        {
            for (var i = 0; i < types.Count; i++)
            {
                var prop = entity.GetType().GetProperty(propertyNames[i]);
                if (prop != null && prop.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
                {
                    continue;
                }

                if (types[i].IsComponentType)
                {
                    NormalizeDateTimePropertiesForComponentType(state[i], types[i]);
                }

                if (types[i].ReturnedClass != typeof(DateTime) && types[i].ReturnedClass != typeof(DateTime?))
                {
                    continue;
                }

                var dateTime = state[i] as DateTime?;

                if (!dateTime.HasValue)
                {
                    continue;
                }

                state[i] = Clock.Normalize(dateTime.Value);
            }
        }

        private static void NormalizeDateTimePropertiesForComponentType(object componentObject, IType type)
        {
            if (componentObject == null)
            {
                return;
            }

            var componentType = type as ComponentType;
            if (componentType == null)
            {
                return;
            }

            for (int i = 0; i < componentType.PropertyNames.Length; i++)
            {
                var propertyName = componentType.PropertyNames[i];
                if (componentType.Subtypes[i].IsComponentType)
                {
                    var prop = componentObject.GetType().GetProperty(propertyName);
                    if (prop == null)
                    {
                        continue;
                    }

                    if (prop.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
                    {
                        continue;
                    }

                    var value = prop.GetValue(componentObject, null);
                    NormalizeDateTimePropertiesForComponentType(value, componentType.Subtypes[i]);
                }

                if (componentType.Subtypes[i].ReturnedClass != typeof(DateTime) && componentType.Subtypes[i].ReturnedClass != typeof(DateTime?))
                {
                    continue;
                }

                var subProp = componentObject.GetType().GetProperty(propertyName);
                if (subProp == null)
                {
                    continue;
                }

                if (subProp.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
                {
                    continue;    
                }

                var dateTime = subProp.GetValue(componentObject) as DateTime?;

                if (!dateTime.HasValue)
                {
                    continue;
                }

                subProp.SetValue(componentObject, Clock.Normalize(dateTime.Value));
            }
        }
    }
}
