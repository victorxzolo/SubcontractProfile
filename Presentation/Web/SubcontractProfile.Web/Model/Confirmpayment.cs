using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class Confirmpayment
    {
        public string Bank { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Balance { get; set; }
        public string SourceBank { get; set; }
        public string branch { get; set; }
        public IFormFile credit { get; set; }
        public string Note { get; set; }

    }
}
