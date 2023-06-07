using System.Linq.Expressions;
using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Specifications.Abstract;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Application.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetBySpecificationAsync(
        IFilterSpecification<TEntity>? filterSpecification = null,
        IIncludeSpecification<TEntity>? includeSpecification = null,
        IOrderSpecification<TEntity>? orderSpecification = null, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate,
        CancellationToken cancellationToken = default);
    
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate, IIncludeSpecification<TEntity> includeSpecification,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(string id, IIncludeSpecification<TEntity>? includeSpecification, CancellationToken cancellationToken = default);
    Task<TResult?> GetByIdAsync<TResult>(string id, CancellationToken cancellationToken = default) where TResult : BaseResult;
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}