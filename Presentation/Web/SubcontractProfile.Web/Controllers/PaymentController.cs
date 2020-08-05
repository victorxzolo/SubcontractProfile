using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strpathAPI;
        private HttpClient client;
        public PaymentController(IConfiguration configuration)
        {
            client = new HttpClient();
            _configuration = configuration;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

        }
        public IActionResult ConfirmPayment()
        {
            ViewData["Controller"] = "Payment";
            ViewData["View"] = "ConfirmPayment";
            return View();
        }
        public IActionResult VerifyPayment()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetDataselect()
        {
            var data = new List<SubcontractProfilePayment>();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Payment/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractProfilePayment>>(dataresponse);
            }

            return Json(new { Data = data });
        }
        [HttpGet]
        public IActionResult Searchconfirmpayment(SubcontractProfileSearchPayment searchPayment)
        {
            var data = new SubcontractProfilePayment();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string x = "http://localhost:66/api/Payment/SearchPayment/null/null/2020-10-7/2020-10-7/2020-12-12/2020-12-12/null";
            //string uriString = string.Format("{0}", strpathAPI + "Payment/SearchPayment");
            HttpResponseMessage response = client.GetAsync(x).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<SubcontractProfilePayment>(dataresponse);
            }
            return Json(new { Data = data });
        }
    }
}
