using System.Threading.Tasks;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Threading;

namespace SharePlatformSystem.Tests.Threading
{
    public class AsyncHelper_Tests
    {
        [Test]
        public void Test1()
        {
            AsyncHelper.RunSync(AsyncMethod1);
            AsyncHelper.RunSync(() => AsyncMethod2(21)).ShouldBe(42);
        }

        private async Task AsyncMethod1()
        {
            await Task.Delay(10);
        }

        private async Task<int> AsyncMethod2(int p)
        {
            await Task.Delay(10);
            return p * 2;
        }
    }
}
