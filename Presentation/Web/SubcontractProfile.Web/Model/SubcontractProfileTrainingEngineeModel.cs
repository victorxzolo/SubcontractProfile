using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.Web.Model
{
    /// =================================================================

    public class SubcontractProfileTrainingEngineerModel 
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

        //public System.DateTime? CreateDate { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

        //public System.DateTime? UpdateDate { get; set; }

      
        public string LocationNameTh { get; set; }

        public string TeamNameTh { get; set; }

    
        public string StaffNameTh { get; set; }

        public string Position { get; set; }

        public string TestStatus { get; set; }


        public string TestStatusName { get; set; }

        public string ContractPhone1 { get; set; }
        public string ContractEmail { get; set; }

        public string SkillLevel { get; set; }

        public string TestReason { get; set; }
        public string TestRemark { get; set; }

        public decimal? CoursePrice { get; set; }

        public string Remark { get; set; }

    }
}