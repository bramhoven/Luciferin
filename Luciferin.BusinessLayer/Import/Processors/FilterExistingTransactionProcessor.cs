using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Logger;

namespace Luciferin.BusinessLayer.Import.Processors
{
    public sealed class FilterExistingTransactionProcessor : ITransactionProcessor
    {
        private readonly ICompositeLogger<FilterExistingTransactionProcessor> _logger;

        private ICollection<FireflyTransaction> _existingTransactions;

        /// <summary>
        /// Initializes a new instance of <see cref="FilterExistingTransactionProcessor" />.
        /// This processors filters transactions based on the external id.
        /// </summary>
        /// <param name="existingTransactions">The existing transactions to use in the filter.</param>
        public FilterExistingTransactionProcessor(ICompositeLogger<FilterExistingTransactionProcessor> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<ProcessorResult> ProcessTransaction(FireflyTransaction transaction)
        {
            await _logger.LogDebug($"Checking if transaction `{transaction.GetCompareString()}` already exists");

            var result = _existingTransactions.Any(ft => string.Equals(ft.ExternalId, transaction.ExternalId, StringComparison.InvariantCultureIgnoreCase));
            if (!result)
            {
                await _logger.LogDebug($"Transaction `{transaction.GetCompareString()}` did not exist");
                return ProcessorResult.Success(transaction, transaction, ProcessorType.ExistingFilter);
            }

            await _logger.LogDebug($"Transaction `{transaction.GetCompareString()}` already exists");
            return ProcessorResult.Fail(transaction, ProcessorType.ExistingFilter);
        }

        /// <summary>
        /// Sets the existing transactions for processing.
        /// </summary>
        /// <param name="transactions">The transactions.</param>
        public void SetExistingTransactions(ICollection<FireflyTransaction> transactions)
        {
            _existingTransactions = transactions;
        }
    }
}