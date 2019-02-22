﻿using System;
using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.Infrastructure;

namespace SharePlatformSystem.Controllers
{
    public class LoginController : Controller
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