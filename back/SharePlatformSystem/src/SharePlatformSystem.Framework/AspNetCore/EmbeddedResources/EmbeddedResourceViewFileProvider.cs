using SharePlatformSystem.Core.Resources.Embedded;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Framework.AspNetCore.EmbeddedResources
{
    public class EmbeddedResourceViewFileProvider : EmbeddedResourceFileProvider
    {
        public EmbeddedResourceViewFileProvider(IIocResolver iocResolver) 
            : base(iocResolver)
        {
        }

        protected override bool IsIgnoredFile(EmbeddedResourceItem resource)
        {
            return resource.FileExtension != "cshtml";
        }
    }
}