namespace Luciferin.Application.UseCases.Requisitions.Delete;

using MediatR;

public record DeleteRequisitionCommand(string AccountId) : IRequest;