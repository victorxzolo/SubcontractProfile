using System;
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
        public AccountController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
       

            strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();
        }

        #region Login
        public IActionResult Login()
        {
            // SubcontractProfileCompanyBLL aa = new SubcontractProfileCompanyBLL();
            ViewBag.ReturnURL = "";
            int dd = SiteSession.CurrentUICulture;
            //SubcontractProfileCompany test = new SubcontractProfileCompany();
            //test.CompanyId = Guid.NewGuid();

            //var aa = _profileCompRepo.GetAll();

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
                    var authenticatedUser = GetUser(model.username, encryptedPassword);
                    if(!string.IsNullOrEmpty(authenticatedUser.Username))
                    {
                        res.Status = true;
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userLogin", authenticatedUser);
                        Url = "/CompanyProfile/Index";

                        res.Status = true;
                        Lang = model.Language != null ? model.Language : "TH";
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "language", Lang);
                        //var str_L= SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "language");
                        //var datauser= SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
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

            return Json(new { redirecturl = Url ,Response= res });
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

            var httpContentUser = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(uriUser, httpContentUser).Result;

            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                authenticatedUser = JsonConvert.DeserializeObject<SubcontractProfileUserModel>(v);
            }

            return authenticatedUser;
        }

        //public bool AuthenLDAP(string userName, string password, out string authenMessage)
        //{
        //    var authLDAPQuery = new LoginModel
        //    {
        //        username = userName,
        //        password = password,
        //    };

        //    //var authenLDAPResult = _QueryProcessor.Execute(authLDAPQuery);
        //    var authenLDAPResult = true;
        //    authenMessage = "";
        //    return authenLDAPResult;
        //}

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
        public IActionResult Register(string language = "TH")
        {
            ViewData["Controller"] = "Register";
            ViewData["View"] = "Register";

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

        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
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

                string uriString = string.Format("{0}/{1}", strpathAPI + "SubDistrict/GetSubDistrictByDistrict", district_id);
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
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);
                    resultZipCode = output.GroupBy(c => c.ZipCode).Select(g => g.First()).ToList();
                }
            }
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {

                output.Add(new SubcontractProfileSubDistrictModel
                {
                    SubDistrictId = 0,
                    SubDistrictNameTh = "กรุณาเลือกแขวง/ตำบล"
                });

                getAllSubDistrictList = output.Select(a => new SelectListItem
                {
                    Text = a.SubDistrictNameTh,
                    Value = a.SubDistrictId.ToString()
                }).OrderBy(c => c.Value).ToList();

               
                getAllZipcodeList = resultZipCode.Select(c => new SelectListItem
                {
                    Text = c.ZipCode,
                    Value = c.ZipCode
                }).ToList();
                getAllZipcodeList.Add(new SelectListItem
                {
                    Text = "กรุณาเลือกรหัสไปรษณีย์",
                    Value = "0"
                });
                getAllZipcodeList.OrderBy(d => d.Value).ToList();
            }
            else
            {

                output.Add(new SubcontractProfileSubDistrictModel
                {
                    SubDistrictId = 0,
                    SubDistrictNameTh = "Select Sub District"
                });
                getAllSubDistrictList = output.Select(a => new SelectListItem
                {
                    Text = a.SubDistrictNameEn,
                    Value = a.SubDistrictId.ToString()
                }).OrderBy(c => c.Value).ToList();

                
                getAllZipcodeList = resultZipCode.Select(c => new SelectListItem
                {
                    Text = c.ZipCode,
                    Value = c.ZipCode
                }).ToList();
                getAllZipcodeList.Add(new SelectListItem
                {
                    Text = "Select Zip Code",
                    Value = "0"
                });
                getAllZipcodeList.OrderBy(d => d.Value).ToList();
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

                string uriString = string.Format("{0}/{1}", strpathAPI + "District/GetDistrictByProvinceId", province_id);
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
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileDistrictModel
                {
                    DistrictId = 0,
                    DistrictNameTh = "กรุณาเลือกเขต/อำเภอ"
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
                    DistrictNameTh = "Select District"
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

            string uriString = region_id==0 ? string.Format("{0}", strpathAPI + "Province/GetAll"): string.Format("{0}/{1}", strpathAPI + "Province/GetProvinceByRegionId", region_id);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(v);
            }
            if(Lang =="")
            {
                getsession();
            }

            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileProvinceModel
                {
                    ProvinceId = 0,
                    ProvinceNameTh = "กรุณาเลือกจังหวัด"
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
                    ProvinceNameTh = "Select Province"
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


            return Json(new { responsetitle = output });
        }

        [HttpPost]
        public IActionResult DDLNationality()
        {
            var output = new List<SubcontractProfileNationalityModel>();
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



            return Json(new { response = output });
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
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileRegionModel
                {
                    RegionId = 0,
                    RegionName = "กรุณาเลือกภาค"
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
                    RegionName = "Select Region"
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
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileBankingModel
                {
                    BankCode = "0",
                    BankName = "กรุณาเลือกชื่อธนาคาร"
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
                    BankName = "Select Bank"
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
        public IActionResult DDLCompanyType()
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
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileCompanyTypeModel
                {
                    CompanyTypeId = "0",
                    CompanyTypeNameTh = "กรุณาเลือกประเภทธุรกิจ"
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
                    CompanyTypeNameEn = "Select Bank"
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
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
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

                //string uriString = string.Format("http://10.138.34.60:8080/phxPartner/v1/partner/ChannelASCProfile.json?filter=(&(inSource={0})(inEvent={1})" +
                //                                 "(inASCCode={2})(inASCMobileNo={3})(inIdNo={4})(inLocationCode={5})(inSAPCode={6})(inUserID={7}))"
                //                                , "FBB", "evLocationInfo",model.asc_code,model.asc_mobile_no,model.id_Number,model.location_code,model.sap_code,
                //                                model.user_id);
                string uriString = string.Format(strpathASCProfile, "FBB", "evLocationInfo", model.asc_code, model.asc_mobile_no, model.id_Number
                                            , model.location_code, model.sap_code, model.user_id);
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
                string uriString = string.Format("{0}/{1}", strpathAPI + "VATService/Get", model.tIN);
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
                                                       outputresponse.Value.vBuildingName != "-" ? "อาคาร " + outputresponse.Value.vBuildingName : "" , " " ,
                                                       outputresponse.Value.vFloorNumber != "-" ? "ชั้นที่ " + outputresponse.Value.vFloorNumber : "" , " " ,
                                                       outputresponse.Value.vRoomNumber != "-" ? "ห้องที่ " + outputresponse.Value.vRoomNumber : "" , " " ,
                                                       outputresponse.Value.vVillageName != "-" ? "หมู่บ้าน " + outputresponse.Value.vVillageName : "" , " " ,
                                                       outputresponse.Value.vMooNumber != "-" ? "หมู่ที่ " + outputresponse.Value.vMooNumber : "" , " " ,
                                                       outputresponse.Value.vSoiName != "-" ? "ซอย " + outputresponse.Value.vSoiName : "" , " " ,
                                                       outputresponse.Value.vStreetName != "-" ? "ถนน " + outputresponse.Value.vStreetName : "" , " " ,
                                                       outputresponse.Value.vThambol != "-" ? "ตำบล/แขวง " + outputresponse.Value.vThambol : "" , " " ,
                                                       outputresponse.Value.vAmphur != "-" ? "อำเภอ/เขต " + outputresponse.Value.vAmphur : "" ," " ,
                                                       outputresponse.Value.vProvince != "-" ? "จังหวัด " + outputresponse.Value.vProvince : "" , " " ,
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
                            if (Lang == "TH")
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
                        response = client.GetAsync(uridistrict + "/" + e.ProvinceId).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputdistrict = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                            string[] s_district = e.district_name.Split(" ");
                            if (Lang == "TH")
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
                        response = client.GetAsync(urisubdistrict + "/" + e.DistrictId).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputsubdistrict = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);

                            string[] s_subdistrict = e.sub_district_name.Split(" ");
                            if (Lang == "TH")
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
            try
            {
                if (ModelState.IsValid)
                {
                    #region Check Username Duplicate

                    #endregion

                    Guid companyId = Guid.NewGuid();
                    model.CompanyId = companyId;
                    model.CreateDate = DateTime.Now;
                    model.CreateBy = "SYSTEM";

                    var dataUploadfile = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaft");

                    if (dataUploadfile != null && dataUploadfile.Count != 0)
                    {
                        #region Copy File to server
                        foreach (var e in dataUploadfile)
                        {
                            resultGetFile = await GetFile(e, model.CompanyId.ToString());

                            string filename = ContentDispositionHeaderValue.Parse(e.ContentDisposition).FileName.Trim('"');
                            filename = EnsureCorrectFilename(filename);

                            switch (e.typefile)
                            {
                                case "CompanyCertifiedFile":
                                    model.CompanyCertifiedFile = filename;
                                    break;
                                case "CommercialRegistrationFile":
                                    model.CommercialRegistrationFile = filename;
                                    break;
                                case "VatRegistrationCertificateFile":
                                    model.VatRegistrationCertificateFile = filename;
                                    break;
                            }
                        }
                        #endregion

                    }
                    if (resultGetFile)
                    {
                        SessionHelper.RemoveSession(HttpContext.Session, "userUploadfileDaft");

                        #region Insert Company

                        string encrypted = Util.EncryptText(model.Password);
                        model.Password = encrypted;

                       

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
                                    addr.CreateBy = "SYSTEM";
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
                                        SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaft");
                                        res.Status = true;
                                res.Message = "Register Success";
                                res.StatusError = "0";
                            }
                            else
                            {
                                res.Status = false;
                                res.Message = "Address Data is not correct, Please Check Data or Contact System Admin";
                                res.StatusError = "-1";
                            }
                            }
                        }
                            else
                            {
                                res.Status = false;
                                res.Message = "Address Data is not correct, Please Check Data or Contact System Admin";
                                res.StatusError = "-1";
                            }
                        #endregion
                    }
                    else
                    {
                        res.Status = false;
                        res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                        res.StatusError = "-1";
                    }
                }
                else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }


                #endregion
            }
                    else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
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


        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Uploadfile(IList<IFormFile> files, string fid,string type_file)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            //FileStream output;
            string strmess = "";
            try
            {
                foreach (FormFile source in files)
                {
                    if (source.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        //using (output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                        //    await source.CopyToAsync(output);

                        if (source.ContentType.ToLower() != "image/jpg" &&
                            source.ContentType.ToLower() != "image/jpeg" &&
                            source.ContentType.ToLower() != "image/pjpeg" &&
                            source.ContentType.ToLower() != "image/gif" &&
                            source.ContentType.ToLower() != "image/x-png" &&
                            source.ContentType.ToLower() != "image/png" &&
                            source.ContentType.ToLower() != "application/pdf"
                            )
                        {
                            statusupload = false;
                            strmess = "Upload type file miss match.";
                        }
                        else
                        {
                            Guid id = Guid.NewGuid();
                            using (var ms = new MemoryStream())
                            {
                                source.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                L_File.Add(new FileUploadModal
                                {
                                    file_id = id,
                                    Fileupload = fileBytes,
                                    typefile = type_file,
                                    ContentDisposition = source.ContentDisposition,
                                    ContentType = source.ContentType,
                                    Filename = filename
                                });
                            }
                            var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaft");
                            //byte[] byteArrayValue = HttpContext.Session.Get("userUploadfileDaft");
                            //var data = FromByteArray<List<FileUploadModal>>(byteArrayValue);

                            //var objComplex = HttpContext.Session.GetObject("userUploadfileDaft");

                            if (data != null)
                            {

                                data.RemoveAll(x => x.file_id.ToString() == fid);
                                data.Add(L_File[0]);
                                SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", data);

                            }
                            else
                            {
                                // HttpContext.Session.Set("userUploadfileDaft", ToByteArray<List<FileUploadModal>>(L_File));

                                SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", L_File);
                            }

                            strmess = "Upload file success";
                        }
                        
                    }
                   


                }
               
            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
                throw;
            }


            return Json(new { status = statusupload, message = strmess, response = (statusupload ? L_File[0].file_id.ToString() : "") });

           // return Json(new { status = statusupload, message = strmess });
        }

     private async Task<bool> GetFile(FileUploadModal file,string guid)
        {
            FileStream output;
            try
            {
                var stream = new MemoryStream(file.Fileupload);
                FormFile files = new FormFile(stream, 0, file.Fileupload.Length, "name", "fileName");

                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = EnsureCorrectFilename(file.Filename);
                using (output = System.IO.File.Create(this.GetPathAndFilename(guid,filename)))
                  await  files.CopyToAsync(output);
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
            return true;
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string guid, string filename)
        {
            //return this.hostingEnvironment.WebRootPath + "\\uploads\\" + filename;
            string dir=_configuration.GetValue<string>("PathUploadfile:Local").ToString();
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
