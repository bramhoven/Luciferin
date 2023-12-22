using Luciferin.Infrastructure.GoCardless.Models;
using Luciferin.Infrastructure.GoCardless.Models.Requests;

namespace Luciferin.Infrastructure.GoCardless.Abstractions;

public interface IGoCardlessService
{
    Task<EndUserAgreement> CreateEndUserAgreement(Institution institution);

    Task<Requisition> CreateRequisition(Institution institution, string name, EndUserAgreement endUserAgreement, string returnUrl);

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
}