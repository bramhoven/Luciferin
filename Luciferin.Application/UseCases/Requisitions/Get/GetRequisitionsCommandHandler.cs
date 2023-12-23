namespace Luciferin.Application.UseCases.Requisitions.Get;

using Abstractions.Providers;
using Core.Entities;
using MediatR;

public class GetRequisitionsCommandHandler : IRequestHandler<GetImportAccountsCommand, ICollection<Requisition>>
{
    private readonly IRequisitionProvider _requisitionProvider;

    public GetRequisitionsCommandHandler(IRequisitionProvider requisitionProvider)
    {
        _requisitionProvider = requisitionProvider;
    }

    public async Task<ICollection<Requisition>> Handle(GetImportAccountsCommand request, CancellationToken cancellationToken)
    {
        return await _requisitionProvider.GetAllAsync();
    }
}