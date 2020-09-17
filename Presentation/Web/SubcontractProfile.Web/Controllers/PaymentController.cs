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
        private readonly string strpathUpload;

        private const int MegaBytes =3* 1024 * 1024;
        public PaymentController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            client = new HttpClient();
            _configuration = configuration;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();
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
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
            if (userProfile == null)
            {
                return RedirectToAction("LogonByUser", "LogonByUser");
            }
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

        [HttpPost]
        public IActionResult Searchconfirmpayment(SubcontractProfileSearchPayment searchPayment)
        {
            var datapayment = new List<SubcontractProfilePaymentModel>();
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
                if (searchPayment.Paymentdatefrom != null)
                {
                    DateTime datefrom = DateTime.ParseExact(searchPayment.Paymentdatefrom, "dd/MM/yyyy", null);
                    model.PaymentDatetimeFrom = datefrom;
                }
                if (searchPayment.Paymentdateto != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchPayment.Paymentdateto, "dd/MM/yyyy", null);
                    model.PaymentDatetimeTo = dateto;
                }

                if (searchPayment.Paymantrequestdatefrom != null)
                {
                    DateTime datefrom = DateTime.ParseExact(searchPayment.Paymantrequestdatefrom, "dd/MM/yyyy", null);
                    model.RequestDateFrom = datefrom;
                }
                if (searchPayment.Paymentrequestdateto != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchPayment.Paymentrequestdateto, "dd/MM/yyyy", null);
                    model.RequestDateTo = dateto;
                }
                if (dataUser == null)
                {
                    getsession();
                }
                else
                {
                    model.CompanyId = dataUser.companyid;
                }
               
                model.PaymentNo = searchPayment.Paymentno;
                model.Request_no = searchPayment.Paymentrequesttraningno;
                model.Status = searchPayment.Paymentstatus;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string str = JsonConvert.SerializeObject(model);
                var uripayment = new Uri(Path.Combine(strpathAPI, "Payment", "SearchPayment"));
                var httpContentPayment = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uripayment, httpContentPayment).Result;


                //string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}", strpathAPI + "Payment/SearchPayment"
                //                                , searchPayment.Paymentno, searchPayment.Paymentrequesttraningno, searchPayment.Paymantrequestdatefrom
                //                                , searchPayment.Paymentrequestdateto, searchPayment.Paymentdatefrom, searchPayment.Paymentdateto
                //                                , searchPayment.Paymentstatus, "null", "null");
                //HttpResponseMessage response = client.GetAsync(uriString).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dataresponse = response.Content.ReadAsStringAsync().Result;
                    datapayment = JsonConvert.DeserializeObject<List<SubcontractProfilePaymentModel>>(dataresponse);

                }
                recordsTotal = datapayment.Count();
                //Paging   
                var data = datapayment.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = datapayment });
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
                List<FileUploadModal> L_File = new List<FileUploadModal>();


                if (data.SlipAttachFile != null)
                {
                    Guid file_id = Guid.NewGuid();
                    L_File.Add(new FileUploadModal
                    {
                        file_id = file_id,
                        //Fileupload = fileBytes,
                        typefile = "SlipAttachFile",
                        //ContentDisposition = source.ContentDisposition,
                        //ContentType = source.ContentType,
                        Filename = data.SlipAttachFile

                    });
                    data.file_id_Slip = file_id;
                    //if (L_File.Count != 0)
                    //{
                    //    GetFile(paymentId, ref L_File);
                    //    SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftPaymentSSO", L_File);
                    //}
                }
            }
            return Json(new { Data = data });

        }
        //[HttpGet]
        //public IActionResult paymentchannal()
        //{
        //    var data = new List<SubcontractDropdownModel>();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/payment_type");
        //    HttpResponseMessage response = client.GetAsync(uriString).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var dataresponse = response.Content.ReadAsStringAsync().Result;
        //        data = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(dataresponse);
        //    }
        //    return Json(new { Data = data });

        //}
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

        [HttpPost]
        public async Task<IActionResult> Updatepayment(SubcontractProfilePaymentModel Payment)
        {
            string dataresponse = "";
            bool status = false;
           
            try
            {
                if(dataUser ==null)
                {
                    getsession();
                }
                Payment.ModifiedDate = DateTime.Now;
                Payment.ModifiedBy = dataUser.UserId.ToString();
                Payment.verifiedDate = DateTime.Now;

                if (Payment.datetimepayment != null)
                {
                    DateTime datefrom = DateTime.ParseExact(Payment.datetimepayment, "dd/MM/yyyy HH:mm", null);
                    Payment.PaymentDatetime = datefrom;
                }

                
                if (await Uploadfile(Payment.FileSilp, Payment.PaymentId, dataUser.UserId.ToString()))
                {
                    Payment.SlipAttachFile = Payment.FileSilp.FileName;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string uriString = string.Format("{0}", strpathAPI + "Payment/Update");
                    var httpContent = new StringContent(JsonConvert.SerializeObject(Payment), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(uriString, httpContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dataresponse = response.Content.ReadAsStringAsync().Result;
                        status= JsonConvert.DeserializeObject<bool>(dataresponse);
                    }
                }
                else
                {
                    status = false;
                }
                
            }
            catch (Exception ex)
            {
                dataresponse = ex.Message;
            }

            return Json(new { Data = dataresponse, Status = status });
        }

        #region VerifyPayment
        [HttpPost]
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

               // model.PaymentNo = searchmodel.PaymentNo;
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

        [HttpPost]
        public IActionResult BindDataTypeTransfer() {
            var output = new List<SubcontractDropdownModel>();
            List<SelectListItem> getAllPaymenttypeList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/payment_type");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
            }
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractDropdownModel
                {
                    dropdown_text="กรุณาเลือกช่องทางการชำระเงิน",
                    dropdown_value=""
                    
                });

                getAllPaymenttypeList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractDropdownModel
                {
                    dropdown_text = "Select Payment Transfer",
                    dropdown_value = ""
                });
                getAllPaymenttypeList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();

            }

            return Json(new { response = getAllPaymenttypeList });
        }

        [HttpPost]
        public IActionResult GetDataBankPayment() {
            var output = new List<SubcontractDropdownModel>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/bank_payment");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
            }
            return Json(new { response = output });
        }


        [HttpPost]
        public IActionResult DDLBank()
        {
            var output = new List<SubcontractProfileBankingModel>();
            List<SelectListItem> getAllBankList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Banking/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileBankingModel>>(v);
            }
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileBankingModel
                {
                    BankCode = "0",
                    BankName = "กรุณาเลือกชื่อธนาคาร"
                });

                getAllBankList = output.Select(a => new SelectListItem
                {
                    Text = a.BankName,
                    Value = a.BankCode
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractProfileBankingModel
                {
                    BankCode = "0",
                    BankName = "Select Bank"
                });
                getAllBankList = output.Select(a => new SelectListItem
                {
                    Text = a.BankName,
                    Value = a.BankCode
                }).OrderBy(c => c.Value).ToList();

            }

            return Json(new { responsebank = getAllBankList });
        }

        [HttpPost]
        public IActionResult GetDataStatus()
        {
            var output = new List<SubcontractDropdownModel>();
            List<SelectListItem> getList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/payment_status");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
            }
            if (Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractDropdownModel
                {
                    dropdown_text = "กรุณาเลือกสถานะ",
                    dropdown_value = ""

                });

                getList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractDropdownModel
                {
                    dropdown_text = "Select Status",
                    dropdown_value = ""
                });
                getList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();

            }

            return Json(new { response = getList });
        }

        [HttpPost]
        public IActionResult GetCompany(string companyname)
        {
            var output = new List<SubcontractProfileCompanyModel>();


            SubcontractProfileCompanyModel model = new SubcontractProfileCompanyModel();

            model.CompanyName = companyname;
            model.SubcontractProfileType = "All";


            var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "SearchCompanyVerify"));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(uriCompany, httpContentCompany).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(result);

            }


            return Json(new { response = output });
        }



        public async Task<IActionResult> GetDataById(string paymentId)
        {
            var PaymentResult = new SubcontractProfilePaymentModel();

            // Getting all company data  
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Payment/GetByPaymentId", paymentId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                PaymentResult = JsonConvert.DeserializeObject<SubcontractProfilePaymentModel>(result);

                List<FileUploadModal> L_File = new List<FileUploadModal>();


                if (PaymentResult.SlipAttachFile != null)
                {
                    Guid file_id = Guid.NewGuid();
                    L_File.Add(new FileUploadModal
                    {
                        file_id = file_id,
                        //Fileupload = fileBytes,
                        typefile = "SlipAttachFile",
                        //ContentDisposition = source.ContentDisposition,
                        //ContentType = source.ContentType,
                        Filename = PaymentResult.SlipAttachFile

                    });
                    PaymentResult.file_id_Slip = file_id;
                }
                //if (L_File.Count != 0)
                //{
                //    GetFile(paymentId, ref L_File);
                //    SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftPaymentSSO", L_File);
                //}


            }

            return Json(PaymentResult);
        }

        public IActionResult SaveVerify(string paymentId,string status,string verifydate,string remark_for_sub)
        {
            bool result = true;
            string mess = "";
            try
            {
                SubcontractProfilePaymentModel model = new SubcontractProfilePaymentModel();
                model.PaymentId = paymentId;
                model.Status = status;
                DateTime dateverify = DateTime.ParseExact(verifydate, "dd/MM/yyyy", null);
                model.verifiedDate = dateverify;
                model.remarkForSub = remark_for_sub;
                model.ModifiedBy = "SYSTEM";//Get Session from SSO********
                model.ModifiedDate = DateTime.Now;

                var uriPayment = new Uri(Path.Combine(strpathAPI, "Payment", "UpdateByVerified"));
                HttpClient clientCompany = new HttpClient();
                clientCompany.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string jj = JsonConvert.SerializeObject(model);

                var httpContentPayment = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = clientCompany.PutAsync(uriPayment, httpContentPayment).Result;
                if(response.IsSuccessStatusCode)
                {
                  var res=  response.Content.ReadAsStringAsync().Result;
                    result= JsonConvert.DeserializeObject<bool>(res);
                }
                if(result)
                {
                    mess = "Update Success";
                }
                else
                {
                    mess = "Update not Success";
                }
            }
            catch (Exception e)
            {
                result = false;
                mess = e.Message;
                throw;
            }
            return Json(new { Status = result, Message = mess });
        }


        //private async Task< bool> GetBlobDownload(string paymentid)
        //{
        //    try
        //    {
        //        if (dataUser == null)
        //        {
        //            getsession();
        //        }
        //        var dataUploadfile = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftPaymentSSO");
        //        string filename = "";
        //        foreach (var e in dataUploadfile)
        //        {
        //            // await CopyFile(e, model.CompanyId.ToString());

        //            filename = ContentDispositionHeaderValue.Parse(e.ContentDisposition).FileName.Trim('"');
        //            filename = EnsureCorrectFilename(filename);
        //        }
              

        //        var path = this.GetPathAndFilename(paymentid, filename,, dataUser.companyid.ToString());

        //        var memory = new MemoryStream();
        //        using (var stream = new FileStream(path, FileMode.Open))
        //        {
        //            await stream.CopyToAsync(memory);
        //            //using (var img = Image.FromStream(stream))
        //            //{
        //            //    // TODO: ResizeImage(img, 100, 100);
        //            //}

        //        }
                
        //        memory.Position = 0;

        //        TempData["Output"] = memory.ToArray();
        //        //return File(path, content);
        //        //return File(memory, GetContentType(path), Path.GetFileName(path));
        //        return true;

        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //        throw;
        //    }
            

        //}
        //public async Task<ActionResult> Downloadfile(string paymentid)
        //{
        //    // retrieve byte array here
        //    var array = TempData["Output"] as byte[];
        //    if (array == null)
        //    {
        //       await GetBlobDownload(paymentid);
        //      array = TempData["Output"] as byte[];
        //    }

        //    var dataUploadfile = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftPaymentSSO");
        //    string filename = "";
        //    foreach (var e in dataUploadfile)
        //    {
        //        // await CopyFile(e, model.CompanyId.ToString());

        //        filename = ContentDispositionHeaderValue.Parse(e.ContentDisposition).FileName.Trim('"');
        //        filename = EnsureCorrectFilename(filename);
        //    }

        //    var path = this.GetPathAndFilename(paymentid, filename);
        //    string content = GetContentType(path);

        //    if (array != null)
        //    {
        //        return File(array, content, Path.GetFileName(path));
        //    }
        //    else
        //    {
        //        return new EmptyResult();
        //    }
        //}
        public async Task<ActionResult> DownloadfileConfirm(string paymentid,string filename)
        {
            if (dataUser == null)
            {
                getsession();
            }
            var path = this.GetPathAndFilename(paymentid, filename, dataUser.companyid.ToString());
            string content = GetContentType(path);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);

            }

            memory.Position = 0;
            var array = memory.ToArray();
            

            if (array != null)
            {
                return File(array, content, Path.GetFileName(path));
            }
            else
            {
                return new EmptyResult();
            }
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}
            };
        }

        #region UploadFile

        [HttpPost]
        public IActionResult CheckFileUpload(List<IFormFile> files)
        {
            bool statusupload = true;
            string strmess = "";
            Guid? fid=null;
            try
            {
                foreach (FormFile source in files)
                {
                    if (source.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        if (
                                source.ContentType.ToLower() != "image/jpg" &&
                                source.ContentType.ToLower() != "image/jpeg" &&
                                source.ContentType.ToLower() != "image/pjpeg" &&
                                source.ContentType.ToLower() != "image/gif" &&
                                source.ContentType.ToLower() != "image/png" &&
                                source.ContentType.ToLower() != "application/pdf"
                                )
                        {
                            statusupload = false;
                            strmess = "Upload type file miss match.";
                        }
                        else
                        {
                            var fileSize = source.Length;
                            if (fileSize > MegaBytes)
                            {
                                statusupload = false;
                                strmess = "Upload file is too large.";
                            }
                            else
                            {
                                fid = Guid.NewGuid();
                                strmess = "Upload file success.";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
            }
            return Json(new { status = statusupload, message = strmess,file_id= fid });
        }
        private async Task<bool> Uploadfile(IFormFile files,string paymentid,string companyid)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            FileStream output;
            string strmess = "";
            try
            {
               
                if (files !=null && files.Length > 0)
                {
                    if (files != null)
                    {
                        string strdir = Path.Combine(strpathUpload, companyid, "Payment",paymentid);
                        if (!Directory.Exists(strdir))
                        {
                            Directory.CreateDirectory(strdir);
                        }
                        else
                        {
                            System.IO.DirectoryInfo di = new DirectoryInfo(strdir);
                            foreach (FileInfo finfo in di.GetFiles())
                            {
                                finfo.Delete();
                            }
                        }

                        
                    }
                    string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        using (output = System.IO.File.Create(this.GetPathAndFilename(paymentid,filename, companyid)))
                            await files.CopyToAsync(output);
                }

            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
                throw;
            }


            return statusupload;

        }
        private bool GetFile(string companyid, ref List<FileUploadModal> L_File)
        {
            bool result = true;
            try
            {
                string pathdir = Path.Combine(strpathUpload, companyid);

                string[] filePaths = Directory.GetFiles(pathdir, "*.*", SearchOption.AllDirectories);



                foreach (string file in filePaths)
                {


                    using (var ms = new MemoryStream(System.IO.File.ReadAllBytes(file)))
                    {

                        foreach (var e in L_File)
                        {
                            string filename = Path.GetFileName(file);
                            filename = EnsureCorrectFilename(filename);
                            var fileBytes = ms.ToArray();


                            if (e.Filename == filename)
                            {
                                e.Fileupload = fileBytes;
                                e.ContentType = Path.GetExtension(Path.GetExtension(file));
                                e.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "files", FileName = filename }.ToString();
                            }
                        }

                    }

                }
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        private string GetPathAndFilename(string paymentid, string filename,string companyid)
        {
            string pathdir = Path.Combine(strpathUpload, companyid, "Payment", paymentid);
            string PathOutput = "";
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
            PathOutput = Path.Combine(pathdir, filename);
            return PathOutput;
        }
        #endregion

        #region Comment
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
        //        });

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
