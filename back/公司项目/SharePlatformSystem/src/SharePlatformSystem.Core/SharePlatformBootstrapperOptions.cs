using SharePlatformSystem.Core.PlugIns;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem
{
    public class SharePlatformBootstrapperOptions
    {
        /// <summary>
        /// Used to disable all interceptors added by SharePlatform.
        /// </summary>
        public bool DisableAllInterceptors { get; set; }

        /// <summary>
        /// IIocManager that is used to bootstrap the SharePlatform system. If set to null, uses global <see cref="SharePlatform.Dependency.IocManager.Instance"/>
        /// </summary>
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// List of plugin sources.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        public SharePlatformBootstrapperOptions()
        {
            IocManager = SharePlatformSystem.Dependency.IocManager.Instance;
            PlugInSources = new PlugInSourceList();
        }
    }
}
