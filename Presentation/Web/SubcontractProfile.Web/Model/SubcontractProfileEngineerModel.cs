using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileEngineerModel
    {
        public System.Guid EngineerId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string StaffCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string FoaCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffNameEn { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AscCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string TshirtSize { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string ContractPhone1 { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string ContractPhone2 { get; set; }

        public string ContractEmail { get; set; }

        public string WorkExperience { get; set; }

        public string WorkExperienceAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string WorkType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CourseSkill { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SkillLevel { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleType { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleBrand { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleSerise { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string VehicleColor { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string VehicleYear { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string VehicleLicensePlate { get; set; }

        public string VehicleAttachFile { get; set; }

        public string ToolOtrd { get; set; }

        public string ToolSplicing { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Position { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string LocationCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string StaffId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TeamCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CitizenId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string BankCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string BankName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AccountNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string AccountName { get; set; }

        public string PersonalAttachFile { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string StaffStatus { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string UpdateBy { get; set; }

    
        public System.DateTime? UpdateDate { get; set; }

        public System.Guid CompanyId { get; set; }
        public System.Guid LocationId { get; set; }
        public System.Guid TeamId { get; set; }
      
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string LocationNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string TeamNameTh { get; set; }
        public string SubcontractType { get; set; }
        public System.Guid PersonalId { get; set; }

        public string SubcontractProfileType { get; set; }



        public Guid file_id__PersonalAttach { get; set; }
        public IFormFile File_PersonalAttach{ get; set; }

        public Guid file_id__VehicleAttach { get; set; }
        public IFormFile File_VehicleAttach { get; set; }

        //public Guid file_id__WorkExperienceAttach { get; set; }
        //public IFormFile File_WorkExperienceAttach { get; set; }

    }
}
