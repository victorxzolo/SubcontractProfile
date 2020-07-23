using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_warranty] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileWarranty_PK
    {

        public string WarrantyId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_warranty] 
    /// =================================================================

    public class SubcontractProfileWarranty : System.ICloneable
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


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
