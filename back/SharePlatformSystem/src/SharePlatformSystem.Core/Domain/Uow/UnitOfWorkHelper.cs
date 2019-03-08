using System.Reflection;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// ���ڼ򻯹������̵�Ԫ�������ࡣ
    /// </summary>
    internal static class UnitOfWorkHelper
    {
        /// <summary>
        /// ���������������UnitOfWorkAttribute���ԣ��򷵻�true��
        /// </summary>
        /// <param name="memberInfo">Ҫ���ķ�����Ϣ</param>
        public static bool HasUnitOfWorkAttribute(MemberInfo memberInfo)
        {
            return memberInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }
}