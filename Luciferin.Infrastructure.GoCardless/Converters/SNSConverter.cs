namespace Luciferin.Infrastructure.GoCardless.Converters;

using System.Text.RegularExpressions;
using Core.Entities;
using Microsoft.Extensions.Options;
using Models;
using Settings;

internal class SnsConverter : ConverterBase
{
    /// <inheritdoc />
    public SnsConverter(IOptionsSnapshot<LuciferinSettings> options) : base(options) { }

    /// <inheritdoc />
    protected override Transaction FillTransactions(Transaction transaction, GCTransaction originalTransaction)
    {
        transaction.ExternalId = originalTransaction.EntryReference;

        var (description, notes) = GetTextFields(originalTransaction.RemittanceInformationUnstructured ?? originalTransaction.CreditorName ?? originalTransaction.DebtorName);
        transaction.Description = description;
        transaction.Notes = GetNotes(originalTransaction, notes);

        return transaction;
    }

    private static (string, string) GetTextFields(string? description)
    {
        const string pattern = @">";
        var splitDescription = Regex.Split(description, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
        var descriptionText = splitDescription[0];
        var notesText = string.Join($"{Environment.NewLine}{Environment.NewLine}", splitDescription);
        return (descriptionText, notesText);
    }
}