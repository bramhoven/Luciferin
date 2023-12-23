using Luciferin.Core.Abstractions;

namespace Luciferin.Application.Abstractions.Repositories;

public interface IGenericRepository<TEntity, TKey>
    where TEntity : class
{
    IQueryable<TEntity> Entities { get; }

    Task<TEntity> GetByKeyAsync(TKey key);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task<ICollection<TEntity>> AddListAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}