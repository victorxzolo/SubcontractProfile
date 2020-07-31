using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileRegionModel
    {
        public int RegionId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string RegionName { get; set; }
    }
}
