using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Nordigen.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Luciferin.BusinessLayer.Import
{
    public interface IImportManager
    {
        #region Methods

        Task<Requisition> AddNewBank(string institutionId, string name, string redirectUrl);

        Task<bool> DeleteBank(string requisitionId);

        Task<bool> CheckAndExecuteAutomaticImport(CancellationToken cancellationToken);

        Task<ICollection<Requisition>> GetImportableRequisitions();

        ValueTask StartImport(IServiceScope scope, CancellationToken cancellationToken);

        #endregion
    }
}