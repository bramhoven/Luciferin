using Luciferin.Application.Abstractions.Providers;
using MediatR;

namespace Luciferin.Application.UseCases.Accounts.RequestConnection;

public class RequestConnectionAccountCommandHandler : IRequestHandler<RequestConnectionAccountCommand, string>
{
    private readonly IAccountProvider _accountProvider;

    public RequestConnectionAccountCommandHandler(IAccountProvider accountProvider)
    {
        _accountProvider = accountProvider;
    }

    public Task<string> Handle(RequestConnectionAccountCommand request, CancellationToken cancellationToken)
    {
        return _accountProvider.RequestNewAccountConnection(request.Name, request.InstitutionId, request.ReturnUrl);
    }
}