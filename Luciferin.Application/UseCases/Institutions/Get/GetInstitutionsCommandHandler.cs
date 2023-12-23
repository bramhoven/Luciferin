namespace Luciferin.Application.UseCases.Institutions.Get;

using Abstractions.Providers;
using Core.Entities;
using MediatR;

public class GetInstitutionsCommandHandler : IRequestHandler<GetInstitutionsCommand, ICollection<Institution>>
{
    private readonly IInstitutionProvider _institutionProvider;

    public GetInstitutionsCommandHandler(IInstitutionProvider institutionProvider)
    {
        _institutionProvider = institutionProvider;
    }

    public async Task<ICollection<Institution>> Handle(GetInstitutionsCommand request, CancellationToken cancellationToken)
    {
        if (request.CountryCode == null)
            return await _institutionProvider.GetAllAsync();
        
        return await _institutionProvider.GetInstitutionsByCountryCode(request.CountryCode);
    }
}