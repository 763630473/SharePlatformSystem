using System;
using System.Collections.Generic;
using AutoMapper;

namespace SharePlatformSystem.AutoMapper
{
    public class SharePlatformAutoMapperConfiguration : ISharePlatformAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public bool UseStaticMapper { get; set; }

        public SharePlatformAutoMapperConfiguration()
        {
            UseStaticMapper = true;
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}