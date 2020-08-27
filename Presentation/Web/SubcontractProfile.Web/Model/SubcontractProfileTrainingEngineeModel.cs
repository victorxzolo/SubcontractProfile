using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{
    /// =================================================================

    public class SubcontractProfileTrainingEngineerModel 
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

     


    }
}