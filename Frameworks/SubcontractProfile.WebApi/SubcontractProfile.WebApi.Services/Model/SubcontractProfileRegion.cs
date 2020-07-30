using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{

    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description: PK class for the table [dbo].[subcontract_profile_region] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileRegion_PK
    {

        public int RegionId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description: Entity class for the table [dbo].[subcontract_profile_region] 
    /// =================================================================

    public class SubcontractProfileRegion : System.ICloneable
    {

        public int RegionId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string RegionName { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
