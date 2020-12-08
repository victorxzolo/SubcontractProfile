using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

namespace SubcontractProfile.Web.Controllers
{
    public class MigrationProfileController : Controller
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
        private readonly IStringLocalizer<MigrationProfileController> _localizer;
 
        public MigrationProfileController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor
         , IStringLocalizer<MigrationProfileController> localizer)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;

            client = new HttpClient();
            _configuration = configuration;
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();

        }
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
        public IActionResult Index()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userAISLogin");
            if (userProfile == null)
            {
                return RedirectToAction("LogonByUser", "LogonByUser");
            }
            getsession();
            ViewData["Controller"] = _localizer["MigrationProfile"];
            ViewData["View"] = _localizer["index"];
            return View();
         
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult onCheckUpload(IFormFile files)
        {

            bool statusupload = true;
            string strmess = "";
            Guid? fid = null;
            try
            {

                if (files != null)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "filesData", files);
                    if (files.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);
                        if (
                                //accept="text/plain, .csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
                                files.ContentType.ToLower() != "text/plain" &&
                            files.ContentType.ToLower() != ".csv" &&
                            files.ContentType.ToLower() != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" &&
                            files.ContentType.ToLower() != "application/vnd.ms-excel")
                        {
                            statusupload = false;
                            strmess = _localizer["MessageUploadmissmatch"];
                        }
                        else
                        {
                           ReadData(files);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                statusupload = false;
                strmess = e.Message.ToString();
            }
            return Json(new { status = statusupload, message = strmess, file_id = fid });
        }
        public String ConvertToDateTimeYYYYMMDD(string strDateTime)
        {
            string sDateTime;
            string[] sDate = strDateTime.Split('/');
            sDateTime = sDate[2] + '-' + sDate[1] + '-' + sDate[0];

            return sDateTime;
        }

        public async Task<IActionResult> ReadData(IFormFile postedFile)
        {
            List<SubcontractProfileLocationModel> LocationList = new List<SubcontractProfileLocationModel>();


            //Get file
            var newfile = new FileInfo(postedFile.FileName);
            var fileExtension = newfile.Extension;
            string locid = string.Empty;
            string loccode = string.Empty;
            //Check if file is an Excel File

            using (MemoryStream ms = new MemoryStream())
            {
                await postedFile.CopyToAsync(ms);

                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Location"];
                    int totalRows = workSheet.Dimension.Rows;
                    Guid locationId; Guid CompanyId;
                    try
                    {

                        for (int i = 2; i <= totalRows; i++)
                        {
                            string LId = workSheet.Cells[i, 1].Value.ToString();
                            string CId = workSheet.Cells[i, 50].Value.ToString();
                            DateTime ddNew = DateTime.Now;
                            if ((LId == "NULL") || (LId == null))
                            {
                                locationId = Guid.NewGuid();
                                // return null;
                            }
                            else
                            {
                                //locationId = Guid.NewGuid();
                                locationId = new Guid(LId);
                            }

                            if ((CId == "NULL") || (CId == null))
                            {
                                CompanyId = Guid.Empty;
                                return null;
                            }
                            else
                            {
                                CompanyId = new Guid(CId); 
                                //CompanyId = Guid.NewGuid();
                            }
                            string _efDate = workSheet.Cells[i, 12].Value.ToString();
                            DateTime EFDATE = new DateTime();
                            if (_efDate == null || _efDate == "NULL")
                            {
                                EFDATE = ddNew;
                            }
                            else
                            {
                                EFDATE = Convert.ToDateTime(_efDate);
                            }

                            string _creDate = workSheet.Cells[i, 40].Value.ToString();
                            DateTime creDate = new DateTime();
                            if (_creDate == null || _creDate == "NULL")
                            {
                                creDate = ddNew;
                            }
                            else
                            {
                                creDate = ddNew;
                            }
                            string _upDate = workSheet.Cells[i, 43].Value.ToString();
                            DateTime upDate = new DateTime();
                            if (_upDate == null || _upDate == "NULL")
                            {
                                upDate = ddNew;
                            }
                            else
                            {
                                upDate = ddNew;
                            }



                            LocationList.Add(new SubcontractProfileLocationModel
                            {
                                LocationId = locationId,
                                LocationCode = workSheet.Cells[i,2].Value.ToString(),
                                LocationName = workSheet.Cells[i,3].Value.ToString(),
                                LocationNameTh = workSheet.Cells[i,4].Value.ToString(),
                                LocationNameEn = workSheet.Cells[i, 5].Value.ToString(),
                                LocationNameAlias = workSheet.Cells[i, 6].Value.ToString(),
                                VendorCode = workSheet.Cells[i, 7].Value.ToString(),
                                StorageLocation = workSheet.Cells[i, 8].Value.ToString(),
                                ShipTo = workSheet.Cells[i, 9].Value.ToString(),
                                OutOfServiceStorageLocation = workSheet.Cells[i, 10].Value.ToString(),
                                SubPhase = workSheet.Cells[i, 11].Value.ToString(),
                                EffectiveDate = EFDATE,
                                ShopType = workSheet.Cells[i, 13].Value.ToString(),
                                VatBranchNumber = workSheet.Cells[i, 14].Value.ToString(),
                                Phone = workSheet.Cells[i, 15].Value.ToString(),
                                CompanyMainContractPhone = workSheet.Cells[i, 16].Value.ToString(),
                                InstallationsContractPhone = workSheet.Cells[i, 17].Value.ToString(),
                                MaintenanceContractPhone = workSheet.Cells[i, 18].Value.ToString(),
                                InventoryContractPhone = workSheet.Cells[i, 19].Value.ToString(),
                                PaymentContractPhone = workSheet.Cells[i, 20].Value.ToString(),
                                EtcContractPhone = workSheet.Cells[i, 21].Value.ToString(),
                                CompanyGroupMail = workSheet.Cells[i, 22].Value.ToString(),
                                InstallationsContractMail = workSheet.Cells[i, 23].Value.ToString(),
                                MaintenanceContractMail = workSheet.Cells[i, 24].Value.ToString(),
                                InventoryContractMail = workSheet.Cells[i, 25].Value.ToString(),
                                PaymentContractMail = workSheet.Cells[i, 26].Value.ToString(),
                                EtcContractMail = workSheet.Cells[i, 27].Value.ToString(),
                                LocationAddress = workSheet.Cells[i, 28].Value.ToString(),
                                PostAddress = workSheet.Cells[i, 29].Value.ToString(),
                                TaxAddress = workSheet.Cells[i, 30].Value.ToString(),
                                WtAddress = workSheet.Cells[i, 31].Value.ToString(),
                                HouseNo = workSheet.Cells[i, 32].Value.ToString(),
                                AreaCode = workSheet.Cells[i, 33].Value.ToString(),
                                BankCode = workSheet.Cells[i, 34].Value.ToString(),
                                BankName = workSheet.Cells[i, 35].Value.ToString(),
                                BankAccountNo = workSheet.Cells[i, 36].Value.ToString(),
                                BankAccountName = workSheet.Cells[i, 37].Value.ToString(),
                                BankAttachFile = workSheet.Cells[i, 38].Value.ToString(),
                                Status = workSheet.Cells[i, 39].Value.ToString(),

                                CreateDate = creDate,
                                CreateBy = workSheet.Cells[i, 41].Value.ToString(),
                                UpdateBy = workSheet.Cells[i, 42].Value.ToString(),
                                UpdateDate = upDate,
                                BankBranchCode = workSheet.Cells[i, 44].Value.ToString(),
                                BankBranchName = workSheet.Cells[i, 45].Value.ToString(),
                                PenaltyContractPhone = workSheet.Cells[i, 46].Value.ToString(),
                                PenaltyContractMail = workSheet.Cells[i, 47].Value.ToString(),
                                ContractPhone = workSheet.Cells[i, 48].Value.ToString(),
                                ContractMail = workSheet.Cells[i, 49].Value.ToString(),
                                CompanyId = CompanyId
                            });
                        }
                        if (LocationList.Count > 0)
                        {
                            // AddDataTableLocation(LocationList);
                           SessionHelper.SetObjectAsJson(HttpContext.Session, "ExcelLocationData", LocationList);
                        }
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.Message.ToString();

                    }



                }
            }



            return null;

        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    
                            pro.SetValue(obj, dr[column.ColumnName], null);
                       
                        
                    else
                        continue;
                }
            }
            return obj;
        }
        public ActionResult AddDataTableLocation ()
        {
            var sessiommodel  = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelLocationData");
         

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

            // Skip number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();

            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();

            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            // Sort Column Direction (asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10, 20, 50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;
            // var model = 
            var Excelocation = new List<SubcontractProfileLocationModel>();
          // List<SubcontractProfileLocationModel> locationImportDetails = new List<SubcontractProfileLocationModel>();
          ///  locationImportDetails = ConvertDataTable<SubcontractProfileLocationModel>(sessiommodel);
            Excelocation = (from DataRow dr in sessiommodel.Rows
                           
                            select new SubcontractProfileLocationModel()
                            {
                                LocationId = Guid.Parse(dr["LocationId"].ToString()),
                                LocationCode = dr["LocationCode"].ToString(),
                                LocationName = dr["LocationName"].ToString(),
                                LocationNameTh = dr["LocationNameTh"].ToString(),
                                LocationNameEn = dr["LocationNameEn"].ToString(),
                                LocationNameAlias = dr["LocationNameAlias"].ToString(),
                                VendorCode = dr["VendorCode"].ToString(),
                                StorageLocation = dr["StorageLocation"].ToString(),
                                ShipTo = dr["ShipTo"].ToString(),
                                OutOfServiceStorageLocation = dr["OutOfServiceStorageLocation"].ToString(),
                                SubPhase = dr["SubPhase"].ToString(),
                                EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"].ToString()),
                                ShopType = dr["ShopType"].ToString(),
                                VatBranchNumber = dr["VatBranchNumber"].ToString(),
                                Phone = dr["Phone"].ToString(),
                                CompanyMainContractPhone = dr["CompanyMainContractPhone"].ToString(),
                                InstallationsContractPhone = dr["InstallationsContractPhone"].ToString(),
                                MaintenanceContractPhone = dr["MaintenanceContractPhone"].ToString(),
                                InventoryContractPhone = dr["InventoryContractPhone"].ToString(),
                                PaymentContractPhone = dr["PaymentContractPhone"].ToString(),
                                EtcContractPhone = dr["EtcContractPhone"].ToString(),
                                CompanyGroupMail = dr["CompanyGroupMail"].ToString(),
                                InstallationsContractMail = dr["InstallationsContractMail"].ToString(),
                                MaintenanceContractMail = dr["MaintenanceContractMail"].ToString(),
                                InventoryContractMail = dr["InventoryContractMail"].ToString(),
                                PaymentContractMail = dr["PaymentContractMail"].ToString(),
                                EtcContractMail = dr["EtcContractMail"].ToString(),
                                LocationAddress = dr["LocationAddress"].ToString(),
                                PostAddress = dr["PostAddress"].ToString(),
                                TaxAddress = dr["TaxAddress"].ToString(),
                                WtAddress = dr["WtAddress"].ToString(),
                                HouseNo = dr["HouseNo"].ToString(),
                                AreaCode = dr["AreaCode"].ToString(),
                                BankCode = dr["BankCode"].ToString(),
                                BankName = dr["BankName"].ToString(),
                                BankAccountNo = dr["BankAccountNo"].ToString(),
                                BankAccountName = dr["BankAccountName"].ToString(),
                                BankAttachFile = dr["BankAttachFile"].ToString(),
                                Status = dr["Status"].ToString(),
                                CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString()),
                                CreateBy = dr["CreateBy"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = Convert.ToDateTime(dr["UpdateDate"].ToString()),
                                BankBranchCode = dr["BankBranchCode"].ToString(),
                                BankBranchName = dr["BankBranchName"].ToString(),
                                PenaltyContractPhone = dr["PenaltyContractPhone"].ToString(),
                                PenaltyContractMail = dr["PenaltyContractMail"].ToString(),
                                ContractPhone = dr["ContractPhone"].ToString(),
                                ContractMail = dr["ContractMail"].ToString(),
                                CompanyId = Guid.Parse(dr["CompanyId"].ToString())

                            }).ToList();



            //Sorting  


            recordsTotal = Excelocation.Count();

            //Paging   
            var data = Excelocation.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });



        }

        public JsonResult BulkInsertLocation(string table)
        {
            ResponseModel result = new ResponseModel();
            if (table == "Location")
            {
                string message = "";
                var LocationList = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelLocationData");
                if (LocationList != null)
                {
                    var ExcelocationList = new List<SubcontractProfileLocationModel>();
                    ExcelocationList = (from DataRow dr in LocationList.Rows

                                        select new SubcontractProfileLocationModel()
                                        {
                                            LocationId = Guid.Parse(dr["LocationId"].ToString()),
                                            LocationCode = dr["LocationCode"].ToString(),
                                            LocationName = dr["LocationName"].ToString(),
                                            LocationNameTh = dr["LocationNameTh"].ToString(),
                                            LocationNameEn = dr["LocationNameEn"].ToString(),
                                            LocationNameAlias = dr["LocationNameAlias"].ToString(),
                                            VendorCode = dr["VendorCode"].ToString(),
                                            StorageLocation = dr["StorageLocation"].ToString(),
                                            ShipTo = dr["ShipTo"].ToString(),
                                            OutOfServiceStorageLocation = dr["OutOfServiceStorageLocation"].ToString(),
                                            SubPhase = dr["SubPhase"].ToString(),
                                            EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"].ToString()),
                                            ShopType = dr["ShopType"].ToString(),
                                            VatBranchNumber = dr["VatBranchNumber"].ToString(),
                                            Phone = dr["Phone"].ToString(),
                                            CompanyMainContractPhone = dr["CompanyMainContractPhone"].ToString(),
                                            InstallationsContractPhone = dr["InstallationsContractPhone"].ToString(),
                                            MaintenanceContractPhone = dr["MaintenanceContractPhone"].ToString(),
                                            InventoryContractPhone = dr["InventoryContractPhone"].ToString(),
                                            PaymentContractPhone = dr["PaymentContractPhone"].ToString(),
                                            EtcContractPhone = dr["EtcContractPhone"].ToString(),
                                            CompanyGroupMail = dr["CompanyGroupMail"].ToString(),
                                            InstallationsContractMail = dr["InstallationsContractMail"].ToString(),
                                            MaintenanceContractMail = dr["MaintenanceContractMail"].ToString(),
                                            InventoryContractMail = dr["InventoryContractMail"].ToString(),
                                            PaymentContractMail = dr["PaymentContractMail"].ToString(),
                                            EtcContractMail = dr["EtcContractMail"].ToString(),
                                            LocationAddress = dr["LocationAddress"].ToString(),
                                            PostAddress = dr["PostAddress"].ToString(),
                                            TaxAddress = dr["TaxAddress"].ToString(),
                                            WtAddress = dr["WtAddress"].ToString(),
                                            HouseNo = dr["HouseNo"].ToString(),
                                            AreaCode = dr["AreaCode"].ToString(),
                                            BankCode = dr["BankCode"].ToString(),
                                            BankName = dr["BankName"].ToString(),
                                            BankAccountNo = dr["BankAccountNo"].ToString(),
                                            BankAccountName = dr["BankAccountName"].ToString(),
                                            BankAttachFile = dr["BankAttachFile"].ToString(),
                                            Status = dr["Status"].ToString(),
                                            CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString()),
                                            CreateBy = dr["CreateBy"].ToString(),
                                            UpdateBy = dr["UpdateBy"].ToString(),
                                            UpdateDate = Convert.ToDateTime(dr["UpdateDate"].ToString()),
                                            BankBranchCode = dr["BankBranchCode"].ToString(),
                                            BankBranchName = dr["BankBranchName"].ToString(),
                                            PenaltyContractPhone = dr["PenaltyContractPhone"].ToString(),
                                            PenaltyContractMail = dr["PenaltyContractMail"].ToString(),
                                            ContractPhone = dr["ContractPhone"].ToString(),
                                            ContractMail = dr["ContractMail"].ToString(),
                                            CompanyId = Guid.Parse(dr["CompanyId"].ToString())

                                        }).ToList();
                    SubcontractProfileLocationModel locationModel;
                    foreach (var dd in ExcelocationList)
                    {
                        locationModel = new SubcontractProfileLocationModel();
                        locationModel.LocationId = dd.LocationId;
                        locationModel.LocationCode = dd.LocationCode;
                        locationModel.LocationCode = dd.LocationCode;
                        locationModel.LocationNameTh = dd.LocationNameTh;
                        locationModel.LocationNameEn = dd.LocationNameEn;
                        locationModel.LocationNameAlias = dd.LocationNameAlias;
                        locationModel.VendorCode = dd.VendorCode;
                        locationModel.StorageLocation = dd.StorageLocation;
                        locationModel.ShipTo = dd.ShipTo;
                        locationModel.OutOfServiceStorageLocation = dd.OutOfServiceStorageLocation;
                        locationModel.SubPhase = dd.SubPhase;
                        locationModel.EffectiveDate = dd.EffectiveDate;
                        locationModel.ShopType = dd.ShopType;
                        locationModel.VatBranchNumber = dd.VatBranchNumber;
                        locationModel.Phone = dd.Phone;
                        locationModel.CompanyMainContractPhone = dd.CompanyMainContractPhone;
                        locationModel.InstallationsContractPhone = dd.InstallationsContractPhone;
                        locationModel.MaintenanceContractPhone = dd.MaintenanceContractPhone;
                        locationModel.InventoryContractPhone = dd.InventoryContractPhone;
                        locationModel.PaymentContractPhone = dd.PaymentContractPhone;
                        locationModel.EtcContractPhone = dd.EtcContractPhone;
                        locationModel.CompanyGroupMail = dd.CompanyGroupMail;
                        locationModel.InstallationsContractMail = dd.InstallationsContractMail;
                        locationModel.MaintenanceContractMail = dd.MaintenanceContractMail;
                        locationModel.InventoryContractMail = dd.InventoryContractMail;
                        locationModel.PaymentContractMail = dd.PaymentContractMail;
                        locationModel.EtcContractMail = dd.EtcContractMail;
                        locationModel.LocationAddress = dd.LocationAddress;
                        locationModel.PostAddress = dd.PostAddress;
                        locationModel.TaxAddress = dd.TaxAddress;
                        locationModel.WtAddress = dd.WtAddress;
                        locationModel.HouseNo = dd.HouseNo;
                        locationModel.AreaCode = dd.AreaCode;
                        locationModel.BankCode = dd.BankCode;
                        locationModel.BankName = dd.BankName;
                        locationModel.BankAccountNo = dd.BankAccountNo;
                        locationModel.BankAccountName = dd.BankAccountName;
                        locationModel.BankAttachFile = dd.BankAttachFile;
                        locationModel.Status = dd.Status;

                        locationModel.CreateDate = dd.CreateDate;
                        locationModel.CreateBy = dd.CreateBy;
                        locationModel.UpdateBy = dd.UpdateBy;
                        locationModel.UpdateDate = dd.UpdateDate;
                        locationModel.BankBranchCode = dd.BankBranchCode;
                        locationModel.BankBranchName = dd.BankBranchName;
                        locationModel.PenaltyContractPhone = dd.PenaltyContractPhone;
                        locationModel.PenaltyContractMail = dd.PenaltyContractMail;
                        locationModel.ContractPhone = dd.ContractPhone;
                        locationModel.ContractMail = dd.ContractMail;
                        locationModel.CompanyId = dd.CompanyId;


                        var LocationResult = new SubcontractProfileLocationModel();

                        var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "Insert"));

                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpContent = new StringContent(JsonConvert.SerializeObject(locationModel), Encoding.UTF8, "application/json");

                        HttpResponseMessage response = client.PostAsync(uriLocation, httpContent).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var results = response.Content.ReadAsStringAsync().Result;

                            result.Status = true;
                            // result.Message = "UploadData:" + ExcelocationList.Count.ToString() + " Record is Success.";
                            result.Message = _localizer["MessageUploadSuccess"];
                            result.StatusError = "0";

                        }
                        else
                        {
                            var results = response.Content.ReadAsStringAsync().Result;
                            //data
                            result.Status = true;
                            result.Message = _localizer["MessageUploadUnSuccess"]; 
                            result.StatusError = "0";
                        }



                    }
                }
                else
                {
                  
                   
                    result.Status = true;
                    // result.Message = "Please input File Before Upload.";
                    result.Message = _localizer["MessageNullFileUpload"];
                    result.StatusError = "0";

                }
           
               
            }
            else {


                result.Status = true;
              //  result.Message = "Please select MasterTable Before Upload.";
                result.Message = _localizer["MessageNullMasterUpload"];
                result.StatusError = "0";
            }


            return Json(result);


        }

    }
}