using Microsoft.AspNetCore.Mvc.Filters;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Framework.AspNetCore.Configuration;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Extensions;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Wrapping;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Results
{
    public class SharePlatformResultFilter : IResultFilter, ITransientDependency
    {
        private readonly ISharePlatformAspNetCoreConfiguration _configuration;
        private readonly ISharePlatformActionResultWrapperFactory _actionResultWrapperFactory;

        public SharePlatformResultFilter(ISharePlatformAspNetCoreConfiguration configuration, 
            ISharePlatformActionResultWrapperFactory actionResultWrapper)
        {
            _configuration = configuration;
            _actionResultWrapperFactory = actionResultWrapper;
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();

            //var clientCacheAttribute = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
            //    methodInfo,
            //    _configuration.DefaultClientCacheAttribute
            //);

            //clientCacheAttribute?.Apply(context);
            
            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    methodInfo,
                    _configuration.DefaultWrapResultAttribute
                );

            if (!wrapResultAttribute.WrapOnSuccess)
            {
                return;
            }

            _actionResultWrapperFactory.CreateFor(context).Wrap(context);
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}
