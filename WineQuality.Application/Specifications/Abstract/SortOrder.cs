using System.Linq.Expressions;

namespace WineQuality.Application.Specifications.Abstract;

public class SortOrder<TEntity>
{
    public Expression<Func<TEntity, object>> KeySelector { get; set; }
    public SortDirection SortDirection { get; set; }
}