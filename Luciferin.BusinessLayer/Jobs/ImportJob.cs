using System.Threading.Tasks;
using Luciferin.BusinessLayer.Import;
using Quartz;

namespace Luciferin.BusinessLayer.Jobs
{
    [DisallowConcurrentExecution]
    public class ImportJob : IJob
    {
        #region Fields

        private readonly IImportManager _importManager;

        #endregion

        #region Constructors

        public ImportJob(IImportManager importManager)
        {
            _importManager = importManager;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public Task Execute(IJobExecutionContext context)
        {
            return _importManager.CheckAndExecuteAutomaticImport(context.CancellationToken);
        }

        #endregion
    }
}