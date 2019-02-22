﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharePlatform.Auth.EfRepository.Domain;
using SharePlatformSystem.Auth.App;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.Auth.App.Request;
using SharePlatformSystem.Infrastructure;

namespace SharePlatformSystem.Controllers
{
    public class ResourcesController : BaseController
    {
        private readonly ResourceApp _app;

        public ResourcesController(IAuth authUtil, ResourceApp app) : base(authUtil)
        {
            _app = app;
        }
        //
        // GET: /UserManager/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Assign()
        {
            return View();
        }

        /// <summary>
        /// 加载角色资源
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="firstId">角色ID</param>
        public string LoadForRole(string appId, string firstId)
        {
            try
            {
                var result = new Response<List<string>>
                {
                    Result = _app.LoadForRole(appId, firstId).Select(u => u.Id).ToList()
                };
                return JsonHelper.Instance.Serialize(result);
            }
            catch (Exception e)
            {
                Result.Code = 500;
                Result.Message = e.InnerException?.Message ?? e.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }


        public string Load([FromQuery]QueryResourcesReq request)
        {
            return JsonHelper.Instance.Serialize(_app.Load(request));
        }

       [HttpPost]
        public string Delete(string[] ids)
        {
            Response resp = new Response();
            try
            {
                _app.Delete(ids);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

       [HttpPost]
        public string Add(Resource obj)
        {
            Response resp = new Response();
            try
            {
                _app.Add(obj);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

       [HttpPost]
        public string Update(Resource obj)
        {
            Response resp = new Response();
            try
            {
                _app.Update(obj);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

    }
}