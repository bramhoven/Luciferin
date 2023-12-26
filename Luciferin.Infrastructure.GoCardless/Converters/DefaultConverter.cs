namespace Luciferin.Infrastructure.GoCardless.Converters;

using Core.Entities;
using Microsoft.Extensions.Options;
using Settings;

internal class DefaultConverter : ConverterBase
{
    /// <inheritdoc />
    public DefaultConverter(IOptionsSnapshot<LuciferinSettings> options) : base(options) { }

    /// <inheritdoc />
    protected override Transaction FillTransactions(Transaction transaction, Models.GCTransaction originalTransaction)
    {
        transaction.ExternalId = originalTransaction.TransactionId;

        var text = originalTransaction.CreditorName ?? originalTransaction.DebtorName;
        transaction.Description = text;
        transaction.Notes = text;

        return transaction;
    }
}