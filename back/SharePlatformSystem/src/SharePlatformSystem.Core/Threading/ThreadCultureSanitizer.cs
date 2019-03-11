using System.Globalization;
using System.Threading;

namespace SharePlatformSystem.Threading
{
    /// <summary>
    /// 这是一个解决应用程序启动问题的方法。
    /// </summary>
    public static class ThreadCultureSanitizer
    {
        public static void Sanitize()
        {
            var currentCulture = CultureInfo.CurrentCulture;

            //任何区域性的顶部都应该是不变的区域性，
            //执行.equals比较，确保
            //找到它，不要无休止地循环
            var invariantCulture = currentCulture;
            while (invariantCulture.Equals(CultureInfo.InvariantCulture) == false)
            {
                invariantCulture = invariantCulture.Parent;
            }

            if (ReferenceEquals(invariantCulture, CultureInfo.InvariantCulture))
            {
                return;
            }

            var thread = Thread.CurrentThread;
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(thread.CurrentCulture.Name);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(thread.CurrentUICulture.Name);
        }
    }
}
