namespace Luciferin.Application.UseCases.Requisitions.Add;

using MediatR;

public record AddRequisitionCommand(string Name, string InstitutionId, string ReturnUrl) : IRequest<string>;