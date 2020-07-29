using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SubcontractProfile.Web.Controllers
{
    public class RegisterController : Controller
    {
        static HttpClient client = new HttpClient();
        public IActionResult Index2()
        {
            return View();
        }
    }
}