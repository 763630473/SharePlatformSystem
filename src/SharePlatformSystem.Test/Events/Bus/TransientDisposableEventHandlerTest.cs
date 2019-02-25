using NUnit.Framework;

namespace SharePlatformSystem.Tests.Events.Bus
{
    public class TransientDisposableEventHandlerTest : EventBusTestBase
    {
        [Test]
        public void Should_Call_Handler_AndDispose()
        {
            EventBus.Register<MySimpleEventData, MySimpleTransientEventHandler>();
            EventBus.Register<MySimpleEventData, MySimpleTransientAsyncEventHandler>();

            EventBus.Trigger(new MySimpleEventData(1));
            EventBus.Trigger(new MySimpleEventData(2));
            EventBus.Trigger(new MySimpleEventData(3));

            Assert.AreEqual(3, MySimpleTransientEventHandler.HandleCount);
            Assert.AreEqual(3, MySimpleTransientEventHandler.DisposeCount);

            Assert.AreEqual(3, MySimpleTransientAsyncEventHandler.HandleCount);
            Assert.AreEqual(3, MySimpleTransientAsyncEventHandler.DisposeCount);
        }
    }
}