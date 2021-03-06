﻿using SharePlatformSystem.Core.Collections.Extensions;
using SharePlatformSystem.Dependency;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharePlatformSystem.Core.Resources.Embedded
{
    public class EmbeddedResourceManager : IEmbeddedResourceManager, ISingletonDependency
    {
        private readonly IEmbeddedResourcesConfiguration _configuration;
        private readonly Lazy<Dictionary<string, EmbeddedResourceItem>> _resources;

        public EmbeddedResourceManager(IEmbeddedResourcesConfiguration configuration)
        {
            _configuration = configuration;
            _resources = new Lazy<Dictionary<string, EmbeddedResourceItem>>(
                CreateResourcesDictionary,
                true
            );
        }

        public EmbeddedResourceItem GetResource(string fullPath)
        {
            var encodedPath = EmbeddedResourcePathHelper.EncodeAsResourcesPath(fullPath);
            return _resources.Value.GetOrDefault(encodedPath);
        }

        public IEnumerable<EmbeddedResourceItem> GetResources(string fullPath)
        {
            var encodedPath = EmbeddedResourcePathHelper.EncodeAsResourcesPath(fullPath);
            if (encodedPath.Length > 0 && !encodedPath.EndsWith("."))
            {
                encodedPath = encodedPath + ".";
            }

            return _resources.Value.Where(k => k.Key.StartsWith(encodedPath)).Select(d => d.Value);
        }

        private Dictionary<string, EmbeddedResourceItem> CreateResourcesDictionary()
        {
            var resources = new Dictionary<string, EmbeddedResourceItem>(StringComparer.OrdinalIgnoreCase);

            foreach (var source in _configuration.Sources)
            {
                source.AddResources(resources);
            }

            return resources;
        }
    }
}