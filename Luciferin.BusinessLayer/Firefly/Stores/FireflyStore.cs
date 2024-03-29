﻿using System;
using System.Collections.Generic;
using Flurl.Http;
using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Enums;
using Luciferin.BusinessLayer.Firefly.Helpers;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Firefly.Models.Requests;
using Luciferin.BusinessLayer.Firefly.Models.Responses.Accounts;
using Luciferin.BusinessLayer.Firefly.Models.Responses.Transactions;
using Luciferin.BusinessLayer.Firefly.Models.Shared;
using Luciferin.BusinessLayer.Logger;
using Luciferin.BusinessLayer.ServiceBus;
using Luciferin.BusinessLayer.Settings;
using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Firefly.Stores
{
    public class FireflyStore : IFireflyStore
    {
        #region Constructors

        public FireflyStore(ISettingsManager settingsManager, ICompositeLogger<FireflyStore> logger, IServiceBus serviceBus)
        {
            _fireflyBaseUrl = settingsManager.GetSetting<StringSetting>(SettingKeyConstants.FireflyUrlKey).Value;
            _fireflyAccessToken = settingsManager.GetSetting<StringSetting>(SettingKeyConstants.FireflyAccessTokenKey).Value;
            _logger = logger;
            _serviceBus = serviceBus;
        }

        #endregion

        #region Fields

        private readonly string _fireflyAccessToken;

        private readonly string _fireflyBaseUrl;

        private readonly ICompositeLogger<FireflyStore> _logger;

        private readonly IServiceBus _serviceBus;

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task AddNewAccounts(ICollection<FireflyAccount> accounts)
        {
            FireflyTransactionResponse response = null;
            foreach (var account in accounts)
            {
                try
                {
                    response = await FireflyRoutes
                                     .Accounts(_fireflyBaseUrl)
                                     .WithOAuthBearerToken(_fireflyAccessToken)
                                     .WithHeader("Accept", "application/json")
                                     .PostJsonAsync(MapToFireflyAccount(account))
                                     .ReceiveJson<FireflyTransactionResponse>()
                                     .ConfigureAwait(false);
                }
                catch (FlurlHttpException e)
                {
                    if (e.StatusCode == 422)
                        _logger.LogError(await e.Call.Response.GetStringAsync());
                }
            }
        }

        /// <inheritdoc />
        public async Task AddNewTag(FireflyTag tag)
        {
            try
            {
                await FireflyRoutes
                      .Tags(_fireflyBaseUrl)
                      .WithOAuthBearerToken(_fireflyAccessToken)
                      .WithHeader("Accept", "application/json")
                      .PostJsonAsync(tag.MapToFireflyApiTag())
                      .ConfigureAwait(false);
            }
            catch (FlurlHttpException e)
            {
                if (e.StatusCode == 422)
                    _logger.LogError(await e.Call.Response.GetStringAsync());
            }
        }

        /// <inheritdoc />
        public async Task AddNewTransactions(IEnumerable<FireflyTransaction> transactions)
        {
            var fireflyTransactions = transactions.ToList();

            var totalTransactions = fireflyTransactions.Count;
            var index = 0;
            foreach (var transaction in fireflyTransactions)
            {
                var request = GetCreateTransactionRequest(transaction);
                FireflyTransactionResponse response = null;
                try
                {
                    response = await FireflyRoutes
                                     .Transactions(_fireflyBaseUrl)
                                     .WithOAuthBearerToken(_fireflyAccessToken)
                                     .WithHeader("Accept", "application/json")
                                     .PostJsonAsync(request)
                                     .ReceiveJson<FireflyTransactionResponse>()
                                     .ConfigureAwait(false);

                    _logger.LogInformation(
                        $"Imported transaction [{++index}/{totalTransactions}] [{response.Data.Id}] [{transaction.Type.ToString()}] {transaction.Description}");
                }
                catch (FlurlHttpException e)
                {
                    if (e.StatusCode == 422)
                        _logger.LogError(await e.Call.Response.GetStringAsync());
                }
                finally
                {
                    if (response != null)
                    {
                        string fireflyUrl = null;
                        if (!string.IsNullOrWhiteSpace(response.Data?.Id))
                            fireflyUrl = FireflyRoutes.ShowTransaction(_fireflyBaseUrl, response.Data?.Id);

                        await _serviceBus.PublishTransactionEvent(transaction, response.Data != null, fireflyUrl);
                    }
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
                                 .GetJsonAsync<FireflyAccountCollectionResponse>()
                                 .ConfigureAwait(false);
            return response.Data.MapToFireflyAccountCollection();
        }

        /// <inheritdoc />
        public async Task<ICollection<FireflyAccount>> GetAccounts()
        {
            var accounts = new List<FireflyAccount>();
            FireflyAccountCollectionResponse response;
            int page = 0;

            do
            {
                response = await FireflyRoutes
                                 .Accounts(_fireflyBaseUrl)
                                 .WithOAuthBearerToken(_fireflyAccessToken)
                                 .WithHeader("Accept", "application/json")
                                 .SetQueryParam("page", ++page)
                                 .GetJsonAsync<FireflyAccountCollectionResponse>()
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
                                     .GetJsonAsync<FireflyTransactionCollectionResponse>();

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
                                 .GetJsonAsync<FireflyTransactionCollectionResponse>();
            return response.Data.FirstOrDefault()?.MapToFireflyTransaction();
        }

        /// <param name="fromDate"></param>
        /// <inheritdoc />
        public async Task<ICollection<FireflyTransaction>> GetTransactions(DateTime fromDate)
        {
            var transactions = new List<FireflyTransaction>();
            FireflyTransactionCollectionResponse transactionCollectionResponse;
            int page = 0;

            do
            {
                transactionCollectionResponse = await FireflyRoutes
                                                      .Transactions(_fireflyBaseUrl)
                                                      .WithOAuthBearerToken(_fireflyAccessToken)
                                                      .SetQueryParam("page", ++page)
                                                      .SetQueryParam("start", fromDate.ToString("yyyy-MM-dd"))
                                                      .GetJsonAsync<FireflyTransactionCollectionResponse>();
                transactions.AddRange(transactionCollectionResponse.Data.MapToFireflyTransactionCollection());
            } while (transactionCollectionResponse.Meta.Pagination.CurrentPage < transactionCollectionResponse.Meta.Pagination.TotalPages);

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
                Type = MapToType(fireflyAccount.Type),
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