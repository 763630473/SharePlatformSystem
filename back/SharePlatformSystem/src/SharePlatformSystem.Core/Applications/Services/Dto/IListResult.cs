using System.Collections.Generic;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// �˽ӿڱ�����Ϊ��׼�����Ա���ͻ���������Ŀ�б�
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list</typeparam>
    public interface IListResult<T>
    {
        /// <summary>
        /// ��Ŀ�嵥��
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}