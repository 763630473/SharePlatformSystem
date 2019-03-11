using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace SharePlatformSystem.Core.Modules
{
    /// <summary>
    /// ���ڴ洢ģ�������������Ϣ��
    /// </summary>
    public class SharePlatformModuleInfo
    {
        /// <summary>
        /// ����ģ�鶨��ĳ��򼯡�
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// ģ������͡�
        /// </summary>
        public Type Type { get; }

        /// <summary>
        ///ģ���ʵ����
        /// </summary>
        public SharePlatformModule Instance { get; }

        /// <summary>
        ///��ģ���Ƿ���Ϊ������أ�
        /// </summary>
        public bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// ��ģ����������ģ�顣
        /// </summary>
        public List<SharePlatformModuleInfo> Dependencies { get; }

        /// <summary>
        ///�����µ�SharePlatformModuleInfo����
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