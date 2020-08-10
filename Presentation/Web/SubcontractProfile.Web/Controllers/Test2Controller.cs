using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SubcontractProfile.Web.Model;
using Microsoft.AspNetCore.Http;

namespace SubcontractProfile.Web.Controllers
{
    public class Test2Controller : Controller
    {
      
        //[HttpPost]
        public IActionResult Login()
        {
           // this.Session.SetString("TransId", "x001");
            return View();
        }

        public IActionResult Index()
        {
         //   var profileData = HttpContext.Session as UserProfileSessionData;

          
            return View();
        }
    }
}
