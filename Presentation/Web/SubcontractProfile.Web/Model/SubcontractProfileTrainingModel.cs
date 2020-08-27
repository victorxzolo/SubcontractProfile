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

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Course { get; set; }

        public System.DateTime? RequestDate { get; set; }

        public string Remark { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? Vat { get; set; }

        public decimal? Tax { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string Status { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string RequestNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string company_name_th { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string tax_id { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string contract_name { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string contract_email { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string contract_phone { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string location_name_th { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string location_name_en { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string team_name_th { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string team_name_en { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string staff_name_th { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string staff_name_en { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string contract_phone1 { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string contract_email_en { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string position { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Engineer_ID { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Engineer_name { get; set; }

    }
}
