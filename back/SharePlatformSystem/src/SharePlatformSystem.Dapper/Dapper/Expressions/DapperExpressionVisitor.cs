using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DapperExtensions;
using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Dapper.Expressions
{
    /// <summary>
    ///此类将表达式func tenty、bool转换为可用于
    ///dapperExtension的谓词系统
    /// </summary>
    /// <typeparam name="TEntity">实体的类型。</typeparam>
    /// <typeparam name="TPrimaryKey">类型的主要关键。</typeparam>
    /// <seealso cref="System.Linq.Expressions.ExpressionVisitor" />
    internal class DapperExpressionVisitor<TEntity, TPrimaryKey> : ExpressionVisitor where TEntity : class, IEntity<TPrimaryKey>
    {
        private PredicateGroup _pg;
        private Expression _processedProperty;
        private bool _unarySpecified;
        private Stack<PredicateGroup> _predicateGroupStack;
        public PredicateGroup _currentGroup { get; set; }
        public DapperExpressionVisitor()
        {
            Expressions = new HashSet<Expression>();
            _predicateGroupStack = new Stack<PredicateGroup>();
        }

        /// <summary>
        ///保留二进制表达式
        /// </summary>
        public HashSet<Expression> Expressions { get; }

        public IPredicate Process(Expression exp)
        {
            _pg = new PredicateGroup { Predicates = new List<IPredicate>() };
            _currentGroup = _pg;
            Visit(Evaluator.PartialEval(exp));

            // 第一个表达式确定根组运算符
            if (Expressions.Any())
            {
                _pg.Operator = Expressions.First().NodeType == ExpressionType.OrElse ? GroupOperator.Or : GroupOperator.And;
            }

            return _pg.Predicates.Count == 1 ? _pg.Predicates[0] : _pg;
        }

        private static Operator DetermineOperator(Expression binaryExpression)
        {
            switch (binaryExpression.NodeType)
            {
                case ExpressionType.Equal:
                    return Operator.Eq;
                case ExpressionType.GreaterThan:
                    return Operator.Gt;
                case ExpressionType.GreaterThanOrEqual:
                    return Operator.Ge;
                case ExpressionType.LessThan:
                    return Operator.Lt;
                case ExpressionType.LessThanOrEqual:
                    return Operator.Le;
                default:
                    return Operator.Eq;
            }
        }

        private IFieldPredicate GetCurrentField()
        {
            return GetCurrentField(_currentGroup);
        }

        private IFieldPredicate GetCurrentField(IPredicateGroup group)
        {
            IPredicate last = group.Predicates.Last();
            if (last is IPredicateGroup)
            {
                return GetCurrentField(last as IPredicateGroup);
            }
            return last as IFieldPredicate;
        }

        private void AddField(MemberExpression exp, Operator op = Operator.Eq, object value = null, bool not = false)
        {
            PredicateGroup pg = _currentGroup;

            //需要从表达式<func<t，bool>>转换为表达式<func<t，object>>，因为这是谓词.field（）所需的
            Expression<Func<TEntity, object>> fieldExp = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(exp, typeof(object)), exp.Expression as ParameterExpression);

            IFieldPredicate field = Predicates.Field(fieldExp, op, value, not);
            pg.Predicates.Add(field);
        }


        #region 访问方法覆盖
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expressions.Add(node);

            ExpressionType nt = node.NodeType;

            if (nt == ExpressionType.OrElse || nt == ExpressionType.AndAlso)
            {
                var pg = new PredicateGroup
                {
                    Predicates = new List<IPredicate>(),
                    Operator = nt == ExpressionType.OrElse ? GroupOperator.Or : GroupOperator.And
                };
                _currentGroup.Predicates.Add(pg);
                _predicateGroupStack.Push(_currentGroup);
                _currentGroup = pg;

            }

            Visit(node.Left);

            if (node.Left is MemberExpression)
            {
                IFieldPredicate field = GetCurrentField();
                field.Operator = DetermineOperator(node);

                if (nt == ExpressionType.NotEqual)
                {
                    field.Not = true;
                }
            }

            Visit(node.Right);
            if (nt == ExpressionType.OrElse || nt == ExpressionType.AndAlso)
            {
                _currentGroup = _predicateGroupStack.Pop();
            }
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.MemberType != MemberTypes.Property || node.Expression.Type != typeof(TEntity))
            {
                throw new NotSupportedException($"The member '{node}' is not supported");
            }

            //如果prop是visitmetholdcall的一部分，则跳过
            if (_processedProperty != null && _processedProperty == node)
            {
                _processedProperty = null;
                return node;
            }

            AddField(node);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            IFieldPredicate field = GetCurrentField();
            field.Value = node.Value;
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Type == typeof(bool) && node.Method.DeclaringType == typeof(string))
            {
                object arg = ((ConstantExpression)node.Arguments[0]).Value;
                var op = Operator.Like;

                switch (node.Method.Name.ToLowerInvariant())
                {
                    case "startswith":
                        arg = arg + "%";
                        break;
                    case "endswith":
                        arg = "%" + arg;
                        break;
                    case "contains":
                        arg = "%" + arg + "%";
                        break;
                    case "equals":
                        op = Operator.Eq;
                        break;
                    default:
                        throw new NotSupportedException($"The method '{node}' is not supported");
                }

                // 这是一个PropertyExpression，但由于它是内部的，因此要使用，我们将强制转换为基memberExpression
                _processedProperty = node.Object;
                var me = _processedProperty as MemberExpression;

                AddField(me, op, arg, _unarySpecified);

                // 重置（如果适用）
                _unarySpecified = false;

                return node;
            }

            throw new NotSupportedException($"The method '{node}' is not supported");
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            _unarySpecified = true;

            return base.VisitUnary(node); //返回基，因为我们希望继续进一步处理-ie随后调用visitmethodcall
        }
        #endregion
    }
}
