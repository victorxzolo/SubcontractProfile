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
            return View();
        }
        public IActionResult SearchCompanyVerify()
        {
            return View();
        }
        public IActionResult CompanyVerify(int id)
        {
            return View();
        }
    }
}
