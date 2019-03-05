using NUnit.Framework;
using SharePlatformSystem.Core;
using SharePlatformSystem.Runtime.Session;
using SharePlatformSystem.TestBase;
using Shouldly;

namespace SharePlatformSystem.Test.TestBase.Runtime.Session
{
    public class SessionTests : SharePlatformIntegratedTestBase<SharePlatformKernelModule>
    {
        [Test]
        public void Should_Be_Default_On_Startup()
        {
            SharePlatformSession.UserId.ShouldBe(null);


            SharePlatformSession.UserId.ShouldBe(null);
        }

        [Test]
        public void Can_Change_Session_Variables()
        {

            SharePlatformSession.UserId = "1";

            var resolvedAbpSession = LocalIocManager.Resolve<ISharePlatformSession>();

            resolvedAbpSession.UserId.ShouldBe("1");


            SharePlatformSession.UserId.ShouldBe("1");
        }
    }
}
