using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SharePlatformSystem.Domain.Uow;

namespace SharePlatformSystem.Framework.AspNetCore.Uow
{
    public class SharePlatformUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UnitOfWorkMiddlewareOptions _options;

        public SharePlatformUnitOfWorkMiddleware(
            RequestDelegate next, 
            IUnitOfWorkManager unitOfWorkManager, 
            IOptions<UnitOfWorkMiddlewareOptions> options)
        {
            _next = next;
            _unitOfWorkManager = unitOfWorkManager;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!_options.Filter(httpContext))
            {
                await _next(httpContext);
                return;
            }

            using (var uow = _unitOfWorkManager.Begin(_options.OptionsFactory(httpContext)))
            {
                await _next(httpContext);
                await uow.CompleteAsync();
            }
        }
    }
}
