using Flurl;

namespace Luciferin.BusinessLayer.Firefly.Helpers
{
    public static class FireflyRoutes
    {
        #region Methods

        #region Static Methods

        public static string Account(string baseUrl, int accountId) => Accounts(baseUrl).AppendPathSegment(accountId);

        public static string Accounts(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegment("accounts");

        public static string AccountTransactions(string baseUrl, int accountId) => Account(baseUrl, accountId).AppendPathSegment("transactions");

        public static string Tags(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegment("tags");

        public static string Transactions(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegment("transactions");

        private static string ApiBaseUrl(string baseUrl) => baseUrl.AppendPathSegments("api", "v1");

        #endregion

        #endregion
    }
}