using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Auth.App;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.Auth.App.Request;
using SharePlatformSystem.Auth.App.Response;
using SharePlatformSystem.Infrastructure;
using SharePlatformSystem.Models;

namespace SharePlatformSystem.Controllers
{
    public class FlowInstancesController : BaseController
    {
        private readonly FlowInstanceApp _app;
        public FlowInstancesController(IAuth authUtil, FlowInstanceApp app) : base(authUtil)
        {
            _app = app;
        }

        //
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 待处理的流程
        /// </summary>
        public ActionResult Wait()
        {
            return View();
        }

        /// <summary>
        /// 已完成的流程
        /// </summary>
        public ActionResult Disposed()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 进度详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Verification()
        {
            return View();
        }

        [HttpPost]
        public string Verification(VerificationReq request)
        {
            try
            {
                _app.Verification(request);

            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        public string Get(string id)
        {
            try
            {
                var flowinstance = _app.Get(id);

                var result = new Response<FlowVerificationResp>
                {
                    Result = flowinstance.MapTo<FlowVerificationResp>()
                };
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
       
        public string Add(JObject obj)
        {
            try
            {
                _app.CreateInstance(obj);
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
        public string Update(FlowInstance obj)
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
        public string Load([FromQuery]QueryFlowInstanceListReq request)
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

    }
}