using System;

namespace SharePlatformSystem.Framework.Models
{
    /// <summary>
    /// 此类用于为Ajax请求创建标准响应。
    /// </summary>
    [Serializable]
    public class AjaxResponse<TResult>: AjaxResponseBase
    {
        /// <summary>
        ///Ajax请求的实际结果对象。
        ///如果<see cref=“ajaxResponseBase.success”/>为真，则设置此参数。
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        ///创建一个指定了<see cref=“ajaxResponse”/>的对象。
        /// <see cref="AjaxResponseBase.Success"/> 设置为真.
        /// </summary>
        /// <param name="result">Ajax请求的实际结果对象</param>
        public AjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        ///创建一个<see cref=“ajaxResponse”/>对象。
        /// <see cref="AjaxResponseBase.Success"/>设置为真.
        /// </summary>
        public AjaxResponse()
        {
            Success = true;
        }

        /// <summary>
        ///创建一个指定了<see cref=“ajaxResponse”/>的对象。
        /// </summary>
        /// <param name="success">指示结果的成功状态</param>
        public AjaxResponse(bool success)
        {
            Success = success;
        }

        /// <summary>
        ///创建一个指定了<see cref=“ajaxResponse”/>的对象。
        /// </summary>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }
}