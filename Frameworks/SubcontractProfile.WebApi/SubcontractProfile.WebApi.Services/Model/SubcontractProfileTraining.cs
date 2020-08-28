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

        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid CompanyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Course { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public decimal? cource_price { get; set; }


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

        public DateTime BookingDate { get; set; }
        public string RemarkForAis { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}

