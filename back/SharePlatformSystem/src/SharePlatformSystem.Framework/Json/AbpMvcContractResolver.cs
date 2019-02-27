using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Core.Timing;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Framework.AspNetCore.Configuration;
using SharePlatformSystem.Json;

namespace SharePlatformSystem.Framework.Json
{
    public class SharePlatformMvcContractResolver : DefaultContractResolver
    {
        private readonly IIocResolver _iocResolver;

        private bool? _useMvcDateTimeFormat { get; set; }

        private string _datetimeFormat { get; set; } = null;

        protected readonly object SyncObj = new object();

        public SharePlatformMvcContractResolver(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            ModifyProperty(member, property);

            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) != null)
            {
                return;
            }

            var converter = new SharePlatformDateTimeConverter();

            if (!_useMvcDateTimeFormat.HasValue)
            {
                lock (SyncObj)
                {
                    if (!_useMvcDateTimeFormat.HasValue)
                    {
                        using (var configuration = _iocResolver.ResolveAsDisposable<ISharePlatformAspNetCoreConfiguration>())
                        {
                            _useMvcDateTimeFormat = configuration.Object.UseMvcDateTimeFormatForAppServices;

                            if (_useMvcDateTimeFormat.Value)
                            {
                                using (var mvcJsonOptions = _iocResolver.ResolveAsDisposable<IOptions<MvcJsonOptions>>())
                                {
                                    _datetimeFormat = mvcJsonOptions.Object.Value.SerializerSettings.DateFormatString;
                                }
                            }
                        }
                    }
                }
            }
            
            // apply DateTimeFormat only if not empty
            if (!_datetimeFormat.IsNullOrWhiteSpace())
            {
                converter.DateTimeFormat = _datetimeFormat;
            }

            property.Converter = converter;
        }
    }
}