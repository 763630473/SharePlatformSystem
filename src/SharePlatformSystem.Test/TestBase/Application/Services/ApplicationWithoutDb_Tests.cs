using NUnit.Framework;
using SharePlatformSystem.Applications.Services;
using SharePlatformSystem.Core;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.TestBase;
using Shouldly;


namespace SharePlatformSystem.Test.TestBase.Application.Services
{
    /// <summary>
    /// Should support working without database or a unit of work.
    /// </summary>
    public class ApplicationWithoutDb_Tests : SharePlatformIntegratedTestBase<SharePlatformKernelModule>
    {
        private readonly IMyAppService _myAppService;
        public ApplicationWithoutDb_Tests()
        {
            LocalIocManager.Register<IMyAppService, MyAppService>(DependencyLifeStyle.Transient);
            _myAppService = Resolve<IMyAppService>();
        }

        [Test]
        public void Test1()
        {
            var output = _myAppService.MyMethod(new MyMethodInput {MyStringValue = "test"});
            output.Result.ShouldBe(42);
        }

        #region Sample Application service

        public interface IMyAppService
        {
            MyMethodOutput MyMethod(MyMethodInput input);
        }

        public class MyAppService : IMyAppService, IApplicationService
        {
            public MyMethodOutput MyMethod(MyMethodInput input)
            {
                return new MyMethodOutput { Result = 42 };
            }
        }

        public class MyMethodInput 
        {
            public string MyStringValue { get; set; }
        }

        public class MyMethodOutput
        {
            public int Result { get; set; }
        }

        #endregion
    }
}
