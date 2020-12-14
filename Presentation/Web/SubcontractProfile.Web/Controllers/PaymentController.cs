using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
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

        private const int MegaBytes = 1024 * 1024;
        private const int TMegaBytes = 3 * 1024 * 1024;

        private readonly IStringLocalizer<PaymentController> _localizer;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private int countupload = 0;

        public PaymentController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor
            , IStringLocalizer<PaymentController> localizer
           , IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _webHostEnvironment = webHostEnvironment;

            client = new HttpClient();
            _configuration = configuration;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();
            getsession();
        }
        public IActionResult ConfirmPayment()
        {
            ViewData["Title"] = _localizer["Payment"];
            getsession();

            ViewData["Controller"] = _localizer["Payment"];
            ViewData["View"] = _localizer["ConfirmPayment"];

            countupload = GetConfigUpload();
            ViewBag.ConfigUpload = countupload.ToString();


            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                ViewBag.Language = "th";
            }
            else
            {
                ViewBag.Language = "en";
            }
                

            return View();
        }
        public IActionResult VerifyPayment()
        {


            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
            if (userProfile == null)
            {
                return RedirectToAction("LogonByUser", "LogonByUser");
            }

            ViewData["Controller"] = _localizer["Payment"];
            ViewData["View"] = _localizer["VerfifiedPayment"];

            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.Name == "th")
            {
                ViewBag.Language = "th";
            }
            else
            {
                ViewBag.Language = "en";
            }

            countupload = GetConfigUpload();
            ViewBag.ConfigUpload = countupload.ToString();

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
                    DateTime datefrom = DateTime.ParseExact(searchPayment.Paymentdatefrom, "dd/MM/yyyy", new CultureInfo("en-US"));
                    model.PaymentDatetimeFrom = datefrom;
                }
                if (searchPayment.Paymentdateto != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchPayment.Paymentdateto, "dd/MM/yyyy", new CultureInfo("en-US"));
                    model.PaymentDatetimeTo = dateto;
                }

                if (searchPayment.Paymantrequestdatefrom != null)
                {
                    DateTime datefrom = DateTime.ParseExact(searchPayment.Paymantrequestdatefrom, "dd/MM/yyyy", new CultureInfo("en-US"));
                    model.RequestDateFrom = datefrom;
                }
                if (searchPayment.Paymentrequestdateto != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchPayment.Paymentrequestdateto, "dd/MM/yyyy", new CultureInfo("en-US"));
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
            jsonFileResult JsonResult = new jsonFileResult();

       

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string uriString = string.Format("{0}/{1}", strpathAPI + "Payment/GetByPaymentId"
                , HttpUtility.UrlEncode(id, Encoding.UTF8));
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataresponse = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<SubcontractProfilePaymentModel>(dataresponse);
                List<FileUploadModal> L_File = new List<FileUploadModal>();

                List<SubcontractProfileFileModel> ListFileSlip = new List<SubcontractProfileFileModel>();
                ListFileSlip = GetListFilePayment(id);

                if (ListFileSlip.Count() > 0)
                {
                    #region Get File from NAS To Temp ******
                    var resultCopyfile = GetFileFromNASToTemp(data.CompanyId.ToString(), id);
            
                    #endregion

                    if (resultCopyfile)
                    {
                        JsonResult.append = true;
                        JsonResult.initialPreview = new List<string>();
                        JsonResult.initialPreviewConfig = new List<initialPreviewConfig>();
                        string urlDelete = Url.Action("DeletefileSession", "Payment");

                        int count= GetConfigUpload();

                        JsonResult.maxFileCount = count - ListFileSlip.Count();


                        foreach (var v in ListFileSlip)
                        {
                            var virtualPath = Path.Combine(Url.Action("DownloadfileTemp", "Payment", new { filename = v.file_Name, paymentid = id }));

                            JsonResult.initialPreview.Add(virtualPath);
                            string content = Path.GetExtension(v.file_Name);

                            if (content == ".pdf")
                            {
                                content = "pdf";
                            }
                            else
                            {
                                content = "image";
                            }

                            JsonResult.initialPreviewConfig.Add(new initialPreviewConfig
                            {
                                caption = v.file_Name,
                                url = urlDelete,
                                key = v.file_Name,
                                fileId = v.file_Name,
                                extra = new
                                {
                                    fid = v.file_Name,
                                    paymentid = id
                                },
                                downloadUrl = Path.Combine(virtualPath),
                                type = content
                            });
                        }
                    }
                    
                }
            }
            return Json(new { Data = data,FileInput= JsonResult });

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

        [HttpPost]
        public async Task<IActionResult> Updatepayment(SubcontractProfilePaymentSendDataModel Payment)
        {
            string dataresponse = "";
            bool status = false;
            string message = "";
            bool resultGetFile = true;
            List<SubcontractProfileFileModel> ListFile = new List<SubcontractProfileFileModel>();
            try
            {
                if (dataUser == null)
                {
                    getsession();
                }
                Payment.ModifiedBy = dataUser.Username;
                Payment.verifiedDate = DateTime.Now;

                if (Payment.datetimepayment != null)
                {
                    DateTime datefrom = DateTime.ParseExact(Payment.datetimepayment, "dd/MM/yyyy HH:mm", new CultureInfo("en-US"));
                    Payment.PaymentDatetime = datefrom;
                }

                #region GetFileTemp

                string contentRootPath = _webHostEnvironment.ContentRootPath;
                var path = Path.Combine(contentRootPath, "upload", "temp", Payment.PaymentId);
                string[] allfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

                List<FileUploadModal> L_files = new List<FileUploadModal>();

                foreach (var file in allfiles)
                {
                    FileInfo info = new FileInfo(file);
                    if (info != null)
                    {
                        #region Copy File to server

                        //string[] arrayfileid = info.Name.Split('_');
                        var fid = Guid.NewGuid();

                        ListFile.Add(new SubcontractProfileFileModel
                        {
                            file_id = fid,
                            company_id = new Guid(Payment.CompanyId.ToString()),
                            CreateBy = Payment.ModifiedBy,
                            CreateDate = DateTime.Now,
                            file_Name = info.Name,
                            payment_id = new Guid(Payment.PaymentId),
                            upload_type = "Payment"

                        });

                        L_files.Add(new FileUploadModal
                        {
                            file_id = fid,
                            Filename = info.Name

                        });

                        #endregion
                    }
                }

                GetFile(path, ref L_files);
                if (L_files!=null && L_files.Count() > 0)
                {
                    foreach (var v in L_files)
                    {
                        var stream = new MemoryStream(v.Fileupload);
                        var Ffile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(v.Filename))
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = v.ContentType,
                            ContentDisposition = v.ContentDisposition
                        };
                        resultGetFile = await Uploadfile(Ffile, Payment.PaymentId, dataUser.companyid.ToString());
                    }

                }
                else
                {
                    resultGetFile = false;
                }
                if (resultGetFile)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(path);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    di.Delete(true);
                }

                #endregion


                if (resultGetFile)
                {

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string uriString = string.Format("{0}", strpathAPI + "Payment/Update");

                    string ss = JsonConvert.SerializeObject(Payment);

                    var httpContent = new StringContent(JsonConvert.SerializeObject(Payment), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(uriString, httpContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dataresponse = response.Content.ReadAsStringAsync().Result;
                        status = JsonConvert.DeserializeObject<bool>(dataresponse);
                        message = _localizer["MessageConfirmPaymentSuccess"];

                        #region Delete File In subcontract_profile_file

                        var uriFileDelete = new Uri(Path.Combine(strpathAPI, "File", "DeleteByPaymentId"));
                        httpContent = new StringContent(JsonConvert.SerializeObject(Payment.PaymentId), Encoding.UTF8, "application/json");
                        var responseFile = await client.PostAsync(uriFileDelete, httpContent);
                        if (responseFile.IsSuccessStatusCode)
                        {
                            #region Insert File In subcontract_profile_file

                            foreach(var e in ListFile)
                            {
                                var uriFileInsert = new Uri(Path.Combine(strpathAPI, "File", "Insert"));
                                httpContent = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                                var responseFileInsert = await client.PostAsync(uriFileInsert, httpContent);
                                if (responseFileInsert.IsSuccessStatusCode)
                                {
                                    dataresponse = response.Content.ReadAsStringAsync().Result;
                                    status = JsonConvert.DeserializeObject<bool>(dataresponse);
                                    message = _localizer["MessageConfirmPaymentSuccess"];
                                }
                            }

                           

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        message = _localizer["MessageConfirmPaymentUnSuccess"];
                    }
                }
                else
                {

                    message = _localizer["MessageConfirmPaymentUnSuccess"];
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { Data = dataresponse, Status = status, Message = message }); ;
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
                    DateTime datefrom = DateTime.ParseExact(searchmodel.PaymentDatetimeFrom, "dd/MM/yyyy", new CultureInfo("en-US"));
                    model.PaymentDatetimeFrom = datefrom;
                }
                if (searchmodel.PaymentDatetimeTo != null)
                {
                    DateTime dateto = DateTime.ParseExact(searchmodel.PaymentDatetimeTo, "dd/MM/yyyy", new CultureInfo("en-US"));
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

                    // paymentResult = paymentResult.Where(x => x.Status != "Waiting").ToList();

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

            output.Add(new SubcontractDropdownModel
            {
                dropdown_text = _localizer["ddlSelectPaymentMethod"],
                dropdown_value = ""

            });
            if (output.Count > 1)
            {
                CultureInfo culture = CultureInfo.CurrentCulture;
                if (culture.Name == "th")
                {


                    getAllPaymenttypeList = output.Select(a => new SelectListItem
                    {
                        Text = a.dropdown_text,
                        Value = a.dropdown_value
                    }).OrderBy(c => c.Value).ToList();
                }
                else
                {
                    getAllPaymenttypeList = output.Select(a => new SelectListItem
                    {
                        Text = a.dropdown_text,
                        Value = a.dropdown_value
                    }).OrderBy(c => c.Value).ToList();

                }
            }
            else
            {
                getAllPaymenttypeList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).ToList();
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

            output.Add(new SubcontractProfileBankingModel
            {
                BankCode = "0",
                BankName = _localizer["ddlSelectBank"]
            });
            if (output.Count > 1)
            {
                CultureInfo culture = CultureInfo.CurrentCulture;
                if (culture.Name == "th")
                {


                    getAllBankList = output.Select(a => new SelectListItem
                    {
                        Text = a.BankName,
                        Value = a.BankCode
                    }).OrderBy(c => c.Value).ToList();
                }
                else
                {
                    getAllBankList = output.Select(a => new SelectListItem
                    {
                        Text = a.BankName,
                        Value = a.BankCode
                    }).OrderBy(c => c.Value).ToList();

                }
            }
            else
            {
                getAllBankList = output.Select(a => new SelectListItem
                {
                    Text = a.BankName,
                    Value = a.BankCode
                }).ToList();
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
            output.Add(new SubcontractDropdownModel
            {
                dropdown_text = _localizer["ddlSelectStatus"],
                dropdown_value = ""

            });
            if (output.Count > 1)
            {
                CultureInfo culture = CultureInfo.CurrentCulture;
                if (culture.Name == "th")
                {


                    getList = output.Select(a => new SelectListItem
                    {
                        Text = a.dropdown_text,
                        Value = a.dropdown_value
                    }).OrderBy(c => c.Value).ToList();
                }
                else
                {
                    getList = output.Select(a => new SelectListItem
                    {
                        Text = a.dropdown_text,
                        Value = a.dropdown_value
                    }).OrderBy(c => c.Value).ToList();

                }
            }
            else
            {
                getList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).ToList();
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
            jsonFileResult JsonResult = new jsonFileResult();

            // Getting all company data  
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Payment/GetByPaymentId", HttpUtility.UrlEncode(paymentId, Encoding.UTF8));

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                PaymentResult = JsonConvert.DeserializeObject<SubcontractProfilePaymentModel>(result);


                List<SubcontractProfileFileModel> ListFileSlip = new List<SubcontractProfileFileModel>();
                ListFileSlip = GetListFilePayment(paymentId);

                if (ListFileSlip.Count() > 0)
                {
                    #region Get File from NAS To Temp ******
                    var resultCopyfile = GetFileFromNASToTemp(PaymentResult.CompanyId.ToString(), paymentId);
                    #endregion

                    if (resultCopyfile)
                    {
                        JsonResult.append = true;
                        JsonResult.initialPreview = new List<string>();
                        JsonResult.initialPreviewConfig = new List<initialPreviewConfig>();
                        string urlDelete = Url.Action("DeletefileSession", "Payment");
                        foreach (var v in ListFileSlip)
                        {
                            JsonResult.initialPreview.Add(Path.Combine(Url.Action("DownloadfileTemp", "Payment", new { filename = v.file_Name, paymentid = paymentId })));
                            string content = Path.GetExtension(v.file_Name).Replace(".", "");

                            if (content == ".pdf")
                            {
                                content = "pdf";
                            }
                            else
                            {
                                content = "image";
                            }

                            JsonResult.initialPreviewConfig.Add(new initialPreviewConfig
                            {
                                caption = v.file_Name,
                                url = urlDelete,
                                key = v.file_Name,
                                fileId = v.file_Name,
                                extra = new
                                {
                                    fid = v.file_Name,
                                    paymentid = paymentId
                                },
                                downloadUrl = Path.Combine(Url.Action("DownloadfileTemp", "Payment", new { filename = v.file_Name, paymentid = paymentId })),
                                type = content
                            });
                        }
                    }

                      


                }


            }

            return Json(new { Payment = PaymentResult, FileInput = JsonResult });
        }

        public IActionResult SaveVerify(string paymentId, string status, string verifydate, string remark_for_sub)
        {
            bool result = true;
            string mess = "";
            try
            {
                var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");

                SubcontractProfilePaymentModel model = new SubcontractProfilePaymentModel();
                model.PaymentId = paymentId;
                model.Status = status;
                DateTime dateverify = DateTime.ParseExact(verifydate, "dd/MM/yyyy", new CultureInfo("en-US"));
                model.verifiedDate = dateverify;
                model.remarkForSub = remark_for_sub;
                model.ModifiedBy = userProfile.Username;//Get Session from SSO********

                var uriPayment = new Uri(Path.Combine(strpathAPI, "Payment", "UpdateByVerified"));
                HttpClient clientCompany = new HttpClient();
                clientCompany.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string jj = JsonConvert.SerializeObject(model);

                var httpContentPayment = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = clientCompany.PutAsync(uriPayment, httpContentPayment).Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<bool>(res);
                }
                if (result)
                {
                    mess = _localizer["MessageUpdateVerifySuccess"];
                }
                else
                {
                    mess = _localizer["MessageUpdateVerifyUnSuccess"];
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

        public async Task<ActionResult> DownloadfileConfirm(string paymentid, string filename, string companyId)
        {
            if (dataUser == null)
            {
                getsession();
            }
            var outputNAS = new List<SubcontractDropdownModel>();
            var outputUser = new List<SubcontractProfileUserModel>();
            #region NAS
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/nas_subcontract");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                outputNAS = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
            }

            string username = outputNAS[0].value1;
            string password = outputNAS[0].value2;
            string ipAddress = @"\\" + outputNAS[0].dropdown_value;
            string destNAS = outputNAS[0].dropdown_text;

            //string username = "PF0QMBH6";
            //string password = "1234";
            //string ipAddress = @"DESKTOP-MMCKBRE";
            //string destNAS = @"D:\NasPath";

            NetworkCredential sourceCredentials = new NetworkCredential { Domain = ipAddress, UserName = username, Password = password };

            #region Select User

            SubcontractProfileUserModel usersearch = new SubcontractProfileUserModel();
            usersearch.companyid = new Guid(companyId);
            var uriStringUser = new Uri(Path.Combine(strpathAPI, "User", "GetByCompanyId"));
            var httpContentUser = new StringContent(JsonConvert.SerializeObject(usersearch), Encoding.UTF8, "application/json");
            response = client.PostAsync(uriStringUser, httpContentUser).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                outputUser = JsonConvert.DeserializeObject<List<SubcontractProfileUserModel>>(result);
            }

            #endregion


            #endregion
            using (new NetworkConnection(destNAS, sourceCredentials))
            {
                var chkpath = this.GetPathAndFilename(paymentid, filename, outputUser[0].UserId.ToString(), destNAS + @"\SubContractProfile\"); //userid
                if (System.IO.File.Exists(chkpath))
                {
                    CheckDirPayment(dataUser.UserId.ToString(), companyId, destNAS + @"\SubContractProfile\");
                }
                var path = this.GetPathAndFilename(paymentid, filename, companyId, destNAS + @"\SubContractProfile\");
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


        }
        private void CheckDirPayment(string userid, string companyid, string dir)
        {
            //string strdir = Path.Combine(@"D:\PathTest\56f28ba9-8f7f-43fa-a598-e88fae450180");
            //var path0 = @"D:\PathTest\ab6362c7-388e-4289-b999-dded33408ea0";
            //Directory.Move(path0, strdir);
            //string strdir = Path.Combine(dir, companyid);

            var path0 = Path.Combine(dir, userid, "Payment");
            string strdir = Path.Combine(dir, companyid, "Payment");

            if (Directory.Exists(Path.Combine(dir, userid)))
            {
                DirectoryInfo dirI = new DirectoryInfo(path0);
                DirectoryInfo[] dirs = dirI.GetDirectories();

                string[] files = Directory.GetFiles(path0);
                Int32 i = dirs.Count() + files.Count();

                if (!Directory.Exists(strdir))
                {
                    Directory.CreateDirectory(strdir);
                }

                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(strdir, subdir.Name);
                    if (!Directory.Exists(temppath))
                        try
                        {
                            Directory.Move(subdir.FullName, temppath);
                        }
                        catch
                        {

                        }
                }
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

        public class jsonFileResult
        {
            public string error { get; set; }
            public List<string> initialPreview { get; set; }
            public List<initialPreviewConfig> initialPreviewConfig { get; set; }
            public bool initialPreviewAsData { get; set; } = true;
            public bool append { get; set; } = true;
            public int maxFileCount { get; set; }



        }
        public class initialPreviewConfig
        {
            public string key { get; set; }
            public string fileId { get; set; }
            public string caption { get; set; }
            public string url { get; set; }
            public object extra { get; set; }
            public string downloadUrl { get; set; }
            public string type { get; set; }
            //public string width { get; set; }
            public string zoomData { get; set; }
        }
        public class ResultChkfile
        {
            public bool status { get; set; }
            public string mess { get;set; }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadMulti(IFormFile files, string companyid,string paymentid)
        {
            string comid = "";
            Guid? fid = null;
            jsonFileResult JsonResult = new jsonFileResult();
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            if (companyid == "" || companyid == null)
            {
                comid = dataUser.companyid.ToString();
            }
            else
            {
                comid = companyid;
            }



                ResultChkfile chk = ValidateFileUpload(files, companyid);
            if (chk.status)
            {

                int count = GetConfigUpload();
                int fCount = 0;
                if (Directory.Exists(Path.Combine(contentRootPath, "upload", "temp", paymentid)))
                {
                    fCount = Directory.GetFiles(Path.Combine(contentRootPath, "upload", "temp", paymentid), "*").Length;
                }

                fCount = fCount + 1;

                string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                filename = EnsureCorrectFilename(filename);

                var path = Path.Combine(contentRootPath, "upload", "temp", filename);
                string content = Path.GetExtension(filename);

                var virtualPath = Path.Combine(Url.Action("DownloadfileTemp", "Payment", new { filename = filename, paymentid = paymentid }));

                string urlDelete = Url.Action("DeletefileSession", "Payment");

                if (fCount <= count)
                {
                    fid = Guid.NewGuid();
                    await UploadfileTemp(files, fid.ToString(), paymentid);

                    if (content == ".pdf")
                    {
                        content = "pdf";
                    }
                    else
                    {
                        content = "image";
                    }

                    JsonResult.append = true;

                    JsonResult.initialPreview = new List<string>();
                    JsonResult.initialPreviewAsData = true;

                    JsonResult.initialPreview.Add(virtualPath);

                    JsonResult.initialPreviewConfig = new List<initialPreviewConfig>();
                    JsonResult.initialPreviewConfig.Add(new initialPreviewConfig
                    {
                        caption = filename,
                        url = urlDelete,
                        key = filename,
                        fileId = filename,
                        //width= "120px",
                        extra = new
                        {
                            fid = filename,
                            paymentid = paymentid
                        },
                        downloadUrl = virtualPath,
                        zoomData = virtualPath,
                        type = content
                    });
                }
                else
                {
                   

                    JsonResult.append = false;
                    CultureInfo culture = CultureInfo.CurrentCulture;
                    if (culture.Name == "th")
                    {
                        JsonResult.error = "ไฟล์ที่คุณเลือกมีจำนวน (" + fCount + ") ซึ่งเกินกว่าที่ระบบอนุญาตที่ " + count + ", กรุณาลองใหม่อีกครั้ง!";
                    }
                    else
                    {
                        JsonResult.error = "Number of files selected for upload (" + fCount + ") exceeds maximum allowed limit of " + count + ".";
                    }


                    JsonResult.initialPreview = new List<string>();
                    JsonResult.initialPreviewConfig = new List<initialPreviewConfig>();
                }
            }
            else
            {
                JsonResult.append = false;
                JsonResult.error = chk.mess;
                JsonResult.initialPreview = new List<string>();
                JsonResult.initialPreviewConfig = new List<initialPreviewConfig>();

            }


           

            

            return Json(JsonResult);
        }
        private ResultChkfile ValidateFileUpload(IFormFile files, string companyid)
        {
            bool statusupload = true;
            string strmess = "";
            Guid? fid = null;
            ResultChkfile rr = new ResultChkfile();
            try
            {
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        if (
                                files.ContentType.ToLower() != "image/jpg" &&
                            files.ContentType.ToLower() != "image/jpeg" &&
                            files.ContentType.ToLower() != "image/pjpeg" &&
                            files.ContentType.ToLower() != "image/gif" &&
                            files.ContentType.ToLower() != "image/png" &&
                            files.ContentType.ToLower() != "image/bmp" &&
                            files.ContentType.ToLower() != "image/tiff" &&
                            files.ContentType.ToLower() != "image/tif" &&
                            files.ContentType.ToLower() != "application/pdf"
                            )
                        {
                            statusupload = false;
                            strmess = _localizer["MessageUploadmissmatch"];

            
                        }
                        else
                        {
                            if (files.ContentType.ToLower() == "application/pdf")
                            {
                                var fileSize = files.Length;
                                if (fileSize > MegaBytes)
                                {
                                    statusupload = false;
                                    strmess = _localizer["MessageUploadtoolage"];

                                }
                                else
                                {
                                    strmess = _localizer["MessageUploadSuccess"];
  
                                }
                            }
                            else
                            {
                                var fileSize = files.Length;
                                if (fileSize > TMegaBytes)
                                {
                                    statusupload = false;
                                    strmess = _localizer["MessageUploadtoolage"];

                                }
                                else
                                {
                                    strmess = _localizer["MessageUploadSuccess"];
                                }
                            }

                        }
                    }

                }
                else
                {
                    statusupload = false;
                    strmess = "File not found";
                }
            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
            }
            rr.status = statusupload;
            rr.mess = strmess;

            return rr;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult CheckFileUpload(IFormFile files,string companyid)
        {
            bool statusupload = true;
            string strmess = "";
            Guid? fid=null;
            jsonFileResult JsonResult = new jsonFileResult();
            try
            {
                if (dataUser == null)
                {
                    getsession();
                }
                if (files !=null)
                {
                    if (files.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        if (
                                files.ContentType.ToLower() != "image/jpg" &&
                            files.ContentType.ToLower() != "image/jpeg" &&
                            files.ContentType.ToLower() != "image/pjpeg" &&
                            files.ContentType.ToLower() != "image/gif" &&
                            files.ContentType.ToLower() != "image/png" &&
                            files.ContentType.ToLower() != "image/bmp" &&
                            files.ContentType.ToLower() != "image/tiff" &&
                            files.ContentType.ToLower() != "image/tif" &&
                            files.ContentType.ToLower() != "application/pdf"
                            )
                        {
                            statusupload = false;
                            strmess = _localizer["MessageUploadmissmatch"];
                        }
                        else
                        {
                            if (files.ContentType.ToLower() == "application/pdf")
                            {
                                var fileSize = files.Length;
                                if (fileSize > MegaBytes)
                                {
                                    statusupload = false;
                                    strmess = _localizer["MessageUploadtoolage"];
                                }
                                else
                                {
                                    fid = Guid.NewGuid();
                                    strmess = _localizer["MessageUploadSuccess"];

                                }
                            }
                            else
                            {
                                var fileSize = files.Length;
                                if (fileSize > TMegaBytes)
                                {
                                    statusupload = false;
                                    strmess = _localizer["MessageUploadtoolage"];
                                }
                                else
                                {
                                    fid = Guid.NewGuid();
                                    strmess = _localizer["MessageUploadSuccess"];
 
                                }
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

            return Json(new { status= statusupload, message= strmess });
        }
        private async Task<bool> Uploadfile(IFormFile files,string paymentid,string companyid)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            FileStream output;
            string strmess = "";
            var outputNAS = new List<SubcontractDropdownModel>();
            try
            {
               
                if (files !=null && files.Length > 0)
                {
                    #region NAS
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/nas_subcontract");
                    HttpResponseMessage response = client.GetAsync(uriString).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var v = response.Content.ReadAsStringAsync().Result;
                        outputNAS = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                    }

                    string username = outputNAS[0].value1;
                    string password = outputNAS[0].value2;
                    string ipAddress = @"\\" + outputNAS[0].dropdown_value;
                    string destNAS = outputNAS[0].dropdown_text;

                    NetworkCredential sourceCredentials = new NetworkCredential { Domain = ipAddress, UserName = username, Password = password };

                    #endregion
                    using (new NetworkConnection(destNAS, sourceCredentials))
                    {
                        if (files != null)
                        {
                            string strdir = Path.Combine(destNAS + @"\SubContractProfile\", companyid, "Payment", paymentid);
                            if (!Directory.Exists(strdir))
                            {
                                Directory.CreateDirectory(strdir);
                            }
                            else
                            {
                                //System.IO.DirectoryInfo di = new DirectoryInfo(strdir);
                                //foreach (FileInfo finfo in di.GetFiles())
                                //{
                                //    finfo.Delete();
                                //}
                            }


                        }
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        using (output = System.IO.File.Create(this.GetPathAndFilename(paymentid, filename, companyid, destNAS + @"\SubContractProfile\")))
                            await files.CopyToAsync(output);
                    }
                    
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

        private async Task<bool> UploadfileTemp(IFormFile files,string fid,string paymentid)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            FileStream output;
            string strmess = "";
            try
            {

                    if (files.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                    using (output = System.IO.File.Create(this.GetPathTempAndFilename(filename, fid, paymentid)))
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

            // return Json(new { status = statusupload, message = strmess });
        }

        [HttpPost]
        public bool DeletefileSession(string fid,string paymentid)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentRootPath, "upload", "temp", paymentid, fid);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
                return true;
        }

        private string GetPathTempAndFilename(string filename,string fid,string paymentid)
        {
            string PathOutput = "";
            

            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            

            string pathdir = Path.Combine(contentRootPath, "upload","temp", paymentid);
            try
            {
                if (!Directory.Exists(pathdir))
                {
                    Directory.CreateDirectory(pathdir);
                }

                PathOutput = Path.Combine(pathdir,filename);
                return PathOutput;
            }
            catch (Exception e)
            {
                PathOutput = "";
                return PathOutput;
            }

        }

        [HttpGet]
        public async Task<ActionResult> DownloadfileTemp(string filename,string paymentid)
        {

            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentRootPath, "upload", "temp", paymentid, filename);
            try
            {
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
                    return File(array, content);
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception e)
            {

                throw;
                return new EmptyResult();
            }
           
        }

        private int GetConfigUpload()
        {

            var output = new List<SubcontractDropdownModel>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/subcontract_config_upload");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
            }
            return Convert.ToInt32(output[0].value1);
        }

        private List<SubcontractProfileFileModel> GetListFilePayment(string paymentid)
        {
            var output = new List<SubcontractProfileFileModel>();
            try
            {

                var model = new SubcontractProfilePaymentModel();

                model.PaymentId = paymentid;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var uriString = new Uri(Path.Combine(strpathAPI, "File", "GetByPaymentId"));

                string rr = JsonConvert.SerializeObject(model);

                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uriString, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileFileModel>>(result);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return output;
        }

        private bool GetFileFromNASToTemp(string companyid,string paymentid)
        {
            var outputNAS = new List<SubcontractDropdownModel>();
   
            try
            {
                #region NAS
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/nas_subcontract");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    outputNAS = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                }

                string username = outputNAS[0].value1;
                string password = outputNAS[0].value2;
                string ipAddress = @"\\" + outputNAS[0].dropdown_value;
                string destNAS = outputNAS[0].dropdown_text;

                NetworkCredential sourceCredentials = new NetworkCredential { Domain = ipAddress, UserName = username, Password = password };
                using (new NetworkConnection(destNAS, sourceCredentials))
                {

                    string strdirNAS = Path.Combine(destNAS + @"\SubContractProfile\", companyid, "Payment", paymentid);
                    DirectoryInfo dirNAS = new DirectoryInfo(strdirNAS);

                    string contentRootPath = _webHostEnvironment.ContentRootPath;
                    string pathdirTemp = Path.Combine(contentRootPath, "upload", "temp", paymentid);
        
                    DirectoryInfo dirTemp = new DirectoryInfo(pathdirTemp);

                    if (!Directory.Exists(pathdirTemp))
                    {
                        Directory.CreateDirectory(pathdirTemp);
                    }

                    if(Directory.Exists(strdirNAS))
                    {
                        foreach (FileInfo fi in dirNAS.GetFiles())
                        {
                            fi.CopyTo(Path.Combine(dirTemp.FullName, fi.Name), true);
                        }
                    }
               }
                #endregion

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

        private bool GetFile(string pathdir, ref List<FileUploadModal> L_File)
        {
            bool result = true;
            try
            {
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
        private string GetPathAndFilename(string paymentid, string filename,string companyid,string dir)
        {
            string pathdir = Path.Combine(dir, companyid, "Payment", paymentid);
            string PathOutput = "";
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
            PathOutput = Path.Combine(pathdir, filename);
            return PathOutput;
        }

        #endregion

        


        #endregion

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
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
