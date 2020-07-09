using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Register(string language="TH")
        {
            ViewData["Controller"] = "Register";
            ViewData["View"] = "Register";
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



        //public  bool IsThaiCulture(this int currentCulture)
        //{
        //    if (currentCulture == 1)
        //        return true;

        //    return false;
        //}

    }

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
   
}
