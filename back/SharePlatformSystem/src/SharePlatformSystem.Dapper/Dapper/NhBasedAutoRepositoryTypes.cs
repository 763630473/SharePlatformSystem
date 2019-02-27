using SharePlatformSystem.Dapper.Repositories;

namespace SharePlatformSystem.Dapper
{
    public class NhBasedDapperAutoRepositoryTypes
    {
        static NhBasedDapperAutoRepositoryTypes()
        {
            Default = new DapperAutoRepositoryTypeAttribute(
                typeof(IDapperRepository<>),
                typeof(IDapperRepository<,>),
                typeof(DapperRepositoryBase<>),
                typeof(DapperRepositoryBase<,>)
            );
        }

        public static DapperAutoRepositoryTypeAttribute Default { get; }
    }
}
