using System.Linq.Expressions;

namespace WineQuality.Application.Specifications.Abstract;

public class FilterSpecification<TEntity> : IFilterSpecification<TEntity>
{
    public Expression<Func<TEntity, bool>> Criteria { get; }
    
    public FilterSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
    
    public static FilterSpecification<TEntity> Default => new(x => true);
}