using System;
using System.Threading;
using System.Threading.Tasks;

namespace FireflyWebImporter.Classes.Queue
{
    public interface IBackgroundTaskQueue
    {
        #region Methods

        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);

        ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem);

        #endregion
    }
}