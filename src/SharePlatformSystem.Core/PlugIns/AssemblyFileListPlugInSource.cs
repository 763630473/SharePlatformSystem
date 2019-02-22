using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharePlatformSystem.Core.PlugIns
{
    //TODO: This class is similar to FolderPlugInSource. Create an abstract base class for them.
    public class AssemblyFileListPlugInSource : IPlugInSource
    {
        public string[] FilePaths { get; }

        private readonly Lazy<List<Assembly>> _assemblies;

        public AssemblyFileListPlugInSource(params string[] filePaths)
        {
            FilePaths = filePaths ?? new string[0];

            _assemblies = new Lazy<List<Assembly>>(LoadAssemblies, true);
        }

        public List<Assembly> GetAssemblies()
        {
            return _assemblies.Value;
        }

        public List<Type> GetModules()
        {
            var modules = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (SharePlatformModule.IsAbpModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new SharePlatformInitializationException("无法从程序集获取模块类型： " + assembly.FullName, ex);
                }
            }

            return modules;
        }

        private List<Assembly> LoadAssemblies()
        {
            return FilePaths.Select(
                Assembly.LoadFile //TODO: Use AssemblyLoadContext.Default.LoadFromAssemblyPath instead?
            ).ToList();
        }
    }
}