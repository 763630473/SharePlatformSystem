using System;
using System.Linq;
using Castle.LoggingFacility.MsLogging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using SharePlatformSystem.Core;
using SharePlatformSystem.Framework.AspNetCore.EmbeddedResources;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Framework.AspNetCore.Security;
using SharePlatformSystem.Framework.AspNetCore.Localization;

namespace SharePlatformSystem.Framework.AspNetCore
{
    public static class SharePlatformApplicationBuilderExtensions
    {
        public static void UseSharePlatform(this IApplicationBuilder app)
        {
            app.UseSharePlatform(null);
        }

        public static void UseSharePlatform([NotNull] this IApplicationBuilder app, Action<SharePlatformApplicationBuilderOptions> optionsAction)
        {
            Check.NotNull(app, nameof(app));

            var options = new SharePlatformApplicationBuilderOptions();
            optionsAction?.Invoke(options);

            if (options.UseCastleLoggerFactory)
            {
                app.UseCastleLoggerFactory();
            }

            InitializeSharePlatform(app);

            if (options.UseSecurityHeaders)
            {
                app.UseSharePlatformSecurityHeaders();
            }
        }    

        private static void InitializeSharePlatform(IApplicationBuilder app)
        {
            var SharePlatformBootstrapper = app.ApplicationServices.GetRequiredService<SharePlatformBootstrapper>();
            SharePlatformBootstrapper.Initialize();

            var applicationLifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() => SharePlatformBootstrapper.Dispose());
        }

        public static void UseCastleLoggerFactory(this IApplicationBuilder app)
        {
            var castleLoggerFactory = app.ApplicationServices.GetService<Castle.Core.Logging.ILoggerFactory>();
            if (castleLoggerFactory == null)
            {
                return;
            }

            app.ApplicationServices
                .GetRequiredService<ILoggerFactory>()
                .AddCastleLogger(castleLoggerFactory);
        }     

        public static void UseSharePlatformSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<SharePlatformSecurityHeadersMiddleware>();
        }
    }
}
