using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileSubDistrictModel
    {
        public int SubDistrictId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string SubDistrictNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubDistrictNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        [System.ComponentModel.DataAnnotations.Required]
        public string ZipCode { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int DistrictId { get; set; }

    }
}
