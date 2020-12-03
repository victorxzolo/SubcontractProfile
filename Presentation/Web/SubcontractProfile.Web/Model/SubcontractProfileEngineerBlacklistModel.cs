using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileEngineerBlacklistModel
    {
        public int engineer_backlist_id { get; set; }
        public DateTime? backlist_date { get; set; }
        public string department_owner { get; set; }
        public string id_card { get; set; }
        public string engineer_name { get; set; }
        public string subcontract_name { get; set; }
        public string type_level { get; set; }
        public string description { get; set; }
        public string channel_ref { get; set; }
    }
}
