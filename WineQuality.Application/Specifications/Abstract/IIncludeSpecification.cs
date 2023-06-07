namespace WineQuality.Application.Specifications.Abstract;

public interface IIncludeSpecification<TEntity>
{
    List<string> Includes { get; }
}