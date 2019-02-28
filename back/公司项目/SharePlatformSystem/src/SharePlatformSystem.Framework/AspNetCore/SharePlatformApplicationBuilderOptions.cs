namespace SharePlatformSystem.Framework.AspNetCore
{
    public class SharePlatformApplicationBuilderOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseCastleLoggerFactory { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseSharePlatformRequestLocalization { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseSecurityHeaders { get; set; }

        public SharePlatformApplicationBuilderOptions()
        {
            UseCastleLoggerFactory = true;
            UseSharePlatformRequestLocalization = true;
            UseSecurityHeaders = true;
        }
    }
}