namespace Luciferin.Application.UseCases.Institutions.Get;

using Core.Entities;
using MediatR;

public record GetInstitutionsCommand : IRequest<ICollection<Institution>>;