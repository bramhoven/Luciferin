using Luciferin.Core.Abstractions;

namespace Luciferin.Application.Abstractions.Providers;

public interface IGenericProvider<T> where T : class, IEntity
{
    Task<T> GetByIdAsync(string id);
    Task<List<T>> GetAllAsync();
}