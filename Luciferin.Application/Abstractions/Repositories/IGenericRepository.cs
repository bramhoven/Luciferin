using Luciferin.Core.Abstractions;

namespace Luciferin.Application.Abstractions.Repositories;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> Entities { get; }

    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> AddListAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}