using System;
using System.Collections.Generic;
using System.Linq;
using FireflyWebImporter.BusinessLayer.Nordigen.Enums;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Helpers
{
    public static class NordigenResponseMappers
    {
        #region Methods

        #region Static Methods

        public static EndUserAgreement MapToEndUserAgreement(this NordigenEndUserAgreementResponse endUserAgreementResponse)
        {
            return new EndUserAgreement
            {
                Accepted = endUserAgreementResponse.Accepted,
                Created = endUserAgreementResponse.Created,
                Id = endUserAgreementResponse.Id,
                AccessScope = endUserAgreementResponse.AccessScope,
                InstitutionId = endUserAgreementResponse.InstitutionId,
                MaxHistoricalDays = endUserAgreementResponse.MaxHistoricalDays,
                AccessValidForDays = endUserAgreementResponse.AccessValidForDays
            };
        }

        public static Institution MapToInstitution(this NordigenInstitutionResponse institutionResponse)
        {
            return new Institution
            {
                Bic = institutionResponse.Bic,
                Countries = institutionResponse.Countries,
                Id = institutionResponse.Id,
                Logo = institutionResponse.Logo,
                Name = institutionResponse.Name,
                TransactionTotalDays = institutionResponse.TransactionTotalDays
            };
        }

        public static ICollection<Institution> MapToInstitutionCollection(this IEnumerable<NordigenInstitutionResponse> institutionResponseCollection)
        {
            return institutionResponseCollection.Select(i => i.MapToInstitution()).ToList();
        }

        public static OpenIdToken MapToOpenIdToken(this NordigenTokenResponse tokenResponse)
        {
            return new OpenIdToken
            {
                Created = DateTime.Now,
                AccessToken = tokenResponse.Access,
                AccessTokenExpires = TimeSpan.FromSeconds(tokenResponse.AccessExpires),
                RefreshToken = tokenResponse.Refresh,
                RefreshTokenExpires = TimeSpan.FromSeconds(tokenResponse.RefreshExpires)
            };
        }

        public static Requisition MapToRequisition(this NordigenRequisitionResponse requisitionResponse)
        {
            return new Requisition
            {
                Accounts = requisitionResponse.Accounts.Select(a => Guid.Parse(a)).ToList(),
                Agreement = requisitionResponse.Agreement,
                AccountSelection = requisitionResponse.AccountSelection,
                Created = requisitionResponse.Created,
                Id = requisitionResponse.Id,
                Link = requisitionResponse.Link,
                Redirect = requisitionResponse.Redirect,
                Reference = requisitionResponse.Reference,
                Ssn = requisitionResponse.Ssn,
                Status = MapStatus(requisitionResponse.Status),
                InstitutionId = requisitionResponse.InstitutionId,
                UserLanguage = requisitionResponse.UserLanguage
            };
        }

        public static ICollection<Requisition> MapToRequisitionCollection(this IEnumerable<NordigenRequisitionResponse> requisitionResponses)
        {
            return requisitionResponses.Select(r => r.MapToRequisition()).ToList();
        }

        private static RequisitionStatus MapStatus(string status)
        {
            switch (status)
            {
                case "CR":
                    return RequisitionStatus.Created;
                case "LN":
                    return RequisitionStatus.Linked;
                case "RJ":
                    return RequisitionStatus.Rejected;
                case "SU":
                    return RequisitionStatus.Suspended;
            }

            return RequisitionStatus.Created;
        }

        #endregion

        #endregion
    }
}