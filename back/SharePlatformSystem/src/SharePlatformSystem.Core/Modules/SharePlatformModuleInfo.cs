using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace SharePlatformSystem.Core.Modules
{
    /// <summary>
    /// 用于存储模块所需的所有信息。
    /// </summary>
    public class SharePlatformModuleInfo
    {
        /// <summary>
        /// 包含模块定义的程序集。
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// 模块的类型。
        /// </summary>
        public Type Type { get; }

        /// <summary>
        ///模块的实例。
        /// </summary>
        public SharePlatformModule Instance { get; }

        /// <summary>
        ///此模块是否作为插件加载？
        /// </summary>
        public bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 此模块的所有相关模块。
        /// </summary>
        public List<SharePlatformModuleInfo> Dependencies { get; }

        /// <summary>
        ///创建新的SharePlatformModuleInfo对象。
        /// </summary>
        public SharePlatformModuleInfo([NotNull] Type type, [NotNull] SharePlatformModule instance, bool isLoadedAsPlugIn)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<SharePlatformModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}