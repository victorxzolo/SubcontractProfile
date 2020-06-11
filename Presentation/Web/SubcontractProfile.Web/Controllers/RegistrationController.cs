using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SubcontractProfile.Web.Controllers
{
    public class RegistrationController : Controller
    {
       
        public IActionResult ActivateProfile()
        {
            ViewData["Controller"] = "Registration";
            ViewData["View"] = "Activate Profile";
            return View();
        }
        public IActionResult SearchCompanyVerify()
        {
            ViewData["Controller"] = "Registration";
            ViewData["View"] = "Search Company Verify";
            return View();
        }
        public IActionResult CompanyVerify(int id)
        {
            ViewData["Controller"] = "Registration";
            ViewData["View"] = "Company Verify";
            return View();
        }
    }
}
