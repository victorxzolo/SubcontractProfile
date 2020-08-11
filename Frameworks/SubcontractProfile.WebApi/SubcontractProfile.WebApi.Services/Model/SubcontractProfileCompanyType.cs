using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{

    /// =================================================================
    /// Author: AIS-X10
    /// Description: PK class for the table [dbo].[subcontract_profile_company_type] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileCompanyType_PK
    {

        public string CompanyTypeId { get; set; }

    }

    /// =================================================================
    /// Author: AIS-X10
    /// Description: Entity class for the table [dbo].[subcontract_profile_company_type] 
    /// =================================================================

    public class SubcontractProfileCompanyType : System.ICloneable
    {

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string CompanyTypeId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string CompanyTypeNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyTypeNameEn { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
