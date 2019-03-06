using NUnit.Framework;
using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Core.Domain.Uow;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Events.Bus;
using SharePlatformSystem.Events.Bus.Entities;
using SharePlatformSystem.Test.NHibernate.Entitys;
using Shouldly;
using System;
using System.Linq;

namespace SharePlatformSystem.Test.NHibernate
{
    public class Basic_Repository_Tests : NHibernateTestBase
    {
        private readonly IRepository<TestEntity> _testRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public Basic_Repository_Tests()
        {
            _testRepository = Resolve<IRepository<TestEntity>>();
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();

            UsingSession(session => session.Save(new TestEntity() { Id = Guid.NewGuid().ToString(), TestName = "emre" }));         
        }

        [Test]
        public void Should_Insert_People()
        {
            _testRepository.Insert(new TestEntity() { Id=Guid.NewGuid().ToString(),TestName = "halil" });

            var insertedPerson = UsingSession(session => session.Query<TestEntity>().FirstOrDefault(p => p.TestName == "halil"));
            insertedPerson.ShouldNotBe(null);
            insertedPerson.IsTransient().ShouldBe(false);
            insertedPerson.TestName.ShouldBe("halil");
        }

        [Test]
        public void Should_Filter_SoftDelete()
        {
            var tests = _testRepository.GetAllList();
            tests.All(p => !p.IsDeleted).ShouldBeTrue();
        }

        [Test]
        public void Should_Get_SoftDeleted_Entities_If_Filter_Is_Disabled()
        {
            using (_unitOfWorkManager.Begin())
            using (_unitOfWorkManager.Current.DisableFilter(SharePlatformDataFilters.SoftDelete))
            {
                var books = _testRepository.GetAllList();
                books.Any(x => x.IsDeleted).ShouldBe(false);
            }
        }

        [Test]
        public void Update_With_Action_Test()
        {
            var userBefore = UsingSession(session => session.Query<TestEntity>().Single(p => p.TestName == "emre"));

            var updatedUser = _testRepository.Update(userBefore.Id, user => user.TestName = "yunus");
            updatedUser.Id.ShouldBe(userBefore.Id);
            updatedUser.TestName.ShouldBe("yunus");

            var userAfter = UsingSession(session => session.Get<TestEntity>(userBefore.Id));
            userAfter.TestName.ShouldBe("yunus");
        }

        [Test]
        public void Should_Trigger_Event_On_Insert()
        {
            var triggerCount = 0;

            Resolve<IEventBus>().Register<EntityCreatedEventData<TestEntity>>(
                eventData =>
                {
                    eventData.Entity.Id = Guid.NewGuid().ToString();
                    eventData.Entity.TestName.ShouldBe("halil");
                    eventData.Entity.IsTransient().ShouldBe(false);
                    triggerCount++;
                });

            _testRepository.Insert(new TestEntity { Id= Guid.NewGuid().ToString(),TestName = "halil" });

            triggerCount.ShouldBe(1);
        }

        [Test]
        public void Should_Trigger_Event_On_Update()
        {
            var triggerCount = 0;

            Resolve<IEventBus>().Register<EntityUpdatedEventData<TestEntity>>(
                eventData =>
                {
                    eventData.Entity.TestName.ShouldBe("emre2");
                    triggerCount++;
                });

            var emrePeson = _testRepository.Single(p => p.TestName == "emre");
            emrePeson.TestName = "emre2";
            _testRepository.Update(emrePeson);

            triggerCount.ShouldBe(1);
        }

        [Test]
        public void Should_Trigger_Event_On_Delete()
        {
            var triggerCount = 0;
           
            var emrePesons = _testRepository.GetAllList(p => p.TestName == "emre");
            foreach(var emrePeson in emrePesons)
            {
                _testRepository.Delete(emrePeson.Id);
                Resolve<IEventBus>().Register<EntityDeletedEventData<TestEntity>>(
               eventData =>
               {
                   eventData.Entity.TestName.ShouldBe("emre");
                   triggerCount++;
               });

            }

            triggerCount.ShouldBe(emrePesons.Count());
            _testRepository.FirstOrDefault(p => p.TestName == "emre").ShouldBe(null);
        }
    }
}
