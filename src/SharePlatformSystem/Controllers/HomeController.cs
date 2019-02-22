using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auth.App.Interface;

namespace SharePlatformSystem.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
       
       
        
        public ActionResult Git()
        {
            return View();
        }


        public HomeController(IAuth authUtil) : base(authUtil)
        {
        }
    }
}