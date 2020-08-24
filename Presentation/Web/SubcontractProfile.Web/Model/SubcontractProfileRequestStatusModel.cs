using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileRequestStatusModel
    {
        public System.Guid request_status_id { get; set; }
        public string request_status { get; set; }
        public string is_active { get; set; }
        public int order_seq { get; set; }
        public string create_by { get; set; }
        public System.DateTime? create_date { get; set; }
    }
}
