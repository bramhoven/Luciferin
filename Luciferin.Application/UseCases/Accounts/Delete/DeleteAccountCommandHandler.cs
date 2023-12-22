namespace Luciferin.Application.UseCases.Accounts.Delete;

using Abstractions.Providers;
using MediatR;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountProvider _accountProvider;

    public DeleteAccountCommandHandler(IAccountProvider accountProvider)
    {
        _accountProvider = accountProvider;
    }

    public Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        return _accountProvider.DeleteAccount(request.AccountId);
    }
}