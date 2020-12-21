using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileSendMailModel
    {
        public string ProcessName { get; set; }
        public string CreateUser { get; set; }
        public string SendTo { get; set; }
        public string SendCC { get; set; }
        public string SendBCC { get; set; }
        public string SendFrom { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromPassword { get; set; }
        public string Port { get; set; }
        public string Domaim { get; set; }
        public string IPMailServer { get; set; }

        public string ReturnMessage { get; set; }
        public string IP_MAIL_SERVER { get; set; }
    }
}
