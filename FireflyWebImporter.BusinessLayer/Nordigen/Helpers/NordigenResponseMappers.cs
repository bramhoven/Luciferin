using System;
using System.Collections.Generic;
using System.Linq;
using FireflyWebImporter.BusinessLayer.Nordigen.Enums;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Helpers
{
    public static class NordigenResponseMappers
    {
        #region Methods

        #region Static Methods

        public static Account MapToAccount(this NordigenAccountResponse accountResponse)
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

        public static AccountDetails MapToAccountDetails(this NordigenAccountDetailsResponse accountDetailsResponse)
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

        public static ICollection<Balance> MapToBalanceCollection(this NordigenAccountBalanceResponse balanceResponse)
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

        public static EndUserAgreement MapToEndUserAgreement(this NordigenEndUserAgreementResponse endUserAgreementResponse)
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

        public static Institution MapToInstitution(this NordigenInstitutionResponse institutionResponse)
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

        public static ICollection<Institution> MapToInstitutionCollection(this IEnumerable<NordigenInstitutionResponse> institutionResponseCollection)
        {
            return institutionResponseCollection.Select(i => i.MapToInstitution()).ToList();
        }

        public static OpenIdToken MapToOpenIdToken(this NordigenTokenResponse tokenResponse)
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

        public static Requisition MapToRequisition(this NordigenRequisitionResponse requisitionResponse)
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

        public static ICollection<Requisition> MapToRequisitionCollection(this IEnumerable<NordigenRequisitionResponse> requisitionResponses)
        {
            return requisitionResponses.Select(r => r.MapToRequisition()).ToList();
        }

        public static ICollection<Transaction> MapToTransactionCollection(this NordigenTransactionResponse transactionResponse)
        {
            var transactions = new List<Transaction>();

            var nordigenTransactions = transactionResponse.Transactions.Booked.Concat(transactionResponse.Transactions.Pending);
            transactions.AddRange(nordigenTransactions.Select(b => new Transaction
            {
                Status = TransactionStatus.Booked,
                BookingDate = b.BookingDate,
                CreditorAccount = b.CreditorAccount == null ? null : new CreditorAccount
                {
                    Iban = b.CreditorAccount?.Iban
                },
                CreditorName = b.CreditorName,
                DebtorAccount = b.DebtorAccount == null ? null : new CreditorAccount
                {
                    Iban = b.DebtorAccount?.Iban
                },
                DebtorName = b.DebtorName,
                TransactionAmount = b.TransactionAmount,
                TransactionId = b.TransactionId,
                ValueDate = b.ValueDate,
                BankTransactionCode = b.BankTransactionCode,
            }));

            return transactions;
        }

        private static RequisitionStatus MapStatus(string status)
        {
            switch (status)
            {
                case "CR":
                    return RequisitionStatus.Created;
                case "LN":
                    return RequisitionStatus.Linked;
                case "RJ":
                    return RequisitionStatus.Rejected;
                case "SU":
                    return RequisitionStatus.Suspended;
            }

            return RequisitionStatus.Created;
        }

        #endregion

        #endregion
    }
}