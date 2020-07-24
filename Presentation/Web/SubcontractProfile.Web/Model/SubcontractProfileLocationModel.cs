using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileLocationModel
    {
        public System.Guid LocationId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string LocationNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
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

        public System.DateTime CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string UpdateBy { get; set; }

        public System.DateTime? UpdateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string BankBranchCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string BankBranchName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string PenaltyContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string PenaltyContractMail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractMail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyId { get; set; }

    }



    #region View
    public class Search_subcontract_profile_location : DataTableAjaxModel //รับ Search จากหน้าจอ
    {
        public string company_name_th { get; set; }
        public string company_name_en { get; set; }
        public string company_alias { get; set; }
        public string company_code { get; set; }
        public string location_name_th { get; set; }
        public string location_name_en { get; set; }
        public string location_code { get; set; }
        public string distribution_channel { get; set; }
        public string channel_sale_group { get; set; }
    }
    #endregion
}
