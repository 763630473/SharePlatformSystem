using System;

namespace SharePlatformSystem.Framework.Models
{
    /// <summary>
    /// 此类用于为Ajax/Remote请求创建标准响应。
    /// </summary>
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        /// <summary>
        /// 创建<see cref=“ajaxResponse”/>对象。
        ///<see cref=“ajaxResponseBase.success”/>is set as true.
        /// </summary>
        public AjaxResponse()
        {

        }

        /// <summary>
        ///创建一个指定了<see cref=“ajaxResponse”/>的对象。
        /// </summary>
        /// <param name="success">指示结果的成功状态</param>
        public AjaxResponse(bool success)
            : base(success)
        {

        }

        /// <summary>
        ///创建一个指定了<see cref=“ajaxResponse”/>的对象。
        ///<see cref=“ajaxResponseBase.success”/>is set as true.
        /// </summary>
        /// <param name="result">实际结果对象</param>
        public AjaxResponse(object result)
            : base(result)
        {

        }

        /// <summary>
        ///创建一个指定了<see cref=“ajaxResponse”/>的对象。
        ///<see cref=“ajaxResponseBase.success”/>设为假。
        /// </summary>
        /// <param name="error">错误详情</param>
        /// <param name="unAuthorizedRequest">用于指示当前用户没有执行此请求的权限</param>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}