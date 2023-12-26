namespace Luciferin.Infrastructure.GoCardless.Converters;

using Abstractions;
using Core.Entities;
using Microsoft.Extensions.Options;
using Models;
using Settings;

public abstract class ConverterBase : ITransactionConverter
{
    private readonly LuciferinSettings _luciferinSettings;

    protected ConverterBase(IOptionsSnapshot<LuciferinSettings> options)
    {
        _luciferinSettings = options.Value;
    }

    /// <inheritdoc />
    public virtual Transaction ConvertTransaction(GCTransaction gcTransaction, string tag)
    {
        var amount = gcTransaction.TransactionAmount.Amount.Replace("-", "");

        var convertedTransaction = new Transaction { RequisitionIban = gcTransaction.RequisitorIban, Amount = amount, Date = gcTransaction.BookingDate, ExternalId = gcTransaction.TransactionId ?? gcTransaction.EntryReference };

        if (!string.IsNullOrWhiteSpace(tag))
        {
            convertedTransaction.Tags = new List<string> { tag };
        }

        return FillTransactions(convertedTransaction, gcTransaction);
    }

    /// <summary>
    ///     Fills the transaction with correct data.
    /// </summary>
    /// <param name="transaction">The new transaction.</param>
    /// <param name="originalTransaction">The original transaction to convert.</param>
    /// <returns></returns>
    protected abstract Transaction FillTransactions(Transaction transaction, GCTransaction originalTransaction);

    /// <summary>
    ///     Gets the full notes description.
    /// </summary>
    /// <param name="gcTransaction">The transaction.</param>
    /// <param name="otherNotes">Other notes to add.</param>
    /// <returns></returns>
    protected string GetNotes(GCTransaction gcTransaction, string otherNotes)
    {
        if (!_luciferinSettings.ExtendedNotes)
        {
            return otherNotes;
        }

        var mdNewLine = $"{Environment.NewLine}{Environment.NewLine}";

        var notes = new List<string>();

        notes.Add("# Import details");
        notes.Add($"Bank: {gcTransaction.RequisitorBank.ToString()}");

        if (!string.IsNullOrWhiteSpace(gcTransaction.CreditorName))
        {
            notes.Add($"Creditor: {gcTransaction.CreditorName}");
        }

        if (!string.IsNullOrWhiteSpace(gcTransaction.DebtorName))
        {
            notes.Add($"Debtor: {gcTransaction.DebtorName}");
        }

        notes.Add("# Description");
        notes.Add(otherNotes);

        notes.Add("# Raw Data");
        notes.Add($"```json{Environment.NewLine}{gcTransaction.RawData}{Environment.NewLine} ```");

        return string.Join(mdNewLine, notes);
    }
}