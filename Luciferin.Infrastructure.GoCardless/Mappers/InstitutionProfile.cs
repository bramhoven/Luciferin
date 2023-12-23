namespace Luciferin.Infrastructure.GoCardless.Mappers;

using AutoMapper;
using Models;

public class InstitutionProfile : Profile
{
    public InstitutionProfile()
    {
        CreateMap<Institution, Core.Entities.Institution>()
            .ReverseMap();
    }
}