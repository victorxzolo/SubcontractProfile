using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileVerhicleBrandModel
    {

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string VerhicleBrandId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VerhicleBrandName { get; set; }
    }
}
