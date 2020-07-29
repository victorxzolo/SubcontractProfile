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
        public string SubDistrictName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string ZipCode { get; set; }

        public int DistrictId { get; set; }

    }
}
