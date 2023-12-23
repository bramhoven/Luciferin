namespace Luciferin.Infrastructure.GoCardless.Providers;

using Abstractions;
using Application.Abstractions.Providers;
using AutoMapper;
using Core.Entities;

public class GoCardlessInstitutionProvider : IInstitutionProvider
{
    private readonly IGoCardlessService _goCardlessService;
    private readonly IMapper _mapper;

    public GoCardlessInstitutionProvider(IMapper mapper, IGoCardlessService goCardlessService)
    {
        _mapper = mapper;
        _goCardlessService = goCardlessService;
    }

    /// <inheritdoc />
    public async Task<Institution> GetByIdAsync(string id)
    {
        var goCardlessInstitution = await _goCardlessService.GetInstitutionAsync(id);
        return _mapper.Map<Institution>(goCardlessInstitution);
    }

    /// <inheritdoc />
    public async Task<List<Institution>> GetAllAsync()
    {
        var goCardlessInstitutions = await _goCardlessService.GetInstitutionsAsync();
        return goCardlessInstitutions.Select(_mapper.Map<Institution>).ToList();
    }
}