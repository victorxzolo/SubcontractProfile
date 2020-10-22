﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SubcontractProfile.Web.Extension;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using SubcontractProfile.Web.Model;
using Microsoft.EntityFrameworkCore.Internal;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Web;
using System.Net;

namespace SubcontractProfile.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string strpathAPI;
        private string strpathASCProfile;
        private string Lang = "";
        private Utilities Util = new Utilities();
        private SubcontractProfileUserModel dataUser = new SubcontractProfileUserModel();
        private string PathNas = "";
        private const int MegaBytes = 1024 * 1024;
        private const int TMegaBytes = 3 * 1024 * 1024;
        private readonly string strpathUpload;

        private readonly IStringLocalizer<AccountController> _localizer;

        private readonly ILogger<AccountController> _logger;
        public AccountController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor
             , IStringLocalizer<AccountController> localizer
            ,ILogger<AccountController> logger
            )
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _localizer = localizer;

            _logger = logger;

            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
       

            strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();

            //NSA
            // PathNas = _configuration.GetValue<string>("PathUploadfile:NAS").ToString();


            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();

        }

        [HttpPost]
        public IActionResult TestNAS()
        {
            string str = "";
            var output = new List<SubcontractDropdownModel>();



            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/nas_subcontract");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                }

                //string username = "nas_fixedbb";
                //string password = "Ais2018fixedbb";
                //string ipAddress = @"\\10.137.32.9";
                //string destNAS = @"\\10.137.32.9\fbb_idcard_ndev001b";

                string username = output[0].value1;
                string password = output[0].value2;
                string ipAddress = @"\\"+output[0].dropdown_value;
                string destNAS = output[0].dropdown_text;

                //string username = "PF0QMBH6";
                //string password = "1234";
                //string NAS = @"DESKTOP-MMCKBRE";
                //string destNAS = @"D:\NasPath";

                // _logger.LogInformation($"Start AccountController::TestNAS Start","");

                NetworkCredential sourceCredentials = new NetworkCredential { Domain = ipAddress, UserName = username, Password = password };
                using (new NetworkConnection(destNAS, sourceCredentials))
                {
                   // _logger.LogInformation($"Start AccountController::TestNAS PASS!!!", "");

                    string strdir = destNAS + @"\SubContractProfile" + @"\f2423a7a-ed2c-4c9b-b766-c37ada227b6d";
                    if (Directory.Exists(strdir))
                    {
                        //_logger.LogInformation($"Start AccountController::TestNAS Found!!!", "");
                        str = "Found";
                    }
                    else
                    {
                        //_logger.LogInformation($"Start AccountController::TestNAS NOT Found!!!", "");
                        Directory.CreateDirectory(destNAS + @"\SubContractProfile" + @"\f2423a7a-ed2c-4c9b-b766-c37ada227b6d");
                        str = "Create Success";
                    }
                }
            }
            catch (Exception e)
            {
               // _logger.LogError("Error AccountController::"+e.Message, "");
                str = e.Message;
            }
            return Json(str);
        }



        #region Lang

        //[HttpPost]
        //public IActionResult SetLanguage(string culture, string returnUrl)
        //{
        //    Response.Cookies.Append(
        //        CookieRequestCultureProvider.DefaultCookieName,
        //        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        //        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        //    );

        //    return LocalRedirect(returnUrl);
        //}


        //public IActionResult SetThai()
        //{
        //    var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("th-TH"));
        //    var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };

        //    Response.Cookies.Append("Web.Language", cookieValue, option);
        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult SetEnglish()
        //{
        //    var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-GB"));
        //    var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };

        //    Response.Cookies.Append("Web.Language", cookieValue, option);
        //    return RedirectToAction(nameof(Index));
        //}

        #endregion

        #region Login
        public IActionResult Login()
        {
            ViewBag.ReturnURL = "";
            //int dd = SiteSession.CurrentUICulture;
            //var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            //var culture = requestCultureFeature.RequestCulture.UICulture;


            return View();
        }

        public List<string> GetScreenConfig(string page)
        {
            List<string> test = new List<string>();
            if (SiteSession.CurrentUICulture == 1)
            {

            }
            else
            {

            }
            return test;
        }



        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            HttpContext.Session.Clear();
            ResponseModel res = new ResponseModel();
            string Url = "";

            if (ModelState.IsValid)
            {
                //if (Configurations.UseLDAP)
                //{
                //    var authenResultMessage = "";
                //    if (AuthenLDAP(model.username, model.password, out authenResultMessage))
                //    {
                //        var authenticatedUser = GetUser(model.username);
                //        //authenticatedUser.AuthenticateType = AuthenticateType.LDAP;
                //        //Response.AppendCookie(CreateAuthenticatedCookie(authenticatedUser.username));
                //        //base.CurrentUser = authenticatedUser;

                //        Url = "/Test/Dashboard";
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("", "Invalid UserName or Password.");
                //    }
                //}
                //else
                //{
                    // bypass authen
                if(model.username !=null && model.password !=null)
                {
                    string encryptedPassword = Util.EncryptText(model.password);
                    //string de = Util.DecryptText("f/bNopH92zWjB3G00w0dBw==");
                    var authenticatedUser = GetUser(model.username, encryptedPassword);
                    if(authenticatedUser != null)
                    {
                        if (!string.IsNullOrEmpty(authenticatedUser.Username))
                        {

                            res.Status = true;
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "userLogin", authenticatedUser);
                            Url = "/CompanyProfile/Index";

                            res.Status = true;
                            Lang = model.Language != null ? model.Language : "TH";
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "language", Lang);
                            //var str_L= SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "language");
                            //var datauser= SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

                            if (model.Language != null && model.Language != "")
                            {
                                SetLanguage(model.Language);
                            }
                            else
                            {
                                SetLanguage("th");
                            }
                        }
                        else
                        {
                            res.Status = false;
                            res.Message = "ชื่อผู้ใช้งานนี้ไม่มีในระบบ กรุณาลองใหม่อีกครับ";
                        }
                    }
                    else
                    {
                        res.Status = false;
                        res.Message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ตรงกันกับในระบบ กรุณาสมัครเพื่อใช้งาน";
                    }
                    
                    // string decrypted = Util.DecryptText(encrypted);
                }
                   
                    //if (null != authenticatedUser && authenticatedUser.ProgramModel != null)
                    //{
                    //    authenticatedUser.AuthenticateType = AuthenticateType.LDAP;
                    //    Response.AppendCookie(CreateAuthenticatedCookie(authenticatedUser.username));
                    //    base.CurrentUser = authenticatedUser;

                    //    return RedirectToAction("Index", "Home");
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("", model.username + " not found.");
                    //}

                   
                //}
            }
           // return RedirectToAction("Index", "CompanyProfile");
            return Json(new { redirecturl = Url ,Response= res });
        }

        private void SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

        }

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

        [HttpPost]
        public IActionResult SetLanguageToPage(string culture,string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
             return LocalRedirect(returnUrl);
        }

        public SubcontractProfileUserModel GetUser(string userName,string password)
        {
            SubcontractProfileUserModel authenticatedUser = new SubcontractProfileUserModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //string uriString = string.Format("{0}/{1}/{2}", strpathAPI + "User/LoginUser", userName, password);
            //HttpResponseMessage response = client.GetAsync(uriString).Result;

            var uriUser = new Uri(Path.Combine(strpathAPI, "User", "LoginUser"));

            var input = new SubcontractProfileUserModel();
            input.Username = userName;
            input.password = password;

            string rr = JsonConvert.SerializeObject(input);

            var httpContentUser = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(uriUser, httpContentUser).Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                authenticatedUser = JsonConvert.DeserializeObject<SubcontractProfileUserModel>(v);
            }

            return authenticatedUser;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //public  bool IsThaiCulture(this int currentCulture)
        //{
        //    if (currentCulture == 1)
        //        return true;

        //    return false;
        //}
        #endregion



        #region Register
        public IActionResult Register(string language = "th")
        {
            SetLanguage("th");

            ViewData["Controller"] = _localizer["Register"];
            ViewData["View"] = _localizer["Register"];

            #region Example GET
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            //string uriString = string.Format("{0}/{1}", strpathAPI + "v1.0/SubcontractProfileCompany/GetALL", 1);
            //HttpResponseMessage response = client.GetAsync(uriString).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var v = response.Content.ReadAsStringAsync().Result;
            //    var t = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
            //}
            #endregion


            #region Example POST
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            //var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.PostAsync(uri, httpContent).Result;
            #endregion

            #region Example PUT
            //HttpClient client = new HttpClient();
            //string uriString = string.Format("{0}{1}", uri, data.Id);
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            //var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.PutAsync(uriString, httpContent).Result;
            #endregion

            #region Example DELETE
            //HttpClient client = new HttpClient();
            //string uriString = string.Format("{0}{1}", uri, id);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = client.DeleteAsync(uriString).Result;
            #endregion


            #region GET SESSION

            getsession();

            #endregion

            return View();
        }

        [HttpPost]
        public IActionResult CheckUsername(string username)
        {
            bool status = true;
            string message = "";
            List<SubcontractProfileUserModel> L_user = new List<SubcontractProfileUserModel>();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "User/CheckUsername"
                    , HttpUtility.UrlEncode(username, Encoding.UTF8));
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    L_user = JsonConvert.DeserializeObject<List<SubcontractProfileUserModel>>(v);
                }
                if (L_user !=null && L_user.Count >0)
                {
                    status = false;
                    message = _localizer["MessageCheckUser"];

                }
            }
            catch (Exception e)
            {
                status = false;
                message = e.Message;
                throw;
            }
            return Json(new { Status = status, Message = message });
        }
        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if(Lang==null || Lang=="")
            {
                Lang = "TH";
            }
            dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }

        #region DDL And Checkbox
        [HttpPost]
        public IActionResult DDLsubcontract_profile_sub_district(int district_id = 0)
        {
            var output = new List<SubcontractProfileSubDistrictModel>();
            var resultZipCode = new List<SubcontractProfileSubDistrictModel>();

            List<SelectListItem> getAllSubDistrictList = new List<SelectListItem>();
            List<SelectListItem> getAllZipcodeList = new List<SelectListItem>();

            if (district_id != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "SubDistrict/GetSubDistrictByDistrict"
                    , HttpUtility.UrlEncode(district_id.ToString(), Encoding.UTF8));
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);
                    resultZipCode = output.GroupBy(c => c.ZipCode).Select(g => g.First()).ToList();
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "SubDistrict/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    if (v != null)
                    {
                        output = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);
                        resultZipCode = output.GroupBy(c => c.ZipCode).Select(g => g.First()).ToList();
                    }
                }
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
            if(culture.Name=="th")
            {
                output.Add(new SubcontractProfileSubDistrictModel
                {
                    SubDistrictId = 0,
                    SubDistrictNameTh = _localizer["SelectSubDistrict"]
                });

                getAllSubDistrictList = output.Select(a => new SelectListItem
                {
                    Text = a.SubDistrictNameTh,
                    Value = a.SubDistrictId.ToString()
                }).OrderBy(c => c.Value).ToList();



                getAllZipcodeList.Add(new SelectListItem
                {
                    Text = _localizer["SelectZipCode"],
                    Value = ""
                });
                getAllZipcodeList.AddRange(resultZipCode.Select(c => new SelectListItem
                {
                    Text = c.ZipCode,
                    Value = c.ZipCode
                }).ToList());
            }
            else
            {
                output.Add(new SubcontractProfileSubDistrictModel
                {
                    SubDistrictId = 0,
                    SubDistrictNameEn = _localizer["SelectSubDistrict"]
                });

                getAllSubDistrictList = output.Select(a => new SelectListItem
                {
                    Text = a.SubDistrictNameEn,
                    Value = a.SubDistrictId.ToString()
                }).OrderBy(c => c.Value).ToList();



                getAllZipcodeList.Add(new SelectListItem
                {
                    Text = _localizer["SelectZipCode"],
                    Value = ""
                });
                getAllZipcodeList.AddRange(resultZipCode.Select(c => new SelectListItem
                {
                    Text = c.ZipCode,
                    Value = c.ZipCode
                }).ToList());
            }
         

            return Json(new { responsesubdistrict = getAllSubDistrictList, responsezipcode = getAllZipcodeList });
        }
        [HttpPost]
        public IActionResult DDLsubcontract_profile_district(int province_id = 0)
        {
            var output = new List<SubcontractProfileDistrictModel>();
            List<SelectListItem> getAllDistrictList = new List<SelectListItem>();
            if (province_id != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "District/GetDistrictByProvinceId"
                    , HttpUtility.UrlEncode(province_id.ToString(), Encoding.UTF8));
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "District/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                }
            }

            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                output.Add(new SubcontractProfileDistrictModel
                {
                    DistrictId = 0,
                    DistrictNameTh = _localizer["SelectDistrict"]
                });
                getAllDistrictList = output.Select(a => new SelectListItem
                {
                    Text = a.DistrictNameTh,
                    Value = a.DistrictId.ToString()
                }).OrderBy(c => c.Value).ToList();
            }
           else
            {
                output.Add(new SubcontractProfileDistrictModel
                {
                    DistrictId = 0,
                    DistrictNameEn = _localizer["SelectDistrict"]
                });
                getAllDistrictList = output.Select(a => new SelectListItem
                {
                    Text = a.DistrictNameEn,
                    Value = a.DistrictId.ToString()
                }).OrderBy(c => c.Value).ToList();
            }
                
          



            return Json(new { responsedistricrt = getAllDistrictList });
        }
        [HttpPost]
        public IActionResult DDLsubcontract_profile_province(int region_id)
        {
            var output = new List<SubcontractProfileProvinceModel>();
            List<SelectListItem> getAllProvinceList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = region_id==0 ? string.Format("{0}", strpathAPI + "Province/GetAll"): 
                string.Format("{0}/{1}", strpathAPI + "Province/GetProvinceByRegionId", HttpUtility.UrlEncode(region_id.ToString(), Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(v);
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                output.Add(new SubcontractProfileProvinceModel
                {
                    ProvinceId = 0,
                    ProvinceNameTh = _localizer["SelectProvince"]
                });
                getAllProvinceList = output.Select(a => new SelectListItem
                {
                    Text = a.ProvinceNameTh,
                    Value = a.ProvinceId.ToString()
                }).OrderBy(c=>c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractProfileProvinceModel
                {
                    ProvinceId = 0,
                    ProvinceNameEn = _localizer["SelectProvince"]
                });
                getAllProvinceList = output.Select(a => new SelectListItem
                {
                    Text = a.ProvinceNameEn,
                    Value = a.ProvinceId.ToString()
                }).OrderBy(c => c.Value).ToList();

            }


            return Json(new { responseprovince = getAllProvinceList });
        }

        [HttpPost]
        public IActionResult DDLTitle()
        {
            var output = new List<SubcontractProfileCompanyTypeModel>();
            List<SelectListItem> outputTh = new List<SelectListItem>();
            List<SelectListItem> outputEn = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //string uriString = string.Format("{0}", strpathAPI + "Title/GetALL");
            string uriString = string.Format("{0}", strpathAPI + "CompanyType/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyTypeModel>>(v);
            }

            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                outputTh.Add(new SelectListItem
                {
                    Text = _localizer["ddlSelect"],
                    Value = "0"
                });
                outputTh.AddRange(output.Select(c => new SelectListItem
                {
                    Text = c.CompanyTypeNameTh,
                    Value = c.CompanyTypeId
                }).ToList());
            }
            else
            {
                outputEn.Add(new SelectListItem
                {
                    Text = _localizer["ddlSelect"],
                    Value = "0"
                });
                outputEn.AddRange(output.Select(c => new SelectListItem
                {
                    Text = c.CompanyTypeNameEn,
                    Value = c.CompanyTypeId
                }).ToList());
            }
            

            return Json(new { responsetitleTH = outputTh, responsetitleEN= outputEn });
        }

        [HttpPost]
        public IActionResult DDLNationality()
        {
            var output = new List<SubcontractProfileNationalityModel>();
            List<SelectListItem> outputTh = new List<SelectListItem>();
            List<SelectListItem> outputEn = new List<SelectListItem>();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Nationality/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileNationalityModel>>(v);
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                outputTh.Add(new SelectListItem
                {
                    Text = _localizer["ddlSelect"],
                    Value = "0"
                });
                outputTh.AddRange(output.Select(c => new SelectListItem
                {
                    Text = c.NationalityTh,
                    Value = c.NationalityId
                }).ToList());
            }
            else
            {
                outputEn.Add(new SelectListItem
                {
                    Text = _localizer["ddlSelect"],
                    Value = "0"
                });
                outputEn.AddRange(output.Select(c => new SelectListItem
                {
                    Text = c.NationalityEn,
                    Value = c.NationalityId
                }).ToList());
            }


            return Json(new { responseTH = outputTh, responseEN = outputEn });
        }

        [HttpPost]
        public IActionResult DDLsubcontract_profile_Region()
        {
            var output = new List<SubcontractProfileRegionModel>();
            List<SelectListItem> getAllRegionList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Region/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileRegionModel>>(v);
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                output.Add(new SubcontractProfileRegionModel
                {
                    RegionId = 0,
                    RegionName = _localizer["SelectZone"]
                });

               getAllRegionList = output.Select(a => new SelectListItem
                {
                    Text = a.RegionName,
                    Value = a.RegionId.ToString()
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractProfileRegionModel
                {
                    RegionId = 0,
                    RegionName = _localizer["SelectZone"]
                });
                getAllRegionList = output.Select(a => new SelectListItem
                {
                    Text = a.RegionName,
                    Value = a.RegionId.ToString()
                }).OrderBy(c => c.Value).ToList();

            }

            return Json(new { responseregion = getAllRegionList });
        }

        [HttpPost]
        public IActionResult DDLBank()
        {
            var output = new List<SubcontractProfileBankingModel>();
            List<SelectListItem> getAllBankList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Banking/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileBankingModel>>(v);
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                output.Add(new SubcontractProfileBankingModel
                {
                    BankCode = "0",
                    BankName = _localizer["SelectBankName"]
                });

                getAllBankList = output.Select(a => new SelectListItem
                {
                    Text = a.BankName,
                    Value = a.BankCode
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractProfileBankingModel
                {
                    BankCode = "0",
                    BankName = _localizer["SelectBankName"]
                });
                getAllBankList = output.Select(a => new SelectListItem
                {
                    Text = a.BankName,
                    Value = a.BankCode
                }).OrderBy(c => c.Value).ToList();

            }

            return Json(new { responsebank = getAllBankList });
        }

        [HttpPost]
        public IActionResult DDLAccountName()
        {
            var output = new List<SubcontractProfileCompanyTypeModel>();
            List<SelectListItem> getAllCompanyTypeList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "CompanyType/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyTypeModel>>(v);
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
                if (culture.Name == "th")
            {
                output.Add(new SubcontractProfileCompanyTypeModel
                {
                    CompanyTypeId = "0",
                    CompanyTypeNameTh = _localizer["SelectAccountName"]
                });

                getAllCompanyTypeList = output.Select(a => new SelectListItem
                {
                    Text = a.CompanyTypeNameTh,
                    Value = a.CompanyTypeId
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractProfileCompanyTypeModel
                {
                    CompanyTypeId = "0",
                    CompanyTypeNameEn = _localizer["SelectAccountName"]
                });
                getAllCompanyTypeList = output.Select(a => new SelectListItem
                {
                    Text = a.CompanyTypeNameEn,
                    Value = a.CompanyTypeId
                }).OrderBy(c => c.Value).ToList();

            }

            return Json(new { responsecompanytype = getAllCompanyTypeList });
        }

        //Checkbox
        [HttpPost]
        public IActionResult GetAddressType()
        {
            var output = new List<SubcontractProfileAddressTypeModel>();
            List<SelectListItem> getAllAddressTypeList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "AddressType/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileAddressTypeModel>>(v);
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
             if (culture.Name == "th")
            {
                getAllAddressTypeList = output.Select(a => new SelectListItem
                {
                    Text = a.AddressTypeNameTh,
                    Value = a.AddressTypeId
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                getAllAddressTypeList = output.Select(a => new SelectListItem
                {
                    Text = a.AddressTypeNameEn,
                    Value = a.AddressTypeId
                }).OrderBy(c => c.Value).ToList();
            }
            return Json(new { responseaddresstype = getAllAddressTypeList });
        }

        [HttpPost]
        public IActionResult GetDataBankAccountType()
        {
            var output = new List<SubcontractDropdownModel>();
            List<SelectListItem> getAllBankAccList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/bank_account_type");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
            }
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                output.Add(new SubcontractDropdownModel
                {
                    dropdown_text = _localizer["SelectAccountType"],
                    dropdown_value = ""

                });

                getAllBankAccList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractDropdownModel
                {
                    dropdown_text = _localizer["SelectAccountType"],
                    dropdown_value = ""
                });
                getAllBankAccList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();

            }
            return Json(new { response = getAllBankAccList });
        }

        #endregion



        #region Comment
        //[HttpPost]
        //public IActionResult SearchLocation(SearchSubcontractProfileLocationViewModel model)
        //{
        //    var result = new SubcontractProfileLocationSearchOutputModel();
        //    int filteredResultsCount;
        //    int totalResultsCount;

        //    var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);


        //    var take = model.length;
        //    var skip = model.start;

        //    string sortBy = "";
        //    bool sortDir = true;



        //    if (model.order != null)
        //    {
        //         in this example we just default sort on the 1st column
        //        sortBy = model.columns[model.order[0].column].data;
        //        sortDir = model.order[0].dir.ToLower() == "asc";
        //    }
        //    model.page_index = skip;
        //    model.page_size = take;
        //    model.sort_col = sortBy;
        //    model.sort_dir = sortDir ? "asc" : "desc";

        //    SearchSubcontractProfileLocationQueryModel query = new SearchSubcontractProfileLocationQueryModel();
        //    query.channel_sale_group = model.channel_sale_group;
        //    query.company_alias = model.company_alias;
        //    query.company_code = model.company_code;
        //    query.company_name_en = model.company_name_en;
        //    query.company_name_th = model.company_name_th;
        //    query.distribution_channel = model.distribution_channel;
        //    query.location_code = model.location_code;
        //    query.location_name_en = model.location_name_en;
        //    query.location_name_th = model.location_name_th;
        //    query.page_index = model.page_index;
        //    query.page_size = model.page_size;
        //    query.sort_col = model.sort_col;
        //    query.sort_dir = model.sort_dir;

        //    var rr = JsonConvert.SerializeObject(query);

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));

        //    string uriString = string.Format("{0}", strpathAPI + "Location/GetListLocation");
        //    var httpContentLocation = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = client.PostAsync(uriString, httpContentLocation).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var v = response.Content.ReadAsStringAsync().Result;
        //        result = JsonConvert.DeserializeObject<SubcontractProfileLocationSearchOutputModel>(v);
        //    }

        //    if (result != null)
        //    {
        //        filteredResultsCount = result.filteredResultsCount; //output from Database
        //        totalResultsCount = result.TotalResultsCount; //output from Database
        //        return Json(new
        //        {
        //            this is what datatables wants sending back
        //            draw = model.draw,
        //            recordsTotal = totalResultsCount,
        //            recordsFiltered = filteredResultsCount,
        //            data = result.ListResult
        //        });
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            draw = model.draw,
        //            recordsTotal = 0,
        //            recordsFiltered = 0,
        //            data = new List<SubcontractProfileLocationModel>()
        //        });
        //    }




        //}
        #endregion'

        [HttpPost]
        public IActionResult SearchLocation(SearchSubcontractProfileLocationViewModel model)
        {
           ASCProfileModel result = new ASCProfileModel();
            List<LocationListModel> locate = new List<LocationListModel>();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                //string u = "http://10.138.34.61:8080/phxPartner/v1/partner/ChannelASCProfile.json?filter=" +
                //    "(&(inSource=FBB)(inEvent=evPersonLocAddressInfo)(inASCCode=000375)(inASCMobileNo=)(inIdNo=)(inLocationCode=1000607)(inPersonType=ALL))";

                string uriString = string.Format(strpathASCProfile, "FBB", "evLocationInfo"
                    , HttpUtility.UrlEncode(model.asc_code, Encoding.UTF8)
                    , HttpUtility.UrlEncode(model.asc_mobile_no, Encoding.UTF8)
                    , HttpUtility.UrlEncode(model.id_Number, Encoding.UTF8)
                    , HttpUtility.UrlEncode(model.location_code, Encoding.UTF8)
                    , HttpUtility.UrlEncode(model.sap_code, Encoding.UTF8)
                    , HttpUtility.UrlEncode(model.user_id, Encoding.UTF8));


                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<ASCProfileModel>(v);
                    if(result.outStatus== "0000")
                    {
                        foreach (var e in result.resultData)
                        {
                            foreach(var r in e.LocationList)
                            {
                                Guid l_id = Guid.NewGuid();
                                locate.Add(new LocationListModel {
                                    outTitle=r.outTitle,
                                    outCompanyName = r.outCompanyName,
                                    outPartnerName = r.outPartnerName,
                                    outCompanyShortName = r.outCompanyShortName,
                                    outTaxId = r.outTaxId,
                                    outWTName = r.outWTName,
                                    outDistChn = r.outDistChn,
                                    outChnSales = r.outChnSales,
                                    outType = r.outType,
                                    outSubType = r.outSubType,
                                    outBusinessType = r.outBusinessType,
                                    outCharacteristic = r.outCharacteristic,
                                    outLocationCode = r.outLocationCode,
                                    outLocationName = r.outLocationName,
                                    outShopArea = r.outShopArea,
                                    outShopType = r.outShopType,
                                    outOperatorClass = r.outOperatorClass,
                                    outLocationPhoneNo = r.outLocationPhoneNo,
                                    outLocationFax = r.outLocationFax,
                                    outContractName = r.outContractName,
                                    outContractPhoneNo = r.outContractPhoneNo,
                                    outLocationStatus = r.outLocationStatus,
                                    outRetailShop = r.outRetailShop,
                                    outBusinessRegistration = r.outBusinessRegistration,
                                    outVatType = r.outVatType,
                                    outEffectiveDt = r.outEffectiveDt,
                                    outIdType = r.outIdType,
                                    outHQFlag = r.outHQFlag,
                                    outChnName = r.outChnName,
                                    outSAPVendorCode = r.outSAPVendorCode,
                                    outMobileForService = r.outMobileForService,
                                    outLocationRegion = r.outLocationRegion,
                                    outLocationSubRegion = r.outLocationSubRegion,
                                    outPaymentChannelCode = r.outPaymentChannelCode,
                                    outPaymentChannelName = r.outPaymentChannelName,
                                    outLocationRemark = r.outLocationRemark,
                                    outBusinessName = r.outBusinessName,
                                    outPubid = r.outPubid,
                                    outLocationAbbr = r.outLocationAbbr,
                                    addressLocationList=r.addressLocationList,
                                    SAPCustomerList=r.SAPCustomerList,
                                    ASCList=r.ASCList,
                                    location_id= l_id.ToString()
                                });

                               

                            }
                           

                        }
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "LocationDealer", locate);
                    }
                    return Json(new
                    {
                        draw = model.draw,
                        recordsTotal = locate.Count(),
                        recordsFiltered = locate.Count(),
                        data = locate
                    });

                }
                else
                {
                    return Json(new
                    {
                        draw = model.draw,
                        recordsTotal = 0,
                        recordsFiltered = 0,
                        data = locate
                    });
                }

               
            }
            catch (Exception e)
            {

                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = locate
                });
                throw;
            }
        }

        [HttpPost]
        public IActionResult GetLocationSession(string location_id)
        {
            List<LocationListModel> resultLocation = new List<LocationListModel>();
            ResponseModel res = new ResponseModel();
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<LocationListModel>>(HttpContext.Session, "LocationDealer");
                if(data !=null)
                {
                    resultLocation = data.Where(x => x.location_id == location_id).ToList();
                    res.Status = true;
                }
               
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
                throw;
            }
            return Json(new
            {
                LocationListModel = resultLocation,
                Response=res

            });
        }

        [HttpPost]
        public IActionResult GetRevenue(SearchVATModel model)
        {
            List<VATModal> ListResult = new List<VATModal>();
            VATModal Vmodel = new VATModal();


            int filteredResultsCount=0;
            int totalResultsCount=0;
            var take = model.length;
            var skip = model.start; 
            string sortBy = "";
            bool sortDir = true;

            try
            {

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower() == "asc";
                }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                string uriString = string.Format("{0}/{1}", strpathAPI + "VATService/Get", HttpUtility.UrlEncode(model.tIN, Encoding.UTF8));
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    var outputresponse = JsonConvert.DeserializeObject<VATResultModel>(v);
                    if(outputresponse.StatusCode=="200")
                    {
                        if(outputresponse.Value !=null)
                        {
                            string straddr = "";
                             straddr= string.Concat( outputresponse.Value.vHouseNumber != "-" ? outputresponse.Value.vHouseNumber : "" , " " ,
                                                       outputresponse.Value.vBuildingName != "-" ? _localizer["Building"]+" " + outputresponse.Value.vBuildingName : "" , " " ,
                                                       outputresponse.Value.vFloorNumber != "-" ? _localizer["Floor"] + " " + outputresponse.Value.vFloorNumber : "" , " " ,
                                                       outputresponse.Value.vRoomNumber != "-" ? _localizer["Room"] + " " + outputresponse.Value.vRoomNumber : "" , " " ,
                                                       outputresponse.Value.vVillageName != "-" ? _localizer["Village"] + " " + outputresponse.Value.vVillageName : "" , " " ,
                                                       outputresponse.Value.vMooNumber != "-" ? _localizer["Moo"] + " " + outputresponse.Value.vMooNumber : "" , " " ,
                                                       outputresponse.Value.vSoiName != "-" ? _localizer["Soi"] + " " + outputresponse.Value.vSoiName : "" , " " ,
                                                       outputresponse.Value.vStreetName != "-" ? _localizer["Street"] + " " + outputresponse.Value.vStreetName : "" , " " ,
                                                       outputresponse.Value.vThambol != "-" ? _localizer["SubDistrict"] + " " + outputresponse.Value.vThambol : "" , " " ,
                                                       outputresponse.Value.vAmphur != "-" ? _localizer["District"] + " " + outputresponse.Value.vAmphur : "" ," " ,
                                                       outputresponse.Value.vProvince != "-" ? _localizer["province"] + " " + outputresponse.Value.vProvince : "" , " " ,
                                                       outputresponse.Value.vPostCode != "-" ? outputresponse.Value.vPostCode : "");
                            ListResult.Add(new VATModal {
                                vAmphur = outputresponse.Value.vAmphur,
                                vBranchName = outputresponse.Value.vBranchName,
                                vBranchNumber = outputresponse.Value.vBranchNumber,
                                vBranchTitleName = outputresponse.Value.vBranchTitleName,
                                vBuildingName = outputresponse.Value.vBuildingName,
                                vBusinessFirstDate = outputresponse.Value.vBusinessFirstDate,
                                vFloorNumber = outputresponse.Value.vFloorNumber,
                                vHouseNumber = outputresponse.Value.vHouseNumber,
                                vMooNumber = outputresponse.Value.vMooNumber,
                                vmsgerr = outputresponse.Value.vmsgerr,
                                vName = outputresponse.Value.vName,
                                vNID = outputresponse.Value.vNID,
                                vPostCode = outputresponse.Value.vPostCode,
                                vProvince = outputresponse.Value.vProvince,
                                vRoomNumber = outputresponse.Value.vRoomNumber,
                                vSoiName = outputresponse.Value.vSoiName,
                                vStreetName = outputresponse.Value.vStreetName,
                                vSurname = outputresponse.Value.vSurname,
                                vThambol = outputresponse.Value.vThambol,
                                vtin = outputresponse.Value.vtin,
                                vtitleName = outputresponse.Value.vtitleName,
                                vVillageName = outputresponse.Value.vVillageName,
                                outConcataddr=straddr
                            });


                            if (sortDir) //asc
                            {
                                if (sortBy == "vBranchName")
                                {
                                    ListResult = ListResult.OrderBy(c => c.vBranchName).ToList();
                                }
                                else if (sortBy == "vBranchNumber")
                                {
                                    ListResult = ListResult.OrderBy(c => c.vBranchNumber).ToList();
                                }
                            }
                            else //desc
                            {
                                if (sortBy == "vBranchName")
                                {
                                    ListResult = ListResult.OrderByDescending(c => c.vBranchName).ToList();
                                }
                                else if (sortBy == "vBranchNumber")
                                {
                                    ListResult = ListResult.OrderByDescending(c => c.vBranchNumber).ToList();
                                }
                            }

                            filteredResultsCount = ListResult.Count();
                            totalResultsCount = ListResult.Count();
                        }
                    }
                    
                }
               
            }
            catch (Exception e)
            {
                throw;
            }

            return Json(new
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = ListResult
            });
        }






        #region DaftAddress Register
        [HttpPost]
        public IActionResult SaveDaftAddress(List<SubcontractProfileAddressModel> daftdata)
        {
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");
                if (data != null && data.Count != 0)
                {
                    foreach(var e in daftdata)
                    {
                        if(e.AddressId ==null)
                        {
                            if (e.location_code != "")//มาจาก dealer
                            {
                                data.RemoveAll(x => x.location_code == e.location_code && x.AddressTypeId == e.AddressTypeId);
                            }
                            List<SubcontractProfileAddressModel> newaddr = new List<SubcontractProfileAddressModel>();
                                newaddr.Add(new SubcontractProfileAddressModel {
                                      AddressTypeId=e.AddressTypeId,
                                      address_type_name=e.address_type_name,
                                      Country = e.Country,
                                      ZipCode = e.ZipCode,
                                      HouseNo = e.HouseNo,
                                      Moo=e.Moo,
                                      VillageName=e.VillageName,
                                      Building=e.Building,
                                      Floor=e.Floor,
                                      RoomNo=e.RoomNo,
                                      Soi=e.Soi,
                                      Road=e.Road,
                                      SubDistrictId=e.SubDistrictId,
                                      sub_district_name=e.sub_district_name,
                                      DistrictId=e.DistrictId,
                                      district_name=e.district_name,
                                      ProvinceId=e.ProvinceId,
                                      province_name=e.province_name,
                                      RegionId=e.RegionId,
                                      outFullAddress=e.outFullAddress,
                                      location_code=e.location_code
                                });
                            foreach(var r in SaveAddressSession(newaddr))
                            {
                                data.Add(r);
                            }
                            
                        }
                        else
                        {
                            data.RemoveAll(x => x.AddressId == e.AddressId);
                            data.Add(e);
                        }
                          
                    }
                    
                }
                else
                {
                  data=  SaveAddressSession(daftdata);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaft", data);
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
        public IActionResult GetDaftAddress(string AddressId)
        {
            try
            {
                if (AddressId != null && AddressId != "")
                {
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");
                    data = data.Where(x => x.AddressId.ToString() == AddressId).ToList();
                    return Json(new { response = data, status = true });
                }
                else
                {
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");
                    return Json(new { response = data, status = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
            }

        }
        [HttpPost]
        public IActionResult DeleteDaftAddress(string AddressId)
        {
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft").ToList();

                data.RemoveAll(x => x.AddressId.ToString().Contains(AddressId));

                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaft", data);
                var data_delete = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");
                if (data_delete == null)
                {
                    SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaft");
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
        public IActionResult SearchAddress(DataTableAjaxModel model)
        {
            try
            {

                int filteredResultsCount;
                int totalResultsCount;

                //var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

                //Guid id = Guid.NewGuid();


                var take = model.length;
                var skip = model.start;

                string sortBy = "";
                bool sortDir = true;

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower() == "asc";
                }

                List<SubcontractProfileAddressModel> result = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");
                if (result != null && result.Count != 0)
                {
                    foreach (var a in result)
                    {
                        string straddr = "";
                        straddr = string.Concat(a.HouseNo != null && a.HouseNo != "" ? a.HouseNo : "", " ",
                                                  a.Building != null && a.Building != "" ? _localizer["Building"] + " " + a.Building : "", " ",
                                                  a.Floor != null && a.Floor != "" ? _localizer["Floor"] + " " + a.Floor : "", " ",
                                                  a.RoomNo != null && a.RoomNo != "" ? _localizer["Room"] + " " + a.RoomNo : "", " ",
                                                  a.VillageName != null && a.VillageName != "" ? _localizer["Village"] + " " + a.VillageName : "", " ",
                                                  a.Moo != null ? _localizer["Moo"] + " " + a.Moo : "", " ",
                                                  a.Soi != null && a.Soi != "" ? _localizer["Soi"] + " " + a.Soi : "", " ",
                                                  a.Road != null && a.Road != "" ? _localizer["Street"] + " " + a.Road : "", " ",
                                                  a.SubDistrictId != 0 ? _localizer["SubDistrict"] + " " + a.sub_district_name : "", " ",
                                                  a.DistrictId != 0 ? _localizer["District"] + " " + a.district_name : "", " ",
                                                  a.ProvinceId != 0 ? _localizer["province"] + " " + a.province_name : "", " ",
                                                  a.ZipCode != "" ? a.ZipCode : "");
                        a.outFullAddress = straddr;
                    }
                    if (sortDir) //asc
                    {
                        if (sortBy == "address_type")
                        {
                            result = result.OrderBy(c => c.address_type_name).ToList();
                        }
                        else if (sortBy == "address")
                        {
                            result = result.OrderBy(c => c.HouseNo).ToList();
                        }
                    }
                    else //desc
                    {
                        if (sortBy == "address_type")
                        {
                            result = result.OrderByDescending(c => c.address_type_name).ToList();
                        }
                        else if (sortBy == "address")
                        {
                            result = result.OrderByDescending(c => c.HouseNo).ToList();
                        }
                    }

                    filteredResultsCount = result.Count(); //output from Database
                    totalResultsCount = result.Count(); //output from Database
                }
                else
                {
                    filteredResultsCount = 0; //output from Database
                    totalResultsCount = 0; //output from Database
                }

                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = result
                });
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> NewRegister(SubcontractProfileCompanyModel model)
        {
            bool resultGetFile = true;
            ResponseModel res = new ResponseModel();
           string strredirecturl = "/Account/Login";
            List<SubcontractProfileUserModel> L_user = new List<SubcontractProfileUserModel>();
            try
            {
                if (ModelState.IsValid)
                {
                    #region Check Username Duplicate
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    string uriStringUser = string.Format("{0}/{1}", strpathAPI + "User/CheckUsername"
                        , HttpUtility.UrlEncode(model.User_name, Encoding.UTF8));
                    HttpResponseMessage response = client.GetAsync(uriStringUser).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var v = response.Content.ReadAsStringAsync().Result;
                        L_user = JsonConvert.DeserializeObject<List<SubcontractProfileUserModel>>(v);
                    }
                    if (L_user != null && L_user.Count > 0)
                    {
                        res.Status = false;
                        res.Message = _localizer["MessageCheckUser"];
                        res.StatusError = "-1";
                    }
                    else
                    {
                        Guid companyId = Guid.NewGuid();
                        model.CompanyId = companyId;
                        model.CreateDate = DateTime.Now;
                        model.CreateBy = "SYSTEM";

                        //var dataUploadfile = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaft");

                        if (model.FileCompanyCertified != null)
                        {
                            #region Copy File to server
                            //foreach (var e in dataUploadfile)
                            //{
                            //    resultGetFile = await GetFile(e, model.CompanyId.ToString());

                            //    string filename = ContentDispositionHeaderValue.Parse(e.ContentDisposition).FileName.Trim('"');
                            //    filename = EnsureCorrectFilename(filename);

                            //    switch (e.typefile)
                            //    {
                            //        case "CompanyCertifiedFile":
                            //            model.CompanyCertifiedFile = filename;
                            //            break;
                            //        case "CommercialRegistrationFile":
                            //            model.CommercialRegistrationFile = filename;
                            //            break;
                            //        case "VatRegistrationCertificateFile":
                            //            model.VatRegistrationCertificateFile = filename;
                            //            break;
                            //    }
                            //}
                            #endregion

                            resultGetFile= await Uploadfile(model.FileCompanyCertified, model.CompanyId.ToString());
                            model.CompanyCertifiedFile = model.FileCompanyCertified.FileName;

                        }
                        if (model.FileCommercialRegistration != null)
                        {
                            resultGetFile= await Uploadfile(model.FileCommercialRegistration, model.CompanyId.ToString());
                            model.CommercialRegistrationFile = model.FileCommercialRegistration.FileName;

                        }
                        if (model.FileVatRegistrationCertificate != null)
                        {
                            resultGetFile= await Uploadfile(model.FileVatRegistrationCertificate, model.CompanyId.ToString());
                            model.VatRegistrationCertificateFile = model.FileVatRegistrationCertificate.FileName;

                        }

                        if (resultGetFile)
                        {
                            //SessionHelper.RemoveSession(HttpContext.Session, "userUploadfileDaft");

                            #region Insert Company

                            //if(model.SubcontractProfileType== "NewSubContract")
                            //{
                            string encrypted = Util.EncryptText(model.Password);
                            model.Password = encrypted;
                            //}


                            model.Status = "Pending";




                            var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "Insert"));
                            HttpClient clientCompany = new HttpClient();
                            clientCompany.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                            var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                            HttpResponseMessage responseCompany = clientCompany.PostAsync(uriCompany, httpContentCompany).Result;
                            if (responseCompany.IsSuccessStatusCode)
                            {
                                #region Insert Address
                                var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");

                                if (dataaddr != null && dataaddr.Count != 0)
                                {
                                    SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaft");

                                    foreach (var d in dataaddr)
                                    {
                                        SubcontractProfileAddressModel addr = new SubcontractProfileAddressModel();
                                        addr.AddressId = d.AddressId;
                                        addr.AddressTypeId = d.AddressTypeId;
                                        addr.Building = d.Building;
                                        addr.City = d.City;
                                        addr.Country = d.Country;
                                        addr.DistrictId = d.DistrictId;
                                        addr.Floor = d.Floor;
                                        addr.HouseNo = d.HouseNo;
                                        addr.Moo = d.Moo;
                                        addr.ProvinceId = d.ProvinceId;
                                        addr.CompanyId = companyId.ToString();
                                        addr.CreateBy = dataUser.UserId.ToString();
                                        addr.CreateDate = DateTime.Now;
                                        addr.RegionId = d.RegionId;
                                        addr.Road = d.Road;
                                        addr.Soi = d.Soi;
                                        addr.RoomNo = d.RoomNo;
                                        addr.SubDistrictId = d.SubDistrictId;
                                        addr.VillageName = d.VillageName;
                                        addr.ZipCode = d.ZipCode;

                                        var uriAddress = new Uri(Path.Combine(strpathAPI, "Address", "Insert"));
                                        HttpClient clientAddress = new HttpClient();
                                        clientAddress.DefaultRequestHeaders.Accept.Add(
                                        new MediaTypeWithQualityHeaderValue("application/json"));

                                        // string rr = JsonConvert.SerializeObject(addr);

                                        var httpContent = new StringContent(JsonConvert.SerializeObject(addr), Encoding.UTF8, "application/json");
                                        HttpResponseMessage responseAddress = clientAddress.PostAsync(uriAddress, httpContent).Result;
                                        if (responseAddress.IsSuccessStatusCode)
                                        {

                                            res.Status = true;
                                            res.Message = _localizer["MessageRegisSuccess"];
                                            res.StatusError = "0";
                                        }
                                        else
                                        {
                                            res.Status = false;
                                            res.Message = _localizer["MessageAddresUnSuccess"];
                                            res.StatusError = "-1";
                                        }
                                    }
                                }
                                else
                                {
                                    res.Status = false;
                                    res.Message = _localizer["MessageAddresUnSuccess"];
                                    res.StatusError = "-1";
                                }
                                #endregion
                            }
                            else
                            {
                                res.Status = false;
                                res.Message = _localizer["MessageUnSuccess"];
                                res.StatusError = "-1";
                            }
                        }
                        else
                        {
                            res.Status = false;
                            res.Message = _localizer["MessageUnSuccess"];
                            res.StatusError = "-1";
                        }


                        #endregion
                    }
                    #endregion
                }
                    else
                {
                    res.Status = false;
                    res.Message = _localizer["MessageUnSuccess"];
                    res.StatusError = "-1";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
                res.StatusError = "-1";

               
                throw;
            }

            return Json(new { Response=res, redirecturl=strredirecturl });
        }


        #region Upload File

        //[HttpPost]
        //public IActionResult TestNAS()
        //{
        //    string str = "";
        //    try
        //    {
        //        using (var impersonator = new Impersonator("nas_fixedbb", "Ais2018fixedbb", "\\10.137.32.9", false))
        //        {
        //            str = "Connect";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        str = e.Message;
        //    }
        //    return Json(str);
        //}


        //[HttpPost]
        //[DisableRequestSizeLimit]
        //public IActionResult Uploadfile(IList<IFormFile> files, string fid,string type_file)
        //{
        //    bool statusupload = true;
        //    List<FileUploadModal> L_File = new List<FileUploadModal>();
        //    //FileStream output;
        //    string strmess = "";
        //    try
        //    {
        //        foreach (FormFile source in files)
        //        {
        //            if (source.Length > 0)
        //            {
        //                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
        //                filename = EnsureCorrectFilename(filename);
        //                //using (output = System.IO.File.Create(this.GetPathAndFilename(filename)))
        //                //    await source.CopyToAsync(output);

        //                if (
        //                    source.ContentType.ToLower() != "image/jpg" &&
        //                    source.ContentType.ToLower() != "image/jpeg" &&
        //                    source.ContentType.ToLower() != "image/pjpeg" &&
        //                    source.ContentType.ToLower() != "image/gif" &&
        //                    source.ContentType.ToLower() != "image/png" &&
        //                    source.ContentType.ToLower() != "image/bmp" &&
        //                    source.ContentType.ToLower() != "image/tiff" &&
        //                    source.ContentType.ToLower() != "image/tif" &&
        //                    source.ContentType.ToLower() != "application/pdf"
        //                    )
        //                {
        //                    statusupload = false;
        //                    strmess = _localizer["MessageUploadmissmatch"];
        //                }
        //                else
        //                {
        //                    var fileSize = source.Length;
        //                    if (fileSize > MegaBytes)
        //                    {
        //                        statusupload = false;
        //                        strmess = _localizer["MessageUploadtoolage"];
        //                    }
        //                    else
        //                    {
        //                        Guid id = Guid.NewGuid();
        //                        using (var ms = new MemoryStream())
        //                        {
        //                            source.CopyTo(ms);
        //                            var fileBytes = ms.ToArray();
        //                            L_File.Add(new FileUploadModal
        //                            {
        //                                file_id = id,
        //                                Fileupload = fileBytes,
        //                                typefile = type_file,
        //                                ContentDisposition = source.ContentDisposition,
        //                                ContentType = source.ContentType,
        //                                Filename = filename
        //                            });
        //                        }
        //                        var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaft");
        //                        //byte[] byteArrayValue = HttpContext.Session.Get("userUploadfileDaft");
        //                        //var data = FromByteArray<List<FileUploadModal>>(byteArrayValue);

        //                        //var objComplex = HttpContext.Session.GetObject("userUploadfileDaft");

        //                        if (data != null)
        //                        {

        //                            data.RemoveAll(x => x.file_id.ToString() == fid);
        //                            data.Add(L_File[0]);
        //                            SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", data);

        //                        }
        //                        else
        //                        {
        //                            // HttpContext.Session.Set("userUploadfileDaft", ToByteArray<List<FileUploadModal>>(L_File));

        //                            SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", L_File);
        //                        }

        //                        strmess = _localizer["MessageUploadSuccess"];
        //                    }

        //                }

        //            }



        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        statusupload = false;
        //        strmess = e.Message.ToString();
        //        throw;
        //    }


        //    return Json(new { status = statusupload, message = strmess, response = (statusupload ? L_File[0].file_id.ToString() : "") });

        //   // return Json(new { status = statusupload, message = strmess });
        //}

        //private async Task<bool> GetFile(FileUploadModal file,string guid)
        //{
        //    FileStream output;
        //    try
        //    {
        //        var stream = new MemoryStream(file.Fileupload);
        //        FormFile files = new FormFile(stream, 0, file.Fileupload.Length, "name", "fileName");

        //        string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        filename = EnsureCorrectFilename(file.Filename);

        //        //var adminToken = new IntPtr();

        //        //using (var impersonator = new Impersonator("nas_fixedbb", "Ais2018fixedbb", "\\10.137.32.9", false))
        //        //{
        //        //using (output = System.IO.File.Create(this.GetPathAndFilename(guid, filename, PathNas)))
        //        using (output = System.IO.File.Create(this.GetPathAndFilename(guid, filename, _configuration.GetValue<string>("PathUploadfile:Local").ToString())))
        //            {
        //                await files.CopyToAsync(output);
        //            }
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //        throw;
        //    }
        //    return true;
        //}

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult CheckFileUpload(IFormFile files)
        {
            bool statusupload = true;
            string strmess = "";
            Guid? fid = null;
            try
            {
                if (files != null)
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

        private async Task<bool> Uploadfile(IFormFile files, string companyid)
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
                            string strdir = destNAS + @"\SubContractProfile\" + companyid;
                            if (!Directory.Exists(strdir))
                            {
                                Directory.CreateDirectory(strdir);
                            }

                        }
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        using (output = System.IO.File.Create(this.GetPathAndFilename(companyid, filename, destNAS + @"\SubContractProfile\")))
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

     
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string guid, string filename,string dir)
        {
           // string dir=_configuration.GetValue<string>("PathUploadfile:Local").ToString();
            string pathdir = Path.Combine(dir, guid);
            string PathOutput = "";
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
            PathOutput = Path.Combine(pathdir, filename);
            return PathOutput;
        }

        #endregion


        #endregion



    }

    #region Login


   

    public class SiteSession
    {
        public static int CurrentUICulture
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "th-TH")
                    return 1;
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    return 2;
                else
                    return 0;
            }
            set
            {
                if (value == 1)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", "th", "TH"));
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", "th", "TH"));
                }
                else if (value == 2)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", "en", "US"));
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", "en", "US"));
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                }

                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }

        public static int LatestUICulture { get; set; }
    }

    #endregion

}
