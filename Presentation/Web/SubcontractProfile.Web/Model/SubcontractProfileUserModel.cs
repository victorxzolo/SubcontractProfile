using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileUserModel
    {
        public System.Guid UserId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Username { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubModuleName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SsoFirstName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SsoLastName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StaffName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string StaffRole { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CreateBy { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string password { get; set; }

   
        public Guid companyid { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string region { get; set; }

        public string SubcontractProfileType { get; set; }

        public AuthenticateType AuthenticateType { get; set; }
        public SSOFields SSOFields { get; set; }
        public bool ForceLogOut { get; set; }
    }

    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool keepme { get; set; }
        public string Language { get; set; }
    }

    public enum AuthenticateType
    {
        NotLoggedOn = 0,
        SSO = 1,
        SSOPartner = 2,
        LDAP = 3
    }


    public class SSOFields
    {
        public string Token { get; set; }
        public string SessionID { get; set; }
        public string UserName { get; set; }
        public string GroupID { get; set; }
        public string SubModuleIDInToken { get; set; }
        public string ClientIP { get; set; }
        public string RoleID { get; set; }
        public string SubModuleID { get; set; }
        public string RoleName { get; set; }
        public string SubModuleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThemeName { get; set; }
        public string TemplateName { get; set; }
        public string EmployeeServiceWebRootUrl { get; set; }
        public string LocationCode { get; set; }
        public string GroupLocation { get; set; }
        public string DepartmentCode { get; set; }
        public string SectionCode { get; set; }
        public string PositionByJob { get; set; }
    }
}


