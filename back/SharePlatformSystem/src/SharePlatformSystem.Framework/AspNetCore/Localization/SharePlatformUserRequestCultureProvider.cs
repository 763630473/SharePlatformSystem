using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using JetBrains.Annotations;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Runtime.Session;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.Framework.AspNetCore.Localization
{
    public class SharePlatformUserRequestCultureProvider : RequestCultureProvider
    {
        public CookieRequestCultureProvider CookieProvider { get; set; }
        public SharePlatformLocalizationHeaderRequestCultureProvider HeaderProvider { get; set; }

        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var SharePlatformSession = httpContext.RequestServices.GetRequiredService<ISharePlatformSession>();
            if (SharePlatformSession.UserId == null)
            {
                return null;
            }

            var settingManager = httpContext.RequestServices.GetRequiredService<ISettingManager>();

            var culture = await settingManager.GetSettingValueForUserAsync(
                LocalizationSettingNames.DefaultLanguage,
                SharePlatformSession.UserId,
                fallbackToDefault: false
            );

            if (!culture.IsNullOrEmpty())
            {
                return new ProviderCultureResult(culture, culture);
            }

            var result = await GetResultOrNull(httpContext, CookieProvider) ??
                         await GetResultOrNull(httpContext, HeaderProvider);

            if (result == null || !result.Cultures.Any())
            {
                return null;
            }

            //如果可用，尝试从cookie设置用户的语言设置。
            await settingManager.ChangeSettingForUserAsync(
                SharePlatformSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                result.Cultures.First().Value
            );

            return result;
        }

        protected virtual async Task<ProviderCultureResult> GetResultOrNull([NotNull] HttpContext httpContext, [CanBeNull] IRequestCultureProvider provider)
        {
            if (provider == null)
            {
                return null;
            }

            return await provider.DetermineProviderCultureResult(httpContext);
        }
    }
}
