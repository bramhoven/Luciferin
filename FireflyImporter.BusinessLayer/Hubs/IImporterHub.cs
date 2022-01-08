using System;
using System.Threading.Tasks;

namespace FireflyImporter.BusinessLayer.Hubs
{
    public interface IImporterHub
    {
        #region Methods

        Task ImportMessageEvent(DateTime time, string message);

        Task ImportTransactionEvent(object transactionEventModel);

        #endregion
    }
}