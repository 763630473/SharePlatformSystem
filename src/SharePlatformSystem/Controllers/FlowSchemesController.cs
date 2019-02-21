﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace SharePlatformSystem.Controllers
{
    public class FlowSchemesController : BaseController
    {
        private readonly FlowSchemeApp _app;

        //
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Design()
        {
            return View();
        }
        public ActionResult Preview()
        {
            return View();
        }

        //流程节点信息
        public ActionResult NodeInfo()
        {
            return View();
        }

        public string Get(string id)
        {
            try
            {
                var result = new Response<FlowScheme> {Result = _app.Get(id)};
                return JsonHelper.Instance.Serialize(result);
            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        //添加或修改
       [HttpPost]
        public string Add(FlowScheme obj)
        {
            try
            {
                _app.Add(obj);

            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        //添加或修改
       [HttpPost]
        public string Update(FlowScheme obj)
        {
            try
            {
                _app.Update(obj);

            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        public string Load([FromQuery]QueryFlowSchemeListReq request)
        {
            return JsonHelper.Instance.Serialize(_app.Load(request));
        }

       [HttpPost]
        public string Delete(string[] ids)
        {
            try
            {
                _app.Delete(ids);
            }
            catch (Exception e)
            {
                Result.Code = 500;
                Result.Message = e.InnerException?.Message ?? e.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }

        public FlowSchemesController(IAuth authUtil, FlowSchemeApp app) : base(authUtil)
        {
            _app = app;
        }
    }
}