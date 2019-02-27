using Microsoft.AspNetCore.Mvc.Filters;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Wrapping
{
    public class NullSharePlatformActionResultWrapper : ISharePlatformActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
            
        }
    }
}