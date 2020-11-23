using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
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
    public class EngineerController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        private const int MegaBytes = 1024 * 1024;
        private const int TMegaBytes = 3 * 1024 * 1024;
        private readonly string strpathUpload;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";
        private readonly IStringLocalizer<EngineerController> _localizer;
        public EngineerController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IStringLocalizer<EngineerController> localizer)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();

        }

        public IActionResult EngineerProfile()
        {
            

            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }

            getsession();
            ViewData["Controller"] = _localizer["Profile"];
                ViewData["View"] = _localizer["EngineerProfile"];


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


            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Engineer/SearchEngineer"
                , HttpUtility.UrlEncode(companyId.ToString(), Encoding.UTF8)
                , HttpUtility.UrlEncode(gLocationId.ToString(), Encoding.UTF8)
               , HttpUtility.UrlEncode(gTeamId.ToString(), Encoding.UTF8)
               , HttpUtility.UrlEncode(staffName, Encoding.UTF8)
               , HttpUtility.UrlEncode(teamCitizen, Encoding.UTF8)
               , HttpUtility.UrlEncode(position, Encoding.UTF8));

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

            string uriString = string.Format("{0}/{1}", strpathAPI + "Engineer/GetByEngineerId"
                , HttpUtility.UrlEncode(engineerId, Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfileEngineerModel>(resultAsysc);

                if (result.PersonalAttachFile != null)
                {
                    Guid file_id = Guid.NewGuid();

                    result.file_id__PersonalAttach = file_id;
                }
                if (result.VehicleAttachFile != null)
                {
                    Guid file_id = Guid.NewGuid();

                    result.file_id__VehicleAttach = file_id;
                }
            }

            return Json(result);
        }

        public JsonResult GetEngineerByTeam(string locationid,string teamid )
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            Guid companyId = userProfile.companyid;

            Guid gTeamid;
            Guid gLocationid;
            if (teamid == null || teamid == "-1")
            {
                gTeamid = Guid.Empty;
            }
            else
            {
                gTeamid = new Guid(teamid);
            }
            if (locationid == null || locationid == "-1")
            {
                gLocationid = Guid.Empty;
            }
            else
            {
                gLocationid = new Guid(locationid);
            }

            var result = new List<SubcontractProfileEngineerModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}/{2}/{3}", strpathAPI + "Engineer/GetEngineerByTeam"
                , HttpUtility.UrlEncode(companyId.ToString(), Encoding.UTF8)
                , HttpUtility.UrlEncode(gLocationid.ToString(), Encoding.UTF8)
                , HttpUtility.UrlEncode(gTeamid.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(resultAsysc);

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

            string uriString = string.Format("{0}/{1}", strpathAPI + "Personal/GetByPersonalId"
                , HttpUtility.UrlEncode(personalId.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfilePersonalModel>(resultAsysc);
                if(result !=null)
                {
                    if (result.CertificateAttachFile != null)
                    {
                        Guid file_id = Guid.NewGuid();

                        result.file_id__CertificateAttach = file_id;
                    }
                    if (result.WorkPermitAttachFile != null)
                    {
                        Guid file_id = Guid.NewGuid();

                        result.file_id__WorkPermitAttach = file_id;
                    }
                    if (result.ProfileImgAttachFile != null)
                    {
                        Guid file_id = Guid.NewGuid();

                        result.file_id__ProfileImgAttach = file_id;
                    }
                }
                
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


        public async Task<ActionResult> OnSave(SubcontractProfileEngineerModel model, SubcontractProfilePersonalModel personal)
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


                    if (model.File_PersonalAttach != null && model.File_PersonalAttach.Length > 0)
                    {
                        await Uploadfile(model.File_PersonalAttach, model.EngineerId.ToString(), userProfile.companyid.ToString(), "Engineer", "I");
                        model.PersonalAttachFile = model.File_PersonalAttach.FileName;

                    }
                    if (model.File_VehicleAttach != null && model.File_VehicleAttach.Length > 0)
                    {
                        await Uploadfile(model.File_VehicleAttach, model.EngineerId.ToString(), userProfile.companyid.ToString(), "Engineer", "I");
                        model.VehicleAttachFile = model.File_VehicleAttach.FileName;
                    }

                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Engineer", "Insert"));

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = clientLocation.PostAsync(uriLocation, httpContent).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
                        personal.engineerId = model.EngineerId;
                        personal.PersonalId = Guid.NewGuid();
                        personal.CreateBy = userProfile.Username;
                        if (personal.dateBirthDay != null)
                        {
                            DateTime datebirthday = DateTime.ParseExact(personal.dateBirthDay, "dd/MM/yyyy", new CultureInfo("en-US"));
                            personal.BirthDate = datebirthday;
                        }
                        if (personal.dateCertificateExpireDate != null)
                        {
                            DateTime datecer= DateTime.ParseExact(personal.dateCertificateExpireDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                            personal.CertificateExpireDate = datecer;
                        }
                        //personal

                        if (personal.File_CertificateAttach != null && personal.File_CertificateAttach.Length > 0)
                        {
                            await Uploadfile(personal.File_CertificateAttach, personal.PersonalId.ToString(), userProfile.companyid.ToString(), "Personal", "I");
                            personal.CertificateAttachFile = personal.File_CertificateAttach.FileName;
                        }
                        if (personal.File_WorkPermitAttach != null && personal.File_WorkPermitAttach.Length > 0)
                        {
                            await Uploadfile(personal.File_WorkPermitAttach, personal.PersonalId.ToString(), userProfile.companyid.ToString(), "Personal", "I");
                            personal.WorkPermitAttachFile = personal.File_WorkPermitAttach.FileName;
                        }
                        if (personal.File_ProfileImgAttach != null && personal.File_ProfileImgAttach.Length > 0)
                        {
                            await Uploadfile(personal.File_ProfileImgAttach, personal.PersonalId.ToString(), userProfile.companyid.ToString(), "Personal", "I");
                            personal.ProfileImgAttachFile = personal.File_ProfileImgAttach.FileName;
                        }

                        var uriLocationPersonal = new Uri(Path.Combine(strpathAPI, "Personal", "Insert"));

                        clientLocation.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpContentPersonal = new StringContent(JsonConvert.SerializeObject(personal), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = clientLocation.PostAsync(uriLocationPersonal, httpContentPersonal).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            result.Status = true;
                            result.Message = _localizer["MessageSuccess"];
                            result.StatusError = "0";
                        }
                        else
                        {
                            result.Status = false;
                            result.Message = _localizer["MessageUnSuccess"];
                            result.StatusError = "-1";
                        }


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

                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Engineer", "Update"));


                    if (model.File_PersonalAttach != null && model.File_PersonalAttach.Length > 0)
                    {
                        await Uploadfile(model.File_PersonalAttach, model.EngineerId.ToString(), userProfile.companyid.ToString(), "Engineer", "U");
                        model.PersonalAttachFile = model.File_PersonalAttach.FileName;

                    }
                    if (model.File_VehicleAttach != null && model.File_VehicleAttach.Length > 0)
                    {
                        await Uploadfile(model.File_VehicleAttach, model.EngineerId.ToString(), userProfile.companyid.ToString(), "Engineer", "U");
                        model.VehicleAttachFile = model.File_VehicleAttach.FileName;
                    }


                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    string str = JsonConvert.SerializeObject(model);
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseResult = clientLocation.PutAsync(uriLocation, httpContent).Result;

                    if (responseResult.IsSuccessStatusCode)
                    {
                        if (personal.dateBirthDay != null)
                        {
                            DateTime datebirthday = DateTime.ParseExact(personal.dateBirthDay, "dd/MM/yyyy", new CultureInfo("en-US"));
                            personal.BirthDate = datebirthday;
                        }
                        if (personal.dateCertificateExpireDate != null)
                        {
                            DateTime datecer = DateTime.ParseExact(personal.dateCertificateExpireDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                            personal.CertificateExpireDate = datecer;
                        }

                        var uriPersonal = new Uri(Path.Combine(strpathAPI, "Personal", "Update"));

                        if (personal.File_CertificateAttach != null && personal.File_CertificateAttach.Length > 0)
                        {
                            await Uploadfile(personal.File_CertificateAttach, personal.PersonalId.ToString(), userProfile.companyid.ToString(), "Personal", "I");
                            personal.CertificateAttachFile = personal.File_CertificateAttach.FileName;
                        }
                        if (personal.File_WorkPermitAttach != null && personal.File_WorkPermitAttach.Length > 0)
                        {
                            await Uploadfile(personal.File_WorkPermitAttach, personal.PersonalId.ToString(), userProfile.companyid.ToString(), "Personal", "I");
                            personal.WorkPermitAttachFile = personal.File_WorkPermitAttach.FileName;
                        }
                        if (personal.File_ProfileImgAttach != null && personal.File_ProfileImgAttach.Length > 0)
                        {
                            await Uploadfile(personal.File_ProfileImgAttach, personal.PersonalId.ToString(), userProfile.companyid.ToString(), "Personal", "I");
                            personal.ProfileImgAttachFile = personal.File_ProfileImgAttach.FileName;
                        }

                        personal.UpdateBy = userProfile.Username;

                        HttpClient clientPersonal = new HttpClient();

                        clientPersonal.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpContentPersonal = new StringContent(JsonConvert.SerializeObject(personal), Encoding.UTF8, "application/json");
                        HttpResponseMessage responseResultPersonal = clientPersonal.PutAsync(uriPersonal, httpContentPersonal).Result;
                        if (responseResultPersonal.IsSuccessStatusCode)
                        {
                            result.Status = true;
                            result.Message = _localizer["MessageSuccess"];
                            result.StatusError = "0";
                        }
                        else
                        {
                            result.Status = false;
                            result.Message = _localizer["MessageUnSuccess"];
                            result.StatusError = "-1";
                        }

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

        public async Task<JsonResult> OnDelete(string engineerId)
        {
            var result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();
            string personalid = "";
            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                #region SearchPersonal
                string uriSearch = string.Format("{0}/{1}", strpathAPI + "Engineer/GetByEngineerId"
                    , HttpUtility.UrlEncode(engineerId, Encoding.UTF8));
                var resultengineer = new SubcontractProfileEngineerModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(uriSearch).Result;
                if(response.IsSuccessStatusCode)
                {
                    var resultAsysc = response.Content.ReadAsStringAsync().Result;
                    resultengineer = JsonConvert.DeserializeObject<SubcontractProfileEngineerModel>(resultAsysc);
                    personalid = resultengineer.PersonalId.ToString();
                }

                #endregion

                string uriString = string.Format("{0}/{1}", strpathAPI + "Engineer/Delete", engineerId);
       
                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseResult = clientLocation.DeleteAsync(uriString).Result;
                if (responseResult.IsSuccessStatusCode)
                {
                    await Deletefile(engineerId, userProfile.companyid.ToString(), "Engineer");
                    if (personalid!=null && personalid != "")
                    {
                        string uriPersonal = string.Format("{0}/{1}", strpathAPI + "Personal/Delete", personalid);
                        clientLocation.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage responsePersonal = clientLocation.DeleteAsync(uriPersonal).Result;
                        if(responsePersonal.IsSuccessStatusCode)
                        {
                            await Deletefile(personalid, userProfile.companyid.ToString(), "Personal");
                        }

                       
                    }

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

        public JsonResult GetPosition()
        {
            var result = new List<SubcontractDropdownModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "subcontract_position");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(resultAsysc);

            }

            return Json(result);
        }

        [HttpPost]
        public IActionResult GetVehicleType()
        {
            var result = new List<SubcontractProfileVerhicleTypeModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "VerhicleType/GetAll");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleTypeModel>>(resultAsysc);

            }

            return Json(result);
        }
        [HttpPost]
        public IActionResult GetVehicleBrand()
        {
            var result = new List<SubcontractProfileVerhicleBrandModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "VerhicleBrand/GetAll");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleBrandModel>>(resultAsysc);

            }

            return Json(result);
        }
        [HttpPost]
        public IActionResult GetVehicleSerise(string VerhicleBrandId)
        {
            var result = new List<SubcontractProfileVerhicleSeriseModel>();
            string uriString = "";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            if (VerhicleBrandId != null && VerhicleBrandId != "")
            {
                uriString = string.Format("{0}/{1}", strpathAPI + "VerhicleSerise/GetByVerhicleBrandId", HttpUtility.UrlEncode(VerhicleBrandId, Encoding.UTF8));
            }
            else
            {
                uriString = string.Format("{0}", strpathAPI + "VerhicleSerise/GetAll");
            }
            

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleSeriseModel>>(resultAsysc);

            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetSizeShirt()
        {
            var result = new List<SubcontractDropdownModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "subcontract_size");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(resultAsysc);

            }

            return Json(result);
        }

        #region UploadFile
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult CheckFileUpload(IFormFile files)
        {
            bool statusupload = true;
            string strmess = "";
            Guid? fid = null;
            try
            {
                if(files !=null)
                {
                    if (files.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        if (
                                files.ContentType.ToLower() != "image/jpg" &&
                                files.ContentType.ToLower() != "image/jpeg" &&
                                files.ContentType.ToLower() != "image/pjpeg" &&
                                files.ContentType.ToLower() != "image/gif" &&
                                files.ContentType.ToLower() != "image/png" &&
                                files.ContentType.ToLower() != "image/bmp" &&
                                files.ContentType.ToLower() != "image/tif" &&
                                 files.ContentType.ToLower() != "image/tiff" &&
                                 files.ContentType.ToLower() != "application/pdf"
                                )
                        {
                            statusupload = false;
                            strmess = _localizer["MessageUploadmissmatch"];
                        }
                        else
                        {
                            var fileSize = files.Length;
                            if (files.ContentType.ToLower() == "application/pdf")
                            {
                                if (fileSize > MegaBytes)
                                {
                                    statusupload = false;
                                    strmess = _localizer["MessageUploadtoolage"];
                                }
                                else
                                {
                                    fid = Guid.NewGuid();
                                    strmess = _localizer["MessageUploadSuccess"];
                                }
                            }
                            else
                            {
                                if (fileSize > TMegaBytes)
                                {
                                    statusupload = false;
                                    strmess = _localizer["MessageUploadtoolage"];
                                }
                                else
                                {
                                    fid = Guid.NewGuid();
                                    strmess = _localizer["MessageUploadSuccess"];
                                }
                            }

                        }
                    }
                }

                    
            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
            }
            return Json(new { status = statusupload, message = strmess, file_id = fid });
        }

        private async Task<bool> Uploadfile(IFormFile files, string id, string companyid,string type,string action)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            FileStream output;
            string strmess = "";
            var outputNAS = new List<SubcontractDropdownModel>();
            try
            {

                if (files != null && files.Length > 0)
                {
                    #region NAS
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/nas_subcontract");
                    HttpResponseMessage response = client.GetAsync(uriString).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var v = response.Content.ReadAsStringAsync().Result;
                        outputNAS = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                    }

                    string username = outputNAS[0].value1;
                    string password = outputNAS[0].value2;
                    string ipAddress = @"\\" + outputNAS[0].dropdown_value;
                    string destNAS = outputNAS[0].dropdown_text;

                    NetworkCredential sourceCredentials = new NetworkCredential { Domain = ipAddress, UserName = username, Password = password };

                    #endregion
                    using (new NetworkConnection(destNAS, sourceCredentials))
                    {
                        if (files != null)
                        {

                            string strdir = Path.Combine(destNAS + @"\SubContractProfile\", companyid, type, id);
                            if (!Directory.Exists(strdir))
                            {
                                Directory.CreateDirectory(strdir);
                            }
                            else
                            {
                                if (action == "U")
                                {
                                    System.IO.DirectoryInfo di = new DirectoryInfo(strdir);
                                    foreach (FileInfo finfo in di.GetFiles())
                                    {
                                        finfo.Delete();
                                    }
                                }

                            }

                        }
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        using (output = System.IO.File.Create(this.GetPathAndFilename(id, filename, companyid, type, destNAS + @"\SubContractProfile\")))
                            await files.CopyToAsync(output);
                    }
                        
                }

            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
                throw;
            }


            return statusupload;

        }

        private string GetPathAndFilename(string id, string filename, string companyid,string type,string dir)
        {
            string pathdir = Path.Combine(dir, companyid, type, id);
            string PathOutput = "";
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
            PathOutput = Path.Combine(pathdir, filename);
            return PathOutput;
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private async Task<bool> Deletefile(string id, string companyid, string type)
        {
            bool result = true;
            var outputNAS = new List<SubcontractDropdownModel>();
            try
            {
                #region NAS
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/nas_subcontract");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    outputNAS = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                }

                string username = outputNAS[0].value1;
                string password = outputNAS[0].value2;
                string ipAddress = @"\\" + outputNAS[0].dropdown_value;
                string destNAS = outputNAS[0].dropdown_text;

                NetworkCredential sourceCredentials = new NetworkCredential { Domain = ipAddress, UserName = username, Password = password };

                #endregion
                using (new NetworkConnection(destNAS, sourceCredentials))
                {
                    string strdir = Path.Combine(destNAS + @"\SubContractProfile\", companyid, type, id);
                    System.IO.DirectoryInfo di = new DirectoryInfo(strdir);
                    if (Directory.Exists(strdir))
                    {
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }

                        Directory.Delete(strdir, true);
                    }
                }
                 

            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}
            };
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
            //dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }
    }
}
