using NHibernate;
using NHibernate.Linq;
using SharePlatformSystem.Collections.Extensions;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SharePlatformSystem.NHibernate.Repositories
{
    /// <summary>
    ///所有使用nhibernate的存储库的基类。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    public class NhRepositoryBase<TEntity, TPrimaryKey> : SharePlatformRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 获取执行数据库操作的nHibernate会话对象。
        /// </summary>
        public virtual ISession Session { get { return _sessionProvider.Session; } }

        private readonly ISessionProvider _sessionProvider;

        /// <summary>
        ///创建一个新的对象。
        /// </summary>
        /// <param name="sessionProvider">用于获取数据库操作会话的会话提供程序</param>
        public NhRepositoryBase(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Session.Query<TEntity>();
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors.IsNullOrEmpty())
            {
                return GetAll();
            }

            var query = GetAll();

            foreach (var propertySelector in propertySelectors)
            {
                //TODO: 测试NHibernate是否支持多次提取。
                query = query.Fetch(propertySelector);
            }

            return query;
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Session.Get<TEntity>(id);
        }

        public override TEntity Load(TPrimaryKey id)
        {
            return Session.Load<TEntity>(id);
        }

        public override TEntity Insert(TEntity entity)
        {
            Session.Save(entity);
            return entity;
        }

        public override TEntity InsertOrUpdate(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public override Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdate(entity));
        }

        public override TEntity Update(TEntity entity)
        {
            Session.Update(entity);
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
                Update(entity);
            }
            else
            {
                Session.Delete(entity);
            }
        }

        public override void Delete(TPrimaryKey id)
        {
            Delete(Session.Load<TEntity>(id));
        }
    }
}