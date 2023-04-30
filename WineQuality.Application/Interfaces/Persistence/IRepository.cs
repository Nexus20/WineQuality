using System.Linq.Expressions;
using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Application.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<Expression<Func<TEntity, BaseEntity>>>? includes = null,
        bool disableTracking = true, CancellationToken cancellationToken = default);
    
    Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<Expression<Func<TEntity, BaseEntity>>>? includes = null,
        bool disableTracking = true, CancellationToken cancellationToken = default) where TResult : BaseResult;

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<TResult?> GetByIdAsync<TResult>(string id, CancellationToken cancellationToken = default) where TResult : BaseResult;
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}