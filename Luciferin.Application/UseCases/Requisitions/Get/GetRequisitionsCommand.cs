namespace Luciferin.Application.UseCases.Requisitions.Get;

using Core.Entities;
using MediatR;

public record GetRequisitionsCommand : IRequest<ICollection<Requisition>>;