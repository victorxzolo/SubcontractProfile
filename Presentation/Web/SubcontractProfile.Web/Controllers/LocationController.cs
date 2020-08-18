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
    public class LocationController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;

        public LocationController(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
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
               , locationCode, locationNameTh, locationNameEn, storageLocation, phoneNo, locationNameAilas);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

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

            }

            return Json(locationResult);
        }

        public ActionResult OnSave(SubcontractProfileLocationModel model)
        {
            ResponseModel result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();

            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                //insert
                if (model.LocationId == Guid.Empty)
                {
                    model.CompanyId = userProfile.companyid;
                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Insert"));

                    clientLocation.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = clientLocation.PostAsync(uriLocation, httpContent).Result;
                    if (responseCompany.IsSuccessStatusCode)
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
                else //update
                {
                    var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Update"));

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
            }

            catch (Exception ex)
            {
                result.Message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อ Administrator.";
                result.Status = false;
                result.StatusError = "0";
            }
            return Json(result);
        }

        public JsonResult OnDelete(string locationId)
        {
            var result = new ResponseModel();
            HttpClient clientLocation = new HttpClient();
            try
            {
                string uriString = string.Format("{0}/{1}", strpathAPI + "Location/Delete", locationId);
              //  var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Delete"));

                clientLocation.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // var httpContent = new StringContent(JsonConvert.SerializeObject(locationId), Encoding.UTF8, "application/json");
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
