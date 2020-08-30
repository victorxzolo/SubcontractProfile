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
    public class ActivateProfileController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;

        public ActivateProfileController(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            //Lang = "TH";
            //strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();

            // HttpContext.Session.SetString("username", username);
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string subcontract_profile_type, string company_name_th, string tax_id
           , string activate_date_fr, string activate_date_to, string activate_status)
        {

            var Result = new List<SubcontractProfileCompanyModel>();

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


            if (subcontract_profile_type.ToUpper() == "ALL")
            {
                subcontract_profile_type = "null";
            }

            if (company_name_th == null)
            {
                company_name_th = "null";
            }

            if (tax_id == null)
            {
                tax_id = "null";
            }

            if (activate_date_fr == null)
            {
                activate_date_fr = "null";
            }

            if (activate_date_to == null)
            {
                activate_date_to = "null";
            }

            if (activate_status == null)
            {
                activate_status = "null";
            }

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Company/SearchActivateProfile",
                subcontract_profile_type, company_name_th, tax_id, activate_date_fr, activate_date_to, activate_status);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        public ActionResult OnSave(SubcontractProfileCompanyModel model)
        {
            ResponseModel result = new ResponseModel();
            HttpClient clientCompany= new HttpClient();

            try
            {
              //  var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                //insert
                if (model.CompanyId != Guid.Empty)
                {
                    model.UpdateBy = "Admin";
                    model.Status = "A";

                    var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "UpdateByActivate"));

                    clientCompany.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseResult = clientCompany.PutAsync(uriCompany, httpContent).Result;

                    if (responseResult.IsSuccessStatusCode)
                    {
                        result.Status = true;
                        result.Message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                        result.StatusError = "0";
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "Data is not correct, Please Check Data or Contact System Admin";
                        result.StatusError = "-1";
                    }
                }
            
            }

            catch (Exception ex)
            {
                result.Message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อ Administrator.";
                result.Status = false;
                result.StatusError = "0";
            }
            return Json(result);
        }

    }
}
