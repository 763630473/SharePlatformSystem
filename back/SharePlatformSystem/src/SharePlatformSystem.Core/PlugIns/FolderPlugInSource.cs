using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SharePlatformSystem.Core.PlugIns
{
    public class FolderPlugInSource : IPlugInSource
    {
        public string Folder { get; }

        public SearchOption SearchOption { get; set; }

        private readonly Lazy<List<Assembly>> _assemblies;
        
        public FolderPlugInSource(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Folder = folder;
            SearchOption = searchOption;

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
                        if (SharePlatformModule.IsSharePlatformModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new SharePlatformInitializationException("无法从程序集获取模块类型：" + assembly.FullName, ex);
                }
            }

            return modules;
        }

        private List<Assembly> LoadAssemblies()
        {
            return AssemblyHelper.GetAllAssembliesInFolder(Folder, SearchOption);
        }
    }
}