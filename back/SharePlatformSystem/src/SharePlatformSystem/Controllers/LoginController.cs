using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                ClaimsIdentity id = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                id.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.User.Id, ClaimValueTypes.String));
                id.AddClaim(new Claim("UserId", result.User.Id));
               // id.AddClaim(new Claim(AbpClaimTypes.Role, "vvv"));
                claimsPrincipal.AddIdentity(id);
                HttpContext.User = claimsPrincipal;
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = true });


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