namespace Luciferin.Application.UseCases.Requisitions.Get;

using Core.Entities;
using MediatR;

public record GetImportAccountsCommand : IRequest<ICollection<Requisition>>;