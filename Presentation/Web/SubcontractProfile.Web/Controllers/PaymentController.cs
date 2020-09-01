using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strpathAPI;
        private HttpClient client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";
        private SubcontractProfileUserModel dataUser = new SubcontractProfileUserModel();
        public PaymentController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            client = new HttpClient();
            _configuration = configuration;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            getsession();
        }
        public IActionResult ConfirmPayment()
        {
            //ViewData["Controller"] = "Payment";
            ViewData["Title"] = "ConfirmPayment";
            return View();
        }
        public IActionResult VerifyPayment()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetDataselect()
        {
            var data = new List<SubcontractProfilePaymentModel>();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Payment/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractProfilePaymentModel>>(dataresponse);
            }

            return Json(new { Data = data });
        }
        [HttpGet]
        public IActionResult Searchconfirmpayment(SubcontractProfileSearchPayment searchPayment)
        {
            var datapayment = new List<SubcontractProfilePaymentModel>();
            try
            {
                SubcontractProfilePaymentModel model = new SubcontractProfilePaymentModel();
                if (searchPayment.Paymentdatefrom != null)
                {
                    DateTime datefrom = DateTime.ParseExact(searchPayment.Paymentdatefrom, "yyyy-MM-dd", null);
                    model.PaymentDatetimeFrom = datefrom;
                }
                if (searchPayment.Paymentdateto != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchPayment.Paymentdateto, "yyyy-MM-dd", null);
                    model.PaymentDatetimeTo = dateto;
                }

                if (searchPayment.Paymantrequestdatefrom != null)
                {
                    DateTime datefrom = DateTime.ParseExact(searchPayment.Paymantrequestdatefrom, "yyyy-MM-dd", null);
                    model.RequestDateFrom = datefrom;
                }
                if (searchPayment.Paymentrequestdateto != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchPayment.Paymentrequestdateto, "yyyy-MM-dd", null);
                    model.RequestDateTo = dateto;
                }


                model.PaymentNo = searchPayment.Paymentno;
                model.Request_no = searchPayment.Paymentrequesttraningno;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var uripayment = new Uri(Path.Combine(strpathAPI, "Payment", "SearchPayment"));
                var httpContentPayment = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uripayment, httpContentPayment).Result;

               
                //string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}", strpathAPI + "Payment/SearchPayment"
                //                                , searchPayment.Paymentno, searchPayment.Paymentrequesttraningno, searchPayment.Paymantrequestdatefrom
                //                                , searchPayment.Paymentrequestdateto, searchPayment.Paymentdatefrom, searchPayment.Paymentdateto
                //                                , searchPayment.Paymentstatus,"null","null");
                //HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataresponse = response.Content.ReadAsStringAsync().Result;
                    datapayment = JsonConvert.DeserializeObject<List<SubcontractProfilePaymentModel>>(dataresponse);

                }
                return Json(new { data = datapayment });
            }
            catch (Exception ex)
            {
                return Json(new { data = datapayment });
            }

        }
        [HttpGet]
        public IActionResult GetByPaymentId(string id)
        {
            var data = new SubcontractProfilePaymentModel();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}/{1}", strpathAPI + "Payment/GetByPaymentId", id);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<SubcontractProfilePaymentModel>(dataresponse);
            }
            return Json(new { Data = data });

        }
        [HttpGet]
        public IActionResult paymentchannal()
        {
            var data = new List<SubcontractDropdownModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/payment_type");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(dataresponse);
            }
            return Json(new { Data = data });

        }
        [HttpGet]
        public IActionResult paymentchannalById(string id)
        {
            var data = new List<SubcontractDropdownModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/payment_type");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(dataresponse);
            }
            return Json(new { Data = data });

        }
        [HttpGet]
        public IActionResult Detailpaymentchannal()
        {
            var data = new List<SubcontractDropdownModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/bank_payment");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(dataresponse);
            }
            return Json(new { Data = data });

        }
        [HttpGet]
        public IActionResult BankTransfer()
        {
            var data = new List<SubcontractProfileBankingModel>();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}", strpathAPI + "Banking/GetAll");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<SubcontractProfileBankingModel>>(dataresponse);
            }
            return Json(new { Data = data });

        }
        [HttpPut]
        public IActionResult Updatepayment(SubcontractProfilePaymentModel Payment)
        {
            string dataresponse = "";
            bool status = false;
            if (Payment.FileSilp != null)
            {
                Payment.SlipAttachFile = Payment.FileSilp.FileName;
            }
            Payment.ModifiedDate = DateTime.Now;
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string uriString = string.Format("{0}", strpathAPI + "Payment/Update");
                var httpContent = new StringContent(JsonConvert.SerializeObject(Payment), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(uriString, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    dataresponse = response.Content.ReadAsStringAsync().Result;
                    status = true;
                }
            }
            catch (Exception ex)
            {
                dataresponse = ex.Message;
            }

            return Json(new { Date = dataresponse, Status = status });
        }

        #region VerifyPayment
        [HttpPost]
        //public IActionResult SearchVerify(string? companyname,string? taxid,
        //                                  //string locationid,string teamid,
        //                                  string? paymentno,string paystatus,
        //                                  string? paymentdatefrom,string? paymentdateto)
        public IActionResult SearchVerify(SubcontractProfilePaymentViewModel searchmodel)
        {
            var paymentResult = new List<SubcontractProfilePaymentModel>();
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
            try
            {
                SubcontractProfilePaymentModel model = new SubcontractProfilePaymentModel();

                if (searchmodel.PaymentDatetimeFrom != null)
                {
                    DateTime datefrom = DateTime.ParseExact(searchmodel.PaymentDatetimeFrom, "dd/MM/yyyy", null);
                    model.PaymentDatetimeFrom = datefrom;
                }
                if (searchmodel.PaymentDatetimeTo != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchmodel.PaymentDatetimeTo, "dd/MM/yyyy", null);
                    model.PaymentDatetimeTo = dateto;
                }

                model.PaymentNo = searchmodel.PaymentNo;
                model.RequestDateFrom = null;
                model.RequestDateTo = null;
                model.Request_no = null;
                model.companyNameTh = searchmodel.companyNameTh;
                model.taxId = searchmodel.taxId;
                model.Status = searchmodel.Status;

                var uripayment = new Uri(Path.Combine(strpathAPI, "Payment", "SearchPayment"));
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string dd = JsonConvert.SerializeObject(model);

                var httpContentPayment = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uripayment, httpContentPayment).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    //data
                    paymentResult = JsonConvert.DeserializeObject<List<SubcontractProfilePaymentModel>>(result);

                }

                //total number of rows count   
                recordsTotal = paymentResult.Count();

                //Paging   
                var data = paymentResult.Skip(skip).Take(pageSize).ToList();

                // Returning Json Data
                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = paymentResult });
                throw;
            }
            
        }

        //[HttpPost]
        //public IActionResult GetLocationByCompany(string companyid)
        //{
        //    var output = new List<SubcontractProfileLocationModel>();
        //    List<SelectListItem> getAllLocationList = new List<SelectListItem>();

        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));

        //    string uriString = companyid == "" ? string.Format("{0}", strpathAPI + "Location/GetAll") : 
        //                                         string.Format("{0}/{1}", strpathAPI + "Province/GetLocationByCompany", companyid);
        //    HttpResponseMessage response = client.GetAsync(uriString).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var v = response.Content.ReadAsStringAsync().Result;
        //        output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
        //    }
        //    if (Lang == "")
        //    {
        //        getsession();
        //    }
        //    if (Lang == "TH")
        //    {
        //        output.Add(new SubcontractProfileLocationModel
        //        {
        //            LocationCode = "",
        //            LocationNameTh = "กรุณาเลือก Location"
        //        }) ;

        //        foreach (var r in output)
        //        {
        //            if (r.LocationCode == "")
        //            {
        //                getAllLocationList.Add(new SelectListItem
        //                {
        //                    Text = "กรุณาเลือก Location",
        //                    Value = ""
        //                });
        //            }
        //            else
        //            {
        //                getAllLocationList.Add(new SelectListItem
        //                {
        //                    Text = r.LocationNameTh,
        //                    Value = r.LocationId.ToString()
        //                });
        //            }
        //        }
        //    }
        //    else
        //    {
        //        output.Add(new SubcontractProfileLocationModel
        //        {
        //            LocationCode = "",
        //            LocationNameEn = "Select Location"
        //        });
        //        foreach (var r in output)
        //        {
        //            if (r.LocationCode == "")
        //            {
        //                getAllLocationList.Add(new SelectListItem
        //                {
        //                    Text = "Select Location",
        //                    Value = ""
        //                });
        //            }
        //            else
        //            {
        //                getAllLocationList.Add(new SelectListItem
        //                {
        //                    Text = r.LocationNameEn,
        //                    Value = r.LocationId.ToString()
        //                });
        //            }
        //        }

        //    }

        //    var result = getAllLocationList.OrderBy(c => c.Value).ToList();

        //    return Json(result);

        //}

        //[HttpPost]
        //public IActionResult DDLTeam(string companyid, string locationId)
        //{
        //    var output = new List<SubcontractProfileTeamModel>();
        //    List<SelectListItem> getAllTeamList = new List<SelectListItem>();

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));

        //    string uriString = "";

        //    uriString = string.Format("{0}/{1}/{2}", strpathAPI + "Team/GetByLocationId", companyid, locationId);


        //    HttpResponseMessage response = client.GetAsync(uriString).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var v = response.Content.ReadAsStringAsync().Result;
        //        output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
        //    }
        //    if (Lang == null || Lang == "")
        //    {
        //        getsession();
        //    }
        //    if (Lang == "TH")
        //    {
        //        output.Add(new SubcontractProfileTeamModel
        //        {
        //            TeamCode = ""

        //        }); ;

        //        foreach (var r in output)
        //        {
        //            if (r.TeamCode == "")
        //            {
        //                getAllTeamList.Add(new SelectListItem
        //                {
        //                    Text = "กรุณาเลือก Team",
        //                    Value = ""
        //                });
        //            }
        //            else
        //            {
        //                getAllTeamList.Add(new SelectListItem
        //                {
        //                    Text = r.TeamNameTh,
        //                    Value = r.TeamId.ToString()
        //                });
        //            }
        //        }

        //    }
        //    else
        //    {
        //        output.Add(new SubcontractProfileTeamModel
        //        {
        //            LocationCode = ""

        //        });

        //        foreach (var r in output)
        //        {
        //            if (r.LocationCode == "")
        //            {
        //                getAllTeamList.Add(new SelectListItem
        //                {
        //                    Text = "Select Team",
        //                    Value = ""
        //                });
        //            }
        //            else
        //            {
        //                getAllTeamList.Add(new SelectListItem
        //                {
        //                    Text = r.TeamNameEn,
        //                    Value = r.TeamId.ToString()
        //                });
        //            }
        //        }

        //    }
        //    var result = getAllTeamList.OrderBy(c => c.Value).ToList();

        //    return Json(new { response = result });
        //}

        #endregion

        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if (Lang == null || Lang == "")
            {
                Lang = "TH";
            }
            dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }

    }
}
