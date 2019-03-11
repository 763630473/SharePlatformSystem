using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharePlatformSystem.Collections.Extensions;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Framework.AspNetCore.Configuration;

namespace SharePlatformSystem.Framework.Models
{
    internal class DefaultErrorInfoConverter : IExceptionToErrorInfoConverter
    {
        private readonly ISharePlatformWebCommonModuleConfiguration _configuration;
        private readonly ILocalizationManager _localizationManager;

        public IExceptionToErrorInfoConverter Next { set; private get; }

        private bool SendAllExceptionsToClients
        {
            get
            {
                return _configuration.SendAllExceptionsToClients;
            }
        }

        public DefaultErrorInfoConverter(
            ISharePlatformWebCommonModuleConfiguration configuration,
            ILocalizationManager localizationManager)
        {
            _configuration = configuration;
            _localizationManager = localizationManager;
        }

        public ErrorInfo Convert(Exception exception)
        {
            var errorInfo = CreateErrorInfoWithoutCode(exception);

            if (exception is IHasErrorCode)
            {
                errorInfo.Code = (exception as IHasErrorCode).Code;
            }

            return errorInfo;
        }

        private ErrorInfo CreateErrorInfoWithoutCode(Exception exception)
        {
            if (SendAllExceptionsToClients)
            {
                return CreateDetailedErrorInfoFromException(exception);
            }

            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is UserFriendlyException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (exception is UserFriendlyException)
            {
                var userFriendlyException = exception as UserFriendlyException;
                return new ErrorInfo(userFriendlyException.Message, userFriendlyException.Details);
            }
         

            if (exception is EntityNotFoundException)
            {
                var entityNotFoundException = exception as EntityNotFoundException;

                if (entityNotFoundException.EntityType != null)
                {
                    return new ErrorInfo(
                        string.Format(
                            L("EntityNotFound"),
                            entityNotFoundException.EntityType.Name,
                            entityNotFoundException.Id
                        )
                    );
                }

                return new ErrorInfo(
                    entityNotFoundException.Message
                );
            }          

            return new ErrorInfo(L("InternalServerError"));
        }

        private ErrorInfo CreateDetailedErrorInfoFromException(Exception exception)
        {
            var detailBuilder = new StringBuilder();

            AddExceptionToDetails(exception, detailBuilder);

            var errorInfo = new ErrorInfo(exception.Message, detailBuilder.ToString());       

            return errorInfo;
        }

        private void AddExceptionToDetails(Exception exception, StringBuilder detailBuilder)
        {
            //异常消息
            detailBuilder.AppendLine(exception.GetType().Name + ": " + exception.Message);

            //用户友好异常的其他信息
            if (exception is UserFriendlyException)
            {
                var userFriendlyException = exception as UserFriendlyException;
                if (!string.IsNullOrEmpty(userFriendlyException.Details))
                {
                    detailBuilder.AppendLine(userFriendlyException.Details);
                }
            }

            //异常堆栈跟踪
            if (!string.IsNullOrEmpty(exception.StackTrace))
            {
                detailBuilder.AppendLine("STACK TRACE: " + exception.StackTrace);
            }

            //内部例外
            if (exception.InnerException != null)
            {
                AddExceptionToDetails(exception.InnerException, detailBuilder);
            }

            //聚合异常的内部异常
            if (exception is AggregateException)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerExceptions.IsNullOrEmpty())
                {
                    return;
                }

                foreach (var innerException in aggException.InnerExceptions)
                {
                    AddExceptionToDetails(innerException, detailBuilder);
                }
            }
        }

      

       
        private string L(string name)
        {
            try
            {
                return _localizationManager.GetString(SharePlatformWebConsts.LocalizaionSourceName, name);
            }
            catch (Exception)
            {
                return name;
            }
        }
    }
}