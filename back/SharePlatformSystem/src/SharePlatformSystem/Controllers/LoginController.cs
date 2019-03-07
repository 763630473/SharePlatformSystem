using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Controllers;
using SharePlatformSystem.Infrastructure;

namespace SharePlatformSystem.Controllers
{
    public class LoginController : SharePlatformController
    {
        private string _appKey = "SharePlatform";

        private IAuth _authUtil;

        public LoginController(IAuth authUtil)
        {
            _authUtil = authUtil;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
     
        //private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        //{
        //    if (identity == null)
        //        identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

        //    //添加身份信息，以便在AbpSession中使用
        //    //identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));

        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        //}

        public string Login(string username, string password)
        {
            var resp = new Response();
            try
            {
                var result = _authUtil.Login(_appKey, username, password);
                if (result.Code == 200)
                {
                   Response.Cookies.Append("Token", result.Token);
                }
                else
                {
                    resp.Code = 500;
                    resp.Message = result.Message;
                }

            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }

            return JsonHelper.Instance.Serialize(resp);
        }

        public ActionResult Logout()
        {

            _authUtil.Logout();
            return RedirectToAction("Index", "Login");
        }
    }
}