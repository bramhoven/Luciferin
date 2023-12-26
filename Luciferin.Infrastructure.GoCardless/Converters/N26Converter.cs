namespace Luciferin.Infrastructure.GoCardless.Converters;

using Core.Entities;
using Microsoft.Extensions.Options;
using Models;
using Settings;

internal class N26Converter : ConverterBase
{
    /// <inheritdoc />
    public N26Converter(IOptionsSnapshot<LuciferinSettings> options) : base(options) { }
    
    /// <inheritdoc />
    protected override Transaction FillTransactions(Transaction transaction, GCTransaction originalTransaction)
    {
        transaction.ExternalId = originalTransaction.TransactionId;

        var text = originalTransaction.CreditorName ?? originalTransaction.DebtorName;
        transaction.Description = text;
        transaction.Notes = GetNotes(originalTransaction, text);

        return transaction;
    }
}