using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileWarrantyModel
    {

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string WarrantyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string WarrantyDesc { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ModifiledBy { get; set; }

        public System.DateTime? ModifiledDate { get; set; }

    }
}
