using Luciferin.Core.Entities;

namespace Luciferin.Application.Abstractions;

public interface IServiceBus
{
    Task PublishTransactionEvent(Transaction transaction, bool successful, string url);
}