using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharePlatformSystem.Framework.Models;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Wrapping
{
    public class SharePlatformEmptyActionResultWrapper : ISharePlatformActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
            actionResult.Result = new ObjectResult(new AjaxResponse());
        }
    }
}