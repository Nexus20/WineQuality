using System.Linq.Expressions;

namespace WineQuality.Application.Specifications.Abstract;

public interface IFilterSpecification<TEntity>
{
    Expression<Func<TEntity, bool>> Criteria { get; }
}