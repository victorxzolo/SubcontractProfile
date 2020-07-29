using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{

    /// =================================================================
    /// Author: AIS Fibre -X10
    /// Description: Entity class for the table [dbo].[subcontract_profile_address_type] 
    /// =================================================================

    public class SubcontractProfileAddressType : System.ICloneable
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


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
