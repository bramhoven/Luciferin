namespace Luciferin.Application.Providers;

using Abstractions.Providers;
using Core.Entities;
using Core.Entities.Importable;

public class ImportProvider : IImportProvider
{
    private readonly IRequisitionProvider _requisitionProvider;
    private readonly ITransactionProvider _transactionProvider;

    public ImportProvider(IRequisitionProvider requisitionProvider, ITransactionProvider transactionProvider)
    {
        _requisitionProvider = requisitionProvider;
        _transactionProvider = transactionProvider;
    }

    public Task<ICollection<Requisition>> GetImportableRequisitions()
    {
        throw new NotImplementedException();
    }

    public async Task<ImportableData> GetImportableData(DateTime dateTime)
    {
        var requisitions = await _requisitionProvider.GetAllAsync();
        var importableRequisitions = new List<ImportableRequisition>();
        foreach (var requisition in requisitions)
        {
            var importableAccounts = new List<ImportableAccount>();
            var accounts = await _requisitionProvider.GetAccountsForRequisition(requisition);

            foreach (var account in accounts)
            {
                var transactions = await _transactionProvider.GetTransactionsForAccount(account, dateTime.AddDays(-1), dateTime);
                var importableAccount = new ImportableAccount
                                        {
                                            Account = account,
                                            ImportableTransactions = transactions
                                        };
                importableAccounts.Add(importableAccount);
            }

            var importableRequisition = new ImportableRequisition
                                        {
                                            Requisition = requisition,
                                            ImportableAccounts = importableAccounts
                                        };
            importableRequisitions.Add(importableRequisition);
        }


        var importableData = new ImportableData
                             {
                                 ImportStart = DateTime.Now,
                                 TransactionStart = dateTime.AddDays(-1),
                                 TransactionEnd = dateTime,
                                 ImportableRequisitions = importableRequisitions
                             };
        return importableData;
    }
}