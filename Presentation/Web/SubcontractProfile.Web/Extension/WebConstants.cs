﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Extension
{
    public static class WebConstants
    {
        public enum AppSettingKeys
        {
            UseLDAP,
            ProjectCodeLdapFBB,
            dowloadReportPath,

        }

        public static class FBBConfigSessionKeys
        {
            public const string User = "User";
            public const string AvoidSessionIDChangesIssue = "__Init";
            public const string AllLov = "AllLov";
            public const string CurrentUICulture = "CurrentUICulture";
            public const string ZipCodeData = "ZipCodeData";
        }

        public static class LovConfigName
        {
            public const string CoverageType = "COVERAGETYPE";
            public const string CoverageStatus = "COVERAGESTATUS";
            public const string RegionCode = "REGION_CODE";
            public const string PortUtilization = "PORT_UTILIZATION";
            public const string FbbConstant = "FBB_CONSTANT";
            public const string DormConstants = "FBBDORM_ADMIN_VALIDATE";
            public const string CardType = "ID_CARD_TYPE";
            public const string CustomerRegisterPageCode = "FBBOR003";

            public const string DisplayPackagePageCode = "FBBOR002";
            public const string CoveragePageCode = "FBBOR001";
            public const string Vas_Package = "FBBORV10";
            public const string FbbExceptionIdCard = "FBB_EXCEPTION_ID_CARD";
        }
    }
}
