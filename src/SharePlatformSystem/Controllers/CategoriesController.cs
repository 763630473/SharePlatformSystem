﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace SharePlatformSystem.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly CategoryApp _app;
        public CategoriesController(IAuth authUtil, CategoryApp app) : base(authUtil)
        {
            _app = app;
        }

        //
        // GET: /UserManager/
        public ActionResult Index()
        {
            return View();
        }

        public string All([FromQuery]QueryCategoryListReq request)
        {
            TableData data = new TableData();
            data = _app.All(request);
            return JsonHelper.Instance.Serialize(data);
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
        public string Add(Category obj)
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
        public string Update(Category obj)
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

        //所有得分类类型
        public string AllTypes()
        {
            var data = _app.AllTypes();
            return JsonHelper.Instance.Serialize(data);
        }

    }
}