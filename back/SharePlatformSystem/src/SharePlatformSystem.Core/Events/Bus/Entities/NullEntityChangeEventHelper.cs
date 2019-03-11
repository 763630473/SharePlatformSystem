using System.Threading.Tasks;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 空对象实现<see cref=“IEntityChangeEventHelper”/>。
    /// </summary>
    public class NullEntityChangeEventHelper : IEntityChangeEventHelper
    {
        /// <summary>
        ///获取类的单个实例。
        /// </summary>
        public static NullEntityChangeEventHelper Instance { get; } = new NullEntityChangeEventHelper();

        private NullEntityChangeEventHelper()
        {

        }

        public void TriggerEntityCreatingEvent(object entity)
        {
            
        }

        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEntityUpdatingEvent(object entity)
        {
            
        }

        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEntityDeletingEvent(object entity)
        {
            
        }

        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEvents(EntityChangeReport changeReport)
        {
            
        }

        public Task TriggerEventsAsync(EntityChangeReport changeReport)
        {
            return Task.FromResult(0);
        }
    }
}