using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_banking] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileBanking_PK
    {

        public System.Guid BankId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_banking] 
    /// =================================================================

    public class SubcontractProfileBanking : System.ICloneable
    {

        public System.Guid BankId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string BankCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string BankBranch { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
