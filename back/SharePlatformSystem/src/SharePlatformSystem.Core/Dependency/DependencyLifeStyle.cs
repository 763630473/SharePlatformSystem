namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// ����ע��ϵͳ��ʹ�õ����͵����ʽ��
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// ���������ڵ�һ�ν���ʱ�����˵�������
        ///ͬһʵ�����ں����Ľ�����
        /// </summary>
        Singleton,

        /// <summary>
        /// ˲�����塣Ϊÿ�ν�������һ������
        /// </summary>
        Transient
    }
}