using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.NetHouse.App.Persion;

namespace SharePlatformSystem.Controllers
{
    public class HomeController : BaseController
    {
        private IPersionManager _persionManager;
        public HomeController(IAuth authUtil, IPersionManager persionManager) : base(authUtil)
        {
            _persionManager = persionManager;
        }
        public ActionResult Index()
        {
            Logger.Error("1");
            Logger.Debug("2");
            Logger.Info("3");
            //_persionManager.InsertPersion();
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