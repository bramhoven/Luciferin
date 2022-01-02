using System;
using System.Text.RegularExpressions;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Converters
{
    public class SNSConverter : ConverterBase
    {
        #region Methods

        /// <inheritdoc />
        public override FireflyTransaction ConvertTransaction(Transaction transaction)
        {
            var fireflyTransaction = base.ConvertTransaction(transaction);
            fireflyTransaction.ExternalId = transaction.EntryReference;

            var (description, notes) = GetTextFields(transaction.RemittanceInformationUnstructured ?? transaction.CreditorName ?? transaction.DebtorName);
            fireflyTransaction.Description = description;
            fireflyTransaction.Notes = notes;

            return fireflyTransaction;
        }

        #region Static Methods

        private static (string, string) GetTextFields(string description)
        {
            const string pattern = @"\s{2,}";
            var splitDescription = Regex.Split(description, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
            var descriptionText = splitDescription[0];
            var notesText = string.Join(Environment.NewLine, splitDescription);
            return (descriptionText, notesText);
        }

        #endregion

        #endregion
    }
}