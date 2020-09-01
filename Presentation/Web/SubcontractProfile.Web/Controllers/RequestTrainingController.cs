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
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class RequestTrainingController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        public RequestTrainingController(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string companyName,  string TaxId , string RequestNo ,  string Status, string reqDateFrom ,string reqDateTo)
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

            //  Guid company_name_th ;
            //  Guid location_id;
            //  Guid team_id;
            //string company_name_th = string.Empty;
            //if (companyName == null || companyName == "-1")
            //{
            //    company_name_th = null;
            //}
            //else
            //{
            //    company_name_th = companyName;
            //}
            if (companyName == null || companyName == "")
            {
                companyName = "null";
            }
            //else
            //{
            //    company_name_th = Guid.Empty;
            //}




            string status = string.Empty;
            if ((Status == null) || (Status == "ALL"))
            {
                status = "null";
            }
            else
            {
                status = Status;
            }
            string date_from = string.Empty;
            if (reqDateFrom == null)
            {
                date_from = "null";
            }
            else
            {
                date_from = reqDateFrom;
            }
            string date_to = string.Empty;
            if (reqDateTo == null)
            {
                date_to = "null";
            }
            else
            {
                date_to = reqDateTo;
            }
            string tax_id = string.Empty;
            if (TaxId == null)
            {
                tax_id = "null";
            }
            else
            {
                tax_id = TaxId;
            }
            string request_no = string.Empty;
            if (RequestNo == null)
            {
                request_no = "null";
            }
            else
            {
                request_no = RequestNo;
            }
           




            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Training/SearchTrainingForApprove", companyName, tax_id, request_no, status, date_from,date_to);

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


        public ActionResult SearchEngineer(string Trainingid)
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

              Guid trainingid ;
            if (Trainingid == null || Trainingid == "-1")
            {
                trainingid = Guid.Empty;
            }
            else
            {
                trainingid = new Guid(Trainingid);
            }







            string uriString = string.Format("{0}/{1}", strpathAPI + "TrainingEngineer/GetTrainingEngineerByTrainingId", trainingid);

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


        [HttpPost]
        public IActionResult GetCompany(string companyname)
        {
            var output = new List<SubcontractProfileCompanyModel>();


            SubcontractProfileCompanyModel model = new SubcontractProfileCompanyModel();

            model.CompanyName = companyname;


            var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "SearchCompanyVerify"));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(uriCompany, httpContentCompany).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(result);

            }


            return Json(new { response = output });
        }


        public IActionResult onSaveTraining(SubcontractProfileTrainingModel model)
        {


            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            ResponseModel res = new ResponseModel();
            try { 
            if (model != null)
            {     
                    model.ModifiedBy = userProfile.Username.ToString();
                    model.bookingDate = Convert.ToDateTime(model._bookingDate);
                    model.Status = "A";
                    //model.Course = "0001";
                    //model.RequestDate = DateTime.Now;
                    //model.Remark = "TTT";
                    //model.TotalPrice = 0;
                    //model.Vat  = 0;
                    //model.Tax  = 0;

                    //model.RequestNo = "0001";
                    //model.ModifiedDate = DateTime.Now;
                    ////Path 
                    HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = new Uri(Path.Combine(strpathAPI, "Training", "UpdateByVerified"));

                //string str = JsonConvert.SerializeObject(Company);

                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(uri, httpContent).Result;


                //HttpResponseMessage response = client.PutAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    res.Status = true;
                    res.Message = "Success";
                    res.StatusError = "-1";
                }
                else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }
            }
            return Json(new { Response = res });
        }
            catch(Exception e)
            {
               
                res.Status = false;
                res.Message = e.Message.ToString();
                res.StatusError = "-1";
                return Json(new { Response = res });
            }
        }

        public JsonResult GetTraining(string TrainingID)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");


            Guid trainingId;

            if (TrainingID == null || TrainingID == "-1")
            {
                trainingId = Guid.Empty;
            }
            else
            {
                trainingId = new Guid(TrainingID);
            }


            var locationResult = new SubcontractProfileTrainingModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Training/GetByTrainingId", trainingId);

                HttpResponseMessage response = client.GetAsync(uriString).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    //data
                    locationResult = JsonConvert.DeserializeObject<SubcontractProfileTrainingModel>(result);

                }

                return Json(locationResult);
            }


        public JsonResult GetCompamy(string companyth)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            var result = new List<SubcontractProfileLocationModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var companyId = "NULL";
            var company_th = companyth;
            var company_en = "NULL";
            var company_alias = "NULL";
            var tax_id = "NULL";

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", strpathAPI + "Company/SearchCompany", companyId, company_th, company_en, company_alias, tax_id);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsync = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(resultAsync);

            }


            return Json(result);
        }

        public JsonResult GetLocation(string Companyid)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            var result = new List<SubcontractProfileLocationModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var companyId = Companyid;


            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetLocationByCompany", companyId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsync = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(resultAsync);

            }


            return Json(result);
        }
        public JsonResult GetTeam(string Companyid ,string locationid)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            string companyId = Companyid;

            var result = new List<SubcontractProfileTeamModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            Guid gLocationId;

            if (locationid == null || locationid == "-1")
            {
                gLocationId = Guid.Empty;
            }
            else
            {
                gLocationId = new Guid(locationid);
            }

            string uriString = string.Format("{0}/{1}/{2}", strpathAPI + "Team/GetByLocationId", companyId, gLocationId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(resultAsysc);

            }

            return Json(result);
        }


        [HttpPost]
        public IActionResult GetDataCourse()
        {
            var output = new List<SubcontractDropdownModel>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/training_couse");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
            }
            return Json(new { response = output });
        }

        public JsonResult GetDropDownList(string _ddlType)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

         

            var result = new List<SubcontractDropdownModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string ddlType = _ddlType;

            string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", ddlType);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(resultAsysc);

            }

            return Json(result);
        }
    }
}