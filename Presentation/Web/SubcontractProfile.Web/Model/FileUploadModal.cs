using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class FileUploadModal
    {
        public Guid file_id { get; set; }
        public byte[] Fileupload { get; set; }

        public string typefile { get; set; }
         public string ContentDisposition { get; set; }
        public string ContentType { get; set; }
        public string Filename { get; set; }

        public string CompanyId { get; set; }

    }
}
