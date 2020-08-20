using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractSearchLocationModel
    {
        public System.Guid CompanyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]

        public string Phone { get; set; }

    }
}
