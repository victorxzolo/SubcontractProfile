using System;
using System.Collections.Generic;
using System.Globalization;
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
using SubcontractProfile.Web.Extension;
using Microsoft.Win32.SafeHandles;

namespace SubcontractProfile.Web.Controllers
{
    public class TestResultController : Controller
    {

        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        CultureInfo culture = new CultureInfo("en-US");
        public TestResultController(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
           
        }

        public IActionResult TestResultUpdate()
        {
            return View();
        }

        public ActionResult Search( string company_name_th, string tax_id
           , string training_date_fr, string training_date_to, string test_date_fr, string test_date_to)
        {

            var Result = new List<SubcontractProfileTrainingModel>();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            // Getting all company data  
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //if (subcontract_profile_type.ToUpper() == "ALL")
            //{
            //    subcontract_profile_type = "null";
            //}

            if (company_name_th == null)
            {
                company_name_th = "null";
            }

            if (tax_id == null)
            {
                tax_id = "null";
            }

            if (training_date_fr == null)
            {
                training_date_fr = "null";
            }
            else
            {
                training_date_fr = Common.ConvertToDateTimeYYYYMMDD(training_date_fr); 
            }

            if (training_date_to == null)
            {
                training_date_to = "null";
            }
            else
            {
                training_date_to = Common.ConvertToDateTimeYYYYMMDD(training_date_to);
            }

            if (test_date_fr == null)
            {
                test_date_fr = "null";
            }
            else
            {
                test_date_fr = Common.ConvertToDateTimeYYYYMMDD(test_date_fr);
            }

            if (test_date_to == null)
            {
                test_date_to = "null";
            }
            else
            {
                test_date_to = Common.ConvertToDateTimeYYYYMMDD(test_date_to);
            }

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Training/SearchTrainingForTest",
                 company_name_th, tax_id, training_date_fr, training_date_to, test_date_fr, test_date_to);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileTrainingModel>>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

    }
}
