using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Tests.Dependency
{
    public class DisposableDependencyObjectWrapper_Tests : TestBaseWithLocalIocManager
    {
        [Test]
        public void ResolveAsDisposable_Should_Work()
        {
            LocalIocManager.Register<SimpleDisposableObject>(DependencyLifeStyle.Transient);

            SimpleDisposableObject simpleObj;

            using (var wrapper = LocalIocManager.ResolveAsDisposable<SimpleDisposableObject>())
            {
                wrapper.Object.ShouldNotBe(null);
                simpleObj = wrapper.Object;
            }

            simpleObj.DisposeCount.ShouldBe(1);
        }

        [Test]
        public void ResolveAsDisposable_With_Constructor_Args_Should_Work()
        {
            LocalIocManager.Register<SimpleDisposableObject>(DependencyLifeStyle.Transient);

            using (var wrapper = LocalIocManager.ResolveAsDisposable<SimpleDisposableObject>(new { myData = 42 }))
            {
                wrapper.Object.MyData.ShouldBe(42);
            }
        }

        [Test]
        public void Using_Test()
        {
            LocalIocManager.Register<SimpleDisposableObject>(DependencyLifeStyle.Transient);
            LocalIocManager.Using<SimpleDisposableObject>(obj => obj.MyData.ShouldBe(0));
        }
    }
}
