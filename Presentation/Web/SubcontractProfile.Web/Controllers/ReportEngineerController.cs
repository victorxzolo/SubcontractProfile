using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class ReportEngineerController : Controller
    {

        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        private const int MegaBytes = 1024 * 1024;
        private const int TMegaBytes = 3 * 1024 * 1024;
        private readonly string strpathUpload;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";
        private readonly IStringLocalizer<ReportEngineerController> _localizer;
        public ReportEngineerController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IStringLocalizer<ReportEngineerController> localizer)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();

        }
        public IActionResult Index()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
            if (userProfile == null)
            {
                return RedirectToAction("LogonByUser", "LogonByUser");
            }

            getsession();
            ViewData["Controller"] = _localizer["Index"];
            ViewData["View"] = _localizer["ReportEngineerController"];


            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if (Lang == null || Lang == "")
            {
                Lang = "TH";
            }
            //dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }
        public IActionResult SearchEngineer(string citizen_id, string staff_name, string contact_phone, string createFrom, string createTo)
        {
            var Result = new List<SubcontractProfileEngineerModel>();

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


            if (string.IsNullOrEmpty(citizen_id))
            {
                citizen_id = "null";
            }


            if (string.IsNullOrEmpty(staff_name))
            {
                staff_name = "null";
            }

            if (string.IsNullOrEmpty(contact_phone))
            {
                contact_phone = "null";
            }



            if (string.IsNullOrEmpty(createFrom))
            {
                createFrom = "null";
            }
            else
            {
                createFrom = Common.ConvertToDateTimeYYYYMMDD(createFrom);
            }


            if (string.IsNullOrEmpty(createTo))
            {
                createTo = "null";
            }
            else
            {
                createTo = Common.ConvertToDateTimeYYYYMMDD(createTo);
            }


            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", strpathAPI + "Engineer/selectEngineer"
                , HttpUtility.UrlEncode(citizen_id, Encoding.UTF8)
                , HttpUtility.UrlEncode(staff_name, Encoding.UTF8)
                , HttpUtility.UrlEncode(contact_phone, Encoding.UTF8)
                , HttpUtility.UrlEncode(createFrom, Encoding.UTF8)
                , HttpUtility.UrlEncode(createTo, Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    //data
                    Result = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(result);
                }
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