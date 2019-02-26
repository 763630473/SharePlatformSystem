using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.ModelBinding
{
    public class SharePlatformDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(DateTime) &&
                context.Metadata.ModelType != typeof(DateTime?))
            {
                return null;
            }

            if (context.Metadata.ContainerType == null)
            {
                return null;
            }

            var dateNormalizationDisabledForClass = context.Metadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);
            var dateNormalizationDisabledForProperty = context.Metadata.ContainerType
                                                                        .GetProperty(context.Metadata.PropertyName)
                                                                        .IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

            if (!dateNormalizationDisabledForClass && !dateNormalizationDisabledForProperty)
            {
                return new SharePlatformDateTimeModelBinder(context.Metadata.ModelType);
            }

            return null;
        }
    }
}