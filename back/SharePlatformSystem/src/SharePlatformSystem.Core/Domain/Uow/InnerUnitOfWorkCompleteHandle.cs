using SharePlatformSystem.Core.Exceptions;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///此句柄用于内部工作范围单位。
    ///内部工作范围单位实际使用外部工作范围单位
    ///并且对“iunitofWorkCompleteHandle.Complete”调用没有影响。
    ///但如果不调用，则会在UOW末尾引发异常以回滚UOW。
    /// </summary>
    internal class InnerUnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {
        public const string DidNotCallCompleteMethodExceptionMessage = "未调用工作单元的完整方法。";

        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;

        public void Complete()
        {
            _isCompleteCalled = true;
        }

        public Task CompleteAsync()
        {
            _isCompleteCalled = true;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (!_isCompleteCalled)
            {
                if (HasException())
                {
                    return;
                }

                throw new SharePlatformException(DidNotCallCompleteMethodExceptionMessage);
            }
        }
        
        private static bool HasException()
        {
            try
            {
                return Marshal.GetExceptionCode() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}