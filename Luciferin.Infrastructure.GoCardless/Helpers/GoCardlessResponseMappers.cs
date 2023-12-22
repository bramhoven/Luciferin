using Luciferin.Infrastructure.GoCardless.Enums;
using Luciferin.Infrastructure.GoCardless.Models;
using Luciferin.Infrastructure.GoCardless.Models.Responses;
using Newtonsoft.Json;

namespace Luciferin.Infrastructure.GoCardless.Helpers;

public static class GoCardlessResponseMappers
{
    #region Methods

    #region Static Methods

    public static Account MapToAccount(this GoCardlessAccountResponse accountResponse)
    {
        return new Account
        {
            Created = accountResponse.Created,
            Iban = accountResponse.Iban,
            Id = accountResponse.Id,
            InstitutionId = accountResponse.InstitutionId,
            LastAccessed = accountResponse.LastAccessed,
            Status = accountResponse.Status
        };
    }

    public static AccountDetails MapToAccountDetails(this GoCardlessAccountDetailsResponse accountDetailsResponse)
    {
        return new AccountDetails
        {
            Currency = accountDetailsResponse.Account.Currency,
            CashAccountType = accountDetailsResponse.Account.CashAccountType,
            Iban = accountDetailsResponse.Account.Iban,
            Name = accountDetailsResponse.Account.Name,
            Product = accountDetailsResponse.Account.Product,
            OwnerName = accountDetailsResponse.Account.OwnerName,
            ResourceId = accountDetailsResponse.Account.ResourceId
        };
    }

    public static ICollection<Balance> MapToBalanceCollection(this GoCardlessAccountBalanceResponse balanceResponse)
    {
        return balanceResponse.Balances.Select(b => new Balance
        {
            BalanceAmount = new BalanceAmount
            {
                Amount = b.BalanceAmount.Amount,
                Currency = b.BalanceAmount.Currency
            },
            BalanceType = b.BalanceType,
            LastChangeDateTime = b.LastChangeDateTime
        }).ToList();
    }

    public static EndUserAgreement MapToEndUserAgreement(this GoCardlessEndUserAgreementResponse endUserAgreementResponse)
    {
        return new EndUserAgreement
        {
            Accepted = endUserAgreementResponse.Accepted,
            Created = endUserAgreementResponse.Created,
            Id = endUserAgreementResponse.Id,
            AccessScope = endUserAgreementResponse.AccessScope,
            InstitutionId = endUserAgreementResponse.InstitutionId,
            MaxHistoricalDays = endUserAgreementResponse.MaxHistoricalDays,
            AccessValidForDays = endUserAgreementResponse.AccessValidForDays
        };
    }

    public static Institution MapToInstitution(this GoCardlessInstitutionResponse institutionResponse)
    {
        return new Institution
        {
            Bic = institutionResponse.Bic,
            Countries = institutionResponse.Countries,
            Id = institutionResponse.Id,
            Logo = institutionResponse.Logo,
            Name = institutionResponse.Name,
            TransactionTotalDays = institutionResponse.TransactionTotalDays
        };
    }

    public static ICollection<Institution> MapToInstitutionCollection(
        this IEnumerable<GoCardlessInstitutionResponse> institutionResponseCollection)
    {
        return institutionResponseCollection.Select(i => i.MapToInstitution()).ToList();
    }

    public static OpenIdToken MapToOpenIdToken(this GoCardlessTokenResponse tokenResponse)
    {
        return new OpenIdToken
        {
            Created = DateTime.Now,
            AccessToken = tokenResponse.Access,
            AccessTokenExpires = TimeSpan.FromSeconds(tokenResponse.AccessExpires),
            RefreshToken = tokenResponse.Refresh,
            RefreshTokenExpires = TimeSpan.FromSeconds(tokenResponse.RefreshExpires)
        };
    }

    public static Requisition MapToRequisition(this GoCardlessRequisitionResponse requisitionResponse)
    {
        return new Requisition
        {
            Accounts = requisitionResponse.Accounts,
            Agreement = requisitionResponse.Agreement,
            AccountSelection = requisitionResponse.AccountSelection,
            Created = requisitionResponse.Created,
            Id = requisitionResponse.Id,
            Link = requisitionResponse.Link,
            Redirect = requisitionResponse.Redirect,
            Reference = requisitionResponse.Reference,
            Ssn = requisitionResponse.Ssn,
            Status = MapStatus(requisitionResponse.Status),
            InstitutionId = requisitionResponse.InstitutionId,
            UserLanguage = requisitionResponse.UserLanguage
        };
    }

    public static ICollection<Requisition> MapToRequisitionCollection(
        this IEnumerable<GoCardlessRequisitionResponse> requisitionResponses)
    {
        return requisitionResponses.Select(r => r.MapToRequisition()).ToList();
    }

    public static ICollection<Transaction> MapToTransactionCollection(
        this GoCardlessTransactionResponse transactionResponse)
    {
        var transactions = new List<Transaction>();

        var nordigenTransactions =
            transactionResponse.Transactions.Booked.Concat(transactionResponse.Transactions.Pending);
        transactions.AddRange(nordigenTransactions.Select(b => new Transaction
        {
            Status = TransactionStatus.Booked,
            BookingDate = b.BookingDate,
            CreditorAccount = b.CreditorAccount == null
                ? null
                : new CreditorAccount
                {
                    Iban = b.CreditorAccount?.Iban ?? b.CreditorAccount?.Bban
                },
            CreditorName = b.CreditorName,
            DebtorAccount = b.DebtorAccount == null
                ? null
                : new CreditorAccount
                {
                    Iban = b.DebtorAccount?.Iban ?? b.CreditorAccount.Bban
                },
            DebtorName = b.DebtorName,
            TransactionAmount = b.TransactionAmount,
            TransactionId = b.TransactionId,
            EntryReference = b.EntryReference,
            ValueDate = b.ValueDate,
            BankTransactionCode = b.BankTransactionCode,
            RemittanceInformationUnstructured = b.RemittanceInformationUnstructured,
            RawData = JsonConvert.SerializeObject(b, Formatting.Indented)
        }));

        return transactions;
    }

    private static RequisitionStatus MapStatus(string status)
    {
        switch (status)
        {
            case "CR":
                return RequisitionStatus.Created;
            case "UA":
                return RequisitionStatus.UndergoingAuthentication;
            case "LN":
                return RequisitionStatus.Linked;
            case "RJ":
                return RequisitionStatus.Rejected;
            case "SU":
                return RequisitionStatus.Suspended;
            case "EX":
                return RequisitionStatus.Expired;
        }

        return RequisitionStatus.Created;
    }

    #endregion

    #endregion
}