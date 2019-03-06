using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Core.Timing;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Test.NHibernate.Entitys;
using Shouldly;

namespace SharePlatformSystem.Test.NHibernate
{
    public class SharePlatformNHibernateInterceptor_Tests : NHibernateTestBase
    {
        private readonly IRepository<TestEntity> _testRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SharePlatformNHibernateInterceptor_Tests()
        {
            _testRepository = Resolve<IRepository<TestEntity>>();
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();

            UsingSession(session =>
            {
                session.Save(new TestEntity
                {
                    TestName = "Villa Borghese",
                    CreationDate = new DateTime(2016, 04, 27, 14, 0, 0, DateTimeKind.Utc),
                    ModificationDate = new DateTime(2017, 04, 27, 14, 0, 0, DateTimeKind.Local),        
                });

            });     
            Clock.Provider = ClockProviders.Utc;
        }

        [Test]
        public void Normalize_DateTime_Kind_Properties_Test()
        {
            var test = _testRepository.GetAllList().First();
            test.CreationDate.Kind.ShouldBe(Clock.Kind);
            test.ModificationDate.Value.Kind.ShouldBe(Clock.Kind);         
        }

        [Test]
        public void Dont_Normalize_DateTime_Kind_Properties_Test()
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                var test = _testRepository.GetAllIncluding(o => o.Items).First();
                test.CreationTime.Kind.ShouldBe(DateTimeKind.Unspecified);

                foreach (var testDetail in test.Items)
                {
                    testDetail.CreationTime.Kind.ShouldBe(DateTimeKind.Unspecified);
                }

                uow.Complete();
            }
        }
    }
}
