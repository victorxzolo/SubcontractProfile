using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{

    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description: PK class for the table [dbo].[subcontract_profile_sub_district] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileSubDistrict_PK
    {

        public int SubDistrictId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description: Entity class for the table [dbo].[subcontract_profile_sub_district] 
    /// =================================================================

    public class SubcontractProfileSubDistrict : System.ICloneable
    {

        public int SubDistrictId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string SubDistrictNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubDistrictNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        [System.ComponentModel.DataAnnotations.Required]
        public string ZipCode { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int DistrictId { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

