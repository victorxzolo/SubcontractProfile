using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractSearchCompanyModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ProfileType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string VendorCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string CompanyNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]

        public string CompanyNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]

        public string CompanyAlias { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]

        public string CompanyCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]

        public string DistibutionChannel { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]

        public string ChannelGroup { get; set; }
    }
}
