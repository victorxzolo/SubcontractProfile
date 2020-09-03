using System;
using System.Collections.Generic;
using System.Text;
namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_training] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileTraining_PK
    {

        public System.Guid TrainingId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_training] 
    /// =================================================================

    public class SubcontractProfileTraining : System.ICloneable
    {

        public System.Guid TrainingId { get; set; }

      //  [System.ComponentModel.DataAnnotations.Required]
        public System.Guid CompanyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
      //  [System.ComponentModel.DataAnnotations.Required]
        public string Course { get; set; }

       // [System.ComponentModel.DataAnnotations.Required]
        public decimal? CoursePrice { get; set; }


       // [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime RequestDate { get; set; }

        public string Remark { get; set; }

      //  [System.ComponentModel.DataAnnotations.Required]
        public decimal TotalPrice { get; set; }

      //  [System.ComponentModel.DataAnnotations.Required]
        public decimal Vat { get; set; }

        public decimal? Tax { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
      //  [System.ComponentModel.DataAnnotations.Required]
        public string Status { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string RequestNo { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(100)]
        //public System.Guid EngineerId { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(100)]
        //public System.Guid TeamId { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(100)]
        //public System.Guid LocationId { get; set; }


        public string CreateBy { get; set; }

        public System.DateTime? CreateDate { get; set; }


        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }
        public DateTime? BookingDate { get; set; }

        public string RemarkForAis { get; set; }

        public DateTime? TestDate { get; set; }

        public string Skill { get; set; }

        public string Grade { get; set; }

        public string PaymentStatus { get; set; }


        //public string CompanyNameTh { get; set; }

        //public string TaxId { get; set; }

        public string ContractName { get; set; }

        public string ContractEmail { get; set; }

        public string ContractPhone { get; set; }

        public decimal CourseName { get; set; }
        public decimal GrandTotal { get; set; }

        //public string LocationNameTh { get; set; }

        //public string LocationNameEn { get; set; }

        //public string TeamNameTh { get; set; }

        //public string TeamNameEn { get; set; }

        //public string StaffNameTh { get; set; }


        //public string StaffNameEn { get; set; }


        //public string ContractPhone1 { get; set; }


        //public string ContractEmailEn { get; set; }

        //public string Position { get; set; }



        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}

