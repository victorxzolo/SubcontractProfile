using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_user] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileUser_PK
    {

        public System.Guid UserId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_user] 
    /// =================================================================

    public class SubcontractProfileUser : System.ICloneable
    {

        public System.Guid UserId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Username { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubModuleName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SsoFirstName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SsoLastName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StaffName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string StaffRole { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string password { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string companyid { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string region { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
