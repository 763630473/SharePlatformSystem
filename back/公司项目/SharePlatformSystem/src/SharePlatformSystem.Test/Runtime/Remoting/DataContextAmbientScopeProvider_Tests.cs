using System.Threading.Tasks;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Runtime.Remoting;

namespace SharePlatformSystem.Tests.Runtime.Remoting
{
    public class DataContextAmbientScopeProvider_Tests
    {
        private const string ContextKey = "SharePlatform.Tests.TestData";

        [Test]
        public void Test_Sync()
        {
            var scopeAccessor = new DataContextAmbientScopeProvider<TestData>(
                new AsyncLocalAmbientDataContext()
            );

            scopeAccessor.GetValue(ContextKey).ShouldBeNull();

            using (scopeAccessor.BeginScope(ContextKey, new TestData(42)))
            {
                scopeAccessor.GetValue(ContextKey).Number.ShouldBe(42);

                using (scopeAccessor.BeginScope(ContextKey, new TestData(24)))
                {
                    scopeAccessor.GetValue(ContextKey).Number.ShouldBe(24);
                }

                scopeAccessor.GetValue(ContextKey).Number.ShouldBe(42);
            }

            scopeAccessor.GetValue(ContextKey).ShouldBeNull();
        }

        [Test]
        public async Task Test_Async()
        {
            var scopeAccessor = new DataContextAmbientScopeProvider<TestData>(
                new AsyncLocalAmbientDataContext()
            );

            scopeAccessor.GetValue(ContextKey).ShouldBeNull();

            await Task.Delay(1);

            using (scopeAccessor.BeginScope(ContextKey, new TestData(42)))
            {
                await Task.Delay(1);

                scopeAccessor.GetValue(ContextKey).Number.ShouldBe(42);

                using (scopeAccessor.BeginScope(ContextKey, new TestData(24)))
                {
                    await Task.Delay(1);

                    scopeAccessor.GetValue(ContextKey).Number.ShouldBe(24);
                }

                await Task.Delay(1);

                scopeAccessor.GetValue(ContextKey).Number.ShouldBe(42);
            }

            await Task.Delay(1);

            scopeAccessor.GetValue(ContextKey).ShouldBeNull();
        }


        public class TestData
        {
            public TestData(int number)
            {
                Number = number;
            }

            public int Number { get; set; }
        }
    }
}
