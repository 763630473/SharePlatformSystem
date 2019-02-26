using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;
using SharePlatformSystem.Applications.Services;
using SharePlatformSystem.Core;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.TestBase;
using Shouldly;

namespace SharePlatformSystem.Test.TestBase.Application.Services
{
    public class ServiceWithDifferentInputs_Tests : SharePlatformIntegratedTestBase<SharePlatformKernelModule>
    {
        private readonly IMyAppService _appService;

        public ServiceWithDifferentInputs_Tests()
        {
            LocalIocManager.Register<IMyAppService, MyAppService>(DependencyLifeStyle.Transient);
            _appService = Resolve<IMyAppService>();
        }

        [Test]
        public async Task GetsExpressionReturnsGenericAsync_Test()
        {
            var result = await _appService.GetsExpressionReturnsGenericAsync<MyEmptyDto>(t => t != null);
            result.ShouldBeOfType(typeof(MyEmptyDto));
        }

        #region Application Service

        public interface IMyAppService : IApplicationService
        {
            Task<T> GetsExpressionReturnsGenericAsync<T>(Expression<Func<T, bool>> predicate)
                where T : class, new();
        }

        public class MyAppService : IMyAppService
        {
            public Task<T> GetsExpressionReturnsGenericAsync<T>(Expression<Func<T, bool>> predicate) where T : class, new()
            {
                return Task.FromResult(new T());
            }
        }

        public class MyEmptyDto
        {
            
        }

        #endregion
    }
}
