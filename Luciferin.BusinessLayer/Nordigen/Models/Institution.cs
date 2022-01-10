using System;
using System.Collections.Generic;

namespace Luciferin.BusinessLayer.Nordigen.Models
{
    public class Institution
    {
        #region Properties

        public string Bic { get; set; }
        public ICollection<string> Countries { get; set; }
        public string Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public int TransactionTotalDays { get; set; }

        #endregion
    }
}