using Luciferin.BusinessLayer.Firefly.Models;

namespace Luciferin.BusinessLayer.Import.Processors
{
    public sealed class ProcessorResult
    {
        private ProcessorResult(FireflyTransaction originalTransaction, ProcessedStatus status, ProcessorType processorType) : this(
            originalTransaction, null, status, processorType) { }

        private ProcessorResult(FireflyTransaction originalTransaction, FireflyTransaction? processedTransaction, ProcessedStatus status, ProcessorType processorType)
        {
            OriginalTransaction = originalTransaction;
            ProcessedTransaction = processedTransaction;
            Status = status;
            ProcessorType = processorType;
        }

        public FireflyTransaction OriginalTransaction { get; }

        public FireflyTransaction? ProcessedTransaction { get; }

        public ProcessedStatus Status { get; }

        public ProcessorType ProcessorType { get; }

        /// <summary>
        /// Creates a failed processor result.
        /// </summary>
        /// <param name="originalTransaction">The original transaction.</param>
        /// <param name="processorType">The type of processor.</param>
        /// <returns></returns>
        public static ProcessorResult Fail(FireflyTransaction originalTransaction, ProcessorType processorType)
        {
            return new ProcessorResult(originalTransaction, ProcessedStatus.Failed, processorType);
        }

        /// <summary>
        /// Creates a duplicate processor result.
        /// </summary>
        /// <param name="originalTransaction">The original transaction.</param>
        /// <param name="processorType">The type of processor.</param>
        /// <returns></returns>
        public static ProcessorResult Duplicate(FireflyTransaction originalTransaction, ProcessorType processorType)
        {
            return new ProcessorResult(originalTransaction, ProcessedStatus.Duplicate, processorType);
        }

        /// <summary>
        /// Creates a success processor result.
        /// </summary>
        /// <param name="originalTransaction">The original transaction.</param>
        /// <param name="processedTransaction">The transaction after being processed.</param>
        /// <param name="processorType">The type of processor.</param>
        /// <returns></returns>
        public static ProcessorResult Success(FireflyTransaction originalTransaction, FireflyTransaction processedTransaction, ProcessorType processorType)
        {
            return new ProcessorResult(originalTransaction, processedTransaction, ProcessedStatus.Success, processorType);
        }
    }
}