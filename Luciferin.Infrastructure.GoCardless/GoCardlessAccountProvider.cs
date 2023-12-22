namespace Luciferin.Infrastructure.GoCardless;

using Abstractions;
using Application.Abstractions.Providers;
using Core.Entities;

public sealed class GoCardlessAccountProvider : IAccountProvider
{
    private readonly IGoCardlessService _goCardlessService;

    public GoCardlessAccountProvider(IGoCardlessService goCardlessService)
    {
        _goCardlessService = goCardlessService;
    }

    public Task<Account> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<string> RequestNewAccountConnection(string name, string institutionId, string returnUrl)
    {
        var institution = await _goCardlessService.GetInstitutionAsync(institutionId);
        var endUserAgreement = await _goCardlessService.CreateEndUserAgreementAsync(institution);
        var requisition = await _goCardlessService.CreateRequisitionAsync(institution, name, endUserAgreement, returnUrl);
        return requisition.Link;
    }

    public async Task<bool> DeleteAccount(string accountId)
    {
        return await _goCardlessService.DeleteRequisitionAsync(accountId);
    }
}