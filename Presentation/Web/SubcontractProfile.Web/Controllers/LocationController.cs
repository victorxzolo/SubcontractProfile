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

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Location/SearchLocation"
                , HttpUtility.UrlEncode(strCompanyId.ToString(), Encoding.UTF8)
               , HttpUtility.UrlEncode(locationCode, Encoding.UTF8)
               , HttpUtility.UrlEncode(locationNameTh, Encoding.UTF8)
               , HttpUtility.UrlEncode(locationNameEn, Encoding.UTF8)
               , HttpUtility.UrlEncode(storageLocation, Encoding.UTF8)
               , HttpUtility.UrlEncode(phoneNo, Encoding.UTF8)
               , HttpUtility.UrlEncode(locationNameAilas, Encoding.UTF8));

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
            var addressResult = new List<SubcontractProfileAddressModel>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetByLocationId", HttpUtility.UrlEncode(locationId, Encoding.UTF8));

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

        #region Address
        [HttpPost]
        public IActionResult GetAddress(string locationid)
        {
            var addressResult = new List<SubcontractProfileAddressModel>();
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault() == null ? "0" : Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault() == null ? "10" : Request.Form["length"].FirstOrDefault();
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
            try
            {
                SubcontractProfileAddressModel input = new SubcontractProfileAddressModel();
                input.LocationId = locationid;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Address/GetByLocationId");

                //string jj = JsonConvert.SerializeObject(input);

                var httpContentCompany = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uriString, httpContentCompany).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    addressResult = JsonConvert.DeserializeObject<List<SubcontractProfileAddressModel>>(v);
                    if (addressResult.Count() != 0)
                    {
                        string uriStringSubdistrict = string.Format("{0}", strpathAPI + "SubDistrict/GetAll");
                        string uriStringDistirct = string.Format("{0}", strpathAPI + "District/GetAll");
                        string uriStringProvince = string.Format("{0}", strpathAPI + "Province/GetAll");
                        string uriStringAddresstype = string.Format("{0}", strpathAPI + "AddressType/GetALL");

                        response = client.GetAsync(uriStringSubdistrict).Result;
                        var s = response.Content.ReadAsStringAsync().Result;
                        var L_subdistirct = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(s);

                        response = client.GetAsync(uriStringDistirct).Result;
                        var d = response.Content.ReadAsStringAsync().Result;
                        var L_distirct = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(d);

                        response = client.GetAsync(uriStringProvince).Result;
                        var p = response.Content.ReadAsStringAsync().Result;
                        var L_province = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(p);

                        response = client.GetAsync(uriStringAddresstype).Result;
                        var a = response.Content.ReadAsStringAsync().Result;
                        var L_addresstype = JsonConvert.DeserializeObject<List<SubcontractProfileAddressTypeModel>>(a);
                        CultureInfo culture = CultureInfo.CurrentCulture;
                        foreach (var f in addressResult)
                        {
                            if (culture.Name == "th")
                            {
                                if (f.AddressTypeId != null)
                                {
                                    f.address_type_name = L_addresstype.Where(x => x.AddressTypeId == f.AddressTypeId).Select(x => x.AddressTypeNameTh).FirstOrDefault().ToString();

                                }
                                if (f.SubDistrictId != null)
                                {
                                    f.sub_district_name = L_subdistirct.Where(x => x.SubDistrictId == f.SubDistrictId).Select(x => x.SubDistrictNameTh).FirstOrDefault().ToString();

                                }
                                if (f.DistrictId != null)
                                {
                                    f.district_name = L_distirct.Where(e => e.DistrictId == f.DistrictId).Select(x => x.DistrictNameTh).FirstOrDefault().ToString();

                                }
                                if (f.ProvinceId != null)
                                {
                                    f.province_name = L_province.Where(x => x.ProvinceId == f.ProvinceId).Select(x => x.ProvinceNameTh).FirstOrDefault().ToString();

                                }
                            }
                            else
                            {
                                if (f.AddressTypeId != null)
                                {
                                    f.address_type_name = L_addresstype.Where(x => x.AddressTypeId == f.AddressTypeId && f.AddressTypeId != null).Select(x => x.AddressTypeNameEn).FirstOrDefault().ToString();

                                }
                                if (f.SubDistrictId != null)
                                {
                                    f.sub_district_name = L_subdistirct.Where(x => x.SubDistrictId == f.SubDistrictId).Select(x => x.SubDistrictNameEn).FirstOrDefault().ToString();

                                }
                                if (f.DistrictId != null)
                                {
                                    f.district_name = L_distirct.Where(e => e.DistrictId == f.DistrictId).Select(x => x.DistrictNameEn).FirstOrDefault().ToString();

                                }
                                if (f.ProvinceId != null)
                                {
                                    f.province_name = L_province.Where(x => x.ProvinceId == f.ProvinceId).Select(x => x.ProvinceNameEn).FirstOrDefault().ToString();

                                }
                            }

                        }
                    }




                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompanySSO", addressResult);
                recordsTotal = addressResult.Count();

                //Paging   
                var data = addressResult.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = new List<SubcontractProfileAddressModel>() });
                throw;
            }

        }
        [HttpPost]
        public IActionResult SaveDaftAddress(List<SubcontractProfileAddressModel> daftdata)
        {
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftLocation");
                if (data != null && data.Count != 0)
                {
                    foreach (var e in daftdata)
                    {
                        if (e.AddressId == null)
                        {
                            //if (e.location_code != "")//มาจาก dealer
                            //{
                            //    data.RemoveAll(x => x.location_code == e.location_code && x.AddressTypeId == e.AddressTypeId);
                            //}
                            data.RemoveAll(x => x.AddressTypeId == e.AddressTypeId);
                            List<SubcontractProfileAddressModel> newaddr = new List<SubcontractProfileAddressModel>();
                            newaddr.Add(new SubcontractProfileAddressModel
                            {
                                AddressTypeId = e.AddressTypeId,
                                address_type_name = e.address_type_name,
                                Country = e.Country,
                                ZipCode = e.ZipCode,
                                HouseNo = e.HouseNo,
                                Moo = e.Moo,
                                VillageName = e.VillageName,
                                Building = e.Building,
                                Floor = e.Floor,
                                RoomNo = e.RoomNo,
                                Soi = e.Soi,
                                Road = e.Road,
                                SubDistrictId = e.SubDistrictId,
                                sub_district_name = e.sub_district_name,
                                DistrictId = e.DistrictId,
                                district_name = e.district_name,
                                ProvinceId = e.ProvinceId,
                                province_name = e.province_name,
                                RegionId = e.RegionId,
                                outFullAddress = e.outFullAddress,
                                location_code = e.location_code,
                                CompanyId = e.CompanyId
                            });
                            foreach (var r in SaveAddressSession(newaddr))
                            {
                                data.Add(r);
                            }

                        }
                        else
                        {

                            data.RemoveAll(x => x.AddressTypeId == e.AddressTypeId);
                            data.Add(e);
                        }

                    }

                }
                else
                {
                    data = SaveAddressSession(daftdata);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompany", data);
                return Json(new { response = data, status = true });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
            }

        }
        private List<SubcontractProfileAddressModel> SaveAddressSession(List<SubcontractProfileAddressModel> daftdata)
        {
            List<SubcontractProfileAddressModel> data = new List<SubcontractProfileAddressModel>();
            try
            {
                var outputprovince = new List<SubcontractProfileProvinceModel>();
                var outputdistrict = new List<SubcontractProfileDistrictModel>();
                var outputsubdistrict = new List<SubcontractProfileSubDistrictModel>();

                string uriprovice = string.Format("{0}", strpathAPI + "Province/GetAll");
                string uridistrict = string.Format("{0}", strpathAPI + "District/GetDistrictByProvinceId");
                string urisubdistrict = string.Format("{0}", strpathAPI + "SubDistrict/GetSubDistrictByDistrict");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                CultureInfo culture = CultureInfo.CurrentCulture;
                foreach (var e in daftdata)
                {
                    Guid addr_id = Guid.NewGuid();
                    e.AddressId = addr_id;


                    if (e.ProvinceId == 0)
                    {
                        response = client.GetAsync(uriprovice).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputprovince = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(v);
                            string[] s_provice = e.province_name.Split(" ");



                            if (culture.Name == "th")
                            {
                                var w = outputprovince.First(x => x.ProvinceNameTh.Contains(s_provice[1].ToString()));
                                e.ProvinceId = w.ProvinceId;
                                e.RegionId = w.RegionId;

                            }
                            else
                            {
                                var w = outputprovince.First(x => x.ProvinceNameEn.Contains(s_provice[1].ToString()));
                                e.ProvinceId = w.ProvinceId;
                                e.RegionId = w.RegionId;
                            }
                        }
                    }
                    if (e.DistrictId == 0)
                    {
                        response = client.GetAsync(uridistrict + "/" + HttpUtility.UrlEncode(e.ProvinceId.ToString(), Encoding.UTF8)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputdistrict = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                            string[] s_district = e.district_name.Split(" ");
                            if (culture.Name == "th")
                            {
                                var w = outputdistrict.First(d => d.DistrictNameTh.Contains(s_district[1].ToString()));
                                e.DistrictId = w.DistrictId;
                            }
                            else
                            {
                                var w = outputdistrict.First(d => d.DistrictNameEn.Contains(s_district[1].ToString()));
                                e.DistrictId = w.DistrictId;
                            }

                        }

                    }
                    if (e.SubDistrictId == 0)
                    {
                        response = client.GetAsync(urisubdistrict + "/" + HttpUtility.UrlEncode(e.DistrictId.ToString(), Encoding.UTF8)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputsubdistrict = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);

                            string[] s_subdistrict = e.sub_district_name.Split(" ");
                            if (culture.Name == "th")
                            {
                                var w = outputsubdistrict.First(d => d.SubDistrictNameTh.Contains(s_subdistrict[1].ToString()));
                                e.SubDistrictId = w.SubDistrictId;
                            }
                            else
                            {
                                var w = outputsubdistrict.First(d => d.SubDistrictNameEn.Contains(s_subdistrict[1].ToString()));
                                e.SubDistrictId = w.SubDistrictId;
                            }

                        }
                    }
                }

                data = daftdata;


            }
            catch (Exception e)
            {

                throw;
            }
            return data;
        }
        [HttpPost]
        public IActionResult DeleteDaftAddress(string AddressId)
        {
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany").ToList();

                data.RemoveAll(x => x.AddressId.ToString().Contains(AddressId));

                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompany", data);
                var data_delete = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany");
                if (data_delete == null)
                {
                    SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaftCompany");
                }
                return Json(new { response = data_delete, status = true });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
                throw;
            }

        }
        [HttpPost]
        public IActionResult GetAddressById(string addressID)
        {
            try
            {
                if (addressID != null && addressID != "")
                {
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftLocation");
                    var result = data.Where(x => x.AddressId.ToString() == addressID).FirstOrDefault();
                    return Json(new { response = result, status = true });
                }
                else
                {
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftLocation");
                    return Json(new { response = data, status = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
            }
            //var result = new SubcontractProfileAddressModel();
            //try
            //{
            //    HttpClient client = new HttpClient();
            //    client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //    string uriString = string.Format("{0}/{1}", strpathAPI + "Address/GetByAddressId", addressID);

            //    //string jj = JsonConvert.SerializeObject(input);

            //    HttpResponseMessage response = client.GetAsync(uriString).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var v = response.Content.ReadAsStringAsync().Result;
            //        result = JsonConvert.DeserializeObject<SubcontractProfileAddressModel>(v);
            //    }
            //    return Json(new { response = result, status = true, message= response.StatusCode });
            //}
            //catch (Exception e)
            //{
            //    return Json(new { message = e.Message, status = false });
            //    throw;
            //}

        }
        #endregion



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

                string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Location/SearchLocation"
                    , HttpUtility.UrlEncode(strCompanyId.ToString(), Encoding.UTF8)
                   , HttpUtility.UrlEncode(locationcode, Encoding.UTF8), "null", "null", "null", "null", "null");

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
                            string strdir = Path.Combine(destNAS + @"\SubContractProfile\", companyid, "Location", locationid);
                            if (!Directory.Exists(strdir))
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
                        using (output = System.IO.File.Create(this.GetPathAndFilename(locationid, filename, companyid, destNAS + @"\SubContractProfile\")))
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

        private string GetPathAndFilename(string locationid, string filename,string companyid,string dir)
        {
            string pathdir = Path.Combine(dir, companyid, "Location", locationid);
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
            var outputNAS = new List<SubcontractDropdownModel>();
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
                var path = this.GetPathAndFilename(locationid, filename, dataUser.companyid.ToString(), destNAS + @"\SubContractProfile\");
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
             
        }

        private async Task<bool> Deletefile(string locationid,string companyid)
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
                    string strdir = Path.Combine(destNAS + @"\SubContractProfile\", companyid, "Location", locationid);
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
            dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }
    }
}
