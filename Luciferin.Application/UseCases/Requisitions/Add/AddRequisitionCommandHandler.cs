namespace Luciferin.Application.UseCases.Requisitions.Add;

using Abstractions.Providers;
using MediatR;

public class AddRequisitionCommandHandler : IRequestHandler<AddRequisitionCommand, string>
{
    private readonly IRequisitionProvider _requisitionProvider;

    public AddRequisitionCommandHandler(IRequisitionProvider requisitionProvider)
    {
        _requisitionProvider = requisitionProvider;
    }

    public Task<string> Handle(AddRequisitionCommand request, CancellationToken cancellationToken)
    {
        return _requisitionProvider.AddRequisitionAsync(request.Name, request.InstitutionId, request.ReturnUrl);
    }
}