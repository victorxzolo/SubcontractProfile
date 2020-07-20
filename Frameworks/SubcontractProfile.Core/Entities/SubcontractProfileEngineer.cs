using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.Core.Entities
{


    /// =================================================================
    /// Author: kessada x10
    /// Description: PK class for the table [dbo].[subcontract_profile_engineer] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileEngineer_PK
    {

        public System.Guid EngineerId { get; set; }

    }

    /// =================================================================
    /// Author: kessada x10
    /// Description: Entity class for the table [dbo].[subcontract_profile_engineer] 
    /// =================================================================

    public class SubcontractProfileEngineer : System.ICloneable
    {

        public System.Guid EngineerId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        [System.ComponentModel.DataAnnotations.Required]
        public string StaffCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string FoaCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AscCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string TshirtSize { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string ContractPhone1 { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string ContractPhone2 { get; set; }

        public string ContractEmail { get; set; }

        public string WorkExperience { get; set; }

        public string WorkExperienceAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string WorkType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CourseSkill { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SkillLevel { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleBrand { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleSerise { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleColor { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string VehicleYear { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string VehicleLicensePlate { get; set; }

        public string VehicleAttachFile { get; set; }

        public string ToolOtrd { get; set; }

        public string ToolSplicing { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Position { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string StaffId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TeamCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CitizenId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string BankCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AccountNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string AccountName { get; set; }

        public string PersonalAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffStatus { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

