using SharePlatformSystem.Core.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharePlatformSystem.Core.Reflection
{
    public class SharePlatformAssemblyFinder : IAssemblyFinder
    {
        private readonly ISharePlatformModuleManager _moduleManager;

        public SharePlatformAssemblyFinder(ISharePlatformModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public List<Assembly> GetAllAssemblies()
        {
            var assemblies = new List<Assembly>();

            foreach (var module in _moduleManager.Modules)
            {
                assemblies.Add(module.Assembly);
                assemblies.AddRange(module.Instance.GetAdditionalAssemblies());
            }

            return assemblies.Distinct().ToList();
        }
    }
}