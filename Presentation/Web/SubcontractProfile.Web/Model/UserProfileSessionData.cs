using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    [Serializable]
    public class UserProfileSessionData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string FullName { get; set; }
    }
}
