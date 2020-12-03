using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{
    public class SubcontractProfileEngineerBlacklist : System.ICloneable
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
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
