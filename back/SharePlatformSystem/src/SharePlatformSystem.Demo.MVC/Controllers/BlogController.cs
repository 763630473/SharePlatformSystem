using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Controllers;

namespace AbpAspNetCoreDemo.PlugIn.Controllers
{
    public class BlogController : SharePlatformController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
