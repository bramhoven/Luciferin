using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;

namespace Luciferin.BusinessLayer.Hubs
{
    public interface IImporterHub
    {
        #region Methods

        Task ImportMessageEvent(DateTime time, string message);

        Task ImportTransactionEvent(FireflyTransaction transaction, bool successful, string fireflyUrl);

        #endregion
    }
}