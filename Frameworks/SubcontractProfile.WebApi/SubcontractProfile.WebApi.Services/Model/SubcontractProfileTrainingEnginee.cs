using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{

    /// =================================================================
    /// Author: AIS-X10
    /// Description: PK class for the table [dbo].[subcontract_profile_training_engineer] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileTrainingEngineer_PK
    {

        public System.Guid TrainingEngineerId { get; set; }

    }

    /// =================================================================
    /// Author: AIS-X10
    /// Description: Entity class for the table [dbo].[subcontract_profile_training_engineer] 
    /// =================================================================

    public class SubcontractProfileTrainingEngineer : System.ICloneable
    {

        public System.Guid TrainingEngineerId { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        public System.Guid TrainingId { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        public System.Guid LocationId { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        public System.Guid TeamId { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        public System.Guid EngineerId { get; set; }

       // public System.DateTime? CreateDate { get; set; }

     
        public string CreateBy { get; set; }

     
        public string LocationNameTh { get; set; }

     
        public string TeamNameTh { get; set; }

        public string StaffNameTh { get; set; }

        public string Position { get; set; }

        public string TestStatus { get; set; }
        public string UpdateBy { get; set; }

        public string ContractEmail { get; set; }

        public string ContractPhone1 { get; set; }

        public string TestStatusName { get; set; }
        public string SkillLevel { get; set; }

        public string TestReason { get; set; }
        public string TestRemark { get; set; }
        public decimal? CoursePrice { get; set; }

        public string Remark { get; set; }

        public string test_reason_text { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}