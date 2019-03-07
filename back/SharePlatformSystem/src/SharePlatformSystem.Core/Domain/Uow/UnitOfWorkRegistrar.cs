using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///���������Ϊ������Ԫ�����������ע����������
    /// </summary>
    internal static class UnitOfWorkRegistrar
    {
        /// <summary>
        ///��ʼ���Ĵ�����
        /// </summary>
        /// <param name="iocManager">IOC ������</param>
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += (key, handler) =>
            {
                var implementationType = handler.ComponentModel.Implementation.GetTypeInfo();

                HandleTypesWithUnitOfWorkAttribute(implementationType, handler);
                HandleConventionalUnitOfWorkTypes(iocManager, implementationType, handler);
            };
        }

        private static void HandleTypesWithUnitOfWorkAttribute(TypeInfo implementationType, IHandler handler)
        {
            if (IsUnitOfWorkType(implementationType) || AnyMethodHasUnitOfWork(implementationType))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }

        private static void HandleConventionalUnitOfWorkTypes(IIocManager iocManager, TypeInfo implementationType, IHandler handler)
        {
            if (!iocManager.IsRegistered<IUnitOfWorkDefaultOptions>())
            {
                return;
            }

            var uowOptions = iocManager.Resolve<IUnitOfWorkDefaultOptions>();

            if (uowOptions.IsConventionalUowClass(implementationType.AsType()))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }

        private static bool IsUnitOfWorkType(TypeInfo implementationType)
        {
            return UnitOfWorkHelper.HasUnitOfWorkAttribute(implementationType);
        }

        private static bool AnyMethodHasUnitOfWork(TypeInfo implementationType)
        {
            return implementationType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(UnitOfWorkHelper.HasUnitOfWorkAttribute);
        }
    }
}