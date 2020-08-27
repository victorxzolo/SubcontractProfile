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

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid TrainingId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid LocationId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid TeamId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid EngineerId { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateUser { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TeamNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StaffNameTh { get; set; }

        public string Position { get; set; }

 
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}