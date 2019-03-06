
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Core.Authorization.Users;
using SharePlatformSystem.Core.Domain.Repositories;

namespace SharePlatformSystem.Framework.Authorization.Users
{
    public class UserStore : SharePlatformUserStore<User>
    {
        public UserStore()
            : base()
        {

        }
    }
}