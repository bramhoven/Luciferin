using Flurl;

namespace Luciferin.Infrastructure.GoCardless.Helpers;

public static class GoCardlessRoutes
{
    public static string Account(string baseUrl, string accountId)
    {
        return ApiBaseUrl(baseUrl).AppendPathSegments("accounts", $"{accountId}/");
    }

    public static string AccountBalances(string baseUrl, string accountId)
    {
        return Account(baseUrl, accountId).AppendPathSegment("balances/");
    }

    public static string AccountTransactions(string baseUrl, string accountId)
    {
        return Account(baseUrl, accountId).AppendPathSegment("transactions/");
    }

    public static string AccountDetails(string baseUrl, string accountId)
    {
        return Account(baseUrl, accountId).AppendPathSegment("details/");
    }

    public static string EndUserAgreements(string baseUrl)
    {
        return ApiBaseUrl(baseUrl).AppendPathSegments("agreements", "enduser/");
    }

    public static string EndUserAgreement(string nordigenBaseUrl, string endUserAgreementId)
    {
        return EndUserAgreements(nordigenBaseUrl).AppendPathSegment($"{endUserAgreementId}/");
    }

    public static string Institution(string baseUrl, string institutionId)
    {
        return Institutions(baseUrl).AppendPathSegment($"{institutionId}/");
    }

    public static string Institutions(string baseUrl)
    {
        return ApiBaseUrl(baseUrl).AppendPathSegment("institutions/");
    }

    public static string Requisition(string baseUrl, string requisitionId)
    {
        return Requisitions(baseUrl).AppendPathSegment($"{requisitionId}/");
    }

    public static string Requisitions(string baseUrl)
    {
        return ApiBaseUrl(baseUrl).AppendPathSegment("requisitions/");
    }

    public static string TokenNew(string baseUrl)
    {
        return ApiBaseUrl(baseUrl).AppendPathSegments("token", "new/");
    }

    public static string TokenRefresh(string baseUrl)
    {
        return ApiBaseUrl(baseUrl).AppendPathSegments("token", "refresh/");
    }

    private static string ApiBaseUrl(string baseUrl)
    {
        return baseUrl.AppendPathSegments("api", "v2");
    }
}