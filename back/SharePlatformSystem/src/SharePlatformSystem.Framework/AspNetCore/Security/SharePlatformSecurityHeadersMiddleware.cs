using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SharePlatformSystem.Framework.AspNetCore.Security
{
    public class SharePlatformSecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SharePlatformSecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            /*x-content-type-options头告诉浏览器不要尝试“猜测”某个资源的mimetype可能是什么，只需获取服务器作为事实返回的mimetype即可。*/
            AddHeaderIfNotExists(httpContext, "X-Content-Type-Options", "nosniff");

            /*X-XSS-Protection是Internet Explorer、Chrome和Safari的一项功能，当页面检测到反映的跨站点脚本（XSS）攻击时，它会阻止页面加载。*/
            AddHeaderIfNotExists(httpContext, "X-XSS-Protection", "1; mode=block");

            /*x-frame-options HTTP响应头可用于指示是否允许浏览器在<frame>、<iframe>或<object>中呈现页面。Sameorigin使其显示在与页面本身同一原点的框架中。规范将由浏览器供应商决定此选项是否适用于顶级、父级或整个链。*/
            AddHeaderIfNotExists(httpContext, "X-Frame-Options", "SAMEORIGIN");

            await _next.Invoke(httpContext);
        }

        private static void AddHeaderIfNotExists(HttpContext context, string key, string value)
        {
            if (!context.Response.Headers.ContainsKey(key))
            {
                context.Response.Headers.Add(key, value);
            }
        }
    }
}
