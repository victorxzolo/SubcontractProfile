﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SubcontractProfile.Web.Controllers
{
    public class MigrationProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}