using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileVerhicleSeriseModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string VerhicleSeriseId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VerhicleSeriseName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string VerhicleBrandId { get; set; }

    }
}
