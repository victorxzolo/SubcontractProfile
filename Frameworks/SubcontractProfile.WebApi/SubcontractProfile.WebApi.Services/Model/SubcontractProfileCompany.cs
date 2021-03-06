﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{


    /// =================================================================
    /// Author: AIS Fibre
    /// Description: PK class for the table [dbo].[subcontract_profile_company] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileCompany_PK
    {

        public System.Guid CompanyId { get; set; }

    }

    /// =================================================================
    /// Author: AIS Fibre
    /// Description: Entity class for the table [dbo].[subcontract_profile_company] 
    /// =================================================================

    public class SubcontractProfileCompany : System.ICloneable
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


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

