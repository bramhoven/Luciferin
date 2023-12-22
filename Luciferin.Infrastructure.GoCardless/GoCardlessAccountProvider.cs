namespace Luciferin.Infrastructure.GoCardless;

using Abstractions;
using Application.Abstractions.Providers;
using Core.Entities;
using Flurl.Http;

public sealed class GoCardlessAccountProvider : IAccountProvider
{
    private readonly IGoCardlessService _goCardlessService;

    public GoCardlessAccountProvider(IGoCardlessService goCardlessService) => this._goCardlessService = goCardlessService;

    public Task<Account> GetByIdAsync(int id) => throw new NotImplementedException();

    public Task<List<Account>> GetAllAsync() => throw new NotImplementedException();

    public async Task<string> RequestNewAccountConnection(string name, string institutionId, string returnUrl)
    {
        try
        {
            var institution = await this._goCardlessService.GetInstitution(institutionId);
            var endUserAgreement = await this._goCardlessService.CreateEndUserAgreement(institution);
            var requisition = await this._goCardlessService.CreateRequisition(institution, name, endUserAgreement, returnUrl);
            return requisition.Link;
        }
        catch (FlurlHttpException e)
        {
            return null;
        }
    }
}