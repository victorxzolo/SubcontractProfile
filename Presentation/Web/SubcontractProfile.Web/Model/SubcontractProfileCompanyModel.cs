using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileCompanyModel
    {
        public System.Guid CompanyId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CompanyCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string CompanyName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string CompanyNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string CompanyNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string CompanyAlias { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DistributionChannel { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ChannelSaleGroup { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VendorCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CustomerCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AreaId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TaxId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string WtName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string VatType { get; set; }

        public string CompanyCertifiedFile { get; set; }

        public string CommercialRegistrationFile { get; set; }

        public string VatRegistrationCertificateFile { get; set; }

        public string ContractAgreementFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DepositAuthorizationLevel { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DepositPaymentType { get; set; }

        public System.DateTime? ContractStartDate { get; set; }

        public System.DateTime? ContractEndDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string OverDraftDeposit { get; set; }

        public decimal? BalanceDeposit { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyStatus { get; set; }

        public string CompanyAddress { get; set; }

        public string VatAddress { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CreateBy { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string UpdateBy { get; set; }

        public System.DateTime? UpdateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string ContractEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string AccountNumber { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string AccountName { get; set; }

        public string AttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BranchCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BranchName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfInstallName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfMaintenName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfAccountName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfInstallPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfMaintenPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfAccountPhone { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfInstallEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfMaintenEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DeptOfAccountEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(250)]
        public string LocationNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(250)]
        public string LocationNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string BankAccountTypeId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubcontractProfileType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CompanyTitleThId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CompanyTitleEnId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(30)]
        public string Status { get; set; }

        public System.DateTime? ActivateDate { get; set; }


        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string User_name { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Password { get; set; }
        public System.DateTime? RegisterDate { get; set; }
        public string ASCCode { get; set; }

        public System.DateTime? verified_date { get; set; }

        public Guid file_id_CompanyCertifiedFile { get; set; }
        public Guid file_id_CommercialRegistrationFile { get; set; }
        public Guid file_id_VatRegistrationCertificateFile { get; set; }
        public Guid file_id_bookbank { get; set; }


        public string TrainingStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? CompanyRegisterDate { get; set; }
        public DateTime? TriningDate { get; set; }
        public DateTime? PaymentDatetime { get; set; }

        public IFormFile FileBookBank { get; set; }
        public IFormFile FileCompanyCertified { get; set; }
        public IFormFile FileCommercialRegistration { get; set; }
        public IFormFile FileVatRegistrationCertificate { get; set; }

        public string RemarkForSub { get; set; }
        public string RegisterDateStr { get; set; }
        public string BankAccountType { get; set; }
        

    }

    public class SubcontractProfileCompanyViewModel
    {
        public string SubcontractProfileType { get; set; }
        public string TaxId { get; set; }
        public string CompanyName { get; set; }
        public string DistributionChannel { get; set; }
        public string ChannelSaleGroup { get; set; }
       
        public string RegisterDateFrom { get; set; }

        public string RegisterDateTo { get; set; }
        public string VendorCode { get; set; }
        public string Status { get; set; }
    }
}
