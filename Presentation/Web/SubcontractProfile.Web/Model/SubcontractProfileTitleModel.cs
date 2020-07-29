using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileTitleModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string TitleId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TitleNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TitleNameEn { get; set; }
    }
}
