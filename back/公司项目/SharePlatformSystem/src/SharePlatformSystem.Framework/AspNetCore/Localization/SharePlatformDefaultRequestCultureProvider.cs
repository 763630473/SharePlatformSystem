using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Localization;

namespace SharePlatformSystem.Framework.AspNetCore.Localization
{
    public class SharePlatformDefaultRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var settingManager = httpContext.RequestServices.GetRequiredService<ISettingManager>();

            var culture = await settingManager.GetSettingValueAsync(LocalizationSettingNames.DefaultLanguage);

            if (culture.IsNullOrEmpty())
            {
                return null;
            }

            return new ProviderCultureResult(culture, culture);
        }
    }
}
