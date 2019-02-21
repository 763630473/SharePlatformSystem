using System;
using System.Linq;

namespace SharePlatformSystem.Auth.App.SSO
{
    public class AppInfoService 
    {
        public AppInfo Get(string appKey)
        {
            //可以从数据库读取
            return _applist.SingleOrDefault(u => u.AppKey == appKey);
        }

        private AppInfo[] _applist = new[]
        {
            new AppInfo
            {
                AppKey = "SharePlatform",
                Icon = "/Areas/SSO/Content/images/logo.png",
                IsEnable = true,
                Remark = "SharePlatform",
                ReturnUrl = "http://localhost:56813",
                Title = "SharePlatform.Core",
                CreateTime = DateTime.Now,
            },
            new AppInfo
            {
                AppKey = "SharePlatformTest",
                Icon = "/Areas/SSO/Content/images/logo.png",
                IsEnable = true,
                Remark = "这只是个模拟的测试站点",
                ReturnUrl = "http://localhost:53050",
                Title = "SharePlatform测试站点",
                CreateTime = DateTime.Now,
            }
        };
    }
}