using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Logger;

namespace Luciferin.BusinessLayer.Import.Processors
{
    public class FilterDuplicateTransactionProcessor : ITransactionProcessor
    {
        private readonly ICompositeLogger<FilterDuplicateTransactionProcessor> _logger;

        private ICollection<ulong> _consistentHashes;

        private ICollection<string> _hashStrings;

        /// <summary>
        /// Initializes a new instance of <see cref="FilterDuplicateTransactionProcessor" />.
        /// This processor filters duplicate transaction based on the values in the transactions.
        /// </summary>
        public FilterDuplicateTransactionProcessor(ICompositeLogger<FilterDuplicateTransactionProcessor> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<ProcessorResult> ProcessTransaction(FireflyTransaction transaction)
        {
            await _logger.LogInformation($"Checking transaction `{transaction.GetCompareString()}` for duplicates");

            var hash = transaction.GetHashString();
            if (_hashStrings.Contains(hash))
            {
                await _logger.LogInformation($"Found duplicate for transaction `{transaction.GetCompareString()}` in {nameof(_hashStrings)}");
                return ProcessorResult.Duplicate(transaction, ProcessorType.DuplicateFilter);
            }

            var consistentHash = transaction.GetConsistentHash();
            if (_consistentHashes.Contains(consistentHash))
            {
                await _logger.LogInformation($"Found duplicate for transaction `{transaction.GetCompareString()}` in {nameof(_consistentHashes)}");
                return ProcessorResult.Duplicate(transaction, ProcessorType.DuplicateFilter);
            }

            await _logger.LogInformation($"Transaction `{transaction.GetCompareString()}` is not duplicate");

            return ProcessorResult.Success(transaction, transaction, ProcessorType.DuplicateFilter);
        }

        /// <summary>
        /// Sets the existing transactions for processing.
        /// </summary>
        /// <param name="transactions">The transactions.</param>
        public void SetExistingTransactions(ICollection<FireflyTransaction> transactions)
        {
            _hashStrings = transactions.Select(t => t.GetHashString()).Distinct().ToList();
            _consistentHashes = transactions.Select(t => t.GetConsistentHash()).Distinct().ToList();
        }
    }
}