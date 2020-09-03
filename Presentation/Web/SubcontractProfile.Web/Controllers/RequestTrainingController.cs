using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
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
                Result = JsonConvert.DeserializeObject<List<RequestTraininigModel>>(result);

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
                Result = JsonConvert.DeserializeObject<List<RequestTraininigModel>>(result);

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

        public ActionResult onSave(SubcontractProfileTrainingRequestModel model)
        {
            ResponseModel result = new ResponseModel();
            try
            {

                HttpClient clientRequest = new HttpClient();
                //    var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
                //RequestTraininigModel model = new RequestTraininigModel();
                //model.TrainingId = new Guid(trainingId);
                //model.CompanyId = new Guid(companyId);
                //model.TeamId = new Guid(teamId);
                //model.LocationId = new Guid(locationId);
                //model.EngineerId = new Guid(engineerId);
                //model.BookingDate = ConvertToDateTimeYYYYMMDD(bookingDate);
                //model.RemarkForAis = RemarkForAis;

                //  model.Status = "A";

                //var uriLocation = new Uri(Path.Combine(strpathAPI, "Training", "UpdateByVerified"));


                //clientLocation.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));
                //var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                //HttpResponseMessage responseResult = clientLocation.PutAsync(uriLocation, httpContent).Result;


                //if (responseResult.IsSuccessStatusCode)
                //{
                //    result.Status = true;
                //    result.Message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                //    result.StatusError = "0";
                //}
                //else
                //{
                //    result.Status = false;
                //    result.Message = "Data is not correct, Please Check Data or Contact System Admin";
                //    result.StatusError = "-1";
                //}


                var uri = new Uri(Path.Combine(strpathAPI, "Training", "UpdateByVerified"));

                clientRequest.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage responseResult = clientRequest.PutAsync(uri, httpContent).Result;

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
                result.Status = false;
                result.Message = "Fail"+ex.Message;
                result.StatusError = "-1";
               
            }
            return Json(result);
        }
        //public ActionResult onSaveTraining(SubcontractProfileTrainingModel model)
        //{

        //    ResponseModel result = new ResponseModel();
        //    var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
        // //   HttpClient clientLocation = new HttpClient();
         
        //    try {
                
        //    if (model != null)
        //    {


        //           // model.TrainingId = model.TrainingId;
        //           // model.CompanyId = model.CompanyId;
                 
        //            model.Status = "A";
                  
        //            model.ModifiedBy = userProfile.Username.ToString();
        //            if (model._bookingDate != "" || model._bookingDate != null)
        //            {
        //                model.BookingDate = ConvertToDateTimeYYYYMMDD(model._bookingDate);
        //            }
                    
        //            model.RemarkForAis = model.RemarkForAis;
        //            //model.Remark = "01";
        //            //model.Course = "01";
        //            //model.CourcePrice = 0;
        //            //model.RequestDate = DateTime.Now;
        //            //model.TotalPrice = 0;
        //            //model.Vat = 0;
        //            //model.Tax = 0;

        //            //model.RequestNo = "1231234";
        //            //model.CoursePrice = 0;

        //            HttpClient client = new HttpClient();
        //            client.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));

        //            var uri = new Uri(Path.Combine(strpathAPI, "Training", "UpdateByVerified"));

        //            //string str = JsonConvert.SerializeObject(Company);

        //            var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        //            HttpResponseMessage response = client.PutAsync(uri, httpContent).Result;


        //            //HttpResponseMessage response = client.PutAsync(uriString).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                result.Status = true;
        //                result.Message = "Success";
        //                result.StatusError = "1";
        //            }
        //            else
        //            {
        //                result.Status = false;
        //                result.Message = "Data is not correct, Please Check Data or Contact System Admin";
        //                result.StatusError = "-1";
        //            }
        //        }

        //    return Json(new { Response = result });
        //     }
        //    catch(Exception e)
        //    {

        //        result.Status = false;
        //        result.Message = e.Message.ToString();
        //        result.StatusError = "-1";
        //        return Json(new { Response = result });
        //    }
        //}

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


            var locationResult = new RequestTraininigModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Training/GetByTrainingId", trainingId);

                HttpResponseMessage response = client.GetAsync(uriString).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    //data
                    locationResult = JsonConvert.DeserializeObject<RequestTraininigModel>(result);

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