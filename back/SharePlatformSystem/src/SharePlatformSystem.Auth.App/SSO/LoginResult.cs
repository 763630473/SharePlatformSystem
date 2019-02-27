using SharePlatformSystem.Infrastructure;

namespace SharePlatformSystem.Auth.App.SSO
{
    public class LoginResult :Response<string>
    {
        public string ReturnUrl;
        public string Token;
    }
}