using System;
using System.Reflection;

namespace SharePlatformSystem.Aspects
{

    internal abstract class AspectAttribute : Attribute
    {
        public Type InterceptorType { get; set; }

        protected AspectAttribute(Type interceptorType)
        {
            InterceptorType = interceptorType;
        }
    }

    internal interface ISharePlatformInterceptionContext
    {
        object Target { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        object ReturnValue { get; }

        bool Handled { get; set; }
    }

    internal interface ISharePlatformBeforeExecutionInterceptionContext : ISharePlatformInterceptionContext
    {

    }


    internal interface ISharePlatformAfterExecutionInterceptionContext : ISharePlatformInterceptionContext
    {
        Exception Exception { get; }
    }

    internal interface ISharePlatformInterceptor<TAspect>
    {
        TAspect Aspect { get; set; }

        void BeforeExecution(ISharePlatformBeforeExecutionInterceptionContext context);

        void AfterExecution(ISharePlatformAfterExecutionInterceptionContext context);
    }

    internal abstract class SharePlatformInterceptorBase<TAspect> : ISharePlatformInterceptor<TAspect>
    {
        public TAspect Aspect { get; set; }

        public virtual void BeforeExecution(ISharePlatformBeforeExecutionInterceptionContext context)
        {
        }

        public virtual void AfterExecution(ISharePlatformAfterExecutionInterceptionContext context)
        {
        }
    }

    internal class Test_Aspects
    {
        internal class MyAspectAttribute : AspectAttribute
        {
            public int TestValue { get; set; }

            public MyAspectAttribute()
                : base(typeof(MyInterceptor))
            {
            }
        }

        internal class MyInterceptor : SharePlatformInterceptorBase<MyAspectAttribute>
        {
            public override void BeforeExecution(ISharePlatformBeforeExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }

            public override void AfterExecution(ISharePlatformAfterExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }
        }

        public class MyService
        {
            [MyAspect(TestValue = 41)] //Usage!
            public void DoIt()
            {

            }
        }

        public class MyClient
        {
            private readonly MyService _service;

            public MyClient(MyService service)
            {
                _service = service;
            }

            public void Test()
            {
                _service.DoIt();
            }
        }
    }
}
