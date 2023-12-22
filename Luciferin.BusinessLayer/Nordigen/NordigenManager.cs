using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Luciferin.BusinessLayer.Exceptions;
using Luciferin.BusinessLayer.Nordigen.Enums;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.BusinessLayer.Nordigen.Models.Requests;
using Luciferin.BusinessLayer.Nordigen.Stores;

namespace Luciferin.BusinessLayer.Nordigen
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
            if (!_store.IsConfigured())
                return null;
            
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
            if (!_store.IsConfigured())
                return null;

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
            if (!_store.IsConfigured())
                return false;

            await ValidateToken();

            return await _store.DeleteEndUserAgreement(endUserAgreementId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteRequisition(string requisitionId)
        {
            if (!_store.IsConfigured())
                return false;

            await ValidateToken();

            return await _store.DeleteRequisition(requisitionId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<Account> GetAccount(string accountId)
        {
            if (!_store.IsConfigured())
                return null;

            await ValidateToken();

            return await _store.GetAccount(accountId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Balance>> GetAccountBalance(string accountId)
        {
            if (!_store.IsConfigured())
                return new List<Balance>();

            await ValidateToken();

            return await _store.GetAccountBalance(accountId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<AccountDetails> GetAccountDetails(string accountId)
        {
            if (!_store.IsConfigured())
                return null;

            await ValidateToken();

            try
            {
                return await _store.GetAccountDetails(accountId, _openIdToken);
            }
            catch (FlurlHttpException e)
            {
                if (e.StatusCode == 409)
                    throw new AccountSuspendedException(accountId);
                
                throw new AccountFailureException(accountId);
            }
        }

        /// <inheritdoc />
        public async Task<ICollection<Transaction>> GetAccountTransactions(string accountId)
        {
            if (!_store.IsConfigured())
                return new List<Transaction>();

            await ValidateToken();

            return await _store.GetAccountTransactions(accountId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Transaction>> GetAccountTransactions(string accountId, DateTime fromDate)
        {
            if (!_store.IsConfigured())
                return new List<Transaction>();

            await ValidateToken();

            return await _store.GetAccountTransactions(accountId, fromDate, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<EndUserAgreement> GetEndUserAgreement(string endUserAgreementId)
        {
            if (!_store.IsConfigured())
                return null;

            await ValidateToken();

            return await _store.GetEndUserAgreement(endUserAgreementId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<Institution> GetInstitution(string institutionId)
        {
            if (!_store.IsConfigured())
                return null;

            await ValidateToken();

            return await _store.GetInstitution(institutionId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Institution>> GetInstitutions(string countryCode)
        {
            if (!_store.IsConfigured())
                return new List<Institution>();

            await ValidateToken();

            try
            {

                return await _store.GetInstitutions(countryCode, _openIdToken);
            }
            catch (FlurlHttpException e)
            {
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<Requisition> GetRequisition(string requisitionId)
        {
            if (!_store.IsConfigured())
                return null;

            await ValidateToken();

            return await _store.GetRequisition(requisitionId, _openIdToken);
        }

        /// <inheritdoc />
        public async Task<ICollection<Requisition>> GetRequisitions()
        {
            if (!_store.IsConfigured())
                return new List<Requisition>();

            await ValidateToken();

            return await _store.GetRequisitions(_openIdToken);
        }

        private async Task<OpenIdToken> GetToken()
        {
            if (!_store.IsConfigured())
                return null;

            return await _store.GetToken();
        }

        private async Task<OpenIdToken> RefreshToken()
        {
            if (!_store.IsConfigured())
                return null;

            return await _store.RefreshToken(_openIdToken);
        }

        private async Task ValidateToken()
        {
            if (!_store.IsConfigured())
                return;

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