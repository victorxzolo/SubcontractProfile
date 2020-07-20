using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.Core.Entities
{


    /// =================================================================
    /// Author: kessada x10
    /// Description: PK class for the table [dbo].[subcontract_profile_location] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileLocation_PK
    {

        public System.Guid LocationId { get; set; }

    }

    /// =================================================================
    /// Author: kessada x10
    /// Description: Entity class for the table [dbo].[subcontract_profile_location] 
    /// =================================================================

    public class SubcontractProfileLocation : System.ICloneable
    {

        public System.Guid LocationId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [System.ComponentModel.DataAnnotations.Required]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        [System.ComponentModel.DataAnnotations.Required]
        public string LocationNameAlias { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VendorCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StorageLocation { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ShipTo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string OutOfServiceStorageLocation { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubPhase { get; set; }

        public System.DateTime? EffectiveDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ShopType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VatBranchNumber { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Phone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyMainContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string InstallationsContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string MaintenanceContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string InventoryContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PaymentContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string EtcContractPhone { get; set; }

        public string CompanyGroupMail { get; set; }

        public string InstallationsContractMail { get; set; }

        public string MaintenanceContractMail { get; set; }

        public string InventoryContractMail { get; set; }

        public string PaymentContractMail { get; set; }

        public string EtcContractMail { get; set; }

        public string LocationAddress { get; set; }

        public string PostAddress { get; set; }

        public string TaxAddress { get; set; }

        public string WtAddress { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string HouseNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AreaCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string BankCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankAccountNo { get; set; }

        public string BankAccountName { get; set; }

        public string BankAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Status { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
