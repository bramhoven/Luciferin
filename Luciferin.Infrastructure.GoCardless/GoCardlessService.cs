using Flurl;
using Flurl.Http;
using Luciferin.Core.Exceptions;
using Luciferin.Infrastructure.GoCardless.Abstractions;
using Luciferin.Infrastructure.GoCardless.Helpers;
using Luciferin.Infrastructure.GoCardless.Models;
using Luciferin.Infrastructure.GoCardless.Models.Requests;
using Luciferin.Infrastructure.GoCardless.Models.Responses;
using Microsoft.Extensions.Options;

namespace Luciferin.Infrastructure.GoCardless;

public class GoCardlessService : IGoCardlessService
{
    private readonly GoCardlessSettings _goCardlessSettings;

    public GoCardlessService(IOptionsSnapshot<GoCardlessSettings> options)
    {
        _goCardlessSettings = options.Value;
    }

    /// <inheritdoc />
    public async Task<EndUserAgreement> CreateEndUserAgreement(Institution institution)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var request = new GoCardlessEndUserAgreementRequest
        {
            AccessScopes = new[] { "balances", "details", "transactions" },
            InstitutionId = institution.Id,
            MaxHistoricalDays = institution.TransactionTotalDays,
            AccessValidForDays = 90
        };

        var endUserAgreementResponse = await GoCardlessRoutes
            .EndUserAgreements(_goCardlessSettings.BaseUrl)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .PostJsonAsync(request)
            .ReceiveJson<GoCardlessEndUserAgreementResponse>();
        return endUserAgreementResponse.MapToEndUserAgreement();
    }

    /// <inheritdoc />
    public async Task<Requisition> CreateRequisition(Institution institution, string name,
        EndUserAgreement endUserAgreement, string returnUrl)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var request = new GoCardlessRequisitionRequest
        {
            Reference = name,
            AgreementId = endUserAgreement.Id,
            InstitutionId = institution.Id,
            RedirectUrl = returnUrl
        };

        var requisitionResponse = await GoCardlessRoutes
            .Requisitions(_goCardlessSettings.BaseUrl)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .PostJsonAsync(request)
            .ReceiveJson<GoCardlessRequisitionResponse>();
        return requisitionResponse.MapToRequisition();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteEndUserAgreement(string endUserAgreementId)
    {
        try
        {
            CheckConfiguration();
            var openIdToken = await GetToken();

            await GoCardlessRoutes
                .EndUserAgreement(_goCardlessSettings.BaseUrl, endUserAgreementId)
                .WithOAuthBearerToken(openIdToken.AccessToken)
                .DeleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteRequisition(string requisitionId)
    {
        try
        {
            CheckConfiguration();
            var openIdToken = await GetToken();

            await GoCardlessRoutes
                .Requisition(_goCardlessSettings.BaseUrl, requisitionId)
                .WithOAuthBearerToken(openIdToken.AccessToken)
                .DeleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<Account> GetAccount(string accountId)
    {
        try
        {
            CheckConfiguration();
            var openIdToken = await GetToken();

            var accountResponse = await GoCardlessRoutes
                .Account(_goCardlessSettings.BaseUrl, accountId)
                .WithOAuthBearerToken(openIdToken.AccessToken)
                .GetJsonAsync<GoCardlessAccountResponse>();
            return accountResponse.MapToAccount();
        }
        catch (FlurlHttpException e)
        {
            if (e.StatusCode == 409)
                throw new AccountSuspendedException(accountId);

            throw new AccountFailureException(accountId);
        }
    }

    /// <inheritdoc />
    public async Task<ICollection<Balance>> GetAccountBalance(string accountId)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var accountBalanceResponse = await GoCardlessRoutes
            .AccountBalances(_goCardlessSettings.BaseUrl, accountId)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .GetJsonAsync<GoCardlessAccountBalanceResponse>();
        return accountBalanceResponse.MapToBalanceCollection();
    }

    /// <inheritdoc />
    public async Task<AccountDetails> GetAccountDetails(string accountId)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var accountDetailsResponse = await GoCardlessRoutes
            .AccountDetails(_goCardlessSettings.BaseUrl, accountId)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .GetJsonAsync<GoCardlessAccountDetailsResponse>();
        return accountDetailsResponse.MapToAccountDetails();
    }

    /// <inheritdoc />
    public async Task<ICollection<Transaction>> GetAccountTransactions(string accountId)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var transactionResponse = await GoCardlessRoutes
            .AccountTransactions(_goCardlessSettings.BaseUrl, accountId)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .GetJsonAsync<GoCardlessTransactionResponse>();
        return transactionResponse.MapToTransactionCollection();
    }

    /// <inheritdoc />
    public async Task<ICollection<Transaction>> GetAccountTransactions(string accountId, DateTime fromDate)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var transactionResponse = await GoCardlessRoutes
            .AccountTransactions(_goCardlessSettings.BaseUrl, accountId)
            .SetQueryParam("date_from", fromDate.ToString("yyyy-MM-dd"))
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .GetJsonAsync<GoCardlessTransactionResponse>();
        return transactionResponse.MapToTransactionCollection();
    }

    /// <inheritdoc />
    public async Task<EndUserAgreement> GetEndUserAgreement(string endUserAgreementId)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var endUserAgreementResponse = await GoCardlessRoutes
            .EndUserAgreement(_goCardlessSettings.BaseUrl, endUserAgreementId)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .GetJsonAsync<GoCardlessEndUserAgreementResponse>();
        return endUserAgreementResponse.MapToEndUserAgreement();
    }

    /// <inheritdoc />
    public async Task<Institution> GetInstitution(string institutionId)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var institutionResponse = await GoCardlessRoutes
            .Institution(_goCardlessSettings.BaseUrl, institutionId)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .GetJsonAsync<GoCardlessInstitutionResponse>();
        return institutionResponse.MapToInstitution();
    }

    /// <inheritdoc />
    public async Task<ICollection<Institution>> GetInstitutions(string countryCode)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var institutionsResponse = await GoCardlessRoutes
            .Institutions(_goCardlessSettings.BaseUrl)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .SetQueryParam("country", countryCode)
            .GetJsonAsync<ICollection<GoCardlessInstitutionResponse>>();
        return institutionsResponse.MapToInstitutionCollection();
    }

    /// <inheritdoc />
    public async Task<Requisition> GetRequisition(string requisitionId)
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var requisitionResponse = await GoCardlessRoutes
            .Requisition(_goCardlessSettings.BaseUrl, requisitionId)
            .WithOAuthBearerToken(openIdToken.AccessToken)
            .GetJsonAsync<GoCardlessRequisitionResponse>();
        return requisitionResponse.MapToRequisition();
    }

    /// <inheritdoc />
    public async Task<ICollection<Requisition>> GetRequisitions()
    {
        CheckConfiguration();
        var openIdToken = await GetToken();

        var requisitionResponses = new List<GoCardlessRequisitionResponse>();

        string next;
        do
        {
            var paginatedResponse = await GoCardlessRoutes
                .Requisitions(_goCardlessSettings.BaseUrl)
                .WithOAuthBearerToken(openIdToken.AccessToken)
                .GetJsonAsync<GoCardlessPaginatedResponse<GoCardlessRequisitionResponse>>();

            requisitionResponses.AddRange(paginatedResponse.Results);
            next = paginatedResponse.Next;
        } while (!string.IsNullOrWhiteSpace(next));

        return requisitionResponses.MapToRequisitionCollection();
    }

    /// <summary>
    ///     Checks whether all the GoCardless settings are set.
    /// </summary>
    /// <returns></returns>
    private bool IsConfigured()
    {
        return !string.IsNullOrWhiteSpace(_goCardlessSettings.BaseUrl)
               && !string.IsNullOrWhiteSpace(_goCardlessSettings.SecretId)
               && !string.IsNullOrWhiteSpace(_goCardlessSettings.SecretKey);
    }

    /// <summary>
    ///     Checks whether the GoCardless settings are set and throws an exception if at least one is missing.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private void CheckConfiguration()
    {
        if (!IsConfigured())
            throw new InvalidOperationException("GoCardless is not configured properly");
    }

    /// <summary>
    ///     Gets the GoCardless OpenId token.
    /// </summary>
    /// <returns></returns>
    private async Task<OpenIdToken> GetToken()
    {
        var tokenResponse = await GoCardlessRoutes
            .TokenNew(_goCardlessSettings.BaseUrl)
            .PostJsonAsync(new GoCardlessTokenRequest
            {
                SecretId = _goCardlessSettings.SecretId,
                SecretKey = _goCardlessSettings.SecretKey
            })
            .ReceiveJson<GoCardlessTokenResponse>();
        return tokenResponse.MapToOpenIdToken();
    }

    /// <summary>
    ///     Refreshes the OpenId Token.
    /// </summary>
    /// <param name="token">The token to refresh.</param>
    /// <returns></returns>
    private async Task<OpenIdToken> RefreshToken(OpenIdToken token)
    {
        var tokenResponse = await GoCardlessRoutes
            .TokenRefresh(_goCardlessSettings.BaseUrl)
            .PostJsonAsync(new GoCardlessTokenRefreshRequest
            {
                Refresh = token.RefreshToken
            })
            .ReceiveJson<GoCardlessTokenResponse>();
        return tokenResponse.MapToOpenIdToken();
    }
}