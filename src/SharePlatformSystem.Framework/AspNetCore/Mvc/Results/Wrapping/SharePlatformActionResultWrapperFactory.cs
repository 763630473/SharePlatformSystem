using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharePlatformSystem.Core;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Wrapping
{
    public class SharePlatformActionResultWrapperFactory : ISharePlatformActionResultWrapperFactory
    {
        public ISharePlatformActionResultWrapper CreateFor(ResultExecutingContext actionResult)
        {
            Check.NotNull(actionResult, nameof(actionResult));

            if (actionResult.Result is ObjectResult)
            {
                return new SharePlatformObjectActionResultWrapper(actionResult.HttpContext.RequestServices);
            }

            if (actionResult.Result is JsonResult)
            {
                return new SharePlatformJsonActionResultWrapper();
            }

            if (actionResult.Result is EmptyResult)
            {
                return new SharePlatformEmptyActionResultWrapper();
            }

            return new NullSharePlatformActionResultWrapper();
        }
    }
}