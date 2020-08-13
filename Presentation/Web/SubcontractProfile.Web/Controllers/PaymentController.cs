using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            //ViewData["Controller"] = "Payment";
            //ViewData["View"] = "ConfirmPayment";
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

            searchPayment.Paymentno = (string.IsNullOrEmpty(searchPayment.Paymentno)) ? "null" : searchPayment.Paymentno;
            searchPayment.Paymentrequesttraningno = (string.IsNullOrEmpty(searchPayment.Paymentrequesttraningno)) ? "null" : searchPayment.Paymentrequesttraningno;
            searchPayment.Paymantrequestdatefrom = (string.IsNullOrEmpty(searchPayment.Paymantrequestdatefrom)) ? "null" : searchPayment.Paymantrequestdatefrom;
            searchPayment.Paymentrequestdateto = (string.IsNullOrEmpty(searchPayment.Paymentrequestdateto)) ? "null" : searchPayment.Paymentrequestdateto;
            searchPayment.Paymentdatefrom = (string.IsNullOrEmpty(searchPayment.Paymentdatefrom)) ? "null" : searchPayment.Paymentdatefrom;
            searchPayment.Paymentdateto = (string.IsNullOrEmpty(searchPayment.Paymentdateto)) ? "null" : searchPayment.Paymentdateto;



            var datapayment = new List<SubcontractProfilePayment>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Payment/SearchPayment", searchPayment.Paymentno, searchPayment.Paymentrequesttraningno, searchPayment.Paymantrequestdatefrom, searchPayment.Paymentrequestdateto, searchPayment.Paymentdatefrom, searchPayment.Paymentdateto, searchPayment.Paymentstatus);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                datapayment = JsonConvert.DeserializeObject<List<SubcontractProfilePayment>>(dataresponse);
            }
            return Json(new { data = datapayment });
        }
        [HttpGet]
        public IActionResult GetByPaymentId(string id)
        {
            var data = new SubcontractProfilePayment();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}/{1}", strpathAPI + "Payment/GetByPaymentId",id);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<SubcontractProfilePayment>(dataresponse);
            }
            return Json(new { Data = data });

        }
        [HttpPost]
        public IActionResult Inserpayment(SubcontractProfilePayment Payment)
        {            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            
            string uriString = string.Format("{0}", strpathAPI + "Payment/Insert");
            var httpContent = new StringContent(JsonConvert.SerializeObject(Payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(uriString, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                
            }

            return Json(new { Date = "" });
        }
    }
}
