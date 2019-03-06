using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Infrastructure;

namespace SharePlatformSystem.Auth.App.SSO
{
    public class LoginResult :Response<string>
    {
        public string ReturnUrl;
        public string Token;
        public User User;
    }
}