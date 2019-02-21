using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace SharePlatformSystem.Controllers
{
    /// <summary>
    /// 基础控制器
    /// <para>用于控制登录用户是否有权限访问指定的Action</para>
    /// </summary>
    public class BaseController : SSOController
    {
        protected Response Result = new Response();
        protected string Controllername;   //当前控制器小写名称
        protected string Actionname;        //当前Action小写名称

        public BaseController(IAuth authUtil) : base(authUtil)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

           var description =
                (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor;

            Controllername = description.ControllerName.ToLower();
            Actionname = description.ActionName.ToLower();

            var config = AutofacExt.GetFromFac<IOptions<AppSetting>>();
            var version = config.Value.Version;
            if (version == "demo" && Request.Method == "POST")
            {
                filterContext.Result = new RedirectResult("/Error/Demo");
                return;
               // throw new Exception("演示版本，不要乱动，当前模块:" + Controllername + "/" + Actionname);
            }

            if (!_authUtil.CheckLogin()) return;

            var function = ((TypeInfo)GetType()).DeclaredMethods.FirstOrDefault(u => u.Name.ToLower() == Actionname);

            if (function == null)
                throw new Exception("未能找到Action");
            //权限验证标识
            var authorize = function.GetCustomAttribute(typeof(AuthenticateAttribute));
            if (authorize == null)
            {
                return;
            }
            var currentModule = _authUtil.GetCurrentUser().Modules.FirstOrDefault(u => u.Url.ToLower().Contains(Controllername));
            //当前登录用户没有Action记录&&Action有authenticate标识
            if (currentModule == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
        }
    }
}