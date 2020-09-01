using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileTrainingModel
    {
        public System.Guid TrainingId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid CompanyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
      [System.ComponentModel.DataAnnotations.Required]
        public string Course { get; set; }

       [System.ComponentModel.DataAnnotations.Required]
        public decimal? CourcePrice { get; set; }


       [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime RequestDate { get; set; }

        public string Remark { get; set; }

       [System.ComponentModel.DataAnnotations.Required]
        public decimal TotalPrice { get; set; }

       [System.ComponentModel.DataAnnotations.Required]
        public decimal Vat { get; set; }

        public decimal? Tax { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Status { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string RequestNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string EngineerId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TeamId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyNameTh { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TaxId { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractName { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractEmail { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractPhone { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationNameTh { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationNameEn { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TeamNameTh { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TeamNameEn { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StaffNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StaffNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractPhone1 { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractEmailEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]

        public string Position { get; set; }

        public string BookingDate { get; set; }
        public string RemarkForAis { get; set; }
        public string Engineer_name { get; set; }
      //  public string EngineerID { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string EngineerName { get; set; }

        public DateTime? TestDate { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Skill { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Grade { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentStatus { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public decimal? CoursePrice { get; set; }

        public string _bookingDate { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
