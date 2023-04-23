using System.Linq.Expressions;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Application.Helpers.Expressions;

internal class ExpressionsHelper
{
    public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>>? left, Expression<Func<T, bool>>? right) where T : BaseEntity
    {
        if (left == null && right == null) 
            throw new ArgumentException("At least one argument must not be null");
        
        if (left == null) return right;
        if (right == null) return left;

        var parameter = Expression.Parameter(typeof(T), "p");
        var combined = new ParameterReplacer(parameter).Visit(Expression.AndAlso(left.Body, right.Body));
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

    public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>>? left, Expression<Func<T, bool>>? right) where T : BaseEntity
    {
        if (left == null && right == null) 
            throw new ArgumentException("At least one argument must not be null");
        
        if (left == null) return right;
        if (right == null) return left;

        var parameter = Expression.Parameter(typeof(T), "p");
        var combined = new ParameterReplacer(parameter).Visit(Expression.OrElse(left.Body, right.Body));
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }
}