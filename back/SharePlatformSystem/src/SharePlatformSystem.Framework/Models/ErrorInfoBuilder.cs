using System;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Framework.AspNetCore.Configuration;

namespace SharePlatformSystem.Framework.Models
{
    public class ErrorInfoBuilder : IErrorInfoBuilder, ISingletonDependency
    {
        private IExceptionToErrorInfoConverter Converter { get; set; }

        public ErrorInfoBuilder(ISharePlatformWebCommonModuleConfiguration configuration, ILocalizationManager localizationManager)
        {
            Converter = new DefaultErrorInfoConverter(configuration, localizationManager);
        }

        public ErrorInfo BuildForException(Exception exception)
        {
            return Converter.Convert(exception);
        }

        /// <summary>
        /// 添加由<see cref=“buildForException”/>方法使用的异常转换器。
        /// </summary>
        /// <param name="converter">转换器对象</param>
        public void AddExceptionConverter(IExceptionToErrorInfoConverter converter)
        {
            converter.Next = Converter;
            Converter = converter;
        }
    }
}
