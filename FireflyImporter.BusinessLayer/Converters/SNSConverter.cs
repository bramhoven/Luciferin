using System;
using System.Text.RegularExpressions;
using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.BusinessLayer.Converters
{
    internal class SNSConverter : ConverterBase
    {
        #region Methods

        /// <inheritdoc />
        protected override FireflyTransaction FillTransactions(FireflyTransaction fireflyTransaction, Transaction transaction)
        {
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