using System.Net;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Events.Bus;
using SharePlatformSystem.Events.Bus.Exceptions;
using SharePlatformSystem.Framework.AspNetCore.Configuration;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Extensions;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Results;
using SharePlatformSystem.Framework.Models;
using SharePlatformSystem.Logging;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.ExceptionHandling
{
    public class SharePlatformExceptionFilter : IExceptionFilter, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public IEventBus EventBus { get; set; }

        private readonly IErrorInfoBuilder _errorInfoBuilder;
        private readonly ISharePlatformAspNetCoreConfiguration _configuration;

        public SharePlatformExceptionFilter(IErrorInfoBuilder errorInfoBuilder, ISharePlatformAspNetCoreConfiguration configuration)
        {
            _errorInfoBuilder = errorInfoBuilder;
            _configuration = configuration;

            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    context.ActionDescriptor.GetMethodInfo(),
                    _configuration.DefaultWrapResultAttribute
                );

            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            HandleAndWrapException(context, wrapResultAttribute);
        }

        protected virtual void HandleAndWrapException(ExceptionContext context, WrapResultAttribute wrapResultAttribute)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            context.HttpContext.Response.StatusCode = GetStatusCode(context, wrapResultAttribute.WrapOnError);

            if (!wrapResultAttribute.WrapOnError)
            {
                return;
            }

            context.Result = new ObjectResult(
                new AjaxResponse(
                    _errorInfoBuilder.BuildForException(context.Exception)
                )
            );

            EventBus.Trigger(this, new SharePlatformHandledExceptionData(context.Exception));

            context.Exception = null; //Handled!
        }

        protected virtual int GetStatusCode(ExceptionContext context, bool wrapOnError)
        {         
            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            if (wrapOnError)
            {
                return (int)HttpStatusCode.InternalServerError;
            }

            return context.HttpContext.Response.StatusCode;
        }
    }
}
