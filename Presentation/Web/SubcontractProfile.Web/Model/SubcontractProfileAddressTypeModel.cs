using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileAddressTypeModel
    {

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AddressTypeId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(250)]
        [System.ComponentModel.DataAnnotations.Required]
        public string AddressTypeNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(250)]
        public string AddressTypeNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }

    }
}
