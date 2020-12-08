using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{
   public  class SubcontractProfileFile
    {
        public System.Guid file_id { get; set; }
        public string upload_type { get; set; }
        public System.Guid payment_id { get; set; }
        public System.Guid company_id { get; set; }
        public string file_Name { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime? CreateDate { get; set; }
    }
}
