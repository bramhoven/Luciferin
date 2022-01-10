using System.Threading.Tasks;
using Flurl;

namespace Luciferin.BusinessLayer.Nordigen.Helpers
{
    public static class NordigenRoutes
    {
        #region Methods

        #region Static Methods

        public static string Account(string baseUrl, string accountId) => ApiBaseUrl(baseUrl).AppendPathSegments("accounts", $"{accountId}/");

        public static string AccountBalances(string baseUrl, string accountId) => Account(baseUrl, accountId).AppendPathSegment("balances/");
        
        public static string AccountTransactions(string baseUrl, string accountId) => Account(baseUrl, accountId).AppendPathSegment("transactions/");
        
        public static string AccountDetails(string baseUrl, string accountId) => Account(baseUrl, accountId).AppendPathSegment("details/");

        public static string EndUserAgreements(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegments("agreements", "enduser/");

        public static string EndUserAgreement(string nordigenBaseUrl, string endUserAgreementId) => EndUserAgreements(nordigenBaseUrl).AppendPathSegment($"{endUserAgreementId}/");

        public static string Institution(string baseUrl, string institutionId) => Institutions(baseUrl).AppendPathSegment($"{institutionId}/");

        public static string Institutions(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegment("institutions/");

        public static string Requisition(string baseUrl, string requisitionId) => Requisitions(baseUrl).AppendPathSegment($"{requisitionId}/");
        
        public static string Requisitions(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegment("requisitions/");

        public static string TokenNew(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegments("token", "new/");

        public static string TokenRefresh(string baseUrl) => ApiBaseUrl(baseUrl).AppendPathSegments("token", "refresh/");

        private static string ApiBaseUrl(string baseUrl) => baseUrl.AppendPathSegments("api", "v2");

        #endregion

        #endregion
    }
}