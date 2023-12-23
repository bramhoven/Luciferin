using System.Collections.Generic;
using System.Linq;

namespace Luciferin.Website.Models
{
    using Core.Entities;

    public class RequisitionList
    {
        #region Properties

        public bool AnyAccounts => Requisitions.Any();

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