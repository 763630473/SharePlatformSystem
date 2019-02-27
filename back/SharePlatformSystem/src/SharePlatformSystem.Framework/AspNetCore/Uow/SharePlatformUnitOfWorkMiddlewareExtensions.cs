using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharePlatformSystem.Framework.AspNetCore.Uow;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class SharePlatformUnitOfWorkMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app, Action<UnitOfWorkMiddlewareOptions> optionsAction = null)
        {
            var options = app.ApplicationServices.GetRequiredService<IOptions<UnitOfWorkMiddlewareOptions>>().Value;
            optionsAction?.Invoke(options);
            return app.UseMiddleware<SharePlatformUnitOfWorkMiddleware>();
        }
    }
}
