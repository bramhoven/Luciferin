using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Firefly.Models;

namespace FireflyImporter.BusinessLayer.ServiceBus
{
    public interface IServiceBus
    {
        #region Methods

        Task PublishTransactionEvent(FireflyTransaction transaction, bool successful);

        #endregion
    }
}