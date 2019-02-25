using System;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Application.Services.Dto;
using SharePlatformSystem.Json;

namespace SharePlatformSystem.Tests.Json
{
    public class EntityDtoSerialization_Tests
    {
        [Test]
        public void Should_Serialize_Types_Derived_From_EntityDto()
        {
            var obj = new MyClass1
            {
                Id = 42,
                Value = new MyClass2
                {
                    Id = 42
                }
            };

            obj.ToJsonString().ShouldNotBeNull();
        }

        public class MyClass1 : EntityDto
        {
            public MyClass2 Value { get; set; }
        }

        public class MyClass2 : EntityDto
        {

        }
    }
}
