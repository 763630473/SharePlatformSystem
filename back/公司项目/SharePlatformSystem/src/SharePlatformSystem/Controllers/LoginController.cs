using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Core.Authorization;
using SharePlatformSystem.Core.Authorization.Users;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Controllers;
using SharePlatformSystem.Framework.Authorization;
using SharePlatformSystem.Framework.Authorization.Users;
using SharePlatformSystem.Infrastructure;

namespace SharePlatformSystem.Controllers
{
    public class LoginController : SharePlatformController
    {
        private string _appKey = "SharePlatform";
        private readonly UserManager _userManager;
        private readonly SignInManager _signInManager;
        private readonly LogInManager _logInManager;
        private IAuth _authUtil;

        public LoginController(IAuth authUtil, UserManager userManager, SignInManager signInManager, LogInManager logInManager)
        {
            _authUtil = authUtil;
            _userManager = userManager;
            _signInManager = signInManager;
            _logInManager= logInManager;
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
                var user = result.User;
                var loginResult = GetLoginResultAsync(user.Name, user.Password);

                _signInManager.SignInAsync(loginResult.Result.Identity, false);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }

            return JsonHelper.Instance.Serialize(resp);
        }
        private async Task<SharePlatformLoginResult<User>> GetLoginResultAsync(string usernameOrEmailAddress, string password)
        {
            var loginResult =await  _logInManager.LoginAsync(usernameOrEmailAddress, password);

            switch (loginResult.Result)
            {
                case SharePlatformLoginResultType.Success:
                    return loginResult;
                default:
                    throw new Exception("333333");
            }
        }
        public ActionResult Logout()
        {

            _authUtil.Logout();
            return RedirectToAction("Index", "Login");
        }
    }
}