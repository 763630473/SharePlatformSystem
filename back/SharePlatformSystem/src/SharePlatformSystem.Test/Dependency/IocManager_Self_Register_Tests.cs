using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Tests.Dependency
{
    public class IocManager_Self_Register_Tests : TestBaseWithLocalIocManager
    {
        [Test]
        public void Should_Self_Register_With_All_Interfaces()
        {
            var registrar = LocalIocManager.Resolve<IIocRegistrar>();
            var resolver = LocalIocManager.Resolve<IIocResolver>();
            var managerByInterface = LocalIocManager.Resolve<IIocManager>();
            var managerByClass = LocalIocManager.Resolve<IocManager>();

            managerByClass.ShouldBeSameAs(registrar);
            managerByClass.ShouldBeSameAs(resolver);
            managerByClass.ShouldBeSameAs(managerByInterface);
        }
    }
}