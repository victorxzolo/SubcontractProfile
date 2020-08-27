using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class TrainingController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        private HttpClient client;

        public TrainingController(IConfiguration configuration)
        {
            _configuration = configuration;
             client = new HttpClient();
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

        public ActionResult Search(string location_Id, string team_Id
      , string status, string date_from, string date_to)
        {

            var Result = new List<SubcontractProfileTrainingModel>();

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
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            Guid gCompanyId = userProfile.companyid;
            Guid gLocationId;
            Guid gTeamId;

            if (location_Id == null || location_Id == "-1")
            {
                gLocationId = Guid.Empty;
            }
            else
            {
                gLocationId = new Guid(location_Id);
            }


            if (team_Id == null || team_Id == "-1")
            {
                gTeamId = Guid.Empty;
            }
            else
            {
                gTeamId = new Guid(team_Id);
            }


            if (status == null)
            {
                status = "null";
            }

            if (date_from == null)
            {
                date_from = "null";
            }

            if (date_to == null)
            {
                date_to = "null";
            }


            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Training/SearchTraining", gCompanyId, gLocationId
               , gTeamId, status, date_from, date_to);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileTrainingModel>>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        [HttpGet]
        public IActionResult GetByCompanyId()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            //Guid companyId = new Guid("3a37a238-1cdf-4af8-980f-00c86822f903");
            Guid companyId = userProfile.companyid;

            var data = new SubcontractProfileCompanyModel();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", companyId);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(dataresponse);
            }
            return Json(new { Data = data });
        }
        [HttpPost]
        public IActionResult Addtraining(SubcontractProfileTrainingModel model)    
        {
            var data = new List<SubcontractProfileTrainingModel>();
            var splitlocation = model.location_name_th.Split(',');
            var splitteam = model.team_name_th.Split(',');
            var splitEngineer = model.Engineer_ID.Split(',');
            Guid trainingId = Guid.NewGuid();
            var trainingObject = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("Datatraining");
            if (trainingObject == null)        
            {
                
                model.TrainingId = trainingId;
                model.location_name_th = splitlocation[1];
                model.team_name_th = splitteam[1];
                model.Engineer_name = splitEngineer[1];
                model.Engineer_ID = splitEngineer[0];                
                data.Add(model);
                HttpContext.Session.SetObjectAsJson("Datatraining", data);

            }
            else
            {
                data = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("Datatraining");             
               
                model.TrainingId = trainingId;
                model.location_name_th = splitlocation[1];
                model.team_name_th = splitteam[1];
                model.Engineer_name = splitEngineer[1];
                model.Engineer_ID = splitEngineer[0];
                data.Add(model);

                HttpContext.Session.SetObjectAsJson("Datatraining", data);
            }
            var training = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("Datatraining");

            return Json(training);
        }
  
        [HttpPut]
        public IActionResult Deletetraining(string[] TrainingId)
        {
            var training = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("Datatraining");

            foreach (var item in TrainingId)
            {
                training.RemoveAll(x => x.TrainingId == new Guid(item));
            }
            HttpContext.Session.SetObjectAsJson("Datatraining", training);
            var deltraining = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("Datatraining");

            return Json(deltraining);
        }
        [HttpPost]
        public IActionResult RequestTraining(SubcontractProfileTrainingModel model)
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            var training = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("Datatraining");
           
            model.TrainingId = Guid.NewGuid();
            model.CompanyId = userProfile.companyid;
            model.CreateDate = DateTime.Now;
            model.CreateBy = userProfile.Username;
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI + "Training/Insert");
            var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(uriString, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                
            }

            return Json(response);
        }
     
    }
}
