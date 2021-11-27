using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models.Requests;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Stores
{
    public interface INordigenStore
    {
        #region Methods

        Task<EndUserAgreement> CreateEndUserAgreement(NordigenEndUserAgreementRequest request, OpenIdToken openIdToken);

        Task<Requisition> CreateRequisition(NordigenRequisitionRequest request, OpenIdToken openIdToken);

        Task<bool> DeleteEndUserAgreement(string endUserAgreementId, OpenIdToken openIdToken);

        Task<bool> DeleteRequisition(string requisitionId, OpenIdToken openIdToken);

        Task<Account> GetAccount(string accountId, OpenIdToken openIdToken);

        Task<ICollection<Balance>> GetAccountBalance(string accountId, OpenIdToken openIdToken);

        Task<AccountDetails> GetAccountDetails(string accountId, OpenIdToken openIdToken);

        Task<ICollection<Transaction>> GetAccountTransactions(string accountId, OpenIdToken openIdToken);

        Task<EndUserAgreement> GetEndUserAgreement(string endUserAgreementId, OpenIdToken openIdToken);

        Task<Institution> GetInstitution(string institutionId, OpenIdToken openIdToken);

        Task<ICollection<Institution>> GetInstitutions(string countryCode, OpenIdToken openIdToken);

        Task<Requisition> GetRequisition(string requisitionId, OpenIdToken openIdToken);

        Task<ICollection<Requisition>> GetRequisitions(OpenIdToken openIdToken);

        Task<OpenIdToken> GetToken();

        Task<OpenIdToken> RefreshToken(OpenIdToken token);

        #endregion
    }
}