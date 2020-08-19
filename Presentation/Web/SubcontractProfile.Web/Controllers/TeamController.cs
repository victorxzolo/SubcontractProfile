using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SubcontractProfile.Web.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult TeamProfile()
        {
            return View();
        }
    }
}
