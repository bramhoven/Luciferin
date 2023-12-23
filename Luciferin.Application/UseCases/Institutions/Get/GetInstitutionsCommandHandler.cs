namespace Luciferin.Application.UseCases.Institutions.Get;

using Abstractions.Providers;
using Core.Entities;
using MediatR;

public class GetInstitutionsCommandHandler : IRequestHandler<GetInstitutionsCommand, ICollection<Institution>>
{
    private readonly IRequisitionProvider _requisitionProvider;

    public GetInstitutionsCommandHandler(IRequisitionProvider requisitionProvider)
    {
        _requisitionProvider = requisitionProvider;
    }

    public Task<ICollection<Institution>> Handle(GetInstitutionsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}