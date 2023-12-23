using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Luciferin.Website.Models.Configuration.FormModels
{
    using Core.Entities;

    public class ConfigurationAddAccountFormModel
    {
        #region Properties
        
        [Required(ErrorMessage = "Bank name is required")]
        public string RequisitionName { get; set; }

        [Required(ErrorMessage = "Institution is required")]
        public string InstitutionId { get; set; }

        public ICollection<Institution>? Institutions { get; set; }

        #endregion
    }
}