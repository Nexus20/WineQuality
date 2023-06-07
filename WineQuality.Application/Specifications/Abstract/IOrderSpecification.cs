namespace WineQuality.Application.Specifications.Abstract;

public interface IOrderSpecification<TEntity>
{
    List<SortOrder<TEntity>> OrderBy { get; }
}