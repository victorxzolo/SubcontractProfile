using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{



    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description: PK class for the table [dbo].[subcontract_profile_province] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileProvince_PK
    {

        public int ProvinceId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description: Entity class for the table [dbo].[subcontract_profile_province] 
    /// =================================================================

    public class SubcontractProfileProvince : System.ICloneable
    {

        public int ProvinceId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string ProvinceNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ProvinceNameEn { get; set; }

        public int? RegionId { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

