using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Import
{
    public interface IImportManager
    {
        #region Methods

        Task<Requisition> AddNewBank(string institutionId, string name, string redirectUrl);

        Task<bool> DeleteBank(string requisitionId);

        Task<ICollection<Requisition>> GetRequisitions();

        ValueTask StartImport(CancellationToken cancellationToken);

        #endregion
    }
}