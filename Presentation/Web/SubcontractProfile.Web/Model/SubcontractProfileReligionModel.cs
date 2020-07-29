using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileReligionModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ReligionId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ReligionNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ReligionNameEn { get; set; }
    }
}
