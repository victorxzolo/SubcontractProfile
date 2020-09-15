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
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
            if (userProfile == null)
            {
                return RedirectToAction("LogonByUser", "LogonByUser");
            }
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

        public ActionResult OnUpdate(SubcontractProfileTrainingRequestModel model)
        {
            ResponseModel result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();

            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");

                model.TestDateStr = Common.ConvertToDateTimeYYYYMMDD(model.TestDateStr);
                model.ModifiedBy = userProfile.Username;
                model.Status = "T";

                var uriLocation = new Uri(Path.Combine(strpathAPI, "Training", "UpdateTestResult"));

                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage responseResult = clientLocation.PutAsync(uriLocation, httpContent).Result;

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

            catch (Exception ex)
            {
                result.Message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อ Administrator.";
                result.Status = false;
                result.StatusError = "0";
            }
            return Json(result);
        }

        public ActionResult OnSave(SubcontractProfileTrainingEngineerModel model)
        {
            ResponseModel result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();

            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");

                model.UpdateBy = userProfile.Username;
                
                var uriLocation = new Uri(Path.Combine(strpathAPI, "TrainingEngineer", "UpdateByTestResult"));

                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage responseResult = clientLocation.PutAsync(uriLocation, httpContent).Result;

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
