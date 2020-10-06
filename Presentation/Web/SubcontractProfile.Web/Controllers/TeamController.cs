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
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";
        private readonly IStringLocalizer<TeamController> _localizer;
        public TeamController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IStringLocalizer<TeamController> localizer)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

        }

        public IActionResult TeamProfile()
        {
            
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }

                ViewData["Controller"] = _localizer["Profile"];
                ViewData["View"] = _localizer["TeamProfile"];

            return View();
        }

        public ActionResult Search(string locationId, string teamcode
          , string teamNameTh, string teamNameEn, string storageLocation, string shipto)
        {

            var Result = new List<SubcontractProfileTeamModel>();

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


            if (locationId == "-1" || locationId==null)
            {
                locationId = "-1";
            }

            if (teamcode == null)
            {
                teamcode = "null";
            }

          
            if (teamNameTh == null)
            {
                teamNameTh = "null";
            }

            if (teamNameEn == null)
            {
                teamNameEn = "null";
            }

            if (storageLocation == null)
            {
                storageLocation = "null";
            }

            if (shipto == null)
            {
                shipto = "null";
            }

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Team/SearchTeam",companyId, locationId
               , teamcode, teamNameTh, teamNameEn, storageLocation, shipto);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        public JsonResult GetDataByLocationId(string locationId)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            Guid companyId = userProfile.companyid;

            var result = new List<SubcontractProfileTeamModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            Guid gLocationId;

            if (locationId == null || locationId == "-1")
            {
                gLocationId = Guid.Empty;
            }
            else
            {
                gLocationId = new Guid(locationId);
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

        public JsonResult GetDataById(string teamId)
        {
            var result = new SubcontractProfileTeamModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Team/GetByTeamId", teamId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfileTeamModel>(resultAsysc);

            }

            return Json(result);
        }

        public JsonResult GetLocationByCompany()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            var result = new List<SubcontractProfileLocationModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var companyId = userProfile.companyid;


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

        public ActionResult OnSave(SubcontractProfileTeamModel model)
        {
            ResponseModel result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();

            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                //insert
                if (model.TeamId == Guid.Empty)
                {
                    model.CompanyId = userProfile.companyid;
                    model.CreateBy = userProfile.Username;
                    model.UpdateBy = userProfile.Username;
                    model.Status = "N";

                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Team", "Insert"));

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = clientLocation.PostAsync(uriLocation, httpContent).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
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
                else //update
                {
                    model.CompanyId = userProfile.companyid;
                    model.CreateBy = userProfile.Username;
                    model.UpdateBy = userProfile.Username;
                 
                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Team", "Update"));

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseResult = clientLocation.PutAsync(uriLocation, httpContent).Result;

                    if (responseResult.IsSuccessStatusCode)
                    {
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
                result.Message = _localizer["MessageError"];
                result.Status = false;
                result.StatusError = "0";
            }
            return Json(result);
        }

        public JsonResult OnDelete(string teamId)
        {
            var result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();
            try
            {
                string uriString = string.Format("{0}/{1}", strpathAPI + "Team/Delete", teamId);
                //  var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Delete"));

                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // var httpContent = new StringContent(JsonConvert.SerializeObject(locationId), Encoding.UTF8, "application/json");
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
                    result.Message = _localizer["MessageDeleteUnSucess"];
                    result.StatusError = "-1";
                }

            }
            catch (Exception ex)
            {
                result.Message = _localizer["MessageDeleteUnSucess"];
                result.StatusError = "-1";
            }
            return Json(result);
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

    }
}
