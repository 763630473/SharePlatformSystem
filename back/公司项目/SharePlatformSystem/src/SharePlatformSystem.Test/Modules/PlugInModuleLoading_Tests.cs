using System.Linq;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.PlugIns;

namespace SharePlatformSystem.Tests.Modules
{
    public class PlugInModuleLoading_Tests : TestBaseWithLocalIocManager
    {
        [Test]
        public void Should_Load_All_Modules()
        {
            //Arrange
            var bootstrapper = SharePlatformBootstrapper.Create<MyStartupModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });

            bootstrapper.PlugInSources.AddTypeList(typeof(MyPlugInModule));

            bootstrapper.Initialize();

            //Act
            var modules = bootstrapper.IocManager.Resolve<ISharePlatformModuleManager>().Modules;

            //Assert
            modules.Count.ShouldBe(6);

            modules.Any(m => m.Type == typeof(SharePlatformKernelModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyStartupModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyModule1)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyModule2)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyPlugInModule)).ShouldBeTrue();
            modules.Any(m => m.Type == typeof(MyPlugInDependedModule)).ShouldBeTrue();

            modules.Any(m => m.Type == typeof(MyNotDependedModule)).ShouldBeFalse();
        }

        [DependsOn(typeof(MyModule1), typeof(MyModule2))]
        public class MyStartupModule: SharePlatformModule
        {

        }

        public class MyModule1 : SharePlatformModule
        {
            
        }

        public class MyModule2 : SharePlatformModule
        {

        }
        
        public class MyNotDependedModule : SharePlatformModule
        {

        }

        [DependsOn(typeof(MyPlugInDependedModule))]
        public class MyPlugInModule : SharePlatformModule
        {
            
        }

        public class MyPlugInDependedModule : SharePlatformModule
        {
            
        }
    }
}
