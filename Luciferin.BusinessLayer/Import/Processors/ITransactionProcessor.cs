using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;

namespace Luciferin.BusinessLayer.Import.Processors
{
    public interface ITransactionProcessor
    {
        /// <summary>
        /// Processes a firefly transaction.
        /// </summary>
        /// <param name="transaction">The transaction to process.</param>
        /// <returns></returns>
        Task<ProcessorResult> ProcessTransaction(FireflyTransaction transaction);
    }
}