using System.Linq;
using Shouldly;
using SharePlatformSystem.Core.PlugIns;
using NUnit.Framework;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core;

namespace SharePlatformSystem.Tests.Modules
{
    public class StartupModuleToBeLast_Tests : TestBaseWithLocalIocManager
    {
        [Test]
        public void StartupModule_ShouldBe_LastModule()
        {
            //Arrange
            var bootstrapper = SharePlatformBootstrapper.Create<MyStartupModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });
            bootstrapper.Initialize();

            //Act
            var modules = bootstrapper.IocManager.Resolve<ISharePlatformModuleManager>().Modules;

            //Assert
            modules.Count.ShouldBe(4);

            modules.Any(m => m.Type == typeof(SharePlatformKernelModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyStartupModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyModule1)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyModule2)).ShouldBeTrue();

            var startupModule = modules.Last();

            startupModule.Type.ShouldBe(typeof(MyStartupModule));
        }

        [Test]
        public void PluginModule_ShouldNotBeLast()
        {
            var bootstrapper = SharePlatformBootstrapper.Create<MyStartupModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });

            bootstrapper.PlugInSources.AddTypeList(typeof(MyPlugInModule));

            bootstrapper.Initialize();

            var modules = bootstrapper.IocManager.Resolve<ISharePlatformModuleManager>().Modules;

            //Assert
            modules.Count.ShouldBe(6);

            modules.Any(m => m.Type == typeof(SharePlatformKernelModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyStartupModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyModule1)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyModule2)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyPlugInModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyPlugInDependedModule)).ShouldBeTrue();

            modules.Last().Type.ShouldBe(typeof(MyStartupModule));
        }

        [DependsOn(typeof(MyModule1), typeof(MyModule2))]
        public class MyStartupModule : SharePlatformModule { }

        public class MyModule1 : SharePlatformModule { }

        public class MyModule2 : SharePlatformModule { }

        [DependsOn(typeof(MyPlugInDependedModule))]
        public class MyPlugInModule : SharePlatformModule { }

        public class MyPlugInDependedModule : SharePlatformModule { }
    }
}
