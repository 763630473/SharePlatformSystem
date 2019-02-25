using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Runtime.Caching;

namespace SharePlatformSystem.Tests.Runtime.Caching
{
    public class AbpCacheData_Tests
    {
        [Test]
        public void Serialize_List_Test()
        {
            List<string> source = new List<string>
            {
                "Stranger Things",
                "The OA",
                "Lost in Space"
            };

            var result = SharePlatformCacheData.Serialize(source);
            result.Type.ShouldBe("System.Collections.Generic.List`1[[System.String]]");
            result.Payload.ShouldBe("[\"Stranger Things\",\"The OA\",\"Lost in Space\"]");
        }

        [Test]
        public void Serialize_Class_Test()
        {
            var source = new MyTestClass
            {
                Field1 = 42,
                Field2 = "Stranger Things"
            };

            var result = SharePlatformCacheData.Serialize(source);
            result.Type.ShouldBe("Abp.Tests.Runtime.Caching.AbpCacheData_Tests+MyTestClass, Abp.Tests");
            result.Payload.ShouldBe("{\"Field1\":42,\"Field2\":\"Stranger Things\"}");
        }

        [Test]
        public void Deserialize_List_Test()
        {
            var json = "{\"Payload\":\"[\\\"Stranger Things\\\",\\\"The OA\\\",\\\"Lost in Space\\\"]\",\"Type\":\"System.Collections.Generic.List`1[[System.String]]\"}";
            var cacheData = SharePlatformCacheData.Deserialize(json);

            cacheData.ShouldNotBeNull();
        }

        [Test]
        public void Deserialize_Class_Test()
        {
            var json = "{\"Payload\": \"{\\\"Field1\\\": 42,\\\"Field2\\\":\\\"Stranger Things\\\"}\",\"Type\":\"Abp.Tests.Runtime.Caching.AbpCacheData_Tests+MyTestClass, Abp.Tests\"}";

            var cacheData = SharePlatformCacheData.Deserialize(json);

            cacheData.ShouldNotBeNull();
        }

        class MyTestClass
        {
            public int Field1 { get; set; }

            public string Field2 { get; set; }
        }
    }
}
