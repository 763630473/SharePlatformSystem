using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    /// Ϊ����ʱ��������ʱ�䷶Χ����ӿڡ�
    /// </summary>
    public interface IZonedDateTimeRange : IDateTimeRange
    {
        /// <summary>
        /// ����ʱ�䷶Χ��ʱ��
        /// </summary>
        string Timezone { get; set; }

        /// <summary>
        /// ��ƫ�ƵĿ�ʼʱ��
        /// </summary>
        DateTimeOffset StartTimeOffset { get; set; }

        /// <summary>
        /// ��ƫ�ƵĽ���ʱ��
        /// </summary>
        DateTimeOffset EndTimeOffset { get; set; }

        /// <summary>
        /// ��UTC��ʾ�Ŀ�ʼʱ��
        /// </summary>
        DateTime StartTimeUtc { get; set; }

        /// <summary>
        ///��UTC��ʾ�Ľ���ʱ��
        /// </summary>
        DateTime EndTimeUtc { get; set; }
    }
}
