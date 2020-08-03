using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfilePayment
    {
        public string PaymentId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentChannal { get; set; }

        public System.DateTime? PaymentDatetime { get; set; }

        public decimal? AmountTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankTransfer { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankBranch { get; set; }

        public string SlipAttachFile { get; set; }

        public string Remark { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }

        public System.Guid TrainingId { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
