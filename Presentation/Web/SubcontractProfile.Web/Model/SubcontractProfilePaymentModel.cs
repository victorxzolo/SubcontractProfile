using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfilePaymentModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentChannal { get; set; }

       // [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime? PaymentDatetime { get; set; }

       // [System.ComponentModel.DataAnnotations.Required]
        public decimal AmountTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        //[System.ComponentModel.DataAnnotations.Required]
        public string BankTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankBranch { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string SlipAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
       // [System.ComponentModel.DataAnnotations.Required]
        public string ContactName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        //[System.ComponentModel.DataAnnotations.Required]
        public string ContactPhoneNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string ContactEmail { get; set; }

        public string Remark { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        //[System.ComponentModel.DataAnnotations.Required]
        public string Status { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid TrainingId { get; set; }


       // [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? CompanyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Request_no { get; set; }

        public System.DateTime? RequestDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string companyNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string taxId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(5000)]
        public string companyAddress { get; set; }

        public IFormFile FileSilp { get; set; }

        public DateTime? verifiedDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(5000)]
        public string remarkForSub { get; set; }


        public System.DateTime? RequestDateFrom { get; set; }

        public System.DateTime? RequestDateTo { get; set; }
        public System.DateTime? PaymentDatetimeFrom { get; set; }
        public System.DateTime? PaymentDatetimeTo { get; set; }

        public string? datetimepayment { get; set; }


        public Guid file_id_Slip { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SubcontractProfilePaymentViewModel
    {
        public string PaymentNo { get; set; }
        public string RequestNo { get; set; }
        public string RequestDateFrom { get; set; }
        public string RequestDateTo { get; set; }
        public string PaymentDatetimeFrom { get; set; }

        public string PaymentDatetimeTo { get; set; }

        public string Status { get; set; }
        public string companyNameTh { get; set; }
        public string taxId { get; set; }
    }
}
