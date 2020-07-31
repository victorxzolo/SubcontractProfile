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
        public FileStream Fileupload { get; set; }

    }
}
