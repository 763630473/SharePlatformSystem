using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Runtime.Session;
using SharePlatformSystem.Application.Services;
using NSubstitute;

namespace SharePlatformSystem.Tests.Dependency
{
    public class PropertyInjection_Tests : TestBaseWithLocalIocManager
    {
        [Test]
        public void Should_Inject_Session_For_ApplicationService()
        {
            var session = Substitute.For<ISharePlatformSession>();
            session.UserId.Returns("42");

            LocalIocManager.Register<MyApplicationService>();
            LocalIocManager.IocContainer.Register(
                Component.For<ISharePlatformSession>().Instance(session)
                );

            var myAppService = LocalIocManager.Resolve<MyApplicationService>();
            myAppService.TestSession();
        }

        private class MyApplicationService : ApplicationService
        {
            public void TestSession()
            {
                SharePlatformSession.ShouldNotBe(null);
                SharePlatformSession.UserId.ShouldBe("42");
            }
        }
    }
}
