using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SubcontractProfile.Web.Model;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace SubcontractProfile.Web.Controllers
{

    public class Test2Controller : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;

        public Test2Controller(IConfiguration configuration)
        {
            _configuration = configuration;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            //Lang = "TH";
            //strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();
        }


        public IActionResult Login()
        {
            // this.Session.SetString("TransId", "x001");
            return View();
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult LoadData()
        {
            try
            {
                var output = new List<SubcontractProfileCompanyModel>();

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all company data  
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Company/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(result);
                    //resultZipCode = output.GroupBy(c => c.ZipCode).Select(g => g.First()).ToList();

                    recordsTotal = result.Length;
                }


                ////Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    output = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                //}
                ////Search  
                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    customerData = customerData.Where(m => m.Name == searchValue);
                //}

                //total number of rows count   

                //Paging   
                //var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = output });

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
