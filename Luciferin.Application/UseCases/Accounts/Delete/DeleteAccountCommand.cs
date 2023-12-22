namespace Luciferin.Application.UseCases.Accounts.Delete;

using MediatR;

public record DeleteAccountCommand(string AccountId) : IRequest;