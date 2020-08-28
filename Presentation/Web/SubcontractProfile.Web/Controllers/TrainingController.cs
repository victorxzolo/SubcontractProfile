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
        [HttpGet]
        public IActionResult Getcourse()
        {          

            var data = new List<SubcontractDropdownModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI +"Dropdown/GetByDropDownName/training_couse");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(dataresponse);
            }
            return Json(data);
        }
        [HttpPost]
        public IActionResult Addtraining(SubcontractProfileTrainingModel model)    
        {
            var dataScreen = new List<SubcontractProfileTrainingModel>();
            var listdataTrainingEngineer = new List<SubcontractProfileTrainingEngineerModel>();
            var dataTrainingEngineer = new SubcontractProfileTrainingEngineerModel();

            var splitlocation = model.location_name_th.Split(',');
            var splitteam = model.team_name_th.Split(',');
            var splitEngineer = model.Engineer_ID.Split(',');
            Guid trainingId = Guid.NewGuid();

            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            var ScreenObject = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("ScreenDatatraining");
            var trainingObject = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingEngineerModel>>("DataInsertTrainingEngineer");

            if (ScreenObject == null && trainingObject ==null)        
            {
                dataTrainingEngineer.TrainingId = trainingId;
                dataTrainingEngineer.LocationId = new Guid(splitlocation[0]);
                dataTrainingEngineer.LocationNameTh = splitlocation[1];
                dataTrainingEngineer.TeamId = new Guid(splitteam[0]);
                dataTrainingEngineer.TeamNameTh = splitteam[1];
                dataTrainingEngineer.EngineerId = new Guid(splitEngineer[0]);
                dataTrainingEngineer.StaffNameTh = splitEngineer[1];
                dataTrainingEngineer.CreateDate = DateTime.Now;
                dataTrainingEngineer.CreateUser = userProfile.Username;
                listdataTrainingEngineer.Add(dataTrainingEngineer);

                model.TrainingId = trainingId;
                model.location_name_th = splitlocation[1];           
                model.team_name_th = splitteam[1];
                model.Engineer_name = splitEngineer[1];
                model.Engineer_ID = splitEngineer[0];
                dataScreen.Add(model);
                HttpContext.Session.SetObjectAsJson("ScreenDatatraining", dataScreen);
                HttpContext.Session.SetObjectAsJson("DataInsertTrainingEngineer",listdataTrainingEngineer);

            }
            else
            {
                dataScreen = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("ScreenDatatraining");
                listdataTrainingEngineer = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingEngineerModel>>("DataInsertTrainingEngineer");

                dataTrainingEngineer.TrainingId = trainingId;
                dataTrainingEngineer.LocationId = new Guid(splitlocation[0]);
                dataTrainingEngineer.LocationNameTh = splitlocation[1];
                dataTrainingEngineer.TeamId = new Guid(splitteam[0]);
                dataTrainingEngineer.TeamNameTh = splitteam[1];
                dataTrainingEngineer.EngineerId = new Guid(splitEngineer[0]);
                dataTrainingEngineer.StaffNameTh = splitEngineer[1];
                dataTrainingEngineer.CreateDate = DateTime.Now;
                dataTrainingEngineer.CreateUser = userProfile.Username;
                listdataTrainingEngineer.Add(dataTrainingEngineer);

                model.TrainingId = trainingId;
                model.location_name_th = splitlocation[1];
                model.team_name_th = splitteam[1];
                model.Engineer_name = splitEngineer[1];
                model.Engineer_ID = splitEngineer[0];
                dataScreen.Add(model);

                HttpContext.Session.SetObjectAsJson("ScreenDatatraining", dataScreen);
                HttpContext.Session.SetObjectAsJson("DataInsertTrainingEngineer", listdataTrainingEngineer);
            }
            var training = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("ScreenDatatraining");

            return Json(training);
        }
  
        [HttpPut]
        public IActionResult Deletetraining(string[] TrainingId)
        {
            var dataScreen = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("ScreenDatatraining");
            var dataTrainingEngineer = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingEngineerModel>>("DataInsertTrainingEngineer");

            foreach (var item in TrainingId)
            {
                dataScreen.RemoveAll(x => x.TrainingId == new Guid(item));
                dataTrainingEngineer.RemoveAll(x => x.TrainingId == new Guid(item));
            }
            HttpContext.Session.SetObjectAsJson("ScreenDatatraining", dataScreen);
            HttpContext.Session.SetObjectAsJson("DataInsertTrainingEngineer", dataTrainingEngineer);
            var deltraining = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingModel>>("ScreenDatatraining");

            return Json(deltraining);
        }
        [HttpPost]
        public IActionResult RequestTraining(SubcontractProfileTrainingModel model)
        {
            ResponseModel result = new ResponseModel();
            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
                var training = HttpContext.Session.GetObjectFromJson<List<SubcontractProfileTrainingEngineerModel>>("DataInsertTrainingEngineer");

                var totalengineer = (training == null) ? 1 : training.Count();
                var spitcouse = model.Course.Split(',');
                var convertprice = Convert.ToInt32(spitcouse[1]);

                DateTime now = DateTime.Now;
                string DateString = now.ToString("yyyyMMdd");

                model.CompanyId = userProfile.companyid;
                model.Course = spitcouse[0];
                model.course_price = convertprice;
                model.TotalPrice = convertprice * totalengineer;
                model.Vat = (model.TotalPrice * 7) / 100;
                model.RequestNo = string.Format("{0}{1}", DateString, "000001");
                model.CreateDate = DateTime.Now;
                model.CreateBy = userProfile.Username;
                //model.TrainingId = Guid.NewGuid();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string uriStringTraining = string.Format("{0}", strpathAPI + "Training/Insert");
                var httpContentTraining = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage responseTraining = client.PostAsync(uriStringTraining, httpContentTraining).Result;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string uriStringTrainingEngineer = string.Format("{0}", strpathAPI + "TrainingEngineer/Insert");


                //if (responseTraining.IsSuccessStatusCode)
                //{
                //    result.Status = true;
                //    result.Message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                //    result.StatusError = "0";

                //}
                //else
                //{
                //    result.Message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อ Administrator.";
                //    result.Status = false;
                //    result.StatusError = "-1";
                //}
                if (training != null)
                {
                    foreach(var i in training)
                    {
                        var httpContentTrainingEngineer = new StringContent(JsonConvert.SerializeObject(i), Encoding.UTF8, "application/json");
                        HttpResponseMessage responseTrainingEngineer = client.PostAsync(uriStringTrainingEngineer, httpContentTrainingEngineer).Result;

                    }
                    

                }

            }
            catch (Exception ex)
            {
                result.Message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อ Administrator.";
                result.Status = false;
                result.StatusError = "0";

            }
           

            return Json(result);
        }
     
    }
}
