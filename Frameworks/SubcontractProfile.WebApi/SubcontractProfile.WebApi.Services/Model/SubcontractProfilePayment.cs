using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_payment] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfilePayment_PK
    {

        public string PaymentId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_payment] 
    /// =================================================================

    public class SubcontractProfilePayment : System.ICloneable
    {
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentChannal { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime? PaymentDatetime { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public decimal AmountTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        //[System.ComponentModel.DataAnnotations.Required]
        public string BankTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankBranch { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        public string SlipAttachFile { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(255)]
        //[System.ComponentModel.DataAnnotations.Required]
        public string ContactName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        //[System.ComponentModel.DataAnnotations.Required]
        public string ContactPhoneNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string ContactEmail { get; set; }

        public string Remark { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(50)]
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


        public DateTime? verifiedDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(5000)]
        public string remarkForSub { get; set; }


        //Search payment verify
        public System.DateTime? RequestDateFrom { get; set; }

        public System.DateTime? RequestDateTo { get; set; }
        public System.DateTime? PaymentDatetimeFrom { get; set; }
        public System.DateTime? PaymentDatetimeTo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

