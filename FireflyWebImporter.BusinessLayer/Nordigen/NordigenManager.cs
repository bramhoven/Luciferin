using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models.Requests;
using FireflyWebImporter.BusinessLayer.Nordigen.Stores;

namespace FireflyWebImporter.BusinessLayer.Nordigen
{
    public class NordigenManager : INordigenManager
    {
        #region Fields

        private readonly INordigenStore _store;

        private OpenIdToken _openIdToken;

        #endregion

        #region Constructors

        public NordigenManager(INordigenStore store)
        {
            _store = store;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<EndUserAgreement> CreateEndUserAgreement(Institution institution)
        {
            await ValidateToken();

            return await _store.CreateEndUserAgreement(new NordigenEndUserAgreementRequest
            {
                AccessScopes = new[] { "balances", "details", "transactions" },
                InstitutionId = institution.Id,
                MaxHistoricalDays = institution.TransactionTotalDays,
                AccessValidForDays = 90
            }, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<Requisition> CreateRequisition(Institution institution, string reference, EndUserAgreement agreement, string redirectUrl)
        {
            await ValidateToken();
            
            return await _store.CreateRequisition(new NordigenRequisitionRequest
            {
                Reference = reference,
                AgreementId = agreement.Id,
                InstitutionId = institution.Id,
                RedirectUrl = redirectUrl
            }, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteEndUserAgreement(string endUserAgreementId)
        {
            await ValidateToken();

            return await _store.DeleteEndUserAgreement(endUserAgreementId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteRequisition(string requisitionId)
        {
            await ValidateToken();
            
            return await _store.DeleteRequisition(requisitionId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<Account> GetAccount(string accountId)
        {
            await ValidateToken();
            
            return await _store.GetAccount(accountId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Balance>> GetAccountBalance(string accountId)
        {
            await ValidateToken();
            
            return await _store.GetAccountBalance(accountId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<AccountDetails> GetAccountDetails(string accountId)
        {
            await ValidateToken();
            
            return await _store.GetAccountDetails(accountId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Transaction>> GetAccountTransactions(string accountId)
        {
            await ValidateToken();
            
            return await _store.GetAccountTransactions(accountId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<EndUserAgreement> GetEndUserAgreement(string endUserAgreementId)
        {
            await ValidateToken();

            return await _store.GetEndUserAgreement(endUserAgreementId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<Institution> GetInstitution(string institutionId)
        {
            await ValidateToken();
            
            return await _store.GetInstitution(institutionId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Institution>> GetInstitutions(string countryCode)
        {
            await ValidateToken();
            
            return await _store.GetInstitutions(countryCode, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<Requisition> GetRequisition(string requisitionId)
        {
            await ValidateToken();

            return await _store.GetRequisition(requisitionId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Requisition>> GetRequisitions()
        {
            await ValidateToken();
            
            return await _store.GetRequisitions(_openIdToken);
        }

        private async Task<OpenIdToken> GetToken()
        {
            return await _store.GetToken();
        }

        private async Task<OpenIdToken> RefreshToken()
        {
            return await _store.RefreshToken(_openIdToken);
        }

        private async Task ValidateToken()
        {
            if (_openIdToken == null)
            {
                _openIdToken = await GetToken();
                return;
            }

            if (_openIdToken.Created.Add(_openIdToken.RefreshTokenExpires) < DateTime.Now)
            {
                _openIdToken = await GetToken();
                return;
            }

            if (_openIdToken.Created.Add(_openIdToken.AccessTokenExpires) < DateTime.Now)
            {
                _openIdToken = await RefreshToken();
                return;
            }

            if (_openIdToken == null)
                throw new InvalidOperationException(nameof(_openIdToken));
        }

        #endregion
    }
}