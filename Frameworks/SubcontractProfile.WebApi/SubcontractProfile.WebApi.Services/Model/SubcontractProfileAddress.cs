using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_address] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileAddress_PK
    {

        public System.Guid AddressId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_address] 
    /// =================================================================

    public class SubcontractProfileAddress : System.ICloneable
    {

        public System.Guid AddressId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string HouseNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string Building { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string Floor { get; set; }

        public int? Moo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string Soi { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string Road { get; set; }

        public int? SubDistrictId { get; set; }

        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}


