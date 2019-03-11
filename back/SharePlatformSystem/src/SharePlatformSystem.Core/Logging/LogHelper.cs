using System;
using System.Linq;
using Castle.Core.Logging;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Logging
{
    /// <summary>
    /// 此类可用于从难以获得对<see cref="ilogger"/>的引用的地方写入日志。
    ///通常情况下，尽可能在属性注入时使用<see cref="ilogger"/>。
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        ///对记录器的引用。
        /// </summary>
        public static ILogger Logger { get; private set; }

        static LogHelper()
        {
            Logger = IocManager.Instance.IsRegistered(typeof(ILoggerFactory))
                ? IocManager.Instance.Resolve<ILoggerFactory>().Create(typeof(LogHelper))
                : NullLogger.Instance;
        }

        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        public static void LogException(ILogger logger, Exception ex)
        {
            var severity = (ex as IHasLogSeverity)?.Severity ?? LogSeverity.Error;

            logger.Log(severity, ex.Message, ex);
        }
        
    }
}
