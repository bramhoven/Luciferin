using System;
using System.Linq;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Converters
{
    public class INGConverter : ConverterBase
    {
        #region Fields

        private const string _descriptionFieldName = "Omschrijving";

        #endregion

        #region Methods

        /// <inheritdoc />
        public override FireflyTransaction ConvertTransaction(Transaction transaction)
        {
            var fireflyTransaction = base.ConvertTransaction(transaction);
            fireflyTransaction.ExternalId = transaction.TransactionId;

            var (description, notes) = GetTextFields(transaction.RemittanceInformationUnstructured ?? transaction.CreditorName ?? transaction.DebtorName);
            fireflyTransaction.Description = description;
            fireflyTransaction.Notes = notes;

            return fireflyTransaction;
        }

        #region Static Methods

        private static (string, string) GetTextFields(string description)
        {
            var splitDescription = description.Split("<br>");
            var descriptionText = splitDescription.FirstOrDefault(d => d.Contains(_descriptionFieldName))?.Replace($"{_descriptionFieldName}:", "").Trim() ?? splitDescription[0];
            var notesText = string.Join(Environment.NewLine, splitDescription);
            return (descriptionText, notesText);
        }

        #endregion

        #endregion
    }
}