using System;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Tests.Domain.Entities
{
    public class EntityHelper_Tests
    {
        [Test]
        public void GetPrimaryKeyType_Tests()
        {
            EntityHelper.GetPrimaryKeyType<Manager>().ShouldBe(typeof(int));
            EntityHelper.GetPrimaryKeyType(typeof(Manager)).ShouldBe(typeof(int));
            EntityHelper.GetPrimaryKeyType(typeof(TestEntityWithGuidPk)).ShouldBe(typeof(Guid));
        }

        private class TestEntityWithGuidPk : Entity<Guid>
        {
            
        }
    }
}
