using Luciferin.Core.Abstractions;

namespace Luciferin.Application.Abstractions.Stores;

public interface IGenericStore<T> where T : class, IEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> AddListAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}