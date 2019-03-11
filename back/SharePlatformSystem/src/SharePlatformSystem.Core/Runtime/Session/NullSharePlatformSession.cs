using SharePlatformSystem.Runtime.Remoting;

namespace SharePlatformSystem.Runtime.Session
{
    /// <summary>
    /// 实现空对象模式<see cref=“ishareplatformsession”/>。
    /// </summary>
    public class NullSharePlatformSession : SharePlatformSessionBase
    {
        /// <summary>
        /// 单例实例。
        /// </summary>
        public static NullSharePlatformSession Instance { get; } = new NullSharePlatformSession();

        public override string UserId => null;


        public override string ImpersonatorUserId => null;


        private NullSharePlatformSession() 
            : base(
                  new DataContextAmbientScopeProvider<SessionOverride>(new AsyncLocalAmbientDataContext())
            )
        {

        }
    }
}