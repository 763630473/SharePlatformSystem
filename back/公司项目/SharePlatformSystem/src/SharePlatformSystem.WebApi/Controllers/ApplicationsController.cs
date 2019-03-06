using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auth.App;
using SharePlatformSystem.Auth.App.Request;
using SharePlatformSystem.Auth.App.Response;

namespace SharePlatformSystem.WebApi.Controllers
{
    /// <summary>
    /// 应用列表
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppManager _app;

        public ApplicationsController(AppManager app) 
        {
            _app = app;
        }
        [HttpGet]
        public TableData Load([FromQuery]QueryAppListReq request)
        {
            var applications = _app.GetList(request);
            return new TableData
            {
                data = applications,
                count = applications.Count
            };
        }

    }
}