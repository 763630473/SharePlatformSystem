using System;

namespace SharePlatformSystem
{
    /// <summary>
    /// ���ڱ�ʾ��������ѡ������
    /// </summary>
    public class NamedTypeSelector
    {
        /// <summary>
        /// ѡ���������ơ�
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ν�
        /// </summary>
        public Func<Type, bool> Predicate { get; set; }

        /// <summary>
        /// �����µġ�NamedTypeSelector������
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="predicate">ν��</param>
        public NamedTypeSelector(string name, Func<Type, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}