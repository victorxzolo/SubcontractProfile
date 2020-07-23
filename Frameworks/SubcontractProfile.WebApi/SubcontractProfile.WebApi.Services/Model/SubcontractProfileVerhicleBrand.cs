using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_verhicle_brand] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileVerhicleBrand_PK
    {

        public string VerhicleBrandId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_verhicle_brand] 
    /// =================================================================

    public class SubcontractProfileVerhicleBrand : System.ICloneable
    {

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string VerhicleBrandId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VerhicleBrandName { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
