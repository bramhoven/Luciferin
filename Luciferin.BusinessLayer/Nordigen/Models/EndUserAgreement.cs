using System;
using System.Collections.Generic;

namespace Luciferin.BusinessLayer.Nordigen.Models
{
    public class EndUserAgreement
    {
        #region Properties
        
        public DateTime? Accepted { get; set; }

        public List<string> AccessScope { get; set; }

        public int AccessValidForDays { get; set; }

        public DateTime Created { get; set; }

        public string Id { get; set; }

        public string InstitutionId { get; set; }

        public int MaxHistoricalDays { get; set; }

        #endregion
    }
}