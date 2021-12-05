using Flurl;

namespace FireflyWebImporter.BusinessLayer.Firefly.Helpers
{
    public static class FireflyRoutes
    {
        #region Methods

        #region Static Methods

        public static string Accounts(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegment("accounts");

        public static string Transactions(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegment("transactions");

        private static string ApiBaseUrl(string baseUrl) => baseUrl.AppendPathSegments("api", "v1");

        #endregion

        #endregion
    }
}