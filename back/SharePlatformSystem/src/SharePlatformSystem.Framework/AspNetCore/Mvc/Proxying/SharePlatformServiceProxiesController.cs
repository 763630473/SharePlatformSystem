using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.Framework.Api.ProxyScripting;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Controllers;
using SharePlatformSystem.Framework.Models;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Proxying
{
    [DontWrapResult]
    [DisableAuditing]
    public class SharePlatformServiceProxiesController : SharePlatformController
    {
        private readonly IApiProxyScriptManager _proxyScriptManager;

        public SharePlatformServiceProxiesController(IApiProxyScriptManager proxyScriptManager)
        {
            _proxyScriptManager = proxyScriptManager;
        }

        [Produces("application/x-javascript")]
        public ContentResult GetAll(ApiProxyGenerationModel model)
        {
            var script = _proxyScriptManager.GetScript(model.CreateOptions());
            return Content(script, "application/x-javascript");
        }
    }
}
