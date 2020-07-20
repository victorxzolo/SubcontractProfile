using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.Core.Entities
{


    /// =================================================================
    /// Author: kessada x10
    /// Description: PK class for the table [dbo].[subcontract_profile_personal] 
    /// It's bit heavy (a lot of useless types in the DB) but you can
    /// use get by PKList even if your pk is a composite one...
    /// =================================================================
    public class SubcontractProfilePersonal_PK
    {

        public System.Guid PersonalId { get; set; }

    }

    /// =================================================================
    /// Author: kessada x10
    /// Description: Entity class for the table [dbo].[subcontract_profile_personal] 
    /// =================================================================

    public class SubcontractProfilePersonal : System.ICloneable
    {

        public System.Guid PersonalId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CitizenId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string TitleName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string FullNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string FullNameTh { get; set; }

        public System.DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Race { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Nationality { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Religion { get; set; }

        public string PassportAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string IdentityBy { get; set; }

        public int? AddressId { get; set; }

        public string IdentityCardAddress { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ContactPhone1 { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ContactPhone2 { get; set; }

        public string ContactEmail { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string WorkPermitNo { get; set; }

        public string WorkPermitAttachFile { get; set; }

        public string ProfileImgAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Education { get; set; }

        public string ThListening { get; set; }

        public string ThSpeaking { get; set; }

        public string ThReading { get; set; }

        public string ThWriting { get; set; }

        public string EnListening { get; set; }

        public string EnSpeaking { get; set; }

        public string EnReading { get; set; }

        public string EnWriting { get; set; }

        public string CertificateType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CertificateNo { get; set; }

        public System.DateTime? CertificateExpireDate { get; set; }

        public string CertificateAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string AccountNumber { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string AccountName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
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
