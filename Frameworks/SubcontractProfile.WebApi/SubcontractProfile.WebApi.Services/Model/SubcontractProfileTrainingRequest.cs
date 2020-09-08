using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Model
{
    public class SubcontractProfileTraining1_PK
    {

        public System.Guid TrainingId { get; set; }

    }

    public class SubcontractProfileTrainingRequest : System.ICloneable
    {
        public System.Guid TrainingId { get; set; }

        ////  [System.ComponentModel.DataAnnotations.Required]
        public System.Guid CompanyId { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(50)]
        //  [System.ComponentModel.DataAnnotations.Required]
        public string ContractPhone { get; set; }

        public string ContractEmail { get; set; }

        public string Course { get; set; }

        // [System.ComponentModel.DataAnnotations.Required]
        public decimal? CoursePrice { get; set; }


        // [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime? RequestDate { get; set; }

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


        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }
        public DateTime? BookingDate { get; set; }
        public string RemarkForAis { get; set; }

        public DateTime? TestDate { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Skill { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Grade { get; set; }

        public string BookingDateStr { get; set; }

        public string RequestDateStr { get; set; }

        public string CompanyNameTh { get; set; }
        public string TaxId { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

