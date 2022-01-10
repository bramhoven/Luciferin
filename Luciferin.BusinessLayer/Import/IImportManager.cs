using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.BusinessLayer.Import
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