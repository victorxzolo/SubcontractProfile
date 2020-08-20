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

        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime PaymentDatetime { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public decimal AmountTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string BankTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankBranch { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string SlipAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        [System.ComponentModel.DataAnnotations.Required]
        public string ContactName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string ContactPhoneNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string ContactEmail { get; set; }

        public string Remark { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Status { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid TrainingId { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid CompanyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Request_no { get; set; }

        public System.DateTime? Request_date { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string companyNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string taxId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(5000)]
        public string companyAddress { get; set; }

        public IFormFile FileSilp { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
