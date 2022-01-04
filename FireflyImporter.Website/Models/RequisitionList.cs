using System.Collections.Generic;
using System.Linq;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.Website.Models
{
    public class RequisitionList
    {
        #region Properties

        public bool AnyRequisitions => Requisitions.Any();

        public bool Deletable { get; set; }

        public ICollection<Requisition> Requisitions { get; }

        #endregion

        #region Constructors

        public RequisitionList(ICollection<Requisition> requisitions)
        {
            Requisitions = requisitions;
        }

        #endregion
    }
}