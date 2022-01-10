using System;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Firefly.Models;

namespace FireflyImporter.BusinessLayer.Hubs
{
    public interface IImporterHub
    {
        #region Methods

        Task ImportMessageEvent(DateTime time, string message);

        Task ImportTransactionEvent(FireflyTransaction transaction, bool successful);

        #endregion
    }
}