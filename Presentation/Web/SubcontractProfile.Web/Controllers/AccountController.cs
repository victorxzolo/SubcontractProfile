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

namespace SubcontractProfile.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strpathAPI;
        private string Lang = "";
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;


            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            Lang = "TH";
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
            string Url = "";

            if (ModelState.IsValid)
            {
                if (Configurations.UseLDAP)
                {
                    var authenResultMessage = "";
                    if (AuthenLDAP(model.username, model.password, out authenResultMessage))
                    {
                        var authenticatedUser = GetUser(model.username);
                        //authenticatedUser.AuthenticateType = AuthenticateType.LDAP;
                        //Response.AppendCookie(CreateAuthenticatedCookie(authenticatedUser.username));
                        //base.CurrentUser = authenticatedUser;

                        Url = "/Test/Dashboard";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName or Password.");
                    }
                }
                else
                {
                    // bypass authen
                    var authenticatedUser = GetUser(model.username);
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

                    Url = "/Test/Dashboard";
                }
            }

            return Json(new { redirecturl = Url });
        }


        public ProfileUserModel GetUser(string userName)
        {
            var userQuery = new subcontract_profile_user
            {
                p_username = userName
            };

            // var authenticatedUser = _QueryProcessor.Execute(userQuery);
            var authenticatedUser = new ProfileUserModel();
            return authenticatedUser;
        }

        public bool AuthenLDAP(string userName, string password, out string authenMessage)
        {
            var authLDAPQuery = new LoginModel
            {
                username = userName,
                password = password,
            };

            //var authenLDAPResult = _QueryProcessor.Execute(authLDAPQuery);
            var authenLDAPResult = true;
            authenMessage = "";
            return authenLDAPResult;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return View();
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

            return View();
        }


        #region DDL
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

                string uriString = string.Format("{0}", strpathAPI + "SubDistrictController/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);
                    resultZipCode = output.GroupBy(c => c.ZipCode).Select(g => g.First()).ToList();
                }
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

                string uriString = string.Format("{0}", strpathAPI + "DistrictController/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                }
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

            string uriString = string.Format("{0}/{1}", strpathAPI + "Province/GetProvinceByRegionId", region_id);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(v);
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
            var output = new List<SubcontractProfileTitleModel>();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Title/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileTitleModel>>(v);
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
        #endregion



        [HttpPost]
        public IActionResult SearchLocation(Search_subcontract_profile_location model)
        {
            var data = new List<subcontract_profile_locationModel>();
            int filteredResultsCount;
            int totalResultsCount;

            //var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);


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
            if (model != null)
            {
                var query = new subcontract_profile_locationQuery()
                {

                    p_company_name_th = model.company_name_th,
                    p_company_name_en = model.company_name_en,
                    p_company_alias = model.company_alias,
                    p_company_code = model.company_code,
                    p_location_name_th = model.location_name_th,
                    p_location_name_en = model.location_name_en,
                    p_location_code = model.location_code,
                    p_distribution_channel = model.distribution_channel,
                    p_channel_sale_group = model.channel_sale_group,
                    PAGE_INDEX = skip,
                    PAGE_SIZE = take
                };
                //var result = _queryProcessor.Execute(query);
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test",
                    location_code = model.location_code,
                    location_name_th = "test33",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test2",
                    location_code = model.location_code,
                    location_name_th = "test34",
                    distribution_channel = "Channel5",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test3",
                    location_code = model.location_code,
                    location_name_th = "test22",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group2"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test",
                    location_code = model.location_code,
                    location_name_th = "test33",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test2",
                    location_code = model.location_code,
                    location_name_th = "test34",
                    distribution_channel = "Channel5",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test3",
                    location_code = model.location_code,
                    location_name_th = "test22",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group2"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test",
                    location_code = model.location_code,
                    location_name_th = "test33",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test2",
                    location_code = model.location_code,
                    location_name_th = "test34",
                    distribution_channel = "Channel5",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test3",
                    location_code = model.location_code,
                    location_name_th = "test22",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group2"


                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test",
                    location_code = model.location_code,
                    location_name_th = "test33",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test2",
                    location_code = model.location_code,
                    location_name_th = "test34",
                    distribution_channel = "Channel5",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test3",
                    location_code = model.location_code,
                    location_name_th = "test22",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group2"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test",
                    location_code = model.location_code,
                    location_name_th = "test33",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test2",
                    location_code = model.location_code,
                    location_name_th = "test34",
                    distribution_channel = "Channel5",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test3",
                    location_code = model.location_code,
                    location_name_th = "test22",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group2"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test",
                    location_code = model.location_code,
                    location_name_th = "test33",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test2",
                    location_code = model.location_code,
                    location_name_th = "test34",
                    distribution_channel = "Channel5",
                    channel_sale_group = "Group3"

                });
                data.Add(new subcontract_profile_locationModel
                {
                    company_name_th = "test3",
                    location_code = model.location_code,
                    location_name_th = "test22",
                    distribution_channel = "Channel2",
                    channel_sale_group = "Group2"

                });

                var result = data.Where(x => x.channel_sale_group.Contains(model.channel_sale_group != null ? model.channel_sale_group : "")).Skip(skip).Take(take).ToList();


                filteredResultsCount = data.Where(x => x.channel_sale_group.Contains(model.channel_sale_group != null ? model.channel_sale_group : "")).Count(); //output from Database
                totalResultsCount = data.Count(); //output from Database

                return Json(new
                {
                    // this is what datatables wants sending back
                    draw = model.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = result
                });
            }
            else
            {
                var result = data.Skip(skip).Take(take).ToList();

                filteredResultsCount = data.Count(); //output from Database
                totalResultsCount = data.Count(); //output from Database

                return Json(new
                {
                    // this is what datatables wants sending back
                    draw = model.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = result
                });
            }











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
                    data.RemoveAll(x => x.AddressId==daftdata[0].AddressId);

                    data.Add(daftdata[0]);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaft", data);
                }
                else
                {
                    foreach(var e in daftdata)
                    {
                        Guid addr_id = Guid.NewGuid();
                        e.AddressId = addr_id;
                    }
                   
                   
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaft", daftdata);
                    data = daftdata;
                }




                return Json(new { response = data, status = true });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
            }

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
        public IActionResult NewRegister(subcontract_profile_New_Register model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Insert Company
                    ////////////////

                    #region Insert Address
                    var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");
                    model.L_address = new List<SubcontractProfileAddressModel>();

                    if (dataaddr != null && dataaddr.Count != 0)
                    {

                        foreach (var d in dataaddr)
                        {
                            model.L_address.Add(new SubcontractProfileAddressModel
                            {
                                AddressId = d.AddressId,
                                AddressTypeId = d.AddressTypeId,
                                Building = d.Building,
                                City = d.City,
                                Country = d.Country,
                                DistrictId = d.DistrictId,
                                Floor = d.Floor,
                                HouseNo = d.HouseNo,
                                Moo = d.Moo,
                                ProvinceId = d.ProvinceId,
                                CompanyId = d.CompanyId,
                                CreateBy = d.CreateBy,
                                CreateDate = DateTime.Now,
                                RegionId = d.RegionId,
                                Road = d.Road,
                                Soi = d.Soi,
                                RoomNo = d.RoomNo,
                                SubDistrictId = d.SubDistrictId,
                                VillageName = d.VillageName,
                                ZipCode = d.ZipCode
                            });
                        }

                    }
                    else
                    {
                        return Json(new
                        {
                            status = "-1",
                            message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                        });
                    }
                    #endregion



                    return Json(new
                    {
                        status = "1",
                        message = "Success"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = "-1",
                        message = "Data isnot correct, Please Check Data or Contact System Admin"
                    });
                }
            }
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }


        [HttpPost]
        public IActionResult Uploadfile(IList<IFormFile> files,string fid)
        {
            bool statusupload=true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            FileStream output;
            string strmess = "";
            try
            {
                foreach (FormFile source in files)
                {
                    Guid id = Guid.NewGuid();


                    //string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                    //filename = this.EnsureCorrectFilename(filename);

                    //using (output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                    //await source.CopyToAsync(output);

                    

                    if (source.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            source.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            L_File.Add(new FileUploadModal
                            {
                                file_id = id,
                                Fileupload = s
                            });
                        }
                    }
                    var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaft");
                    if (data != null)
                    {
                       
                        data.RemoveAll(x => x.file_id.ToString() == fid);
                        data.Add(L_File[0]);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", data);

                    }
                    else
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", L_File);
                    }
                    
                    
                }
                strmess = "Upload file success";
            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
                throw;
            }


            return Json(new { status = statusupload ,message= strmess ,response= L_File[0].file_id });
        }
      
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            //return this.hostingEnvironment.WebRootPath + "\\uploads\\" + filename;
            string path = "D:\\2.Document\\SubcontractProfile" + "\\uploads\\" + filename;
            return path;
        }

        #endregion
    }

    #region Login
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class ProfileUserModel
    {
        public string ret_code { get; set; }
        public List<subcontract_profile_user_model> cur { get; set; }
    }
    public class subcontract_profile_user_model
    {
        public string username { get; set; }
        public string sub_module_name { get; set; }
        public string sso_first_name { get; set; }
        public string sso_last_name { get; set; }
        public string staff_name { get; set; }
        public string staff_role { get; set; }
    }
    public class subcontract_profile_user
    {
        public string p_username { get; set; }
        public string p_password { get; set; }
    }

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



    #region Register 
    public class subcontract_profile_locationQuery //ส่งเข้าDatabase
    {
        public string p_company_name_th { get; set; }
        public string p_company_name_en { get; set; }
        public string p_company_alias { get; set; }
        public string p_company_code { get; set; }
        public string p_location_name_th { get; set; }
        public string p_location_name_en { get; set; }
        public string p_location_code { get; set; }
        public string p_distribution_channel { get; set; }
        public string p_channel_sale_group { get; set; }
        public int PAGE_INDEX { get; set; }
        public int PAGE_SIZE { get; set; }
        public string ret_code { get; set; }
        public string cur { get; set; }
    }

    public class subcontract_profile_locationModel //รับจากDatabase
    {
        public string company_name_th { get; set; }
        public string location_name_th { get; set; }
        public string distribution_channel { get; set; }
        public string channel_sale_group { get; set; }
        public string location_code { get; set; }
        //public decimal RowNumber { get; set; }
        //public decimal CNT { get; set; }
    }



    public class subcontract_profile_New_Register //ส่งค่ามาจากหน้าจอ
    {
        public string subcontract_profile_type { get; set; }
        public string location_code { get; set; }
        public string location_name_th { get; set; }
        public string location_name_en { get; set; }
        public string distribution_channel { get; set; }
        public string channel_sale_group { get; set; }
        public string tax_id { get; set; }
        public string company_alias { get; set; }
        public string company_title_name_th { get; set; } //รอเอกสารspec เพิ่ม
        public string company_name_th { get; set; }
        public string company_title_name_en { get; set; } //รอเอกสารspec เพิ่ม
        public string company_name_en { get; set; }
        public string wt_name { get; set; }
        public string vat_type { get; set; }
        //public string house_no { get; set; }
        //public string building { get; set; }
        //public string floor { get; set; }
        //public string moo { get; set; }
        //public string soi { get; set; }
        //public string road { get; set; }
        //public string sub_district_id { get; set; }
        //public string district_id { get; set; }
        //public string province_id { get; set; }
        //public string region_id { get; set; }
        //public string address_type_id { get; set; }
        //public string zip_code { get; set; }
        //public string country { get; set; }
        //public string village_name { get; set; }
        //public string room_no { get; set; }

        public List<SubcontractProfileAddressModel> L_address { get; set; }

        public string company_Email { get; set; }
        public string contract_name { get; set; }
        public string contract_phone { get; set; }
        public string contract_email { get; set; }
        public string dept_of_install_name { get; set; }
        public string dept_of_install_phone { get; set; }
        public string dept_of_install_email { get; set; }
        public string dept_of_mainten_name { get; set; }
        public string dept_of_mainten_phone { get; set; }
        public string dept_of_mainten_email { get; set; }
        public string dept_of_Account_name { get; set; }
        public string dept_of_Account_phone { get; set; }
        public string dept_of_Account_email { get; set; }
        public string account_Name { get; set; }
        public string branch_Name { get; set; }
        public string branch_Code { get; set; }
        public string bank_account_type_id { get; set; }
        public string company_certified_file { get; set; }
        public string commercial_registration_file { get; set; }
        public string vat_registration_certificate_file { get; set; }
    }

    public class subcontract_profileCommand //ส่งเข้าDatabase
    {
        public subcontract_profileCommand()
        {
            this.ret_code = -1;
            this.ret_msg = "";
        }
        public string p_subcontract_profile_type { get; set; }
        public string p_location_code { get; set; }
        public string p_location_name_th { get; set; }
        public string p_location_name_en { get; set; }
        public string p_distribution_channel { get; set; }
        public string p_channel_sale_group { get; set; }
        public string p_tax_id { get; set; }
        public string p_company_alias { get; set; }
        public string p_company_title_name_th { get; set; }//รอเอกสารspec เพิ่ม
        public string p_company_name_th { get; set; }
        public string p_company_title_name_en { get; set; }//รอเอกสารspec เพิ่ม
        public string p_company_name_en { get; set; }
        public string p_wt_name { get; set; }
        public string p_vat_type { get; set; }
        public string p_house_no { get; set; }
        public string p_building { get; set; }
        public string p_floor { get; set; }
        public string p_moo { get; set; }
        public string p_soi { get; set; }
        public string p_road { get; set; }
        public string p_sub_district_id { get; set; }
        public string p_district_id { get; set; }
        public string p_province_id { get; set; }
        public string p_region_id { get; set; }
        public string p_address_type_id { get; set; }
        public string p_zip_code { get; set; }
        public string p_country { get; set; }
        public string p_village_name { get; set; }
        public string p_room_no { get; set; }
        public string p_company_Email { get; set; }
        public string p_contract_name { get; set; }
        public string p_contract_phone { get; set; }
        public string p_contract_email { get; set; }
        public string p_dept_of_install_name { get; set; }
        public string p_dept_of_install_phone { get; set; }
        public string p_dept_of_install_email { get; set; }
        public string p_dept_of_mainten_name { get; set; }
        public string p_dept_of_mainten_phone { get; set; }
        public string p_dept_of_mainten_email { get; set; }
        public string p_dept_of_Account_name { get; set; }
        public string p_dept_of_Account_phone { get; set; }
        public string p_dept_of_Account_email { get; set; }
        public string p_account_Name { get; set; }
        public string p_branch_Name { get; set; }
        public string p_branch_Code { get; set; }
        public string p_bank_account_type_id { get; set; }
        public string p_company_certified_file { get; set; }
        public string p_commercial_registration_file { get; set; }
        public string p_vat_registration_certificate_file { get; set; }


        public Nullable<decimal> ret_code { get; set; }
        public string ret_msg { get; set; }
    }

    #endregion

}
