using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{
    public class SubcontractProfileRequestStatus_PK
    {

        public System.Guid request_status_id { get; set; }

    }
    public class SubcontractProfileRequestStatus : System.ICloneable
    {
        public System.Guid request_status_id { get; set; }
        public string request_status { get; set; }
        public string is_active { get; set; }
        public int order_seq { get; set; }
        public string create_by { get; set; }
        public System.DateTime? create_date { get; set; }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
