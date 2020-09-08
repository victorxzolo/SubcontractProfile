﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Extension
{
    public class Common
    {

        public static DateTime ConvertToDateTime(string strDateTime)
        {
            DateTime dtFinaldate; string sDateTime;
            try { dtFinaldate = Convert.ToDateTime(strDateTime); }
            catch (Exception e)
            {
                string[] sDate = strDateTime.Split('/');
                sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                dtFinaldate = Convert.ToDateTime(sDateTime);
            }
            return dtFinaldate;
        }

        public static String ConvertToDateTimeYYYYMMDD(string strDateTime)
        {
            string sDateTime;
            string[] sDate = strDateTime.Split('/');
            sDateTime = sDate[2] + '-' + sDate[1] + '-' + sDate[0];
          
            return sDateTime;
        }

        public static String ConvertToDateTimeDDMMYYYY(string strDateTime)
        {
            string sDateTime;
            string[] sDate = strDateTime.Split('/');
            sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];

            return sDateTime;
        }
    }
}
