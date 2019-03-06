using System;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Json;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Timing;
using SharePlatformSystem.Core.Localization;

namespace SharePlatformSystem.Tests.Json
{
    public class JsonSerializationHelper_Tests
    {
        [Test]
        public void Should_Simply_Serialize_And_Deserialize()
        {
            var str = JsonSerializationHelper.SerializeWithType(new LocalizableString("Foo", "Bar"));
            var result = (LocalizableString)JsonSerializationHelper.DeserializeWithType(str);
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Foo");
            result.SourceName.ShouldBe("Bar");
        }

        [Test]
        public void Should_Deserialize_With_Different_Assembly_Version()
        {
            var str = "SharePlatformSystem.Core.Localization.LocalizableString, SharePlatformSystem.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null|{\"SourceName\":\"Bar\",\"Name\":\"Foo\"}";
            var result = (LocalizableString)JsonSerializationHelper.DeserializeWithType(str);
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Foo");
            result.SourceName.ShouldBe("Bar");
        }

        [Test]
        public void Should_Deserialize_With_DateTime()
        {
            Clock.Provider = ClockProviders.Utc;

            var str = "SharePlatformSystem.Tests.Json.JsonSerializationHelper_Tests+MyClass2, SharePlatformSystem.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null|{\"Date\":\"2016-04-13T16:58:10.526+08:00\"}";
            var result = (MyClass2)JsonSerializationHelper.DeserializeWithType(str);
            result.ShouldNotBeNull();
            result.Date.ShouldBe(new DateTime(2016, 04, 13, 08, 58, 10, 526, Clock.Kind));
            result.Date.Kind.ShouldBe(Clock.Kind);
        }

        [Test]
        public void Should_Deserialize_Without_DateTime_Normalization()
        {
            Clock.Provider = ClockProviders.Utc;

            var str1 = "SharePlatformSystem.Tests.Json.JsonSerializationHelper_Tests+MyClass3,SharePlatformSystem.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null|{\"Date\":\"2016-04-13T16:58:10.526+08:00\"}";
            var result1 = (MyClass3)JsonSerializationHelper.DeserializeWithType(str1);
            result1.ShouldNotBeNull();
            result1.Date.Kind.ShouldBe(DateTimeKind.Local);

            var str2 = "SharePlatformSystem.Tests.Json.JsonSerializationHelper_Tests+MyClass4,SharePlatformSystem.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null|{\"Date\":\"2016-04-13T16:58:10.526+08:00\"}";
            var result2 = (MyClass4)JsonSerializationHelper.DeserializeWithType(str2);
            result2.ShouldNotBeNull();
            result2.Date.Kind.ShouldBe(DateTimeKind.Local);
        }

        public class MyClass1
        {
            public string Name { get; set; }

            public MyClass1()
            {

            }

            public MyClass1(string name)
            {
                Name = name;
            }
        }

        public class MyClass2
        {
            public DateTime Date { get; set; }

            public MyClass2(DateTime date)
            {
                Date = date;
            }
        }

        [DisableDateTimeNormalization]
        public class MyClass3
        {
            public DateTime Date { get; set; }

            public MyClass3(DateTime date)
            {
                Date = date;
            }
        }

        public class MyClass4
        {
            [DisableDateTimeNormalization]
            public DateTime Date { get; set; }

            public MyClass4(DateTime date)
            {
                Date = date;
            }
        }
    }
}