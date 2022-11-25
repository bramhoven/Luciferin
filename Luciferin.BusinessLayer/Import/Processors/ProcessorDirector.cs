using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;

namespace Luciferin.BusinessLayer.Import.Processors
{
    public sealed class ProcessorDirector
    {
        private readonly ICollection<ITransactionProcessor> _processors;

        private readonly ICollection<ProcessorResult> _results;

        public ProcessorDirector()
        {
            _processors = new List<ITransactionProcessor>();
            _results = new List<ProcessorResult>();
        }

        /// <summary>
        /// Adds a processor to this director.
        /// </summary>
        /// <param name="processor"></param>
        public void AddProcessor(ITransactionProcessor processor)
        {
            _processors.Add(processor);
        }

        /// <summary>
        /// Processes a list of transactions.
        /// </summary>
        /// <param name="transactions">The list of transactions to process.</param>
        /// <returns></returns>
        public async Task<ICollection<ProcessorResult>> ProcessTransactions(ICollection<FireflyTransaction> transactions)
        {
            var results = new List<ProcessorResult>();
            foreach (var transaction in transactions)
                results.Add(await ProcessTransaction(transaction));

            return results;
        }

        /// <summary>
        /// Processes a single transaction.
        /// </summary>
        /// <param name="transaction">The transaction to process.</param>
        /// <returns></returns>
        public async Task<ProcessorResult> ProcessTransaction(FireflyTransaction transaction)
        {
            foreach (var processor in _processors)
            {
                var result = await processor.ProcessTransaction(transaction);
                _results.Add(result);

                if (result.Status == ProcessedStatus.Success)
                    continue;

                return result;
            }

            return ProcessorResult.Success(transaction, transaction, ProcessorType.None);
        }
    }
}