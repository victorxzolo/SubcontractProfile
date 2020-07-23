using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_title] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileTitle_PK
    {

        public string TitleId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_title] 
    /// =================================================================

    public class SubcontractProfileTitle : System.ICloneable
    {

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string TitleId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TitleNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TitleNameEn { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

