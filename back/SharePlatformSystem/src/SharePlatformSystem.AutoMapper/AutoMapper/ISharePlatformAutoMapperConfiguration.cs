using System;
using System.Collections.Generic;
using AutoMapper;

namespace SharePlatformSystem.AutoMapper
{
    public interface ISharePlatformAutoMapperConfiguration
    {
        List<Action<IMapperConfigurationExpression>> Configurators { get; }

        /// <summary>
        ///使用static<see cref=“mapper.instance”/>。
        ///默认值：真。
        /// </summary>
        bool UseStaticMapper { get; set; }
    }
}