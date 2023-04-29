using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Domain.Entities.Abstract;
using WineQuality.Infrastructure.Persistence;

namespace WineQuality.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly IMapper Mapper;
    protected readonly ApplicationDbContext DbContext;

    public Repository(ApplicationDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        Mapper = mapper;
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
            return GetAllAsync(cancellationToken);

        return DbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
            return DbContext.Set<TEntity>().FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        return DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
    }

    public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<Expression<Func<TEntity, BaseEntity>>>? includes = null,
        bool disableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return orderBy(query).ToListAsync(cancellationToken: cancellationToken);
        return query.ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<Expression<Func<TEntity, BaseEntity>>>? includes = null,
        bool disableTracking = true, CancellationToken cancellationToken = default) where TResult : BaseResult
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        return query.ProjectTo<TResult>(Mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken: cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
            return DbContext.Set<TEntity>().CountAsync(cancellationToken: cancellationToken);

        return DbContext.Set<TEntity>().CountAsync(predicate, cancellationToken: cancellationToken);
    }

    public virtual Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public virtual Task<TResult?> GetByIdAsync<TResult>(string id, CancellationToken cancellationToken = default) where TResult : BaseResult
    {
        return DbContext.Set<TEntity>()
            .ProjectTo<TResult>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().AddAsync(entity, cancellationToken).AsTask();
    }

    public void Update(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}