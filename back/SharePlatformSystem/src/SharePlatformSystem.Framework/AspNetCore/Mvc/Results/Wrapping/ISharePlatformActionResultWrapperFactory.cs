using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Wrapping
{
    public interface ISharePlatformActionResultWrapperFactory : ITransientDependency
    {
        ISharePlatformActionResultWrapper CreateFor([NotNull] ResultExecutingContext actionResult);
    }
}