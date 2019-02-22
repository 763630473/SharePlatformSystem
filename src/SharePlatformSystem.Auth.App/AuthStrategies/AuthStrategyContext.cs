using SharePlatformSystem.Infrastructure;
using SharePlatform.Auth.EfRepository.Domain;
using SharePlatformSystem.Auth.App.Response;
using System.Collections.Generic;
namespace SharePlatformSystem.Auth.App
{
    /// <summary>
    ///  授权策略上下文，一个典型的策略模式
    /// </summary>
    public class AuthStrategyContext
    {
        private readonly IAuthStrategy _strategy;
        public AuthStrategyContext(IAuthStrategy strategy)
        {
            this._strategy = strategy;
        }

        public User User
        {
            get { return _strategy.User; }
        }

        public List<ModuleView> Modules
        {
            get { return _strategy.Modules; }
        }

        public List<ModuleElement> ModuleElements
        {
            get { return _strategy.ModuleElements; }
        }

        public List<Role> Roles
        {
            get { return _strategy.Roles; }
        }

        public List<Resource> Resources
        {
            get { return _strategy.Resources; }
        }

        public List<Org> Orgs
        {
            get { return _strategy.Orgs; }
        }

        public List<KeyDescription> GetProperties(string moduleCode)
        {
            return _strategy.GetProperties(moduleCode);
        }

    }

}
