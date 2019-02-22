using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Modules
{
    /// <summary>
    ///用于定义abp模块与其他模块的依赖关系。
    ///应该用于从“abpmodule”派生的类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// 依赖模块的类型。
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        ///用于定义模块与其他模块的依赖关系。
        /// </summary>
        /// <param name="dependedModuleTypes">依赖模块的类型</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}