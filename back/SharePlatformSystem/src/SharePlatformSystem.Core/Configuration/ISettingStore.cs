using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// �˽ӿ����ڴ�/������Դ�����ݿ⣩��ȡ/�����������á�
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        ///��ȡ���û��ֵ��
        /// </summary>
        /// <param name="userId">UserId or null</param>
        /// <param name="name">���õ�����</param>
        /// <returns>���ö���</returns>
        Task<SettingInfo> GetSettingOrNullAsync(string userId, string name);

        /// <summary>
        /// ɾ�����á�
        /// </summary>
        /// <param name="setting">Ҫɾ��������</param>
        Task DeleteAsync(SettingInfo setting);

        /// <summary>
        ///������á�
        /// </summary>
        /// <param name="setting">�������</param>
        Task CreateAsync(SettingInfo setting);

        /// <summary>
        /// �������á�
        /// </summary>
        /// <param name="setting">�������</param>
        Task UpdateAsync(SettingInfo setting);

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="userId">UserId or null</param>
        /// <returns>�����б�</returns>
        Task<List<SettingInfo>> GetAllListAsync(string userId);
    }
}