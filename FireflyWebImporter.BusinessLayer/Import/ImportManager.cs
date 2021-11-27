using System;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Nordigen;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Import
{
    public class ImportManager : IImportManager
    {
        #region Fields

        private readonly INordigenManager _nordigenManager;

        #endregion

        #region Constructors

        public ImportManager(INordigenManager nordigenManager)
        {
            _nordigenManager = nordigenManager;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<Requisition> AddNewBank(string institutionId, string bankName, string redirectUrl)
        {
            var institution = await _nordigenManager.GetInstitution(institutionId);
            var endUserAgreement = await _nordigenManager.CreateEndUserAgreement(institution);
            return await _nordigenManager.CreateRequisition(institution, bankName, endUserAgreement, redirectUrl);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteBank(string requisitionId)
        {
            try
            {
                var requisition = await _nordigenManager.GetRequisition(requisitionId);
                var endUserAgreement = await _nordigenManager.GetEndUserAgreement(requisition.Agreement);
                
                await _nordigenManager.DeleteEndUserAgreement(endUserAgreement.Id);
                await _nordigenManager.DeleteRequisition(requisition.Id);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}