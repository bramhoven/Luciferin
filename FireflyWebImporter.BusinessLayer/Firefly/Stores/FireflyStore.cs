using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Firefly.Helpers;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Firefly.Models.Responses.Transactions;
using Flurl.Http;

namespace FireflyWebImporter.BusinessLayer.Firefly.Stores
{
    public class FireflyStore : IFireflyStore
    {
        #region Fields

        private readonly string _fireflyAccessToken;

        private readonly string _fireflyBaseUrl;

        #endregion

        #region Constructors

        public FireflyStore(string fireflyBaseUrl, string fireflyAccessToken)
        {
            _fireflyBaseUrl = fireflyBaseUrl;
            _fireflyAccessToken = fireflyAccessToken;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<ICollection<FireflyTransaction>> GetTransactions()
        {
            var transactionResponse = await FireflyRoutes
                                            .Transactions(_fireflyBaseUrl)
                                            .WithOAuthBearerToken(_fireflyAccessToken)
                                            .GetJsonAsync<FireflyTransactionResponse>();
            return transactionResponse.Data.MapToFireflyTransactionCollection();
        }

        #endregion
    }
}