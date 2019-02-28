using Microsoft.AspNetCore.Mvc.Filters;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Wrapping
{
    public interface ISharePlatformActionResultWrapper
    {
        void Wrap(ResultExecutingContext actionResult);
    }
}