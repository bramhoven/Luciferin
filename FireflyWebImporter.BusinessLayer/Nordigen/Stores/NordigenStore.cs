using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Nordigen.Helpers;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models.Requests;
using FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses;
using Flurl;
using Flurl.Http;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Stores
{
    public class NordigenStore : INordigenStore
    {
        #region Fields

        private readonly string _nordigenBaseUrl;
        private readonly string _nordigenSecretId;
        private readonly string _nordigenSecretKey;

        #endregion

        #region Constructors

        public NordigenStore(string nordigenBaseUrl, string nordigenSecretId, string nordigenSecretKey)
        {
            _nordigenBaseUrl = nordigenBaseUrl;
            _nordigenSecretId = nordigenSecretId;
            _nordigenSecretKey = nordigenSecretKey;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<EndUserAgreement> CreateEndUserAgreement(NordigenEndUserAgreementRequest request, OpenIdToken openIdToken)
        {
            var endUserAgreementResponse = await NordigenRoutes
                                                 .EndUserAgreements(_nordigenBaseUrl)
                                                 .WithOAuthBearerToken(openIdToken.AccessToken)
                                                 .PostJsonAsync(request)
                                                 .ReceiveJson<NordigenEndUserAgreementResponse>();
            return endUserAgreementResponse.MapToEndUserAgreement();
        }

        /// <inheritdoc />
        public async Task<Requisition> CreateRequisition(NordigenRequisitionRequest request, OpenIdToken openIdToken)
        {
            var requisitionResponse = await NordigenRoutes
                                            .Requisitions(_nordigenBaseUrl)
                                            .WithOAuthBearerToken(openIdToken.AccessToken)
                                            .PostJsonAsync(request)
                                            .ReceiveJson<NordigenRequisitionResponse>();
            return requisitionResponse.MapToRequisition();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteEndUserAgreement(string endUserAgreementId, OpenIdToken openIdToken)
        {
            try
            {
                await NordigenRoutes
                      .EndUserAgreement(_nordigenBaseUrl, endUserAgreementId)
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
        public async Task<bool> DeleteRequisition(string requisitionId, OpenIdToken openIdToken)
        {
            try
            {
                await NordigenRoutes
                      .Requisition(_nordigenBaseUrl, requisitionId)
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
        public async Task<Account> GetAccount(string accountId, OpenIdToken openIdToken)
        {
            var accountResponse = await NordigenRoutes
                                        .Account(_nordigenBaseUrl, accountId)
                                        .WithOAuthBearerToken(openIdToken.AccessToken)
                                        .GetJsonAsync<NordigenAccountResponse>();
            return accountResponse.MapToAccount();
        }

        /// <inheritdoc />
        public async Task<ICollection<Balance>> GetAccountBalance(string accountId, OpenIdToken openIdToken)
        {
            var accountBalanceResponse = await NordigenRoutes
                                               .AccountBalances(_nordigenBaseUrl, accountId)
                                               .WithOAuthBearerToken(openIdToken.AccessToken)
                                               .GetJsonAsync<NordigenAccountBalanceResponse>();
            return accountBalanceResponse.MapToBalanceCollection();
        }

        /// <inheritdoc />
        public async Task<AccountDetails> GetAccountDetails(string accountId, OpenIdToken openIdToken)
        {
            var accountDetailsResponse = await NordigenRoutes
                                               .AccountDetails(_nordigenBaseUrl, accountId)
                                               .WithOAuthBearerToken(openIdToken.AccessToken)
                                               .GetJsonAsync<NordigenAccountDetailsResponse>();
            return accountDetailsResponse.MapToAccountDetails();
        }

        /// <inheritdoc />
        public async Task<ICollection<Transaction>> GetAccountTransactions(string accountId, OpenIdToken openIdToken)
        {
            var transactionResponse = await NordigenRoutes
                                            .AccountTransactions(_nordigenBaseUrl, accountId)
                                            .WithOAuthBearerToken(openIdToken.AccessToken)
                                            .GetJsonAsync<NordigenTransactionResponse>();
            return transactionResponse.MapToTransactionCollection();
        }

        /// <inheritdoc />
        public async Task<ICollection<Transaction>> GetAccountTransactions(string accountId, DateTime fromDate, OpenIdToken openIdToken)
        {
            var transactionResponse = await NordigenRoutes
                                            .AccountTransactions(_nordigenBaseUrl, accountId)
                                            .SetQueryParam("date_from", fromDate.ToString("yyyy-MM-dd"))
                                            .WithOAuthBearerToken(openIdToken.AccessToken)
                                            .GetJsonAsync<NordigenTransactionResponse>();
            return transactionResponse.MapToTransactionCollection();
        }

        /// <inheritdoc />
        public async Task<EndUserAgreement> GetEndUserAgreement(string endUserAgreementId, OpenIdToken openIdToken)
        {
            var endUserAgreementResponse = await NordigenRoutes
                                                 .EndUserAgreement(_nordigenBaseUrl, endUserAgreementId)
                                                 .WithOAuthBearerToken(openIdToken.AccessToken)
                                                 .GetJsonAsync<NordigenEndUserAgreementResponse>();
            return endUserAgreementResponse.MapToEndUserAgreement();
        }

        /// <inheritdoc />
        public async Task<Institution> GetInstitution(string institutionId, OpenIdToken openIdToken)
        {
            var institutionResponse = await NordigenRoutes
                                            .Institution(_nordigenBaseUrl, institutionId)
                                            .WithOAuthBearerToken(openIdToken.AccessToken)
                                            .GetJsonAsync<NordigenInstitutionResponse>();
            return institutionResponse.MapToInstitution();
        }

        /// <inheritdoc />
        public async Task<ICollection<Institution>> GetInstitutions(string countryCode, OpenIdToken openIdToken)
        {
            var institutionsResponse = await NordigenRoutes
                                             .Institutions(_nordigenBaseUrl)
                                             .WithOAuthBearerToken(openIdToken.AccessToken)
                                             .SetQueryParam("country", countryCode)
                                             .GetJsonAsync<ICollection<NordigenInstitutionResponse>>();
            return institutionsResponse.MapToInstitutionCollection();
        }

        /// <inheritdoc />
        public async Task<Requisition> GetRequisition(string requisitionId, OpenIdToken openIdToken)
        {
            var requisitionResponse = await NordigenRoutes
                                            .Requisition(_nordigenBaseUrl, requisitionId)
                                            .WithOAuthBearerToken(openIdToken.AccessToken)
                                            .GetJsonAsync<NordigenRequisitionResponse>();
            return requisitionResponse.MapToRequisition();
        }

        /// <inheritdoc />
        public async Task<ICollection<Requisition>> GetRequisitions(OpenIdToken openIdToken)
        {
            var requisitionResponses = new List<NordigenRequisitionResponse>();

            string next;
            do
            {
                var paginatedResponse = await NordigenRoutes
                                              .Requisitions(_nordigenBaseUrl)
                                              .WithOAuthBearerToken(openIdToken.AccessToken)
                                              .GetJsonAsync<NordigenPaginatedResponse<NordigenRequisitionResponse>>();

                requisitionResponses.AddRange(paginatedResponse.Results);
                next = paginatedResponse.Next;
            } while (!string.IsNullOrWhiteSpace(next));

            return requisitionResponses.MapToRequisitionCollection();
        }

        /// <inheritdoc />
        public async Task<OpenIdToken> GetToken()
        {
            var tokenResponse = await NordigenRoutes
                                      .TokenNew(_nordigenBaseUrl)
                                      .PostJsonAsync(new NordigenTokenRequest
                                      {
                                          SecretId = _nordigenSecretId,
                                          SecretKey = _nordigenSecretKey
                                      })
                                      .ReceiveJson<NordigenTokenResponse>();
            return tokenResponse.MapToOpenIdToken();
        }

        /// <inheritdoc />
        public async Task<OpenIdToken> RefreshToken(OpenIdToken token)
        {
            var tokenResponse = await NordigenRoutes
                                      .TokenRefresh(_nordigenBaseUrl)
                                      .PostJsonAsync(new NordigenTokenRefreshRequest
                                      {
                                          Refresh = token.RefreshToken
                                      })
                                      .ReceiveJson<NordigenTokenResponse>();
            return tokenResponse.MapToOpenIdToken();
        }

        #endregion
    }
}