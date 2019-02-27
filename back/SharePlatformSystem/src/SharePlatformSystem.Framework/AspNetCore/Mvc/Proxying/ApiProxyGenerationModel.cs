using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Framework.Api.ProxyScripting;
using SharePlatformSystem.Framework.Api.ProxyScripting.Generators.JQuery;
using System.Linq;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Proxying
{
    public class ApiProxyGenerationModel 
    {
        public string Type { get; set; }

        public bool UseCache { get; set; }

        public string Modules { get; set; }

        public string Controllers { get; set; }

        public string Actions { get; set; }

        public ApiProxyGenerationModel()
        {
            UseCache = true;
        }

        public void Normalize()
        {
            if (Type.IsNullOrEmpty())
            {
                Type = JQueryProxyScriptGenerator.Name;
            }
        }

        public ApiProxyGenerationOptions CreateOptions()
        {
            var options = new ApiProxyGenerationOptions(Type, UseCache);

            if (!Modules.IsNullOrEmpty())
            {
                options.Modules = Modules.Split('|').Select(m => m.Trim()).ToArray();
            }

            if (!Controllers.IsNullOrEmpty())
            {
                options.Controllers = Controllers.Split('|').Select(m => m.Trim()).ToArray();
            }

            if (!Actions.IsNullOrEmpty())
            {
                options.Actions = Actions.Split('|').Select(m => m.Trim()).ToArray();
            }

            return options;
        }
    }
}