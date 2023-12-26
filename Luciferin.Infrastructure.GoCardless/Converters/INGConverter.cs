namespace Luciferin.Infrastructure.GoCardless.Converters;

using Core.Entities;
using Microsoft.Extensions.Options;
using Models;
using Settings;

internal class IngConverter : ConverterBase
{
    private const string _descriptionFieldName = "Omschrijving";

    private const string _nameFieldName = "Naam";

    /// <inheritdoc />
    public IngConverter(IOptionsSnapshot<LuciferinSettings> options) : base(options) { }

    /// <inheritdoc />
    protected override Transaction FillTransactions(Transaction transaction, GCTransaction originalTransaction)
    {
        transaction.ExternalId = originalTransaction.TransactionId;

        var (description, notes) = GetTextFields(originalTransaction.RemittanceInformationUnstructured, originalTransaction.CreditorName, originalTransaction.DebtorName);
        transaction.Description = description;
        transaction.Notes = GetNotes(originalTransaction, notes);

        return transaction;
    }

    private static (string, string) GetTextFields(string? description, string? creditorName, string? debtorName)
    {
        var splitDescription = description.Split("<br>");
        var descriptionText = splitDescription.FirstOrDefault(d => d.Contains(_descriptionFieldName))?.Replace($"{_descriptionFieldName}:", "").Trim();

        if (string.IsNullOrWhiteSpace(descriptionText))
        {
            descriptionText = splitDescription[0].Replace($"{_nameFieldName}:", "").Trim();
        }

        if (string.IsNullOrWhiteSpace(descriptionText))
        {
            descriptionText = creditorName;
        }

        if (string.IsNullOrWhiteSpace(descriptionText))
        {
            descriptionText = debtorName;
        }

        var notesText = string.Join($"{Environment.NewLine}{Environment.NewLine}", splitDescription);
        return (descriptionText, notesText);
    }
}