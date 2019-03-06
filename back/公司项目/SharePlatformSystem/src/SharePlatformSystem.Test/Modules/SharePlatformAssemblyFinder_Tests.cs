using System.Linq;
using System.Reflection;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Core.Reflection.Extensions;
using static SharePlatformSystem.Tests.Modules.PlugInModuleLoading_Tests;

namespace SharePlatformSystem.Tests.Modules
{
    public class SharePlatformAssemblyFinder_Tests: TestBaseWithLocalIocManager
    {
        [Test]
        public void Should_Get_Module_And_Additional_Assemblies()
        {
            //Arrange
            var bootstrapper = SharePlatformBootstrapper.Create<MyStartupModule>(options =>
            {
                options.IocManager = LocalIocManager;
            });

            bootstrapper.Initialize();

            //Act
            var assemblies = bootstrapper.IocManager.Resolve<SharePlatformAssemblyFinder>().GetAllAssemblies();

            //Assert
            assemblies.Count.ShouldBe(2);

          
            assemblies.Any(a => a == typeof(SharePlatformKernelModule).GetAssembly()).ShouldBeTrue();
        }

       
    }
}