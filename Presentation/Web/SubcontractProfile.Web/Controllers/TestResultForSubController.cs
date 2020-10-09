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
using Microsoft.Win32.SafeHandles;
using Microsoft.Extensions.Localization;
using System.Web;

namespace SubcontractProfile.Web.Controllers
{
    public class TestResultForSubController : Controller
    {

        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        CultureInfo culture = new CultureInfo("en-US");
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";
        private readonly IStringLocalizer<TestResultForSubController> _localizer;
        public TestResultForSubController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IStringLocalizer<TestResultForSubController> localizer)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

        }

        public IActionResult TestResultForSub()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }
            getsession();

                ViewData["Controller"] = _localizer["Training"];
                ViewData["View"] = _localizer["TestResult"];


            return View();
        }

        public ActionResult SearchForSub(
        string training_date_fr, string training_date_to, string test_date_fr, string test_date_to, string status)
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

            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");


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

            if (status == "-1")
            {
                status = "null";
            }



            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Training/SearchTrainingForSub",
                HttpUtility.UrlEncode(userProfile.companyid.ToString(), Encoding.UTF8)
                , HttpUtility.UrlEncode(training_date_fr, Encoding.UTF8)
                , HttpUtility.UrlEncode(training_date_to, Encoding.UTF8)
                , HttpUtility.UrlEncode(test_date_fr, Encoding.UTF8)
                , HttpUtility.UrlEncode(test_date_to, Encoding.UTF8)
                , HttpUtility.UrlEncode(status, Encoding.UTF8));

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

        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if (Lang == null || Lang == "")
            {
                Lang = "TH";
            }
            // datauser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }

    }
}
