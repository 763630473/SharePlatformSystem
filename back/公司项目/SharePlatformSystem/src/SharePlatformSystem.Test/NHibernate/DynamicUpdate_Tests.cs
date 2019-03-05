using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Test.NHibernate.Entitys;
using Shouldly;

namespace SharePlatformSystem.Test.NHibernate
{
    public class DynamicUpdate_Tests : NHibernateTestBase
    {
        private readonly IRepository<TestEntity> _testRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DynamicUpdate_Tests()
        {
            _testRepository = Resolve<IRepository<TestEntity>>();
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            UsingSession(session => session.Save(new TestEntity { TestName = "Hitchhikers Guide to the Galaxy" }));
        }

        [Test]
        public void Should_Set_CreatorUserId_When_DynamicInsert_Is_Enabled()
        {
            SharePlatformSession.UserId = "1";

            using (var uow = _unitOfWorkManager.Begin())
            {
                var test = _testRepository.Get("1");
                test.ShouldNotBeNull();
                test.TestName = "Hitchhiker's Guide to the Galaxy";
                _testRepository.Update(test);
                uow.Complete();
            }

            var test2 = _testRepository.Get("1");
            test2.LastModifierUserId.ShouldNotBeNull();
        }
    }
}
