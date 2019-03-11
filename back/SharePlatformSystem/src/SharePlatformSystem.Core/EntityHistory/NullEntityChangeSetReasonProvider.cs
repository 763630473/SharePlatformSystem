using SharePlatformSystem.Runtime.Remoting;

namespace SharePlatformSystem.EntityHistory
{
    /// <summary>
    /// 实现空对象模式<see cref=“IEntityChangeSetReasonProvider”/>。
    /// </summary>
    public class NullEntityChangeSetReasonProvider : EntityChangeSetReasonProviderBase
    {
        /// <summary>
        ///单例实例。
        /// </summary>
        public static NullEntityChangeSetReasonProvider Instance { get; } = new NullEntityChangeSetReasonProvider();

        public override string Reason => null;

        private NullEntityChangeSetReasonProvider()
            : base(
                  new DataContextAmbientScopeProvider<ReasonOverride>(new AsyncLocalAmbientDataContext())
            )
        {

        }
    }
}
