using Microsoft.AspNetCore.Mvc;

namespace SharePlatformSystem.Controllers
{
    public class ErrorController : Controller
    {
        public string Demo()
        {
            return JsonHelper.Instance.Serialize(new Response
            {
                Code = 500,
                Message = "演示版本，不要乱动"
            });
        }
    }
}