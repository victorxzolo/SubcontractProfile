using System;
using System.Collections.Generic;
using System.Text;


namespace SubcontractProfile.Core.Entities
{


    /// =================================================================
    /// Author: kessada x10
    /// Description: PK class for the table [dbo].[subcontract_profile_company] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfileCompany_PK
    {

        public System.Guid CompanyId { get; set; }

    }

    /// =================================================================
    /// Author: kessada x10
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


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
