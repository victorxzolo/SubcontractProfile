﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}
