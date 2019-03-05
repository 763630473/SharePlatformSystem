using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using SharePlatformSystem.Applications.Services;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Timing;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.TestBase;
using Shouldly;

namespace SharePlatformSystem.Test.TestBase.Application.Services
{
    public class Validation_Tests : SharePlatformIntegratedTestBase<SharePlatformKernelModule>
    {
        private readonly IMyAppService _myAppService;

        public Validation_Tests()
        {
            LocalIocManager.Register<IMyAppService, MyAppService>(DependencyLifeStyle.Transient);
            _myAppService = LocalIocManager.Resolve<IMyAppService>();
        }

        [Test]
        public void Should_Work_Proper_With_Right_Inputs()
        {
            var output = _myAppService.MyMethod(new MyMethodInput { MyStringValue = "test" });
            output.Result.ShouldBe(42);
        }

        [Test]
        public void Should_Work_With_Right_Nesned_Inputs()
        {
            var output = _myAppService.MyMethod2(new MyMethod2Input
            {
                MyStringValue2 = "test 1",
                Input1 = new MyMethodInput { MyStringValue = "test 2" },
                DateTimeValue = Clock.Now
            });
            output.Result.ShouldBe(42);
        }                        
        [Test]
        public void Should_Work_If_Array_Is_Null_But_DisabledValidation_For_Method()
        {
            _myAppService.MyMethod4_2(new MyMethod4Input());
        }

        [Test]
        public void Should_Work_If_Array_Is_Null_But_DisabledValidation_For_Property()
        {
            _myAppService.MyMethod5(new MyMethod5Input());
        }           

        [Test]
        public void Should_Stop_Recursive_Validation_In_A_Constant_Depth()
        {
            _myAppService.MyMethod8(new MyClassWithRecursiveReference { Value = "42" }).Result.ShouldBe(42);
        }

        [Test]
        public void Should_Allow_Null_For_Nullable_Enums()
        {
            _myAppService.MyMethodWithNullableEnum(null);
        }

        #region Nested Classes

        public interface IMyAppService
        {
            MyMethodOutput MyMethod(MyMethodInput input);
            MyMethodOutput MyMethod2(MyMethod2Input input);
            MyMethodOutput MyMethod3(MyMethod3Input input);
            MyMethodOutput MyMethod4(MyMethod4Input input);
            MyMethodOutput MyMethod4_2(MyMethod4Input input);
            MyMethodOutput MyMethod5(MyMethod5Input input);
            MyMethodOutput MyMethod8(MyClassWithRecursiveReference input);
            void MyMethodWithNullableEnum(MyEnum? value);
        }

        public class MyAppService : IMyAppService, IApplicationService
        {
            public MyMethodOutput MyMethod(MyMethodInput input)
            {
                return new MyMethodOutput { Result = 42 };
            }

            public MyMethodOutput MyMethod2(MyMethod2Input input)
            {
                return new MyMethodOutput { Result = 42 };
            }

            public MyMethodOutput MyMethod3(MyMethod3Input input)
            {
                return new MyMethodOutput { Result = 42 };
            }

            public MyMethodOutput MyMethod4(MyMethod4Input input)
            {
                return new MyMethodOutput { Result = 42 };
            }
            public MyMethodOutput MyMethod4_2(MyMethod4Input input)
            {
                return new MyMethodOutput { Result = 42 };
            }

            public MyMethodOutput MyMethod5(MyMethod5Input input)
            {
                return new MyMethodOutput { Result = 42 };
            }

         

            public MyMethodOutput MyMethod8(MyClassWithRecursiveReference input)
            {
                return new MyMethodOutput { Result = 42 };
            }

            public void MyMethodWithNullableEnum(MyEnum? value)
            {
                
            }
        }

        public class MyMethodInput
        {
            [Required]
            [MinLength(3)]
            public string MyStringValue { get; set; }
        }

        public class MyMethod2Input
        {
            [Required]
            [MinLength(2)]
            public string MyStringValue2 { get; set; }

            public DateTime DateTimeValue { get; set; }

            [Required]
            public MyMethodInput Input1 { get; set; }
        }

        public class MyMethod3Input
        {
            [Required]
            [MinLength(2)]
            public string MyStringValue2 { get; set; }

            public List<MyClassInList> ListItems { get; set; }

            public MyClassInList[] ArrayItems { get; set; }
        }

        public class MyMethod4Input
        {
            [Required]
            public MyClassInList[] ArrayItems { get; set; }
        }

        public class MyMethod5Input
        {
          
            public MyClassInList[] ArrayItems { get; set; }
        }

        
        public class MyClassInList
        {
            [Required]
            [MinLength(3)]
            public string ValueInList { get; set; }
        }

        public class MyMethodOutput
        {
            public int Result { get; set; }
        }

        public class MyClassWithRecursiveReference
        {
            public MyClassWithRecursiveReference Reference { get; }

            [Required]
            public string Value { get; set; }

            public MyClassWithRecursiveReference()
            {
                Reference = this;
            }
        }

        public enum MyEnum
        {
            Value1,
            Value2
        }

        #endregion
    }
}
