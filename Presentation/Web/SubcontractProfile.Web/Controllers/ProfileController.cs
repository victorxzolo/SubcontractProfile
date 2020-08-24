using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SubcontractProfile.Web.Extension;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using SubcontractProfile.Web.Model;
using SubcontractProfile;
using System.Data;
using Microsoft.AspNetCore.Http.Connections;
using System.IO;

namespace SubcontractProfile.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strpathAPI;
        private string strpathASCProfile;

        public ProfileController(IConfiguration configuration)
        {
            _configuration = configuration;


            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();

            strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();
        }

        public IActionResult CompanyIndex()
        {
            return View();
        }
        public IActionResult EngineerIndex()
        {
            return View();
        }
        public IActionResult LocationIndex()
        {
            return View();
        }

        public IActionResult TeamIndex()
        {
            return View();
        }

        #region Company

        [HttpPost]
        public IActionResult SearchCompany(SubcontractSearchCompanyModel Company)
        {
            int filteredResultsCount;
            int totalResultsCount;
            var output = new List<SubcontractProfileCompanyModel>();
            //var resultZipCode = new List<SubcontractProfileCompanyModel>();
            var model = new List<SubcontractSearchCompanyModel>();
           //string aaavvvv = "/ / / /ร้านกิ่วลมไอที/ / / / /";
            if (Company != null)
            {
                if (Company.CompanyAlias == null) Company.CompanyAlias = "null";
                if (Company.CompanyNameEn == null) Company.CompanyNameEn = "null";
                if (Company.CompanyNameTh == null) Company.CompanyNameTh = "null";
                if (Company.CompanyCode == null) Company.CompanyCode = "null";

                Company.ProfileType = "null";
                Company.LocationCode = "null";
                Company.VendorCode = "null";
                Company.DistibutionChannel = "null";
                Company.ChannelGroup = "null";


                //Command.Handle(model)
                //output ret_code,ret
                //ret_msg
                HttpClient client = new HttpClient();
                
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                
                //string uriString = string.Format( strpathAPI + "Company/SearchCompany", Company.ProfileType,
                //   Company.LocationCode, Company.VendorCode, Company.CompanyNameTh, Company.CompanyNameEn, Company.CompanyAlias, Company.CompanyCode,
                //   Company.DistibutionChannel, Company.ChannelGroup);
                string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}", strpathAPI + "Company/SearchCompany", Company.ProfileType,
                    Company.LocationCode, Company.VendorCode, Company.CompanyNameTh, Company.CompanyNameEn, Company.CompanyAlias, Company.CompanyCode,
                    Company.DistibutionChannel, Company.ChannelGroup);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                return Json(new
                {
                    draw = 10 ,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }
            else
            {
                return Json(new
                {
                    status = "-1",
                    message = "Data isnot correct, Please Check Data or Contact System Admin"
                });
            }
        }

        [HttpPost]
        public IActionResult GetCompany(string CompanyID)
        {
            int filteredResultsCount;
            int totalResultsCount;

            if (CompanyID != "0")
            {
                var output = new SubcontractProfileCompanyModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", CompanyID);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(v);
                }
                return Json(new { response = output });
            }
            else
            {
                var output = new List<SubcontractProfileCompanyModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Company/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                //return Json(new { response = output });
                return Json(new
                {
                    draw = 10,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }

            //return Json(new { response = output, responsezipcode = resultZipCode });
        }

        [HttpPost]
        public IActionResult UpdateCompany(SubcontractProfileCompanyModel model)
        {
            ResponseModel res = new ResponseModel();
            if (model != null)
            {
                model.UpdateDate = DateTime.Now;
                model.UpdateBy = "ADMIN";
                //Path 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = new Uri(Path.Combine(strpathAPI, "Company", "Update"));

                //string str = JsonConvert.SerializeObject(Company);

                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(uri, httpContent).Result;


                //HttpResponseMessage response = client.PutAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    res.Status = true;
                    res.Message = "Success";
                    res.StatusError = "-1";
                }
                else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }
            }
            return Json(new { Response = res });
        }

        [HttpPost]
        public IActionResult DeleteCompany(string CompanyId)
        {
            var output = new List<SubcontractProfileCompanyModel>();
            ResponseModel res = new ResponseModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpClient client = new HttpClient();
            //string uriString = string.Format("{0}{1}", uri, id);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = client.DeleteAsync(uriString).Result;

            string uriString = string.Format("{0}/{1}", strpathAPI + "Company/Delete", CompanyId);
            HttpResponseMessage response = client.DeleteAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                res.Status = true;
                res.Message = "Register Success";
                res.StatusError = "0";
            }

            return Json(new { Response = res});
        }

        #endregion

        #region Engineer
        [HttpPost]
        public IActionResult GetEngineer(string EngineerId)
        {
            int filteredResultsCount;
            int totalResultsCount;

            if (EngineerId != "0")
            {
                var output = new SubcontractProfileEngineerModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Engineer/GetByEngineerId", EngineerId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfileEngineerModel>(v);
                }
                return Json(new { response = output});
            }
            else
            {
                var output = new List<SubcontractProfileEngineerModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Engineer/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                //return Json(new { response = output });
                return Json(new
                {
                    draw = 10,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }

        }


        [HttpPost]
        public IActionResult AddEngineer(SubcontractProfileEngineerModel model)
        {
            try
            {

                if (model != null)
                {
                    model.EngineerId = Guid.NewGuid();
                    model.CreateDate = DateTime.Now;
                    model.CreateBy = "ADMIN";
                    var uri = new Uri(Path.Combine(strpathAPI, "Engineer", "Insert"));
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = client.PostAsync(uri, httpContentCompany).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            status = "1",
                            message = "Success"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "No Success"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = "-1",
                        message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                    });
                }

            }
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateEngineer(SubcontractProfileEngineerModel model)
        {
            ResponseModel res = new ResponseModel();
            if (model != null)
            {
                model.UpdateDate = DateTime.Now;
                model.UpdateBy = "ADMIN";
                //Path 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = new Uri(Path.Combine(strpathAPI, "Engineer", "Update"));
                //string str = JsonConvert.SerializeObject(Company);

                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(uri, httpContent).Result;

                //HttpResponseMessage response = client.PutAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    res.Status = true;
                    res.Message = "Success";
                    res.StatusError = "-1";
                }
                else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }
            }
            return Json(new { Response = res });
        }

        [HttpPost]
        public IActionResult DeleteEngineer(string EngineerId)
        {
            var output = new List<SubcontractProfileEngineerModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Engineer/Delete", EngineerId);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(v);
            }

            return Json(new { response = output });
        }
        #endregion

        #region Location

        [HttpPost]
        public IActionResult SearchLocation(SubcontractSearchLocationModel Location)
        {
            int filteredResultsCount;
            int totalResultsCount;
            var output = new List<SubcontractProfileLocationModel>();
            //var resultZipCode = new List<SubcontractProfileCompanyModel>();
            //var model = new List<SubcontractProfileLocationModel>();
            //string aaavvvv = "/ / / /ร้านกิ่วลมไอที/ / / / /";
            if (Location != null)
            {
                if (Location.CompanyId == null) Location.CompanyId = Guid.NewGuid();
                if (Location.LocationCode == null) Location.LocationCode = "null";
                if (Location.LocationNameTh == null) Location.LocationNameTh = "null";
                if (Location.LocationNameEn == null) Location.LocationNameEn = "null";
                if (Location.Phone == null) Location.Phone = "null";


                //Command.Handle(model)
                //output ret_code,ret
                //ret_msg
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", strpathAPI + "Location/SearchLocation", Location.CompanyId,
                    Location.LocationCode, Location.LocationNameTh, Location.LocationNameEn, Location.Phone);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                return Json(new
                {
                    draw = 10,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }
            else
            {
                return Json(new
                {
                    status = "-1",
                    message = "Data isnot correct, Please Check Data or Contact System Admin"
                });
            }
        }

        [HttpPost]
        public IActionResult GetLocation(string LocationId)
        {
            int filteredResultsCount;
            int totalResultsCount;

            if (LocationId != "0")
            {
                var output = new SubcontractProfileLocationModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetByLocationId", LocationId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfileLocationModel>(v);
                }
                return Json(new { response = output });
            }
            else
            {
                var output = new List<SubcontractProfileLocationModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Location/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                //return Json(new { response = output });
                return Json(new
                {
                    draw = 10,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }

            
        }

        [HttpPost]
        public IActionResult AddLocation(SubcontractProfileLocationModel model)
        {
            try
            {

                //var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileLocationModel>>(HttpContext.Session, "Engineer");
                //model.ListLocation = new List<SubcontractProfileLocationModel>();

                if (model != null)
                {
                    model.LocationId = Guid.NewGuid();
                    model.CreateDate = DateTime.Now;
                    model.CreateBy = "ADMIN";
                    var uri = new Uri(Path.Combine(strpathAPI, "Location", "Insert"));
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = client.PostAsync(uri, httpContentCompany).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            status = "1",
                            message = "Success"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "No Success"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = "-1",
                        message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                    });
                }

            }
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateLocation()
        {
            var output = new List<SubcontractProfileLocationModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Location/Update");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
            }

            return Json(new { response = output });
        }

        [HttpPost]
        public IActionResult DeleteLocation(string LocationId)
        {
            ResponseModel res = new ResponseModel();
            var output = new List<SubcontractProfileLocationModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/Delete", LocationId);
            HttpResponseMessage response = client.DeleteAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                res.Status = true;
                res.Message = "Delete Success";
                res.StatusError = "0";
            }

            return Json(new { Response = res });
        }

        #endregion

        #region team

        [HttpPost]
        public IActionResult GetTeam(string TeamId)
        {
            int filteredResultsCount;
            int totalResultsCount;
            

            if (TeamId != "0")
            {
                var output = new SubcontractProfileTeamModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Team/GetByTeamId", TeamId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfileTeamModel>(v);
                    //resultZipCode = output.GroupBy(c => c.TeamId).Select(g => g.First()).ToList();
                }
                return Json(new { response = output});
            }
            else
            {
                var output = new List<SubcontractProfileTeamModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Team/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                //return Json(new { response = output });
                return Json(new
                {
                    draw = 10,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }
        }

        [HttpPost]
        public IActionResult AddTeam(SubcontractProfileTeamModel model)
        {
            try
            {
                if (model != null)
                {
                    model.TeamId = Guid.NewGuid();
                    model.CreateDate = DateTime.Now;
                    model.CreateBy = "ADMIN";
                    var uri = new Uri(Path.Combine(strpathAPI, "Team", "Insert"));
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = client.PostAsync(uri, httpContent).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            status = "1",
                            message = "Success"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "No Success"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = "-1",
                        message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                    });
                }

            }
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateTeam(SubcontractProfileTeamModel model)
        {
            ResponseModel res = new ResponseModel();
            if (model != null)
            {
                model.UpdateDate = DateTime.Now;
                model.UpdateBy = "ADMIN";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = new Uri(Path.Combine(strpathAPI, "Team", "Update"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(uri, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    res.Status = true;
                    res.Message = "Success";
                    res.StatusError = "-1";
                }
                else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }
            }
            return Json(new { Response = res
        });
        }

        [HttpPost]
        public IActionResult DeleteTeam(string TeamId)
        {
            var output = new List<SubcontractProfileTeamModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Team/Delete", TeamId);
            HttpResponseMessage response = client.DeleteAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
            }

            return Json(new { response = output });
        }

        #endregion

        #region Address
        [HttpPost]
        public IActionResult GetAddress(string AddressId)
        {
            int filteredResultsCount;
            int totalResultsCount;

            if (AddressId != "0")
            {
                var output = new SubcontractProfileAddressModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Address/GetByAddressId", AddressId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfileAddressModel>(v);
                }
                return Json(new { response = output });
            }
            else
            {
                var output = new List<SubcontractProfileAddressModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Address/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileAddressModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                //return Json(new { response = output });
                return Json(new
                {
                    draw = 10,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }

        }
        [HttpPost]
        public IActionResult AddAddress(SubcontractProfileAddressModel model)
        {
            try
            {

                    if (model != null)
                    {
                        model.AddressId = Guid.NewGuid();
                        model.CreateDate = DateTime.Now;
                        model.CreateBy = "ADMIN";
                    var uri = new Uri(Path.Combine(strpathAPI, "Address", "Insert"));
                    HttpClient client= new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = client.PostAsync(uri, httpContentCompany).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            status = "1",
                            message = "Success"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "No Success"
                    });
                }
                    else
                    {
                        return Json(new
                        {
                            status = "-1",
                            message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                        });
                    }
                    
            }
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }
        [HttpPost]
        public IActionResult DeleteAddress(string AddressId)
        {
            //var output = new List<SubcontractProfileCompanyModel>();
            ResponseModel res = new ResponseModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpClient client = new HttpClient();
            //string uriString = string.Format("{0}{1}", uri, id);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = client.DeleteAsync(uriString).Result;

            string uriString = string.Format("{0}/{1}", strpathAPI + "Address/Delete", AddressId);
            HttpResponseMessage response = client.DeleteAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                res.Status = true;
                res.Message = "Delete Success";
                res.StatusError = "0";
            }

            return Json(new { Response = res });
        }

        [HttpPost]
        public IActionResult UpdateAddress(SubcontractProfileAddressModel model)
        {
            ResponseModel res = new ResponseModel();
            if (model != null)
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = "ADMIN";
                //Path 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = new Uri(Path.Combine(strpathAPI, "Address", "Update"));
                //string str = JsonConvert.SerializeObject(Company);

                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(uri, httpContent).Result;

                //HttpResponseMessage response = client.PutAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    res.Status = true;
                    res.Message = "Success";
                    res.StatusError = "-1";
                }
                else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }
            }
            return Json(new { Response = res });
        }

        #endregion

        #region Personal
        [HttpPost]
        public IActionResult GetPersonal(string PersonalId)
        {
            int filteredResultsCount;
            int totalResultsCount;

            if (PersonalId != "0")
            {
                var output = new SubcontractProfilePersonalModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Personal/GetByPersonalId", PersonalId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfilePersonalModel>(v);
                }
                return Json(new { response = output });
            }
            else
            {
                var output = new List<SubcontractProfilePersonalModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Personal/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfilePersonalModel>>(v);
                }
                filteredResultsCount = output.Count(); //output from Database
                totalResultsCount = output.Count(); //output from Database
                //return Json(new { response = output });
                return Json(new
                {
                    draw = 10,//draw = tbModel,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = output
                });
            }

        }

        [HttpPost]
        public IActionResult AddPersonal(SubcontractProfilePersonalModel model)
        {
            try
            {

                if (model != null)
                {
                    model.PersonalId = Guid.NewGuid();
                    model.CreateDate = DateTime.Now;
                    model.CreateBy = "ADMIN";
                    var uri = new Uri(Path.Combine(strpathAPI, "Personal", "Insert"));
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseCompany = client.PostAsync(uri, httpContentCompany).Result;
                    if (responseCompany.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            status = "1",
                            message = "Success"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "No Success"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = "-1",
                        message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                    });
                }

            }
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdatePersonal(SubcontractProfilePersonalModel model)
        {
            ResponseModel res = new ResponseModel();
            if (model != null)
            {
                model.UpdateDate = DateTime.Now;
                model.UpdateBy = "ADMIN";
                //Path 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = new Uri(Path.Combine(strpathAPI, "Personal", "Update"));
                //string str = JsonConvert.SerializeObject(Company);

                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(uri, httpContent).Result;

                //HttpResponseMessage response = client.PutAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    res.Status = true;
                    res.Message = "Success";
                    res.StatusError = "-1";
                }
                else
                {
                    res.Status = false;
                    res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }
            }
            return Json(new { Response = res });
        }
        #endregion


        #region DDL
        [HttpPost]
        public IActionResult DDLTitle()
        {
            var output = new List<SubcontractProfileTitleModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Title/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileTitleModel>>(v);
            }



            return Json(new { response = output });
        }

        [HttpPost]
        public IActionResult DDLsubcontract_profile_province()
        {
            var output = new List<SubcontractProfileProvinceModel>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Province/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(v);
            }

            return Json(new { response = output });
        }

        public IActionResult DDLsubcontract_profile_district(int province_id = 0)
        {
            var output = new List<SubcontractProfileDistrictModel>();
            if (province_id != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "District/GetDistrictByProvinceId", province_id);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "District/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                }
            }


            return Json(new { response = output });
        }

        [HttpPost]
        public IActionResult DDLsubcontract_profile_sub_district(int district_id = 0)
        {
            var output = new List<SubcontractProfileSubDistrictModel>();
            var resultZipCode = new List<SubcontractProfileSubDistrictModel>();

            if (district_id != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "SubDistrict/GetSubDistrictByDistrict", district_id);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);
                    resultZipCode = output.GroupBy(c => c.ZipCode).Select(g => g.First()).ToList();
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "SubDistrict/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);
                    resultZipCode = output.GroupBy(c => c.ZipCode).Select(g => g.First()).ToList();
                }
            }

            return Json(new { response = output, responsezipcode = resultZipCode });
        }
        [HttpPost]
        public IActionResult DDLregion_profile_region(int region_id = 0)
        {
            var output = new List<SubcontractProfileRegionModel>();

            if (region_id != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Region/GetByRegionId", region_id);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileRegionModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Region/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileRegionModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLbank_profile_bank(int bank_id = 0)
        {

            if (bank_id != 0)
            {
                var output = new SubcontractProfileBankingModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Banking/GetByBankId", bank_id);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfileBankingModel>(v);
                }
                return Json(new { response = output });
            }
            else
            {
                var output = new List<SubcontractProfileBankingModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Banking/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileBankingModel>>(v);
                }
                return Json(new { response = output });
            }

            
        }
        [HttpPost]
        public IActionResult DDLnationality(int nationality_Id = 0)
        {
            var output = new List<SubcontractProfileNationalityModel>();

            if (nationality_Id != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Nationality/GetByNationalityId", nationality_Id);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileNationalityModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Nationality/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileNationalityModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLverhicleBrand(int verhicleBrandId = 0)
        {
            var output = new List<SubcontractProfileVerhicleBrandModel>();

            if (verhicleBrandId != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "VerhicleBrand/GetByVerhicleBrandId", verhicleBrandId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleBrandModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "VerhicleBrand/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleBrandModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLverhicleSerise(int verhicleSeriseId = 0)
        {
            var output = new List<SubcontractProfileVerhicleSeriseModel>();

            if (verhicleSeriseId != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "VerhicleSerise/GetByVerhicleSeriseId", verhicleSeriseId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleSeriseModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "VerhicleSerise/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleSeriseModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLverhicleType(int verhicleTypeId = 0)
        {
            var output = new List<SubcontractProfileVerhicleTypeModel>();

            if (verhicleTypeId != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "VerhicleType/GetByVerhicleTypeId", verhicleTypeId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleTypeModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "VerhicleType/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileVerhicleTypeModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLrace(int raceId = 0)
        {
            var output = new List<SubcontractProfileRaceModel>();

            if (raceId != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Race/GetByRaceId", raceId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileRaceModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Race/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileRaceModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLreligion(int religionId = 0)
        {
            var output = new List<SubcontractProfileReligionModel>();

            if (religionId != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Religion/GetByReligionId", religionId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileReligionModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Religion/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileReligionModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        [HttpPost]
        public IActionResult DDLwarranty(int warrantyId = 0)
        {
            var output = new List<SubcontractProfileWarrantyModel>();

            if (warrantyId != 0)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Warranty/GetByWarrantyId", warrantyId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileWarrantyModel>>(v);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Warranty/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileWarrantyModel>>(v);
                }
            }

            return Json(new { response = output });
        }
        #endregion

        #region Upload File


        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Uploadfile(IList<IFormFile> files, string fid, string type_file)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            //FileStream output;
            string strmess = "";
            try
            {
                foreach (FormFile source in files)
                {
                    if (source.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        //using (output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                        //    await source.CopyToAsync(output);


                        Guid id = Guid.NewGuid();
                        using (var ms = new MemoryStream())
                        {
                            source.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            L_File.Add(new FileUploadModal
                            {
                                file_id = id,
                                Fileupload = fileBytes,
                                typefile = type_file,
                                ContentDisposition = source.ContentDisposition,
                                ContentType = source.ContentType,
                                Filename = filename
                            });
                        }
                    }
                    var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaft");
                    //byte[] byteArrayValue = HttpContext.Session.Get("userUploadfileDaft");
                    //var data = FromByteArray<List<FileUploadModal>>(byteArrayValue);

                    //var objComplex = HttpContext.Session.GetObject("userUploadfileDaft");

                    if (data != null)
                    {

                        data.RemoveAll(x => x.file_id.ToString() == fid);
                        data.Add(L_File[0]);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", data);

                    }
                    else
                    {
                        // HttpContext.Session.Set("userUploadfileDaft", ToByteArray<List<FileUploadModal>>(L_File));

                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaft", L_File);
                    }


                }
                strmess = "Upload file success";
            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
                throw;
            }


            return Json(new { status = statusupload, message = strmess, response = L_File[0].file_id });

            // return Json(new { status = statusupload, message = strmess });
        }

        private async Task<bool> GetFile(FileUploadModal file, string guid)
        {
            FileStream output;
            try
            {
                var stream = new MemoryStream(file.Fileupload);
                FormFile files = new FormFile(stream, 0, file.Fileupload.Length, "name", "fileName");

                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = EnsureCorrectFilename(file.Filename);
                using (output = System.IO.File.Create(this.GetPathAndFilename(guid, filename)))
                    await files.CopyToAsync(output);
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
            return true;
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string guid, string filename)
        {
            //return this.hostingEnvironment.WebRootPath + "\\uploads\\" + filename;
            string dir = _configuration.GetValue<string>("PathUploadfile:Local").ToString();
            string pathdir = Path.Combine(dir, guid);
            string PathOutput = "";
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
            PathOutput = Path.Combine(pathdir, filename);
            return PathOutput;
        }

        #endregion

        [HttpPost]
        public IActionResult getCodeBank(string bank_id)
        {

            if (bank_id != "0")
            {
                var output = new SubcontractProfileBankingModel();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "Banking/GetByBankId", bank_id);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<SubcontractProfileBankingModel>(v);
                }
                return Json(new { response = output });
            }
            else
            {
                var output = new List<SubcontractProfileBankingModel>();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Banking/GetAll");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileBankingModel>>(v);
                }
                return Json(new { response = output });
            }


        }

        public IActionResult _PartialCompany()
        {
            return PartialView("_PartialCompanyEdit");
        }
        public IActionResult _PartialTeam()
        {
            return PartialView("_PartialTeamEdit");
        }
        public IActionResult _PartialLocation()
        {
            return PartialView("_PartialLocationEdit");
        }
        public IActionResult _PartialEngineer()
        {
            return PartialView("_PartialEngineerEdit");
        }
    }


    public class subcontract_profile_New_Company 
    {

        public List<SubcontractProfileCompanyModel> ListCompany{ get; set; }

        public List<SubcontractProfileEngineerModel> ListEngineer { get; set; }

        public List<SubcontractProfileLocationModel> ListLocation { get; set; }

        public List<SubcontractProfileTeamModel> ListTeam { get; set; }

    }
}