using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileDistrictModel
    {
        public int DistrictId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DistrictName { get; set; }

        public int ProvinceId { get; set; }
    }
}
