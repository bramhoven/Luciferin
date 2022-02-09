using System;
using System.Text.RegularExpressions;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.BusinessLayer.Settings;

namespace Luciferin.BusinessLayer.Converters
{
    internal class SnsConverter : ConverterBase
    {
        #region Constructors

        /// <inheritdoc />
        public SnsConverter(ISettingsManager settingsManager) : base(settingsManager) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override FireflyTransaction FillTransactions(FireflyTransaction fireflyTransaction, Transaction transaction)
        {
            fireflyTransaction.ExternalId = transaction.EntryReference;

            var (description, notes) = GetTextFields(transaction.RemittanceInformationUnstructured ?? transaction.CreditorName ?? transaction.DebtorName);
            fireflyTransaction.Description = description;
            fireflyTransaction.Notes = GetNotes(transaction, notes);

            return fireflyTransaction;
        }

        #region Static Methods

        private static (string, string) GetTextFields(string description)
        {
            const string pattern = @">";
            var splitDescription = Regex.Split(description, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
            var descriptionText = splitDescription[0];
            var notesText = string.Join($"{Environment.NewLine}{Environment.NewLine}", splitDescription);
            return (descriptionText, notesText);
        }

        #endregion

        #endregion
    }
}