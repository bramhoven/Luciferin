using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;

namespace Luciferin.BusinessLayer.ServiceBus
{
    public interface IServiceBus
    {
        #region Methods

        Task PublishTransactionEvent(FireflyTransaction transaction, bool successful, string fireflyUrl);

        #endregion
    }
}