using SharePlatformSystem.Auth.EfRepository.Core;
using System.Security.Claims;

namespace SharePlatformSystem.Core.Authorization.Users
{
    public class SharePlatformLoginResult<TUser>
        where TUser : Entity
    {
        public SharePlatformLoginResultType Result { get; private set; }

        public TUser User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public SharePlatformLoginResult(SharePlatformLoginResultType result, TUser user = null)
        {
            Result = result;
            User = user;
        }

        public SharePlatformLoginResult(TUser user, ClaimsIdentity identity)
            : this(SharePlatformLoginResultType.Success)
        {
            User = user;
            Identity = identity;
        }
    }
}