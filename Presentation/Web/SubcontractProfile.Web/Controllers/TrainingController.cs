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
    public class TrainingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strpathAPI;
        private HttpClient client;
        public TrainingController(IConfiguration configuration)
        {
            client = new HttpClient();
            _configuration = configuration;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

        }
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

        [HttpGet]
        public IActionResult GetDropdrowLocationCode()
        {
            var data = new List<SubcontractProfileLocationModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI + "Location/GetAll");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(dataresponse);
            }
            return Json(new { Data = data });
        }
        [HttpGet]
        public IActionResult GetDropdrowTeamName()
        {
            var data = new List<SubcontractProfileTeamModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI + "Team/GetAll");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(dataresponse);
            }
            return Json(new { Data = data });
        }
    }
}
