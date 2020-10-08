﻿using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;

        private readonly string strpathUpload;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";
        private SubcontractProfileUserModel dataUser = new SubcontractProfileUserModel();

        private const int MegaBytes = 1024 * 1024;
        private const int TMegaBytes = 3 * 1024 * 1024;
        private readonly IStringLocalizer<LocationController> _localizer;
        public LocationController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IStringLocalizer<LocationController> localizer)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();
            //Lang = "TH";
            //strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();

            // HttpContext.Session.SetString("username", username);
        }

        // GET: LocationController
        public ActionResult LocationProfile()
        {
          
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }
            getsession();
            ViewData["Controller"] = _localizer["Profile"];
                ViewData["View"] = _localizer["LocationProfile"];

            // ViewBag.Pageitem = "Location";
            return View();
        }

    
        public ActionResult Search(string locationCode, string locationNameAilas, string locationNameTh
            , string locationNameEn, string storageLocation, string phoneNo)
        {

            var Result = new List<SubcontractProfileLocationModel>();

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

            Guid strCompanyId = userProfile.companyid;


            if (locationCode == null)
            {
                locationCode = "null";
            }

            if (locationNameAilas == null)
            {
                locationNameAilas = "null";
            }

            if (locationNameTh == null)
            {
                locationNameTh = "null";
            }

            if (locationNameEn == null)
            {
                locationNameEn = "null";
            }

            if (storageLocation == null)
            {
                storageLocation = "null";
            }

            if (phoneNo == null)
            {
                phoneNo = "null";
            }

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Location/SearchLocation", strCompanyId
               , locationCode, HttpUtility.UrlEncode(locationNameTh, Encoding.UTF8), locationNameEn, storageLocation, phoneNo, locationNameAilas);

            var httpContentSearch = uriString;

            HttpResponseMessage response = client.GetAsync(httpContentSearch).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        public JsonResult GetDataById(string locationId)
        {
            var locationResult = new SubcontractProfileLocationModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetByLocationId", locationId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                locationResult = JsonConvert.DeserializeObject<SubcontractProfileLocationModel>(result);
                if (locationResult.BankAttachFile != null)
                {
                    Guid file_id = Guid.NewGuid();

                    locationResult.file_id__BankAttach = file_id;
                }
            }

            return Json(locationResult);
        }

        [HttpPost]
        public async Task<ActionResult> OnSave(SubcontractProfileLocationModel model)
        {
            ResponseModel result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();

            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                //insert
                if (model.LocationId == Guid.Empty)
                {
                    model.LocationId = Guid.NewGuid();
                    if (model.File_BankAttach != null && model.File_BankAttach.Length > 0)
                    {
                        await Uploadfile(model.File_BankAttach, model.LocationId.ToString(), userProfile.companyid.ToString());
                        model.BankAttachFile = model.File_BankAttach.FileName;

                    }
                    
                    model.CompanyId = userProfile.companyid;
                    model.CreateBy = userProfile.Username;
                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Insert"));

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = clientLocation.PostAsync(uriLocation, httpContent).Result;
                    if (responseCompany.IsSuccessStatusCode)
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
                else //update
                {
                    if (model.File_BankAttach != null && model.File_BankAttach.Length > 0)
                    {
                        await Uploadfile(model.File_BankAttach, model.LocationId.ToString(), userProfile.companyid.ToString());
                        model.BankAttachFile = model.File_BankAttach.FileName;

                    }

                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Update"));
                    model.UpdateBy = userProfile.Username;

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseResult = clientLocation.PutAsync(uriLocation, httpContent).Result;

                    if (responseResult.IsSuccessStatusCode)
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
            }

            catch (Exception ex)
            {
                result.Message = _localizer["MessageError"];
                result.Status = false;
                result.StatusError = "0";
            }
            return Json(result);
        }

        public async Task<JsonResult> OnDelete(string locationId)
        {
            var result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();
            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
                string uriString = string.Format("{0}/{1}", strpathAPI + "Location/Delete", locationId);
              //  var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Delete"));

                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // var httpContent = new StringContent(JsonConvert.SerializeObject(locationId), Encoding.UTF8, "application/json");
                HttpResponseMessage responseResult = clientLocation.DeleteAsync(uriString).Result;
                if (responseResult.IsSuccessStatusCode)
                {
                   

                    if(await Deletefile(locationId, userProfile.companyid.ToString()))
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

        public IActionResult CheckLocationCode(string locationcode)
        {
            bool status = true;
            string message = "";
            List<SubcontractProfileLocationModel> L_location= new List<SubcontractProfileLocationModel>();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                Guid strCompanyId = userProfile.companyid;

                string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Location/SearchLocation", strCompanyId
                   , locationcode, "null", "null", "null", "null", "null");

                HttpResponseMessage response = client.GetAsync(uriString).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    //data
                    L_location = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(result);

                }
                if (L_location != null && L_location.Count > 0)
                {

                        status = false;
                        message = _localizer["MessageCheckLocationCode"];


                }
            }
            catch (Exception w)
            {
                status = false;
                message = w.Message;
                throw;
            }
            return Json(new { Status = status, Message = message });
        }

        #region Upload
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
                            files.ContentType.ToLower() != "image/tiff" &&
                            files.ContentType.ToLower() != "image/tif" &&
                            files.ContentType.ToLower() != "application/pdf"
                                )
                        {
                            statusupload = false;
                            strmess = _localizer["MessageUploadmissmatch"];
                        }
                        else
                        {
                            if (files.ContentType.ToLower() == "application/pdf")
                            {
                                var fileSize = files.Length;
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
                                var fileSize = files.Length;
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

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private async Task<bool> Uploadfile(IFormFile files, string locationid,string companyid)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            FileStream output;
            string strmess = "";
            try
            {

                if (files != null && files.Length > 0)
                {
                    if (files != null)
                    {
                        string strdir = Path.Combine(strpathUpload, companyid, "Location",locationid);
                        if(!Directory.Exists(strdir))
                        {
                            Directory.CreateDirectory(strdir);
                        }
                        else
                        {
                            System.IO.DirectoryInfo di = new DirectoryInfo(strdir);
                            foreach (FileInfo finfo in di.GetFiles())
                            {
                                finfo.Delete();
                            }
                        }
                        
                    }
                    string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                    filename = EnsureCorrectFilename(filename);
                    using (output = System.IO.File.Create(this.GetPathAndFilename(locationid, filename,companyid)))
                        await files.CopyToAsync(output);
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

        private string GetPathAndFilename(string locationid, string filename,string companyid)
        {
            string pathdir = Path.Combine(strpathUpload, companyid, "Location", locationid);
            string PathOutput = "";
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
            PathOutput = Path.Combine(pathdir, filename);
            return PathOutput;
        }

        public async Task<ActionResult> DownloadfileLocation(string locationid, string filename)
        {
            if (dataUser == null)
            {
                getsession();
            }
            var path = this.GetPathAndFilename(locationid, filename, dataUser.companyid.ToString());
            string content = GetContentType(path);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);

            }

            memory.Position = 0;
            var array = memory.ToArray();


            if (array != null)
            {
                return File(array, content, Path.GetFileName(path));
            }
            else
            {
                return new EmptyResult();
            }
        }

        private async Task<bool> Deletefile(string locationid,string companyid)
        {
            bool result = true;
            try
            {
                string strdir = Path.Combine(strpathUpload, companyid, "Location", locationid);
                System.IO.DirectoryInfo di = new DirectoryInfo(strdir);
                if(Directory.Exists(strdir))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                    Directory.Delete(strdir, true);
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
        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if (Lang == null || Lang == "")
            {
                Lang = "TH";
            }
            dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }
    }
}
