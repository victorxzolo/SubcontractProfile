using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_team] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileTeam_PK
    {

        public System.Guid TeamId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_team] 
    /// =================================================================

    public class SubcontractProfileTeam : System.ICloneable
    {

        public System.Guid TeamId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string TeamCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string TeamName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string TeamNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string TeamNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ShipTo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StageLocal { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string OosStorageLocation { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string VendorCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string JobType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubcontractType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubcontractSubType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string WarrantyMa { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string WarrantyInstall { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ServiceSkill { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string InstallationsContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string MaintenanceContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string EtcContractPhone { get; set; }

        public string InstallationsContractEmail { get; set; }

        public string MaintenanceContractEmail { get; set; }

        public string EtcContractEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Status { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string UpdateBy { get; set; }

        public System.DateTime? UpdateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationNameTh { get; set; }

        public System.Guid CompanyId { get; set; }
        public System.Guid LocationId { get; set; }

        public string CompanyStatus { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

