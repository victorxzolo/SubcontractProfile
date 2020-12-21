using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfilePersonalModel
    {
        public System.Guid engineerId { get; set; }
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
        public  string _BirthDate { get; set; }
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

        public System.DateTime CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string UpdateBy { get; set; }

        public System.DateTime? UpdateDate { get; set; }


        public Guid file_id__CertificateAttach { get; set; }
        public IFormFile File_CertificateAttach { get; set; }

        public Guid file_id__WorkPermitAttach { get; set; }
        public IFormFile File_WorkPermitAttach { get; set; }

        public Guid file_id__ProfileImgAttach { get; set; }
        public IFormFile File_ProfileImgAttach { get; set; }

        public string? dateBirthDay { get; set; }
        public string? dateCertificateExpireDate { get; set; }
        public string _CertificateExpireDate { get; set; }
    }
}
