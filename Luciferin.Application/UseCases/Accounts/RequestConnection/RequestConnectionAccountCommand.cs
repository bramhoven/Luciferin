using MediatR;

namespace Luciferin.Application.UseCases.Accounts.RequestConnection;

public record RequestConnectionAccountCommand(string Name, string InstitutionId, string ReturnUrl) : IRequest<string>;