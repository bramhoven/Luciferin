namespace Luciferin.Application.UseCases.Requisitions.Delete;

using Abstractions.Providers;
using MediatR;

public class DeleteRequisitionCommandHandler : IRequestHandler<DeleteRequisitionCommand>
{
    private readonly IRequisitionProvider _requisitionProvider;

    public DeleteRequisitionCommandHandler(IRequisitionProvider requisitionProvider)
    {
        _requisitionProvider = requisitionProvider;
    }

    public Task Handle(DeleteRequisitionCommand request, CancellationToken cancellationToken)
    {
        return _requisitionProvider.DeleteRequisitionAsync(request.AccountId);
    }
}