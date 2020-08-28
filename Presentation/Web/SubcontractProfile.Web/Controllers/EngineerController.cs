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
    public class EngineerController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;

        public EngineerController(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

        }

        public IActionResult EngineerProfile()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }
           // ViewBag.Pageitem = "Engineer";
            return View();
        }

        public JsonResult GetBankName()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            var result = new List<SubcontractProfileBankingModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var companyId = userProfile.companyid;


            string uriString = string.Format("{0}", strpathAPI + "Banking/GetAll");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsync = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileBankingModel>>(resultAsync);

            }


            return Json(result);
        }


        public ActionResult Search(string locationId, string teamId
          , string staffName, string teamCitizen, string position)
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

            Guid companyId = userProfile.companyid;
            Guid gLocationId;
            Guid gTeamId;

            if (locationId == null || locationId == "-1" )
            {
                gLocationId = Guid.Empty;
            }
            else
            {
                gLocationId = new Guid(locationId);
            }


            if(teamId == null || teamId == "-1" )
            {
                gTeamId = Guid.Empty;
            }
            else
            {
                gTeamId = new Guid(teamId);
            }

            if (staffName == null)
            {
                staffName = "null";
            }

            if (teamCitizen == null)
            {
                teamCitizen = "null";
            }

            if (position == null)
            {
                position = "null";
            }


            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Engineer/SearchEngineer", companyId, gLocationId
               , gTeamId, staffName, teamCitizen, position);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        public JsonResult GetDataById(string engineerId)
        {
            var result = new SubcontractProfileEngineerModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Engineer/GetByEngineerId", engineerId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfileEngineerModel>(resultAsysc);

            }

            return Json(result);
        }
        public JsonResult getEngineerByTeamId(string Teamid,string Locationid)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            Guid companyId = userProfile.companyid;

            Guid gTeamid;
            Guid gLocationid;
            if (Teamid == null || Teamid == "-1")
            {
                gTeamid = Guid.Empty;
            }
            else
            {
                gTeamid = new Guid(Teamid);
            }
            if (Locationid == null || Locationid == "-1")
            {
                gLocationid = Guid.Empty;
            }
            else
            {
                gLocationid = new Guid(Locationid);
            }
          
            var result = new List<SubcontractProfileEngineerModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Engineer/GetAll");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(resultAsysc);
                result.Where(x => x.TeamId == gTeamid && x.CompanyId == companyId && x.LocationId == gLocationid).ToList();               

            }

            return Json(result);
        }

        public JsonResult GetDataPersonalById(Guid personalId)
        {
            var result = new SubcontractProfilePersonalModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Personal/GetByPersonalId", personalId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfilePersonalModel>(resultAsysc);

            }

            return Json(result);
        }

        public JsonResult GetTitleName()
        {
            var  result = new List<SubcontractDropdownModel>(); 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "title_name");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject <List<SubcontractDropdownModel>>(resultAsysc);

            }

            return Json(result);
        }


        public ActionResult OnSave(SubcontractProfileEngineerModel model, SubcontractProfilePersonalModel personal)
        {
            ResponseModel result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();

            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                //insert
                if (model.EngineerId == Guid.Empty)
                {
                    model.CompanyId = userProfile.companyid;
                    model.CreateBy = userProfile.Username;
                    model.UpdateBy = userProfile.Username;
                    model.StaffCode = DateTime.Now.ToString("yyyyMMddhhmmss");

                    model.EngineerId = Guid.NewGuid();

                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Engineer", "Insert"));

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = clientLocation.PostAsync(uriLocation, httpContent).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
                        personal.engineerId = model.EngineerId;
                        //personal
                        var uriLocationPersonal = new Uri(Path.Combine(strpathAPI, "Personal", "Insert"));

                        clientLocation.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpContentPersonal = new StringContent(JsonConvert.SerializeObject(personal), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = clientLocation.PostAsync(uriLocationPersonal, httpContentPersonal).Result;
                        if (response.IsSuccessStatusCode)
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
                    else
                    {
                        result.Status = false;
                        result.Message = "Data is not correct, Please Check Data or Contact System Admin";
                        result.StatusError = "-1";
                    }
                }
                else //update
                {
                    model.CompanyId = userProfile.companyid;
                    model.CreateBy = userProfile.Username;
                    model.UpdateBy = userProfile.Username;

                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Engineer", "Update"));

                   

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseResult = clientLocation.PutAsync(uriLocation, httpContent).Result;

                    if (responseResult.IsSuccessStatusCode)
                    {
                        var uriPersonal = new Uri(Path.Combine(strpathAPI, "Personal", "Update"));

                        personal.UpdateBy = userProfile.Username;

                        HttpClient clientPersonal = new HttpClient();

                        clientPersonal.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpContentPersonal = new StringContent(JsonConvert.SerializeObject(personal), Encoding.UTF8, "application/json");
                        HttpResponseMessage responseResultPersonal = clientPersonal.PutAsync(uriPersonal, httpContentPersonal).Result;
                        if (responseResultPersonal.IsSuccessStatusCode)
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

        public JsonResult OnDelete(string engineerId)
        {
            var result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();
            try
            {
                string uriString = string.Format("{0}/{1}", strpathAPI + "Engineer/Delete", engineerId);
       
                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseResult = clientLocation.DeleteAsync(uriString).Result;
                if (responseResult.IsSuccessStatusCode)
                {
                    result.Message = "ลบข้อมูลเรียบร้อย";
                    result.Status = true;
                    result.StatusError = "0";
                }
                else
                {
                    result.Status = false;
                    result.Message = "ลบข้อมูลไม่สำเร็จ กรุณาติดต่อ Administrator.";
                    result.StatusError = "-1";
                }

            }
            catch (Exception ex)
            {
                result.Message = "ลบข้อมูลไม่สำเร็จ กรุณาติดต่อ Administrator.";
                result.StatusError = "-1";
            }
            return Json(result);
        }

    }
}
