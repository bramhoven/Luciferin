namespace Luciferin.Infrastructure.GoCardless.Mappers;

using AutoMapper;
using Core.Entities;
using Models;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<GCAccount, Account>()
            .ReverseMap();
    }
}