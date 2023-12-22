using Luciferin.Infrastructure.GoCardless.Models;
using Luciferin.Infrastructure.GoCardless.Models.Requests;

namespace Luciferin.Infrastructure.GoCardless.Abstractions;

public interface IGoCardlessService
{
    Task<EndUserAgreement> CreateEndUserAgreementAsync(Institution institution);

    Task<Requisition> CreateRequisitionAsync(Institution institution, string name, EndUserAgreement endUserAgreement, string returnUrl);

    Task<bool> DeleteEndUserAgreementAsync(string endUserAgreementId);

    Task<bool> DeleteRequisitionAsync(string requisitionId);

    Task<Account> GetAccountAsync(string accountId);

    Task<ICollection<Balance>> GetAccountBalanceAsync(string accountId);

    Task<AccountDetails> GetAccountDetailsAsync(string accountId);

    Task<ICollection<Transaction>> GetAccountTransactionsAsync(string accountId);

    Task<ICollection<Transaction>> GetAccountTransactionsAsync(string accountId, DateTime fromDate);

    Task<EndUserAgreement> GetEndUserAgreementAsync(string endUserAgreementId);

    Task<Institution> GetInstitutionAsync(string institutionId);

    Task<ICollection<Institution>> GetInstitutionsAsync(string countryCode);

    Task<Requisition> GetRequisitionAsync(string requisitionId);

    Task<ICollection<Requisition>> GetRequisitionsAsync();
}