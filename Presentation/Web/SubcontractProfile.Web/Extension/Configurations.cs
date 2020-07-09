using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Extension
{
    public static class Configurations
    {
        public static bool UseLDAP
        {
            get
            {
                var useLDAP = false;
                bool.TryParse(ConfigurationManager.AppSettings[WebConstants.AppSettingKeys.UseLDAP.ToString()], out useLDAP);
                return useLDAP;
            }
        }

        public static string ProjectCodeLdapFBB
        {
            get { return ConfigurationManager.AppSettings[WebConstants.AppSettingKeys.ProjectCodeLdapFBB.ToString()]; }
        }

        public static string dowloadReportPath
        {
            get { return ConfigurationManager.AppSettings[WebConstants.AppSettingKeys.dowloadReportPath.ToString()]; }
        }


    }
}
