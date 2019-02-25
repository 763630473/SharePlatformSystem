using System.Collections.Generic;

namespace SharePlatformSystem.Core.Resources.Embedded
{
    public interface IEmbeddedResourcesConfiguration
    {
        List<EmbeddedResourceSet> Sources { get; }
    }
}