using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileTrainingModel
    {


        public System.Guid TrainingId { get; set; }


        public System.Guid CompanyId { get; set; }

        public string ContractName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]

        public string ContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]

        public string ContractEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]

        public string Course { get; set; }


        public decimal? CoursePrice { get; set; }


        public System.DateTime? RequestDate { get; set; }

        public string Remark { get; set; }


        public decimal? TotalPrice { get; set; }


        public decimal? Vat { get; set; }

        public decimal? Tax { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]

        public string Status { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string RequestNo { get; set; }

        public DateTime? BookingDate { get; set; }

        public string RemarkForAis { get; set; }

        public DateTime? TestDate { get; set; }

        public string Skill { get; set; }

        public string Grade { get; set; }


        public string CreateBy { get; set; }

        public System.DateTime? CreateDate { get; set; }


        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }


        public string CompanyNameTh { get; set; }
        public string TaxId { get; set; }

        public string PaymentStatus { get; set; }

        public Decimal? TotalAmount { get; set; }

   
        public string BookingDateStr { get; set; }

        public string RequestDateStr { get; set; }
        public string TestDateStr { get; set; }

        public string CourseName { get; set; }

        public string RemarkForTest { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
