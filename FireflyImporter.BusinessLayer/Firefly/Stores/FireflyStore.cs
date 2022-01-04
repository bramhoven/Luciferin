using System.Collections.Generic;
using Flurl.Http;
using System.Linq;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Firefly.Enums;
using FireflyImporter.BusinessLayer.Firefly.Helpers;
using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Firefly.Models.Requests;
using FireflyImporter.BusinessLayer.Firefly.Models.Responses.Accounts;
using FireflyImporter.BusinessLayer.Firefly.Models.Responses.Transactions;
using FireflyImporter.BusinessLayer.Firefly.Models.Shared;
using Microsoft.Extensions.Logging;

namespace FireflyImporter.BusinessLayer.Firefly.Stores
{
    public class FireflyStore : IFireflyStore
    {
        #region Fields

        private readonly string _fireflyAccessToken;

        private readonly string _fireflyBaseUrl;

        private readonly ILogger<FireflyStore> _logger;

        #endregion

        #region Constructors

        public FireflyStore(string fireflyBaseUrl, string fireflyAccessToken, ILogger<FireflyStore> logger)
        {
            _fireflyBaseUrl = fireflyBaseUrl;
            _fireflyAccessToken = fireflyAccessToken;
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task AddNewTransactions(IEnumerable<FireflyTransaction> transactions)
        {
            var fireflyTransactions = transactions.ToList();

            var totalTransactions = fireflyTransactions.Count;
            var index = 0;
            foreach (var transaction in fireflyTransactions)
            {
                var request = GetCreateTransactionRequest(transaction);
                try
                {
                    var response = await FireflyRoutes
                                         .Transactions(_fireflyBaseUrl)
                                         .WithOAuthBearerToken(_fireflyAccessToken)
                                         .WithHeader("Accept", "application/json")
                                         .PostJsonAsync(request)
                                         .ConfigureAwait(false);

                    _logger.LogInformation($"Imported transaction [{++index}/{totalTransactions}] [{transaction.Type.ToString()}] {transaction.Description}");
                }
                catch (FlurlHttpException e)
                {
                    if (e.StatusCode == 422)
                        _logger.LogError(await e.Call.Response.GetStringAsync());
                }
            }
        }

        /// <inheritdoc />
        public async Task<ICollection<FireflyAccount>> GetAccounts(AccountType accountType)
        {
            var response = await FireflyRoutes
                                 .Accounts(_fireflyBaseUrl)
                                 .WithOAuthBearerToken(_fireflyAccessToken)
                                 .WithHeader("Accept", "application/json")
                                 .SetQueryParam("type", MapToType(accountType))
                                 .GetJsonAsync<FireflyAccountResponse>()
                                 .ConfigureAwait(false);
            return response.Data.MapToFireflyAccountCollection();
        }

        /// <inheritdoc />
        public async Task<ICollection<FireflyAccount>> GetAccounts()
        {
            var accounts = new List<FireflyAccount>();
            FireflyAccountResponse response;
            int page = 0;

            do
            {
                response = await FireflyRoutes
                                 .Accounts(_fireflyBaseUrl)
                                 .WithOAuthBearerToken(_fireflyAccessToken)
                                 .WithHeader("Accept", "application/json")
                                 .SetQueryParam("page", ++page)
                                 .GetJsonAsync<FireflyAccountResponse>()
                                 .ConfigureAwait(false);
                accounts.AddRange(response.Data.MapToFireflyAccountCollection());
            } while (response.Meta.Pagination.CurrentPage < response.Meta.Pagination.TotalPages);

            return accounts;
        }

        /// <inheritdoc />
        public async Task<FireflyTransaction> GetFirstTransactionOfAccount(int accountId)
        {
            var infoResponse = await FireflyRoutes
                                     .AccountTransactions(_fireflyBaseUrl, accountId)
                                     .WithOAuthBearerToken(_fireflyAccessToken)
                                     .WithHeader("Accept", "application/json")
                                     .SetQueryParam("limit", 1)
                                     .GetJsonAsync<FireflyTransactionResponse>();

            var lastPage = infoResponse.Meta.Pagination.TotalPages;
            var response = await FireflyRoutes
                                 .AccountTransactions(_fireflyBaseUrl, accountId)
                                 .WithOAuthBearerToken(_fireflyAccessToken)
                                 .WithHeader("Accept", "application/json")
                                 .SetQueryParams(new
                                 {
                                     limit = 1,
                                     page = lastPage
                                 })
                                 .GetJsonAsync<FireflyTransactionResponse>();
            return response.Data.FirstOrDefault()?.MapToFireflyTransaction();
        }

        /// <inheritdoc />
        public async Task<ICollection<FireflyTransaction>> GetTransactions()
        {
            var transactions = new List<FireflyTransaction>();
            FireflyTransactionResponse response;
            int page = 0;

            do
            {
                response = await FireflyRoutes
                                 .Transactions(_fireflyBaseUrl)
                                 .WithOAuthBearerToken(_fireflyAccessToken)
                                 .SetQueryParam("page", ++page)
                                 .GetJsonAsync<FireflyTransactionResponse>();
                transactions.AddRange(response.Data.MapToFireflyTransactionCollection());
            } while (response.Meta.Pagination.CurrentPage < response.Meta.Pagination.TotalPages);

            return transactions;
        }

        /// <inheritdoc />
        public async Task UpdateAccount(FireflyAccount fireflyAccount)
        {
            try
            {
                await FireflyRoutes
                      .Account(_fireflyBaseUrl, fireflyAccount.Id)
                      .WithOAuthBearerToken(_fireflyAccessToken)
                      .WithHeader("Accept", "application/json")
                      .PutJsonAsync(MapToFireflyAccount(fireflyAccount));
            }
            catch (FlurlHttpException e)
            {
                if (e.StatusCode == 422)
                    _logger.LogError(await e.Call.Response.GetStringAsync());
            }
        }

        #region Static Methods

        private static FireflyStoreTransactionsRequest GetCreateTransactionRequest(FireflyTransaction transaction)
        {
            return new FireflyStoreTransactionsRequest
            {
                ApplyRules = true,
                FireWebhooks = true,
                GroupTitle = transaction.Description,
                ErrorIfDuplicateHash = false,
                Transactions = new List<FireflyApiTransaction> { transaction.MapToFireflyApiTransaction() }
            };
        }

        private static FireflyAccountAttributes MapToFireflyAccount(FireflyAccount fireflyAccount)
        {
            return new FireflyAccountAttributes
            {
                AccountNumber = fireflyAccount.AccountNumber,
                Active = fireflyAccount.Active,
                AccountRole = fireflyAccount.AccountRole,
                Bic = fireflyAccount.Bic,
                Order = fireflyAccount.Order,
                CurrentBalance = fireflyAccount.CurrentBalance,
                CurrencyCode = fireflyAccount.CurrencyCode,
                CurrencyId = fireflyAccount.CurrencyId,
                CurrencySymbol = fireflyAccount.CurrencySymbol,
                CreditCardType = fireflyAccount.CreditCardType,
                Iban = fireflyAccount.Iban,
                Interest = fireflyAccount.Interest,
                IncludeNetWorth = fireflyAccount.IncludeNetWorth,
                InterestPeriod = fireflyAccount.InterestPeriod,
                Latitude = fireflyAccount.Latitude,
                Longitude = fireflyAccount.Longitude,
                LiabilityDirection = fireflyAccount.LiabilityDirection,
                LiabilityType = fireflyAccount.LiabilityType,
                MonthlyPaymentDate = fireflyAccount.MonthlyPaymentDate,
                Name = fireflyAccount.Name,
                Notes = fireflyAccount.Notes,
                OpeningBalance = fireflyAccount.OpeningBalance,
                OpeningBalanceDate = fireflyAccount.OpeningBalanceDate,
                ZoomLevel = fireflyAccount.ZoomLevel
            };
        }

        private static string MapToType(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Asset:
                    return "asset";
                case AccountType.Expense:
                    return "expense";
                case AccountType.Revenue:
                    return "revenue";
                case AccountType.Liability:
                    return "liability";
            }

            return string.Empty;
        }

        #endregion

        #endregion
    }
}