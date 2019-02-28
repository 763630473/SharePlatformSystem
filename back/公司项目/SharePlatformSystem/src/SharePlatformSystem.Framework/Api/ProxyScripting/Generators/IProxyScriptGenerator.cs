using SharePlatformSystem.Framework.Api.Modeling;

namespace SharePlatformSystem.Framework.Api.ProxyScripting.Generators
{
    public interface IProxyScriptGenerator
    {
        string CreateScript(ApplicationApiDescriptionModel model);
    }
}