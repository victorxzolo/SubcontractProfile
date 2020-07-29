using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileProvinceModel
    {
        public int ProvinceId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ProvinceName { get; set; }
    }
}
