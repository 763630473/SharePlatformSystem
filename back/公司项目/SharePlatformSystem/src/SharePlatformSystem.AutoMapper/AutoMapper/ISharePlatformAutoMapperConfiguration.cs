using System;
using System.Collections.Generic;
using AutoMapper;

namespace SharePlatformSystem.AutoMapper
{
    public interface ISharePlatformAutoMapperConfiguration
    {
        List<Action<IMapperConfigurationExpression>> Configurators { get; }

        /// <summary>
        /// Use static <see cref="Mapper.Instance"/>.
        /// Default: true.
        /// </summary>
        bool UseStaticMapper { get; set; }
    }
}