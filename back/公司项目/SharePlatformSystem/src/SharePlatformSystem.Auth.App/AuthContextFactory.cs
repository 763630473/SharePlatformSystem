using SharePlatformSystem.Applications.Services;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Auth.EfRepository.Interface;

namespace SharePlatformSystem.Auth.App
{
    /// <summary>
    ///  加载用户所有可访问的资源/机构/模块
    /// </summary>
    public class AuthContextFactory:ApplicationService
    {
        private SystemAuthStrategy _systemAuth;
        private readonly NormalAuthStrategy _normalAuthStrategy;
        private readonly IUnitWork _unitWork;

        public AuthContextFactory(SystemAuthStrategy sysStrategy
            , NormalAuthStrategy normalAuthStrategy
            , IUnitWork unitWork)
        {
            _systemAuth = sysStrategy;
            _normalAuthStrategy = normalAuthStrategy;
            _unitWork = unitWork;
        }

        public AuthStrategyContext GetAuthStrategyContext(string username)
        {
            IAuthStrategy service = null;
             if (username == "System")
            {
                service= _systemAuth;
            }
            else
            {
                service = _normalAuthStrategy;
                service.User = _unitWork.FindSingle<User>(u => u.Account == username);
            }

         return new AuthStrategyContext(service);
        }
    }
}