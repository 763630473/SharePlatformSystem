using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Framework.Models;
using SharePlatformSystem.Logging;
using System;
using System.Runtime.Serialization;

namespace SharePlatformSystem.Framework
{
    /// <summary>
    /// ���쳣����ֱ����ʾ���û���
    /// </summary>
    [Serializable]
    public class UserFriendlyException : SharePlatformException, IHasLogSeverity, IHasErrorCode
    {
        /// <summary>
        /// �й��쳣��������Ϣ��
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// ���������롣
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///�쳣�������ԡ�
        /// Ĭ��ֵ: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// ���캯��.
        /// </summary>
        public UserFriendlyException()
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// �������л��Ĺ��캯����
        /// </summary>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        public UserFriendlyException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="severity">�쳣������</param>
        public UserFriendlyException(string message, LogSeverity severity)
            : base(message)
        {
            Severity = severity;
        }

        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="code">�������</param>
        /// <param name="message">�쳣��Ϣ</param>
        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="details">�й��쳣��������Ϣ</param>
        public UserFriendlyException(string message, string details)
            : this(message)
        {
            Details = details;
        }

        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="details">�й��쳣��������Ϣ</param>
        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="innerException">�ڲ��쳣</param>
        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="details">�й��쳣��������Ϣ</param>
        /// <param name="innerException">�ڲ��쳣</param>
        public UserFriendlyException(string message, string details, Exception innerException)
            : this(message, innerException)
        {
            Details = details;
        }
    }
}