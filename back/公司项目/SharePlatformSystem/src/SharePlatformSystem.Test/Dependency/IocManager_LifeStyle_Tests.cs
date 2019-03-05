﻿using Castle.MicroKernel.Registration;
using Shouldly;
using NUnit.Framework;

namespace SharePlatformSystem.Tests.Dependency
{
    public class IocManager_LifeStyle_Tests : TestBaseWithLocalIocManager
    {
        [Test]
        public void Should_Call_Dispose_Of_Transient_Dependency_When_Object_Is_Released()
        {
            LocalIocManager.IocContainer.Register(
                Component.For<SimpleDisposableObject>().LifestyleTransient()
                );

            var obj = LocalIocManager.IocContainer.Resolve<SimpleDisposableObject>();

            LocalIocManager.IocContainer.Release(obj);

            obj.DisposeCount.ShouldBe(1);
        }

        [Test]
        public void Should_Call_Dispose_Of_Transient_Dependency_When_IocManager_Is_Disposed()
        {
            LocalIocManager.IocContainer.Register(
                Component.For<SimpleDisposableObject>().LifestyleTransient()
                );

            var obj = LocalIocManager.IocContainer.Resolve<SimpleDisposableObject>();

            LocalIocManager.Dispose();

            obj.DisposeCount.ShouldBe(1);
        }

        [Test]
        public void Should_Call_Dispose_Of_Singleton_Dependency_When_IocManager_Is_Disposed()
        {
            LocalIocManager.IocContainer.Register(
                Component.For<SimpleDisposableObject>().LifestyleSingleton()
                );

            var obj = LocalIocManager.IocContainer.Resolve<SimpleDisposableObject>();

            LocalIocManager.Dispose();

            obj.DisposeCount.ShouldBe(1);
        }
    }
}
