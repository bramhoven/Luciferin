using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Nordigen
{
    public interface INordigenManager
    {
        #region Methods

        Task<EndUserAgreement> CreateEndUserAgreement(Institution institution);

        Task<Requisition> CreateRequisition(Institution institution, string reference, EndUserAgreement agreement, string redirectUrl);

        Task<bool> DeleteEndUserAgreement(string endUserAgreementId);

        Task<bool> DeleteRequisition(string requisitionId);

        Task<Account> GetAccount(string accountId);

        Task<ICollection<Balance>> GetAccountBalance(string accountId);

        Task<AccountDetails> GetAccountDetails(string accountId);

        Task<ICollection<Transaction>> GetAccountTransactions(string accountId);
        
        Task<ICollection<Transaction>> GetAccountTransactions(string accountId, DateTime fromDate);

        Task<EndUserAgreement> GetEndUserAgreement(string endUserAgreementId);

        Task<Institution> GetInstitution(string institutionId);

        Task<ICollection<Institution>> GetInstitutions(string countryCode);

        Task<Requisition> GetRequisition(string requisitionId);

        Task<ICollection<Requisition>> GetRequisitions();

        #endregion
    }
}