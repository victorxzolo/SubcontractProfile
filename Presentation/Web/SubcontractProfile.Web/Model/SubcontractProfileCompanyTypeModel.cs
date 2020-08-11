using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileCompanyTypeModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string CompanyTypeId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string CompanyTypeNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyTypeNameEn { get; set; }
    }
}
