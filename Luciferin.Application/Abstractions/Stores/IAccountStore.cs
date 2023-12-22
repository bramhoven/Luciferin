using Luciferin.Application.Abstractions.Repositories;
using Luciferin.Core.Entities;

namespace Luciferin.Application.Abstractions.Stores;

public interface IAccountStore : IGenericRepository<Account>
{
}