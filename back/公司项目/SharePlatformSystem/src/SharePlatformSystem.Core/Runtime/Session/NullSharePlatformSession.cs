using SharePlatformSystem.Runtime.Remoting;

namespace SharePlatformSystem.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="ISharePlatformSession"/>.
    /// </summary>
    public class NullSharePlatformSession : SharePlatformSessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullSharePlatformSession Instance { get; } = new NullSharePlatformSession();

        /// <inheritdoc/>
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