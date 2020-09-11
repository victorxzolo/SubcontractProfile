using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class SubcontractDropdownController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        private HttpClient client;

        public SubcontractDropdownController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
        }

        [HttpGet]
        public IActionResult GetDropDownList(string dropdownname)
        {
            var data = new List<SubcontractDropdownModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", dropdownname);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(dataresponse);
            }
            return Json(data);
        }
    }
}
