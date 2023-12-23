namespace Luciferin.Application.UseCases.Institutions.Get;

using Core.Entities;
using MediatR;

public record GetInstitutionsCommand(string? CountryCode = null) : IRequest<ICollection<Institution>>;