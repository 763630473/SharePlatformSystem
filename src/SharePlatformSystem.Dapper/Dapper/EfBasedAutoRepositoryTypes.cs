﻿using SharePlatformSystem.Dapper.Repositories;

namespace SharePlatformSystem.Dapper
{
    public static class EfBasedDapperAutoRepositoryTypes
    {
        static EfBasedDapperAutoRepositoryTypes()
        {
            Default = new DapperAutoRepositoryTypeAttribute(
                typeof(IDapperRepository<>),
                typeof(IDapperRepository<,>),
                typeof(DapperEfRepositoryBase<,>),
                typeof(DapperEfRepositoryBase<,,>)
            );
        }

        public static DapperAutoRepositoryTypeAttribute Default { get; }
    }
}
