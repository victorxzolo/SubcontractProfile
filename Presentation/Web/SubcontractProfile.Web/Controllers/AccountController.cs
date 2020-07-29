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

namespace SubcontractProfile.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strpathAPI;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;


            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
        }
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


        #region Register
        public IActionResult Register(string language = "TH")
        {
            ViewData["Controller"] = "Register";
            ViewData["View"] = "Register";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "v1.0/SubcontractProfileCompany/GetALL", 1);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                var t = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
            }


            return View();
        }


        [HttpPost]
        public IActionResult DDLsubcontract_profile_sub_district(int district_id = 0)
        {
            var output = new List<subcontract_profile_sub_district>();
            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 0,
                sub_district_name = "--Select Sub District--"
            });
            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 1,
                sub_district_name = "วังทองหลาง",
                zip_code = "10310",
                district_id = 1
            });
            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 2,
                sub_district_name = "คลองเจ้าคุณสิงห์",
                zip_code = "10310",
                district_id = 1
            });
            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 3,
                sub_district_name = "คลองจั่น",
                zip_code = "10240",
                district_id = 2
            });
            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 4,
                sub_district_name = "หัวหมาก",
                zip_code = "10240",
                district_id = 2
            });


            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 5,
                sub_district_name = "แพรกษา",
                zip_code = "10280",
                district_id = 3
            });
            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 6,
                sub_district_name = "บางหญ้าแพรก",
                zip_code = "10130",
                district_id = 4
            });

            if (district_id != 0)
            {
                output = output.Where(x => x.district_id == district_id).ToList();
                output.Add(new subcontract_profile_sub_district
                {
                    sub_district_id = 0,
                    sub_district_name = "--Select Sub District--"
                });
                output = output.OrderBy(x => x.sub_district_id).ToList();
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLsubcontract_profile_district(int province_id = 0)
        {
            var output = new List<subcontract_profile_district>();
            output.Add(new subcontract_profile_district
            {
                district_id = 0,
                district_name = "--Select District--"
            });
            output.Add(new subcontract_profile_district
            {
                district_id = 1,
                district_name = "วังทองหลาง",
                province_id = 1
            });
            output.Add(new subcontract_profile_district
            {
                district_id = 2,
                district_name = "บางกะปิ",
                province_id = 1
            });

            output.Add(new subcontract_profile_district
            {
                district_id = 3,
                district_name = "เมืองสมุทรปราการ",
                province_id = 2
            });
            output.Add(new subcontract_profile_district
            {
                district_id = 4,
                district_name = "พระประแดง",
                province_id = 2
            });
            if (province_id != 0)
            {
                output = output.Where(x => x.province_id == province_id).ToList();
                output.Add(new subcontract_profile_district
                {
                    district_id = 0,
                    district_name = "--Select District--"
                });
                output = output.OrderBy(x => x.district_id).ToList();
            }


            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLsubcontract_profile_province()
        {
            var output = new List<subcontract_profile_province>();
            output.Add(new subcontract_profile_province
            {
                province_id = 0,
                province_name = "--Select Province--"

            });
            output.Add(new subcontract_profile_province
            {
                province_id = 1,
                province_name = "กรุงเทพฯ"

            });
            output.Add(new subcontract_profile_province
            {
                province_id = 2,
                province_name = "สมุทรปราการ"

            });
            return Json(new { response = output });
        }


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
                    var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaft");
                    model.L_address = new List<SubcontractProfileAddressModel>();

                    if (dataaddr != null && dataaddr.Count != 0)
                    {

                        foreach (var d in dataaddr)
                        {
                            model.L_address.Add(new SubcontractProfileAddressModel
                            {
                                //address_type_id = d.,
                                //building = d.building,
                                //country = d.country,
                                //district_id = d.district_id,
                                //floor = d.floor,
                                //house_no = d.house_no,
                                //moo = d.moo,
                                //province_id = d.province_id,
                                //region_id = d.region_id,
                                //road = d.road,
                                //room_no = d.room_no,
                                //soi = d.soi,
                                //sub_district_id = d.sub_district_id,
                                //village_name = d.village_name,
                                //zip_code = d.zip_code
                                AddressId=d.AddressId,
                                Building=d.Building,
                                DistrictId=d.DistrictId,
                                Floor=d.Floor,
                                HouseNo=d.HouseNo,
                                Moo=d.Moo,
                                Road=d.Road,
                                Soi=d.Soi,
                                SubDistrictId=d.SubDistrictId,
                                ProvinceId=d.ProvinceId,
                                village_name=d.village_name,
                                country=d.country,
                                region_id=d.region_id
                            });
                        }

                        //Command.Handle(model)
                        //output ret_code,ret
                        //ret_msg
                    }
                    else
                    {
                        return Json(new
                        {
                            status = "-1",
                            message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                        });
                    }
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


    #region Address

    public class subcontract_profile_sub_district
    {
        public int sub_district_id { get; set; }
        public string sub_district_name { get; set; }
        public string zip_code { get; set; }
        public int district_id { get; set; }
    }
    public class subcontract_profile_district
    {
        public int district_id { get; set; }
        public string district_name { get; set; }
        public int province_id { get; set; }
    }
    public class subcontract_profile_province
    {
        public int province_id { get; set; }
        public string province_name { get; set; }
    }

    #endregion


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
