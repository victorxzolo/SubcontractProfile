using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.EntitySql;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class RequestTrainingController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";

        private readonly IStringLocalizer<RequestTrainingController> _localizer;
        public RequestTrainingController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IStringLocalizer<RequestTrainingController> localizer)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

        }
        public IActionResult Index()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
            if (userProfile == null)
            {
                return RedirectToAction("LogonByUser", "LogonByUser");
            }
            getsession();

                ViewData["Controller"] = _localizer["Training"];
                ViewData["View"] = _localizer["RequestTraining"];

            return View();
        }

        public IActionResult request()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var companyResult = GetCompanyDataById(userProfile.companyid);
            ViewBag.CompanyStatus = companyResult.Status;

            getsession();

                ViewData["Controller"] = _localizer["Training"];
                ViewData["View"] = _localizer["RequestTraining"];


            return View();
        }

        public  SubcontractProfileCompanyModel GetCompanyDataById(Guid companyId)
        {
            var companyResult = new SubcontractProfileCompanyModel();

            // Getting all company data  
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId"
                , HttpUtility.UrlEncode(companyId.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                companyResult = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);


            }

            return companyResult;
        }

        [HttpPost]
        public ActionResult Search(string companyName,  string TaxId , string RequestNo , 
            string Status, string reqDateFrom ,string reqDateTo, string bookingDateFrom, string bookingDateTo)
        {

            var Result = new List<RequestTraininigModel>();

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

       
            if (string.IsNullOrEmpty(companyName) )
            {
                companyName = "null";
            }
          

            if (string.IsNullOrEmpty(TaxId))
            {
                TaxId = "null";
            }
           

            string status = string.Empty;
            if ((Status == null) || (Status == "-1"))
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
                date_from = Common.ConvertToDateTimeYYYYMMDD(reqDateFrom);
            }

            string date_to = string.Empty;
            if (reqDateTo == null)
            {
                date_to = "null";
            }
            else
            {
                date_to = Common.ConvertToDateTimeYYYYMMDD(reqDateTo); 
            }

           
            if (string.IsNullOrEmpty(bookingDateFrom))
            {
                bookingDateFrom = "null";
            }
            else
            {
                bookingDateFrom = Common.ConvertToDateTimeYYYYMMDD(bookingDateFrom);
            }

          
            if (string.IsNullOrEmpty(bookingDateTo))
            {
                bookingDateTo = "null";
            }
            else
            {
                bookingDateTo = Common.ConvertToDateTimeYYYYMMDD(bookingDateTo);
            }


            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Training/SearchTrainingForApprove"
                , HttpUtility.UrlEncode(companyName, Encoding.UTF8)
                , HttpUtility.UrlEncode(TaxId, Encoding.UTF8)
                , HttpUtility.UrlEncode(status, Encoding.UTF8)
                , HttpUtility.UrlEncode(date_from, Encoding.UTF8)
                , HttpUtility.UrlEncode(date_to, Encoding.UTF8)
                , HttpUtility.UrlEncode(bookingDateFrom, Encoding.UTF8)
                , HttpUtility.UrlEncode(bookingDateTo, Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    //data
                    Result = JsonConvert.DeserializeObject<List<RequestTraininigModel>>(result);
                }
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

            var Result = new List<SubcontractProfileTrainingEngineerModel>();

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


            string uriString = string.Format("{0}/{1}", strpathAPI + "TrainingEngineer/GetTrainingEngineerByTrainingId"
                , HttpUtility.UrlEncode(trainingid.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileTrainingEngineerModel>>(result); //RequestTraininigModel

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

        public  String ConvertToDateTimeYYYYMMDD(string strDateTime)
        {
            string sDateTime;
            string[] sDate = strDateTime.Split('/');
            sDateTime = sDate[2] + '-' + sDate[1] + '-' + sDate[0];

            return sDateTime;
        }

        public ActionResult onSave(SubcontractProfileTrainingRequestModel model
            ,List<SubcontractProfileTrainingEngineerModel> engineerModel)
        {
            ResponseModel result = new ResponseModel();
            SubcontractProfileTrainingEngineerModel _engineerModel;
            try
            {

                HttpClient clientRequest = new HttpClient();
                 var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
               if (!string.IsNullOrEmpty(model.BookingDateStr)){
                    model.BookingDateStr = ConvertToDateTimeYYYYMMDD(model.BookingDateStr);
                }
       
                var uri = new Uri(Path.Combine(strpathAPI, "Training", "UpdateByVerified"));

                clientRequest.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage responseResult = clientRequest.PutAsync(uri, httpContent).Result;

               
                if (responseResult.IsSuccessStatusCode)
                {
                    //insert engineer
                    if (engineerModel != null)
                    {
                        if (engineerModel.Count > 0)
                        {
                            HttpClient client = new HttpClient();
                            foreach (var engineer in engineerModel)
                            {
                                //_engineerModel = new SubcontractProfileTrainingEngineerModel();
                                //_engineerModel.TrainingEngineerId = engineer.TrainingEngineerId;
                                //_engineerModel.TrainingId = engineer.TrainingId;
                                //_engineerModel.EngineerId = engineer.EngineerId;
                                //_engineerModel.UpdateBy = userProfile.Username;
                                //_engineerModel.TestStatus = "N";
                                //_engineerModel.CoursePrice = engineer.CoursePrice;
                                engineer.UpdateBy = userProfile.Username;
                                engineer.TestStatus = "N";

                                var uriengineer = new Uri(Path.Combine(strpathAPI, "TrainingEngineer", "Update"));

                                client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                                var httpContenten = new StringContent(JsonConvert.SerializeObject(engineer), Encoding.UTF8, "application/json");
                                HttpResponseMessage responseEn = client.PutAsync(uriengineer, httpContenten).Result;

                                if (responseEn.IsSuccessStatusCode)
                                {

                                }

                            }
                        }
                    }

                    result.Status = true;
                    result.Message = _localizer["MessageSaveSuccess"];
                    result.StatusError = "0";
                }
                else
                {
                    result.Status = false;
                    result.Message = _localizer["MessageUnSuccess"];
                    result.StatusError = "-1";
                }


            }
            catch (Exception ex) 
            {
                result.Status = false;
                result.Message = "Fail"+ex.Message;
                result.StatusError = "-1";
               
            }
            return Json(result);
        }


        public ActionResult onNotApprove(SubcontractProfileTrainingRequestModel model
          , List<SubcontractProfileTrainingEngineerModel> engineerModel)
        {
            ResponseModel result = new ResponseModel();
            SubcontractProfileTrainingEngineerModel _engineerModel;
            try
            {

                HttpClient clientRequest = new HttpClient();
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
                if (!string.IsNullOrEmpty(model.BookingDateStr))
                {
                    model.BookingDateStr = ConvertToDateTimeYYYYMMDD(model.BookingDateStr);
                }

                var uri = new Uri(Path.Combine(strpathAPI, "Training", "UpdateByVerified"));

                clientRequest.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage responseResult = clientRequest.PutAsync(uri, httpContent).Result;


                if (responseResult.IsSuccessStatusCode)
                {
                    //insert engineer
                    if (engineerModel != null)
                    {
                        if (engineerModel.Count > 0)
                        {
                            HttpClient client = new HttpClient();
                            foreach (var engineer in engineerModel)
                            {
                                //_engineerModel = new SubcontractProfileTrainingEngineerModel();
                                //_engineerModel.TrainingEngineerId = engineer.TrainingEngineerId;
                                //_engineerModel.TrainingId = engineer.TrainingId;
                                //_engineerModel.EngineerId = engineer.EngineerId;
                                //_engineerModel.UpdateBy = userProfile.Username;
                                //_engineerModel.TestStatus = "N";
                                //_engineerModel.CoursePrice = engineer.CoursePrice;
                                engineer.UpdateBy = userProfile.Username;
                                engineer.TestStatus = "N";

                                var uriengineer = new Uri(Path.Combine(strpathAPI, "TrainingEngineer", "Update"));

                                client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                                var httpContenten = new StringContent(JsonConvert.SerializeObject(engineer), Encoding.UTF8, "application/json");
                                HttpResponseMessage responseEn = client.PutAsync(uriengineer, httpContenten).Result;

                                if (responseEn.IsSuccessStatusCode)
                                {

                                }

                            }
                        }
                    }

                    result.Status = true;
                    result.Message = _localizer["MessageSaveSuccess"];
                    result.StatusError = "0";
                }
                else
                {
                    result.Status = false;
                    result.Message = _localizer["MessageUnSuccess"];
                    result.StatusError = "-1";
                }


            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Fail" + ex.Message;
                result.StatusError = "-1";

            }
            return Json(result);
        }


        public JsonResult GetTraining(string TrainingID)
        {
            var data = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "EngineerData");

            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    data.Clear();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "EngineerData", data);
                }
            }

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


               var resultModel = new SubcontractProfileTrainingModel();

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Training/GetByTrainingId"
                    , HttpUtility.UrlEncode(trainingId.ToString(), Encoding.UTF8));

                HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                resultModel = JsonConvert.DeserializeObject<SubcontractProfileTrainingModel>(result);


            }

            return Json(resultModel);
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

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", strpathAPI + "Company/SearchCompany"
                , HttpUtility.UrlEncode(companyId, Encoding.UTF8)
                , HttpUtility.UrlEncode(company_th, Encoding.UTF8)
                , HttpUtility.UrlEncode(company_en, Encoding.UTF8)
                , HttpUtility.UrlEncode(company_alias, Encoding.UTF8)
                , HttpUtility.UrlEncode(tax_id, Encoding.UTF8));

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


            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetLocationByCompany"
                , HttpUtility.UrlEncode(companyId, Encoding.UTF8));

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

            string uriString = string.Format("{0}/{1}/{2}", strpathAPI + "Team/GetByLocationId"
                , HttpUtility.UrlEncode(companyId, Encoding.UTF8)
                , HttpUtility.UrlEncode(gLocationId.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(resultAsysc);

            }

            return Json(result);
        }


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

            string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName"
                , HttpUtility.UrlEncode(ddlType, Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                if (resultAsysc != null)
                {
                    //data
                    result = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(resultAsysc);
                }
            }

            return Json(result);
        }


        #region Request Training
        [HttpPost]
         public ActionResult SearchRequest(
            string Status, string reqDateFrom, string reqDateTo)
        {

            var Result = new List<RequestTraininigModel>();

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

            var company_id = userProfile.companyid;
            
            string status = string.Empty;
            if ((Status == null) || (Status == "-1"))
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
                date_from = Common.ConvertToDateTimeYYYYMMDD(reqDateFrom);
            }

            string date_to = string.Empty;
            if (reqDateTo == null)
            {
                date_to = "null";
            }
            else
            {
                date_to = Common.ConvertToDateTimeYYYYMMDD(reqDateTo);
            }


            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}", strpathAPI + "Training/SearchTraining"
                , HttpUtility.UrlEncode(company_id.ToString(), Encoding.UTF8)
                , HttpUtility.UrlEncode(status, Encoding.UTF8)
                , HttpUtility.UrlEncode(date_from, Encoding.UTF8)
                , HttpUtility.UrlEncode(date_to, Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    //data
                    Result = JsonConvert.DeserializeObject<List<RequestTraininigModel>>(result);
                }
            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        public JsonResult AddEngineer(SubcontractProfileTrainingEngineerModel model)
        {
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
            var resultEngineer = new List<SubcontractProfileTrainingEngineerModel>();
            resultEngineer = AddDataTable(model);

              //if (model.LocationId != null)
              //{
              //    resultEngineer.Add(model);
              //}

              recordsTotal = resultEngineer.Count();

            //Paging   
            var data = resultEngineer.Skip(skip).Take(pageSize).ToList();
  
            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });

        }

        private List<SubcontractProfileTrainingEngineerModel> AddDataTable(SubcontractProfileTrainingEngineerModel model)
        {
            DataTable dt = new DataTable("engineer");
            dt.Columns.Add("LocationId", typeof(string));
            dt.Columns.Add("TeamId", typeof(string));
            dt.Columns.Add("EngineerId", typeof(string));
            dt.Columns.Add("LocationNameTh", typeof(string));
            dt.Columns.Add("TeamNameTh", typeof(string));
            dt.Columns.Add("StaffNameTh", typeof(string));

             var data = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "EngineerData");
            
            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    dt = data;
                    DataRow row = dt.NewRow();
                    row["LocationId"] = model.LocationId;
                    row["TeamId"] = model.TeamId;
                    row["EngineerId"] = model.EngineerId;
                    row["LocationNameTh"] = model.LocationNameTh;
                    row["TeamNameTh"] = model.TeamNameTh;
                    row["StaffNameTh"] = model.StaffNameTh;
                    dt.Rows.Add(row);

                    dt = dt.DefaultView.ToTable( /*distinct*/ true);
                }
                else
                {
                    //Data  
                    dt.Rows.Add(model.LocationId, model.TeamId, model.EngineerId, model.LocationNameTh, model.TeamNameTh, model.StaffNameTh);

                }
            }
            else
            {
                //Data  
                dt.Rows.Add(model.LocationId, model.TeamId, model.EngineerId, model.LocationNameTh, model.TeamNameTh, model.StaffNameTh);

            }

            var resultEngineer = new List<SubcontractProfileTrainingEngineerModel>();

            resultEngineer = (from DataRow dr in dt.Rows
                              select new SubcontractProfileTrainingEngineerModel()
                              {
                                  LocationId = Guid.Parse(dr["LocationId"].ToString()),
                                  TeamId = Guid.Parse(dr["TeamId"].ToString()),
                                  EngineerId = Guid.Parse(dr["EngineerId"].ToString()),
                                  LocationNameTh = dr["LocationNameTh"].ToString(),
                                  TeamNameTh = dr["TeamNameTh"].ToString(),
                                  StaffNameTh = dr["StaffNameTh"].ToString()
                              }).ToList();


           
            SessionHelper.SetObjectAsJson(HttpContext.Session, "EngineerData", dt);

            return resultEngineer;
        }


        public JsonResult GetTrainingAdd()
        {
            // SessionHelper.RemoveSession(HttpContext.Session, "EngineerData");
            var data = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "EngineerData");
         
            if(data !=null)
            {
                if (data.Rows.Count > 0)
                {
                    data.Clear();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "EngineerData", data);
                }
            }

            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            Guid companyID = userProfile.companyid;


            var companyResult = new SubcontractProfileCompanyModel();

            // Getting all company data  
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId"
                , HttpUtility.UrlEncode(companyID.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                companyResult = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);


            }



            return Json(companyResult);
        }


        public ActionResult onSaveRequestTraining(SubcontractProfileTrainingModel model)
        {
            HttpClient client = new HttpClient();
            ResponseModel result = new ResponseModel();
            SubcontractProfileTrainingEngineerModel engineerModel;

            try
            {
                var data = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "EngineerData");
                if (data == null || data.Rows.Count == 0)
                {
                    result.Status = false;
                    result.Message = _localizer["MessageAddTrainee"];
                    result.StatusError = "-1";

                    return Json(result);
                }

                HttpClient clientRequest = new HttpClient();
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                if (model.TrainingId == Guid.Empty)
                {

                    model.RequestDateStr = ConvertToDateTimeYYYYMMDD(model.RequestDateStr);
                    model.RequestDateStrTo = ConvertToDateTimeYYYYMMDD(model.RequestDateStrTo);
                    model.CompanyId = userProfile.companyid;
                    model.CreateBy = userProfile.Username;
                    model.TrainingId = Guid.NewGuid();

                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Training", "Insert"));

                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(uriLocation, httpContent).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        //insert engineer

                        if (data != null)
                        {
                            if (data.Rows.Count > 0)
                            {

                                foreach (DataRow dr in data.Rows)
                                {
                                    engineerModel = new SubcontractProfileTrainingEngineerModel();
                                    engineerModel.TrainingEngineerId = Guid.NewGuid();
                                    engineerModel.TrainingId = model.TrainingId;
       
                                    engineerModel.EngineerId = Guid.Parse(dr["EngineerId"].ToString());
                                    engineerModel.CreateBy = userProfile.Username;
                                    engineerModel.CoursePrice = model.CoursePrice;

                                    var uriengineer = new Uri(Path.Combine(strpathAPI, "TrainingEngineer", "Insert"));

                                    client.DefaultRequestHeaders.Accept.Add(
                                    new MediaTypeWithQualityHeaderValue("application/json"));

                                    var httpContenten = new StringContent(JsonConvert.SerializeObject(engineerModel), Encoding.UTF8, "application/json");
                                    HttpResponseMessage responseEn = client.PostAsync(uriengineer, httpContenten).Result;

                                    if (responseEn.IsSuccessStatusCode)
                                    {

                                    }

                                }
                            }
                        }

                        result.Status = true;
                        result.Message = _localizer["MessageSaveSuccess"];
                        result.StatusError = "0";
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = _localizer["MessageUnSuccess"];
                        result.StatusError = "-1";
                    }

                }
                else //edit
                {
                    var uri = new Uri(Path.Combine(strpathAPI, "Training", "Update"));

                    model.RequestDateStr = ConvertToDateTimeYYYYMMDD(model.RequestDateStr);
                    model.RequestDateStrTo = ConvertToDateTimeYYYYMMDD(model.RequestDateStrTo);
                    model.ModifiedBy = userProfile.Username;
                    model.Status = "Y";

                    clientRequest.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseResult = clientRequest.PutAsync(uri, httpContent).Result;

                    if (responseResult.IsSuccessStatusCode)
                    {
                        //delete
                        string uriString = string.Format("{0}/{1}", strpathAPI + "TrainingEngineer/DeleteByTriningId", model.TrainingId);

                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage responseResultDel = client.DeleteAsync(uriString).Result;
                        if (responseResult.IsSuccessStatusCode)
                        {
                            result.Message = _localizer["MessageDeleteSuccess"];
                            result.Status = true;
                            result.StatusError = "0";
                        }

                        //insert engineer
                        if (data != null)
                        {
                            if (data.Rows.Count > 0)
                            {

                                foreach (DataRow dr in data.Rows)
                                {
                                    engineerModel = new SubcontractProfileTrainingEngineerModel();
                                    engineerModel.TrainingEngineerId = Guid.NewGuid();
                                    engineerModel.TrainingId = model.TrainingId;
                                    //engineerModel.LocationId = Guid.Parse(dr["LocationId"].ToString());
                                    //engineerModel.TeamId = Guid.Parse(dr["TeamId"].ToString());
                                    engineerModel.EngineerId = Guid.Parse(dr["EngineerId"].ToString());
                                    engineerModel.CreateBy = userProfile.Username;
                                    engineerModel.CoursePrice = model.CoursePrice;

                                    var uriengineer = new Uri(Path.Combine(strpathAPI, "TrainingEngineer", "Insert"));

                                    //string rr = JsonConvert.SerializeObject(engineerModel);

                                    client.DefaultRequestHeaders.Accept.Add(
                                    new MediaTypeWithQualityHeaderValue("application/json"));
                                    var httpContenten = new StringContent(JsonConvert.SerializeObject(engineerModel), Encoding.UTF8, "application/json");
                                    HttpResponseMessage responseEn = client.PostAsync(uriengineer, httpContenten).Result;

                                    if (responseEn.IsSuccessStatusCode)
                                    {

                                    }

                                }
                            }
                        }

                        result.Status = true;
                        result.Message = _localizer["MessageSaveSuccess"];
                        result.StatusError = "0";
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = _localizer["MessageUnSuccess"];
                        result.StatusError = "-1";
                    }
                }


            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Fail" + ex.Message;
                result.StatusError = "-1";

            }
            return Json(result);
        }

        public JsonResult OnDelete(Guid trainingId)
        {
            var result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();
            try
            {

                string uriString = string.Format("{0}/{1}", strpathAPI + "Training/Delete", trainingId);
            

                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseResult = clientLocation.DeleteAsync(uriString).Result;
                if (responseResult.IsSuccessStatusCode)
                {
                    result.Message = _localizer["MessageDeleteSuccess"];
                    result.Status = true;
                    result.StatusError = "0";
                }
                else
                {
                    result.Status = false;
                    result.Message = _localizer["MessageDeleteUnSuccess"];
                    result.StatusError = "-1";
                }

            }
            catch (Exception ex)
            {
                result.Message = _localizer["MessageDeleteUnSuccess"];
                result.StatusError = "-1";
            }
            return Json(result);
        }


        public JsonResult OnDeleteEngineer(string locationId, string teamId, string engineerId)
        {

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

            ResponseModel result = new ResponseModel();
            var datatable = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "EngineerData");

            if (datatable != null)
            {
                for (int i = datatable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = datatable.Rows[i];
                    if (dr["LocationId"].ToString().ToUpper() == locationId.ToUpper()
                       && dr["TeamId"].ToString().ToUpper() == teamId.ToUpper()
                       && dr["EngineerId"].ToString().ToUpper() == engineerId.ToUpper())
                    {
                        dr.Delete();

                    }
                }
            }

            var resultEngineer = new List<SubcontractProfileTrainingEngineerModel>();

            resultEngineer = (from DataRow dr in datatable.Rows
                              select new SubcontractProfileTrainingEngineerModel()
                              {
                                  LocationId = Guid.Parse(dr["LocationId"].ToString()),
                                  TeamId = Guid.Parse(dr["TeamId"].ToString()),
                                  EngineerId = Guid.Parse(dr["EngineerId"].ToString()),
                                  LocationNameTh = dr["LocationNameTh"].ToString(),
                                  TeamNameTh = dr["TeamNameTh"].ToString(),
                                  StaffNameTh = dr["StaffNameTh"].ToString()
                              }).ToList();



            datatable.AcceptChanges();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "EngineerData", datatable);

            result.Status = true;
            result.Message = _localizer["MessageDeleteSuccess"];

            recordsTotal = resultEngineer.Count();

            //Paging   
            var data = resultEngineer.Skip(skip).Take(pageSize).ToList();

            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });

        }

        public ActionResult SearchEngineerRequest(string Trainingid)
        {

            var Result = new List<SubcontractProfileTrainingEngineerModel>();

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

            Guid trainingid;
            if (Trainingid == null || Trainingid == "-1")
            {
                trainingid = Guid.Empty;
            }
            else
            {
                trainingid = new Guid(Trainingid);
            }


            string uriString = string.Format("{0}/{1}", strpathAPI + "TrainingEngineer/GetTrainingEngineerByTrainingId"
                , HttpUtility.UrlEncode(trainingid.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileTrainingEngineerModel>>(result);

            }

            var resultEngineer = new List<SubcontractProfileTrainingEngineerModel>();
            var trainingEn = new  SubcontractProfileTrainingEngineerModel();
            for (var i = 0; i < Result.Count; i++)
            {
                trainingEn.TrainingId = Result[i].TrainingId;
                trainingEn.LocationId = Result[i].LocationId;
                trainingEn.TeamId = Result[i].TeamId;
                trainingEn.EngineerId = Result[i].EngineerId;
                trainingEn.TrainingEngineerId = Result[i].TrainingEngineerId;
                trainingEn.LocationNameTh = Result[i].LocationNameTh;
                trainingEn.TeamNameTh = Result[i].TeamNameTh;
                trainingEn.StaffNameTh = Result[i].StaffNameTh;

                resultEngineer = AddDataTable(trainingEn);
            }
           
          

            //total number of rows count   
            recordsTotal = resultEngineer.Count();

            //Paging   
            var data = resultEngineer.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }


        #endregion

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
           // datauser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }
    }
}