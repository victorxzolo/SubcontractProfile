using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string StatusError { get; set; }
    }
}
