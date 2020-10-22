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
using Microsoft.AspNetCore.Localization;

namespace SubcontractProfile.Web.Controllers
{
    public class LogonByUserController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string strpathAPI;
        private string strpathASCProfile;
        private string Lang = "";
        private Utilities Util = new Utilities();
        private SubcontractProfileUserModel dataAISUser = new SubcontractProfileUserModel();

        private const int MegaBytes = 1024 * 1024;
        public LogonByUserController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();


            strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();
        }

        public IActionResult logonbyuser()
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
                if (model.username != null && model.password != null)
                {
                    string encryptedPassword = Util.EncryptText(model.password);
                    var authenticatedUser = GetUser(model.username, encryptedPassword);
                    if (authenticatedUser != null)
                    {
                        if (!string.IsNullOrEmpty(authenticatedUser.Username))
                        {
                            res.Status = true;
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "userAISLogin", authenticatedUser);
                            Url = "~/Registration/SearchCompanyVerify";

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
            // return  RedirectToAction("SearchCompanyVerify", "Registration");
       
            return Json(new { redirecturl = Url, Response = res });
        }


        public SubcontractProfileUserModel GetUser(string userName, string password)
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


        public SubcontractProfileUserModel GetUserSSO(string userName)
        {
            SubcontractProfileUserModel authenticatedUser = new SubcontractProfileUserModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //string uriString = string.Format("{0}/{1}/{2}", strpathAPI + "User/LoginUser", userName, password);
            //HttpResponseMessage response = client.GetAsync(uriString).Result;

            var uriUser = new Uri(Path.Combine(strpathAPI, "User", "LoginUserSSO"));

            var input = new SubcontractProfileUserModel();
            input.Username = userName;

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
            return RedirectToAction("Logonbyuser", "LogonByUser");
        }
        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if (Lang == null || Lang == "")
            {
                Lang = "TH";
            }
           
            dataAISUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userAISLogin");
        }
        private SSOFields LoadSSOFieldsFromPostData(IFormCollection form)
        {
            var ssoFields = new SSOFields();

            ssoFields.Token = form["token"];

            var tokenValues = ssoFields.Token.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            ssoFields.SessionID = (tokenValues.Length > 0) ? tokenValues[0] : null;
            ssoFields.UserName = (tokenValues.Length > 1) ? tokenValues[1] : null;
            ssoFields.GroupID = (tokenValues.Length > 2) ? tokenValues[2] : null;
            ssoFields.SubModuleIDInToken = (tokenValues.Length > 3) ? tokenValues[3] : null;
            ssoFields.ClientIP = (tokenValues.Length > 4) ? (tokenValues[4] == "null" ? null : tokenValues[4]) : null;

            ssoFields.RoleID = form["rid"];
            ssoFields.SubModuleID = form["sid"];
            ssoFields.RoleName = form["rn"];
            ssoFields.SubModuleName = form["sn"];
            ssoFields.FirstName = form["fn"];
            ssoFields.LastName = form["ln"];
            ssoFields.ThemeName = form["theme"];
            ssoFields.TemplateName = form["template"];
            ssoFields.EmployeeServiceWebRootUrl = form["host"];
            ssoFields.LocationCode = form["lc"];
            ssoFields.GroupLocation = form["gl"];
            ssoFields.DepartmentCode = form["dc"];
            ssoFields.SectionCode = form["sc"];
            ssoFields.PositionByJob = form["pt"];



            return ssoFields;
        }

        [HttpPost]
      //  [AllowAnonymous]
        //    [CustomActionFilter(LogType = "LogOnBySSO")]
        public ActionResult LogOnBySSO()
        {
            ResponseModel res = new ResponseModel();
            string Url = "";
            try
            {
                // var ssoData = HttpContext.Request.Form;
                var ssoData = HttpContext.Request.Form;



                var ssoFields = LoadSSOFieldsFromPostData(ssoData);


                if(ssoFields.UserName !=null && ssoFields.UserName !="")
                {
                    var authenticatedUser = GetUserSSO(ssoFields.UserName);
                    if(authenticatedUser != null)
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userAISLogin", authenticatedUser);
                        return RedirectToAction("SearchCompanyVerify", "Registration");
                    }
                    else
                    {
                        HttpContext.Session.Clear();
                        HttpContext.SignOutAsync();
                        return RedirectToAction("logonbyuser", "Account");
                    }

                }
                else
                {
                    HttpContext.Session.Clear();
                    HttpContext.SignOutAsync();
                    return RedirectToAction("logonbyuser", "Account");
                }


                // _Logger.Info(ssoFields.Token);
                // _Logger.Info(ssoFields.UserName);

                //get profile
                //  _Logger.Info("Get User Model");
                //var authenticatedUser = GetUser(ssoFields.UserName, "");
                //if (authenticatedUser != null)
                //{
                //    authenticatedUser.AuthenticateType = AuthenticateType.SSO;
                //    authenticatedUser.SSOFields = ssoFields;


                //    res.Status = true;
                //    SessionHelper.SetObjectAsJson(HttpContext.Session, "userAISLogin", authenticatedUser);
                //    Url = "/Registration/SearchCompanyVerify";

                //    res.Status = true;
                //    //Lang = model.Language != null ? model.Language : "TH";
                //    // SessionHelper.SetObjectAsJson(HttpContext.Session, "language", Lang);
                //    //var str_L= SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "language");
                //    //var datauser= SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
                //    return RedirectToAction("SearchCompanyVerify", "Registration");
                //}
                //else
                //{

                //    HttpContext.Session.Clear();
                //    HttpContext.SignOutAsync();
                //    return RedirectToAction("Login", "Account");

                //}
            }
            catch (Exception ex)
            {
                return RedirectToAction("logonbyuser", "Account");
            }

            
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

        public void SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

        }

    }
}