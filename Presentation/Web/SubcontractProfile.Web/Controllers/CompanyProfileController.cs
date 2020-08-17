using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class CompanyProfileController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;

        public CompanyProfileController(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            //Lang = "TH";
            //strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();
        }

        // GET: CompanyProfileController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyProfileController/Search/5
        public ActionResult Search(string companyNameTh, string companyNameEn, string companyAilas
            ,string taxId)
        {
            var companyResult = new List<SubcontractProfileCompanyModel>();

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

            ///{subcontract_profile_type}/{location_code}/{vendor_code}/{company_th}/{company_en}/{company_alias}/{company_code}/{distibution_channel}/{channel_sale_group}

            //    System.Guid strCompanyId = new Guid("3A37A238-1CDF-4AF8-980F-00C86822F903");
            string strCompanyId = "3A37A238-1CDF-4AF8-980F-00C86822F903";


            if (companyNameTh ==null)
            {
                companyNameTh = "null";
            }

            if (companyNameEn == null)
            {
                companyNameEn = "null";
            }

            if (companyAilas == null)
            {
                companyAilas = "null";
            }

            if (taxId == null)
            {
                taxId = "null";
            }

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", strpathAPI + "Company/SearchCompany", strCompanyId
                , companyNameTh, companyNameEn, companyAilas, taxId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                companyResult = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(result);

            }


            //total number of rows count   
            recordsTotal = companyResult.Count();

            //Paging   
            var data = companyResult.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }


        // GET: CompanyProfileController/GetDataById/5
        public JsonResult GetDataById(string companyId)
        {
            var companyResult = new SubcontractProfileCompanyModel();
   
            // Getting all company data  
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", companyId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                companyResult = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);

            }

            return Json(companyResult);
        }

        // GET: CompanyProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

      
        public ActionResult OnSave(SubcontractProfileCompanyModel mpdel)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
