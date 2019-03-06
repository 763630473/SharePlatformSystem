using System;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public class SharePlatformIdentityBuilder : IdentityBuilder
    {
        public SharePlatformIdentityBuilder(IdentityBuilder identityBuilder)
            : base(identityBuilder.UserType, identityBuilder.RoleType, identityBuilder.Services)
        {
        }
    }
}