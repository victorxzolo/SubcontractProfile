using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SubcontractProfile.Web.Extension;

namespace SubcontractProfile.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            ViewBag.ReturnURL = "";
            int dd = SiteSession.CurrentUICulture;
            return View();
        }

        public List<string> GetScreenConfig(string page)
        {
            List<string> test = new List<string>();
            if (SiteSession.CurrentUICulture==1)
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

                    Url= "/Test/Dashboard";
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
            return View();
        }

       
        [HttpPost]
        public IActionResult DDLsubcontract_profile_sub_district(int district_id=0)
        {
            var output = new List<subcontract_profile_sub_district>();
            output.Add(new subcontract_profile_sub_district
            {
                sub_district_id = 0,
                sub_district_name = "--Select Sub District--"
            });
            output.Add(new subcontract_profile_sub_district {
                sub_district_id=1,
                sub_district_name="วังทองหลาง",
                zip_code="10310",
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
                output = output.Where(x => x.district_id==district_id).ToList();
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLsubcontract_profile_district(int province_id=0)
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
                province_id=1
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
            if (province_id!=0)
            {
                output = output.Where(x => x.province_id==province_id).ToList();
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
                province_id=1,
                province_name="กรุงเทพฯ"

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
  
                var query = new subcontract_profile_locationQuery()
                {

                         p_company_name_th =model.company_name_th,
                         p_company_name_en =model.company_name_en,
                         p_company_alias=model.company_alias,
                         p_company_code =model.company_code,
                         p_location_name_th=model.location_name_th,
                         p_location_name_en =model.location_name_en,
                         p_location_code=model.location_code,
                         p_distribution_channel =model.distribution_channel,
                         p_channel_sale_group=model.channel_sale_group,
                    PAGE_INDEX = model.PageIndex,
                    PAGE_SIZE = model.PageSize
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



            return Json(new { response = data });
        }

        [HttpPost]
        public IActionResult DaftAddress(List<subcontract_profile_address> daftdata)
        {
            var data = new List<subcontract_profile_address>();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaft", daftdata);

            return Json(new { response = data });
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

    public class Search_subcontract_profile_location: DataSourceRequest //รับ Search จากหน้าจอ
    {
        public string company_name_th { get; set; }
        public string company_name_en { get; set; }
        public string company_alias { get; set; }
        public string company_code { get; set; }
        public string location_name_th { get; set; }
        public string location_name_en { get; set; }
        public string location_code { get; set; }
        public string distribution_channel { get; set; }
        public string channel_sale_group { get; set; }
    }
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

    public class DataSourceRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public string Filter { get; set; }
    }

    #region Address
    public class subcontract_profile_address
    {
        public string address_type_id { get; set; }
        public string country { get; set; }
        public string zip_code { get; set; }
        public string house_no { get; set; }
        public string moo { get; set; }
        public string village_name { get; set; }
        public string building { get; set; }
        public string floor { get; set; }
        public string room_no { get; set; }
        public string soi { get; set; }
        public string road { get; set; }
        public string sub_district_id { get; set; }
        public string district_id { get; set; }
        public string province_id { get; set; }
        public string region_id { get; set; }
    }

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

    #endregion

}
