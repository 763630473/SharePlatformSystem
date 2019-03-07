namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// 用于获取/设置当前“iunitofwork”。
    /// </summary>
    public interface ICurrentUnitOfWorkProvider
    {
        /// <summary>
        ///获取/设置当前“iunitofwork”。
        ///如果可能，设置为空将返回到外部工作单元。
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}