namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    ///�˽ӿڱ����������¼�������ʵ�֣�
    ///ֻ��һ�����Ͳ������̳н�ʹ�ô˲�����
    //
    ///���磻
    ///����ѧ���̳��ˡ�������EntityCreatedEventDataѧ����
    ///���EntityCreatedEventDataʵ�֣�Ҳ�ᴥ��EntityCreatedEventData Person
    ///�˽ӿڡ�
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        ///��ȡ�Դ����������ʵ��������������Ĳ�����
        /// </summary>
        /// <returns>���캯������</returns>
        object[] GetConstructorArgs();
    }
}