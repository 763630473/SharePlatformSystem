namespace SharePlatformSystem.Framework.Models
{
    public abstract class AjaxResponseBase
    {
        /// <summary>
        /// 此属性可用于将用户重定向到指定的URL。
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        ///指示结果的成功状态。
        ///如果该值为假，则设置<see cref=“error”/>。
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误详细信息（必须且仅当<see cref=“success”/>为false时设置）。
        /// </summary>
        public ErrorInfo Error { get; set; }

        /// <summary>
        ///此属性可用于指示当前用户没有执行此请求的权限。
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        /// <summary>
        ///Ajax响应的特殊签名。它在客户机中用于检测这是否是由SharePlatform包装的响应。
        /// </summary>
        public bool __SharePlatform { get; } = true;
    }
}