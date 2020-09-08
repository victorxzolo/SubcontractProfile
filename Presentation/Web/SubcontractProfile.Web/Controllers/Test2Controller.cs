using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SubcontractProfile.Web.Model;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Immutable;
using DataTables.AspNetCore.Mvc;
using SubcontractProfile.Web.Extension;
using DataTables.AspNetCore.Mvc.Binder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Security.Principal;

namespace SubcontractProfile.Web.Controllers
{

    public class Test2Controller : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;

        public Test2Controller(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            //Lang = "TH";
            //strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();
        }


        [HttpPost]
        public ActionResult SaveComments(int id, string comments)
        {
            //var actions = new Actions(User.Identity.Name);
            var status = comments;
            return Content(status);
        }


        public IActionResult Login()
        {
            // this.Session.SetString("TransId", "x001");
            return View();
        }

        public IActionResult Index()
        {

            testUser();
                return View();
        }

     public string testUser()
        {
            //using (WindowsIdentity user = WindowsIdentity.GetCurrent())
            //using (Impersonator wi = new Impersonator("nas_fixedbb", "Fixe1012@Ais", "10.138.47.98", false))
            //{
            //    WindowsIdentity.RunImpersonated(wi.Identity.AccessToken, () =>
            //    {
            //        WindowsIdentity useri = WindowsIdentity.GetCurrent();
            //        System.Console.WriteLine(useri.Name);
            //    }

            // );
            try
            {
                using (new Impersonator("Administrator", "P@ssw0rd", "10.104.240.92", false))
                {

                }
            }
            catch(Exception ex)
            {

            }
           
            return "";
        }

        [HttpPost]
        public JsonResult searchCompany(string asc_code)
        {
            var companyname_th = asc_code;
            return Json(companyname_th);
        }

        //[HttpPost]
        //public JsonResult AjaxPostCall(Employee employeeData)
        //{
        //    Employee employee = new Employee
        //    {
        //        Name = employeeData.Name,
        //        Designation = employeeData.Designation,
        //        Location = employeeData.Location
        //    };
        //    return Json(employee);
        //}

   
        public ActionResult LoadData([DataTablesRequest] DataTablesRequest dataRequest)
        {
            try
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

                string uriString = string.Format("{0}", strpathAPI + "Company/GetAll");
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

                //  var sa = new JsonSerializerSettings();
                //var countries = new List<SubcontractProfileCompanySearchModel>
                //{
                //    new SubcontractProfileCompanySearchModel {CompanyNameTh = "US", CompanyCode = "United States"},
                //    new SubcontractProfileCompanySearchModel {CompanyNameTh = "CA", CompanyCode = "Canada"}
                //};

       
                // Returning Json Data
                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });

                //return Json(data
                //  .Select(e => new
                //  {
                //      CompanyId = e.CompanyId,
                //      CompanyNameTh = e.CompanyNameTh,
                //      CompanyNameEn = e.CompanyNameEn,   
                //      TaxId = e.TaxId,

                //  })
                //  .ToDataTablesResponse(dataRequest, recordsTotal, recordsTotal));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

 
}
