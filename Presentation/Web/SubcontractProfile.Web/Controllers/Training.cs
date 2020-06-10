using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SubcontractProfile.Web.Controllers
{
    public class Training : Controller
    {
        public IActionResult RequestTraining()
        {
            return View();
        }
        public IActionResult Reporttestresult()
        {
            return View();
        }
        public IActionResult Reporttestresultauthorities()
        {
            return View();
        }
        public IActionResult RequestTrainingauthorities()
        {
            return View();
        }
    }
}
