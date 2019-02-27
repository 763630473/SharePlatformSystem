using Microsoft.AspNetCore.Mvc.Filters;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Caching
{
    public interface IClientCacheAttribute
    {
        void Apply(ResultExecutingContext context);
    }
}