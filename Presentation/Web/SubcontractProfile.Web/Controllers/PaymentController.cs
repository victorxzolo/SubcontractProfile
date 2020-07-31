using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SubcontractProfile.Web.Model;
    
namespace SubcontractProfile.Web.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult ConfirmPayment()
        {
            return View();
        }
        public IActionResult VerifyPayment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Searchconfirmpayment(Searchconfirmpayment data)
        {
           

            return Json( new {data ="true" });

        }
        [HttpPost]
        public IActionResult confirmpayment(Confirmpayment data)
        {


            return Json(new { data = "true" });

        }
    }
}
