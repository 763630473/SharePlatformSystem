using System.Reflection;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// 用于简化工作流程单元的助手类。
    /// </summary>
    internal static class UnitOfWorkHelper
    {
        /// <summary>
        /// 如果给定方法具有UnitOfWorkAttribute属性，则返回true。
        /// </summary>
        /// <param name="memberInfo">要检查的方法信息</param>
        public static bool HasUnitOfWorkAttribute(MemberInfo memberInfo)
        {
            return memberInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }
}