using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_district] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileDistrict_PK
    {

        public int DistrictId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_district] 
    /// =================================================================

    public class SubcontractProfileDistrict : System.ICloneable
    {

        public int DistrictId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DistrictName { get; set; }

        public int ProvinceId { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

