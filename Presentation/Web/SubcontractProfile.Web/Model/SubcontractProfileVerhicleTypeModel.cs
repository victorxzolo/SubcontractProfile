using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileVerhicleTypeModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string VerhicleTypeId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VerhiclTypeName { get; set; }
    }
}
