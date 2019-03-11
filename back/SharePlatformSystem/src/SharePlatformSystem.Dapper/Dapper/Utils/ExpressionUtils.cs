using System;
using System.Linq.Expressions;

namespace SharePlatformSystem.Dapper.Utils
{
    internal class ExpressionUtils
    {
        /// <summary>
        ///制造谓词
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">名称</param>
        /// <param name="value">值.</param>
        /// <param name="typeOfValue">值的类型。</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> MakePredicate<T>(
            string name,
            object value,
            Type typeOfValue = null)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), typeof(T).Name);
            MemberExpression memberExp = Expression.Property(param, name);
            BinaryExpression body = Expression.Equal(memberExp, typeOfValue == null ? Expression.Constant(value) : Expression.Constant(value, typeOfValue));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
