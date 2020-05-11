using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubcontractProfile.Entity.Model.Mapping;
using SubcontractProfile.Entity.PanelModel;
//using SubcontractProfile.Web.Models;

namespace SubcontractProfile.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
      //  private readonly DBContext _context;
        public HomeController(ILogger<HomeController> logger, DBContext context)
        {
            _logger = logger;
           // _context = context;
        }
        
        public IActionResult Index()
        {
           
           // ViewData["TestConn"] = TestConnect();

            return View();
        }
        //public string TestConnect()
        //{
        //    //var Data = new LovDataModel();
        //    string result = string.Empty;
        //    try
        //    {


        //         var Data = _context.Lov_Config.Where(a => a.LOV_TYPE.Equals("TEST_CONN")&&a.ACTIVEFLAG.Equals("Y")).FirstOrDefault();

        //        if (Data != null)
        //        {
        //            result = Data.LOV_VAL1.ToString();
        //        }
        //        else
        //        {
        //            result = "Connect Fail";
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.Message.ToString();
        //    }
        //    return result;
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
