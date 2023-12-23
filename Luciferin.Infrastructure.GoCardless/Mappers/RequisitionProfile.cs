namespace Luciferin.Infrastructure.GoCardless.Mappers;

using AutoMapper;
using Models;
using Requisition = Core.Entities.Requisition;

public class RequisitionProfile : Profile
{
    public RequisitionProfile()
    {
        CreateMap<GCRequisition, Requisition>()
            .ForMember(r => r.Accounts, opts => opts.Ignore())
            .ReverseMap();
    }
}