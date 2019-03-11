using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using SharePlatformSystem.Core.Reflection.Extensions;
namespace SharePlatformSystem.AutoMapper
{
    public static class AutoMapperConfigurationExtensions
    {
        public static void CreateAutoAttributeMaps(this IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<AutoMapAttributeBase>())
            {
                autoMapAttribute.CreateMap(configuration, type);
            }
        }
        public static void CreateAutoAttributeMaps(this IMapperConfigurationExpression configuration, Type type, Type[] targetTypes, MemberList memberList)
        {

            //获取源中具有automapkeyattribute的所有属性
            var sourceKeysPropertyInfo = type.GetProperties()
                                             .Where(w => w.GetCustomAttribute<AutoMapKeyAttribute>() != null)
                                             .Select(s => s).ToList();

            foreach (var targetType in targetTypes)
            {
                if (!sourceKeysPropertyInfo.Any())
                {
                    configuration.CreateMap(type, targetType, memberList);
                    continue;
                }

                BinaryExpression equalityComparer = null;

                //在lambda表达式中表示源示例：（source）=>…
                ParameterExpression sourceParameterExpression = Expression.Parameter(type, "source");
                //在lambda表达式中表示目标示例：（target）=>…
                ParameterExpression targetParameterExpression = Expression.Parameter(targetType, "target");


                //我们可以使用多个automapkey来比较
                foreach (PropertyInfo propertyInfo in sourceKeysPropertyInfo)
                {
                    //在lambda表达式中表示参数的特定属性示例：（source）=>source.id
                    MemberExpression sourcePropertyExpression = Expression.Property(sourceParameterExpression, propertyInfo);

                    //为目标查找与之比较的同名属性
                    //例如，如果我们在源代码中有属性id上的attribut automapkey，那么我们希望在目标中获取要比较的id，而不是
                    var targetPropertyInfo = targetType.GetProperty(sourcePropertyExpression.Member.Name);

                    //如果目标中不存在automapkeyattribute为的属性，则会发生这种情况
                    if (targetPropertyInfo is null)
                    {
                        continue;
                    }

                    //在lambda表达式中，表示参数的特定属性示例：（target）=>target.id
                    MemberExpression targetPropertyExpression = Expression.Property(targetParameterExpression, targetPropertyInfo);

                    //比较源中automapkey定义的属性与目标中的属性不同
                    //示例（source，target）=>source.id==target.id
                    BinaryExpression equal = Expression.Equal(sourcePropertyExpression, targetPropertyExpression);

                    if (equalityComparer is null)
                    {
                        equalityComparer = equal;
                    }
                    else
                    {
                        //如果我们比较多个键，我们要在
                        //Exemple : (source, target) => source.Email == target.Email && source.UserName == target.UserName
                        equalityComparer = Expression.And(equalityComparer, equal);
                    }
                }

                //如果目标中没有匹配的automapkey
                //在这种情况下，我们添加默认映射
                if (equalityComparer is null)
                {
                    configuration.CreateMap(type, targetType, memberList);
                    continue;
                }

                //我们需要使func<sourcetype，targettype，bool>的泛型类型来调用后面的expression.lambda
                var funcGenericType = typeof(Func<,,>).MakeGenericType(type, targetType, typeof(bool));

                //生成表达式的方法信息。lambda<func<sourceType，targetType，bool>>稍后调用
                var lambdaMethodInfo = typeof(Expression).GetMethod("Lambda", 2, 1).MakeGenericMethod(funcGenericType);

                //调用expression.lambda
                var expressionLambdaResult = lambdaMethodInfo.Invoke(null, new object[] { equalityComparer, new ParameterExpression[] { sourceParameterExpression, targetParameterExpression } });

                //获取imapperConfigurationExpression.createMap<source，target>
                var createMapMethodInfo = configuration.GetType().GetMethod("CreateMap", 1, 2).MakeGenericMethod(type, targetType);

                //调用configuration.createmap<source，target>（）。
                var createMapResult = createMapMethodInfo.Invoke(configuration, new object[] { memberList });

                var autoMapperCollectionAssembly = Assembly.Load("AutoMapper.Collection");

                var autoMapperCollectionTypes = autoMapperCollectionAssembly.GetTypes();

                var equalityComparisonGenericMethodInfo = autoMapperCollectionTypes
                                         .Where(w => !w.IsGenericType && !w.IsNested)
                                         .SelectMany(s => s.GetMethods()).Where(w => w.Name == "EqualityComparison")
                                         .FirstOrDefault()
                                         .MakeGenericMethod(type, targetType);

                //调用EqualityComparison
                //Exemple configuration.CreateMap<Source, Target>().EqualityComparison((source, target) => source.Id == target.Id)
                equalityComparisonGenericMethodInfo.Invoke(createMapResult, new object[] { createMapResult, expressionLambdaResult });
            }
        }
    }
}