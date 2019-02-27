using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Auditing;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Conventions;
using SharePlatformSystem.Framework.AspNetCore.Mvc.ExceptionHandling;
using SharePlatformSystem.Framework.AspNetCore.Mvc.ModelBinding;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Results;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Uow;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc
{
    internal static class SharePlatformMvcOptionsExtensions
    {
        public static void AddSharePlatform(this MvcOptions options, IServiceCollection services)
        {
            AddConventions(options, services);
            AddFilters(options);
            AddModelBinders(options);
        }

        private static void AddConventions(MvcOptions options, IServiceCollection services)
        {
            options.Conventions.Add(new SharePlatformAppServiceConvention(services));
        }

        private static void AddFilters(MvcOptions options)
        {
            //options.Filters.AddService(typeof(SharePlatformAuthorizationFilter));
            options.Filters.AddService(typeof(SharePlatformAuditActionFilter));
            options.Filters.AddService(typeof(SharePlatformUowActionFilter));
           // options.Filters.AddService(typeof(SharePlatformExceptionFilter));
           // options.Filters.AddService(typeof(SharePlatformResultFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new SharePlatformDateTimeModelBinderProvider());
        }
    }
}