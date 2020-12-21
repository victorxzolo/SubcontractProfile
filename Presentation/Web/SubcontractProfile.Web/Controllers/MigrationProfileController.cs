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
        public IActionResult onCheckUpload(IFormFile files, string table)
        {
            string _table = table;
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
                           ReadData(files , _table);
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

        public async Task<IActionResult> ReadData(IFormFile postedFile, string TableName)
        {
            if (TableName == "Location")
            {
                List<SubcontractProfileLocationModel> LocationList = new List<SubcontractProfileLocationModel>();


                //Get file
                var newfile = new FileInfo(postedFile.FileName);
                var fileExtension = newfile.Extension;

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
                                string Locationcode;
                                if (workSheet.Cells[i, 1].Value == null)
                                {
                                    Locationcode = null;
                                }
                                else
                                {
                                    Locationcode = workSheet.Cells[i, 1].Value.ToString();
                                }
                                string LocationName;
                                if (workSheet.Cells[i, 2].Value == null)
                                {
                                    LocationName = null;
                                }
                                else
                                {
                                    LocationName = workSheet.Cells[i, 2].Value.ToString();
                                }
                                string LocationNameTh;
                                if (workSheet.Cells[i, 3].Value == null)
                                {
                                    LocationNameTh = null;
                                }
                                else
                                {
                                    LocationNameTh = workSheet.Cells[i, 3].Value.ToString();
                                }

                                string LocationNameEn;
                                if (workSheet.Cells[i, 4].Value == null)
                                {
                                    LocationNameEn = null;
                                }
                                else
                                {
                                    LocationNameEn = workSheet.Cells[i, 4].Value.ToString();
                                }
                                string LocationNameAlias;
                                if (workSheet.Cells[i, 5].Value == null)
                                {
                                    LocationNameAlias = null;
                                }
                                else
                                {
                                    LocationNameAlias = workSheet.Cells[i, 5].Value.ToString();
                                }
                                string vendorcode;
                                if (workSheet.Cells[i, 6].Value == null)
                                {
                                    vendorcode = null;
                                }
                                else
                                {
                                    vendorcode = workSheet.Cells[i, 6].Value.ToString();
                                }
                                string StorageLocation;
                                if (workSheet.Cells[i, 7].Value == null)
                                {
                                    StorageLocation = null;
                                }
                                else
                                {
                                    StorageLocation = workSheet.Cells[i, 7].Value.ToString();
                                }

                                string ShipTo;
                                if (workSheet.Cells[i, 8].Value == null)
                                {
                                    ShipTo = null;
                                }
                                else
                                {
                                    ShipTo = workSheet.Cells[i, 8].Value.ToString();
                                }
                                string OutOfServiceStorageLocation;
                                if (workSheet.Cells[i, 9].Value == null)
                                {
                                    OutOfServiceStorageLocation = null;
                                }
                                else
                                {
                                    OutOfServiceStorageLocation = workSheet.Cells[i, 9].Value.ToString();
                                }

                                string SubPhase;
                                if (workSheet.Cells[i, 10].Value == null)
                                {
                                    SubPhase = null;
                                }
                                else
                                {
                                    SubPhase = workSheet.Cells[i, 10].Value.ToString();
                                }

                                string _efDate;
                                DateTime? EFDATE = new DateTime();
                                if (workSheet.Cells[i, 11].Value == null)
                                {
                                    EFDATE = null;
                                }
                                else
                                {
                                    _efDate = workSheet.Cells[i, 11].Value.ToString();
                                    EFDATE = Convert.ToDateTime(_efDate);
                                }
                                string ShopType;
                                if (workSheet.Cells[i, 12].Value == null)
                                {
                                    ShopType = null;
                                }
                                else
                                {
                                    ShopType = workSheet.Cells[i, 12].Value.ToString();
                                }
                                string VatBranchNumber;
                                if (workSheet.Cells[i, 13].Value == null)
                                {
                                    VatBranchNumber = null;
                                }
                                else
                                {
                                    VatBranchNumber = workSheet.Cells[i, 13].Value.ToString();
                                }

                                string Phone;
                                if (workSheet.Cells[i, 14].Value == null)
                                {
                                    Phone = null;
                                }
                                else
                                {
                                    Phone = workSheet.Cells[i, 14].Value.ToString();
                                }
                                string CompanyMainContractPhone;
                                if (workSheet.Cells[i, 15].Value == null)
                                {
                                    CompanyMainContractPhone = null;
                                }
                                else
                                {
                                    CompanyMainContractPhone = workSheet.Cells[i, 15].Value.ToString();
                                }
                                string InstallationsContractPhone;
                                if (workSheet.Cells[i, 16].Value == null)
                                {
                                    InstallationsContractPhone = null;
                                }
                                else
                                {
                                    InstallationsContractPhone = workSheet.Cells[i, 16].Value.ToString();
                                }

                                string MaintenanceContractPhone;
                                if (workSheet.Cells[i, 17].Value == null)
                                {
                                    MaintenanceContractPhone = null;
                                }
                                else
                                {
                                    MaintenanceContractPhone = workSheet.Cells[i, 17].Value.ToString();
                                }

                                string InventoryContractPhone;
                                if (workSheet.Cells[i, 18].Value == null)
                                {
                                    InventoryContractPhone = null;
                                }
                                else
                                {
                                    InventoryContractPhone = workSheet.Cells[i, 18].Value.ToString();
                                }
                                string PaymentContractPhone;
                                if (workSheet.Cells[i, 19].Value == null)
                                {
                                    PaymentContractPhone = null;
                                }
                                else
                                {
                                    PaymentContractPhone = workSheet.Cells[i, 19].Value.ToString();
                                }
                                string EtcContractPhone;
                                if (workSheet.Cells[i, 20].Value == null)
                                {
                                    EtcContractPhone = null;
                                }
                                else
                                {
                                    EtcContractPhone = workSheet.Cells[i, 20].Value.ToString();
                                }
                                string CompanyGroupMail;
                                if (workSheet.Cells[i, 21].Value == null)
                                {
                                    CompanyGroupMail = null;
                                }
                                else
                                {
                                    CompanyGroupMail = workSheet.Cells[i, 21].Value.ToString();
                                }
                                string InstallationsContractMail;
                                if (workSheet.Cells[i, 22].Value == null)
                                {
                                    InstallationsContractMail = null;
                                }
                                else
                                {
                                    InstallationsContractMail = workSheet.Cells[i, 22].Value.ToString();
                                }

                                string MaintenanceContractMail;
                                if (workSheet.Cells[i, 23].Value == null)
                                {
                                    MaintenanceContractMail = null;
                                }
                                else
                                {
                                    MaintenanceContractMail = workSheet.Cells[i, 23].Value.ToString();
                                }
                                string InventoryContractMail;
                                if (workSheet.Cells[i, 24].Value == null)
                                {
                                    InventoryContractMail = null;
                                }
                                else
                                {
                                    InventoryContractMail = workSheet.Cells[i, 24].Value.ToString();
                                }
                                string PaymentContractMail;
                                if (workSheet.Cells[i, 25].Value == null)
                                {
                                    PaymentContractMail = null;
                                }
                                else
                                {
                                    PaymentContractMail = workSheet.Cells[i, 25].Value.ToString();
                                }
                                string EtcContractMail;
                                if (workSheet.Cells[i, 26].Value == null)
                                {
                                    EtcContractMail = null;
                                }
                                else
                                {
                                    EtcContractMail = workSheet.Cells[i, 26].Value.ToString();
                                }

                                string LocationAddress;
                                if (workSheet.Cells[i, 27].Value == null)
                                {
                                    LocationAddress = null;
                                }
                                else
                                {
                                    LocationAddress = workSheet.Cells[i, 27].Value.ToString();
                                }
                                string PostAddress;
                                if (workSheet.Cells[i, 28].Value == null)
                                {
                                    PostAddress = null;
                                }
                                else
                                {
                                    PostAddress = workSheet.Cells[i, 28].Value.ToString();
                                }
                                string TaxAddress;
                                if (workSheet.Cells[i, 29].Value == null)
                                {
                                    TaxAddress = null;
                                }
                                else
                                {
                                    TaxAddress = workSheet.Cells[i, 29].Value.ToString();
                                }
                                string WtAddress;
                                if (workSheet.Cells[i, 30].Value == null)
                                {
                                    WtAddress = null;
                                }
                                else
                                {
                                    WtAddress = workSheet.Cells[i, 30].Value.ToString();
                                }
                                string HouseNo;
                                if (workSheet.Cells[i, 31].Value == null)
                                {
                                    HouseNo = null;
                                }
                                else
                                {
                                    HouseNo = workSheet.Cells[i, 31].Value.ToString();
                                }
                                string AreaCode;
                                if (workSheet.Cells[i, 32].Value == null)
                                {
                                    AreaCode = null;
                                }
                                else
                                {
                                    AreaCode = workSheet.Cells[i, 32].Value.ToString();
                                }
                                string BankCode;
                                if (workSheet.Cells[i, 33].Value == null)
                                {
                                    BankCode = null;
                                }
                                else
                                {
                                    BankCode = workSheet.Cells[i, 33].Value.ToString();
                                }
                                string BankName;
                                if (workSheet.Cells[i, 34].Value == null)
                                {
                                    BankName = null;
                                }
                                else
                                {
                                    BankName = workSheet.Cells[i, 34].Value.ToString();
                                }

                                string BankAccountNo;
                                if (workSheet.Cells[i, 35].Value == null)
                                {
                                    BankAccountNo = null;
                                }
                                else
                                {
                                    BankAccountNo = workSheet.Cells[i, 35].Value.ToString();
                                }
                                string BankAccountName;
                                if (workSheet.Cells[i, 36].Value == null)
                                {
                                    BankAccountName = null;
                                }
                                else
                                {
                                    BankAccountName = workSheet.Cells[i, 36].Value.ToString();
                                }
                                string BankAttachFile;
                                if (workSheet.Cells[i, 37].Value == null)
                                {
                                    BankAttachFile = null;
                                }
                                else
                                {
                                    BankAttachFile = workSheet.Cells[i, 37].Value.ToString();
                                }
                                string Status;
                                if (workSheet.Cells[i, 38].Value == null)
                                {
                                    Status = null;
                                }
                                else
                                {
                                    Status = workSheet.Cells[i, 38].Value.ToString();
                                }


                                DateTime ddNew = DateTime.Now;


                                string _creDate = workSheet.Cells[i, 39].Value.ToString();
                                DateTime creDate = new DateTime();
                                if (_creDate == null || _creDate == "NULL")
                                {
                                    creDate = ddNew;
                                }
                                else
                                {
                                    creDate = ddNew;
                                }

                                string CreateBy;
                                if (workSheet.Cells[i, 40].Value == null)
                                {
                                    CreateBy = null;
                                }
                                else
                                {
                                    CreateBy = workSheet.Cells[i, 40].Value.ToString();
                                }

                                string UpdateBy;
                                if (workSheet.Cells[i, 41].Value == null)
                                {
                                    UpdateBy = null;
                                }
                                else
                                {
                                    UpdateBy = workSheet.Cells[i, 41].Value.ToString();
                                }


                                string _upDate;
                                DateTime? upDate = new DateTime();
                                if (workSheet.Cells[i, 42].Value == null)
                                {
                                    upDate = ddNew;
                                }
                                else
                                {
                                    _upDate = workSheet.Cells[i, 42].Value.ToString();
                                    upDate = Convert.ToDateTime(upDate);
                                }


                                string BankBranchCode;
                                if (workSheet.Cells[i, 43].Value == null)
                                {
                                    BankBranchCode = null;
                                }
                                else
                                {
                                    BankBranchCode = workSheet.Cells[i, 43].Value.ToString();
                                }
                                string BankBranchName; //44


                                if (workSheet.Cells[i, 44].Value == null)
                                {
                                    BankBranchName = null;
                                }
                                else
                                {
                                    BankBranchName = workSheet.Cells[i, 45].Value.ToString();
                                }
                                string PenaltyContractPhone;//45
                                if (workSheet.Cells[i, 45].Value == null)
                                {
                                    PenaltyContractPhone = null;
                                }
                                else
                                {
                                    PenaltyContractPhone = workSheet.Cells[i, 45].Value.ToString();
                                }
                                string PenaltyContractMail; //46
                                if (workSheet.Cells[i, 46].Value == null)
                                {
                                    PenaltyContractMail = null;
                                }
                                else
                                {
                                    PenaltyContractMail = workSheet.Cells[i, 46].Value.ToString();
                                }
                                string ContractPhone; //47
                                if (workSheet.Cells[i, 47].Value == null)
                                {
                                    ContractPhone = null;
                                }
                                else
                                {
                                    ContractPhone = workSheet.Cells[i, 47].Value.ToString();
                                }
                                string ContractMail; //48
                                if (workSheet.Cells[i, 48].Value == null)
                                {
                                    ContractMail = null;
                                }
                                else
                                {
                                    ContractMail = workSheet.Cells[i, 48].Value.ToString();
                                }

                                LocationList.Add(new SubcontractProfileLocationModel
                                {

                                    LocationCode = Locationcode,

                                    LocationName = LocationName,
                                    LocationNameTh = LocationNameTh,
                                    LocationNameEn = LocationNameEn,
                                    LocationNameAlias = LocationNameAlias,
                                    VendorCode = vendorcode,
                                    StorageLocation = StorageLocation,
                                    ShipTo = ShipTo,
                                    OutOfServiceStorageLocation = OutOfServiceStorageLocation,
                                    SubPhase = SubPhase,
                                    EffectiveDate = EFDATE,
                                    ShopType = ShopType,
                                    VatBranchNumber = VatBranchNumber,
                                    Phone = Phone,
                                    CompanyMainContractPhone = CompanyMainContractPhone,
                                    InstallationsContractPhone = InstallationsContractPhone,
                                    MaintenanceContractPhone = MaintenanceContractPhone,
                                    InventoryContractPhone = InventoryContractPhone,
                                    PaymentContractPhone = PaymentContractPhone,
                                    EtcContractPhone = EtcContractPhone,
                                    CompanyGroupMail = CompanyGroupMail,
                                    InstallationsContractMail = InstallationsContractMail,
                                    MaintenanceContractMail = MaintenanceContractMail,
                                    InventoryContractMail = InventoryContractMail,
                                    PaymentContractMail = PaymentContractMail,
                                    EtcContractMail = EtcContractMail,
                                    LocationAddress = LocationAddress,
                                    PostAddress = PostAddress,
                                    TaxAddress = TaxAddress,
                                    WtAddress = WtAddress,
                                    HouseNo = HouseNo,
                                    AreaCode = AreaCode,
                                    BankCode = BankCode, //33
                                    BankName = BankName, //34
                                    BankAccountNo = BankAccountNo, //35
                                    BankAccountName = BankAccountName, //36
                                    BankAttachFile = BankAttachFile,//37
                                    Status = Status, //38

                                    CreateDate = creDate,
                                    CreateBy = CreateBy,
                                    UpdateBy = UpdateBy,
                                    UpdateDate = upDate,
                                    BankBranchCode = BankBranchCode, //43
                                    BankBranchName = BankBranchName, //44
                                    PenaltyContractPhone = PenaltyContractPhone, //45
                                    PenaltyContractMail = PenaltyContractMail, //46
                                    ContractPhone = ContractPhone, //47
                                    ContractMail = ContractMail, //48
                                    // CompanyId = CompanyId
                                });
                            }
                            if (LocationList.Count > 0)
                            {
                                SessionHelper.SetObjectAsJson(HttpContext.Session, "ExcelLocationData", LocationList);
                                AddDataTableLocation();

                            }
                        }
                        catch (Exception Ex)
                        {
                            string msg = Ex.Message.ToString();

                        }



                    }
                }
            }
            else if (TableName == "Team")
            {
                List<SubcontractProfileTeamModel> TeamList = new List<SubcontractProfileTeamModel>();
                var newfile = new FileInfo(postedFile.FileName);
                var fileExtension = newfile.Extension;
                using (MemoryStream ms = new MemoryStream())
                {
                    await postedFile.CopyToAsync(ms);

                    using (ExcelPackage package = new ExcelPackage(ms))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Team"];
                        int totalRows = workSheet.Dimension.Rows;
                        Guid TeamId; Guid? CompanyId; Guid? LocationId;
                        try
                        {

                            for (int i = 2; i <= totalRows; i++)
                            {
                                DateTime ddNew = DateTime.Now;
                                //string TId = workSheet.Cells[i, 1].Value.ToString();


                                //if ((TId == "NULL") || (TId == null))
                                //{
                                //    TeamId = Guid.NewGuid();
                                //    // return null;
                                //}
                                //else
                                //{
                                //    //locationId = Guid.NewGuid();
                                //    TeamId = new Guid(TId);
                                //}
                                //string CId = workSheet.Cells[i, 24].Value.ToString();


                                //if ((CId == "NULL") || (CId == null))
                                //{
                                //    CompanyId = new Guid();
                                //    // return null;
                                //}
                                //else
                                //{
                                //    //locationId = Guid.NewGuid();
                                //    CompanyId = new Guid(CId);
                                //}
                                //string  LId = workSheet.Cells[i, 25].Value.ToString();


                                //if ((LId == "NULL") || (LId == null))
                                //{
                                //    LocationId = new Guid(); 
                                //    // return null;
                                //}
                                //else
                                //{
                                //    //locationId = Guid.NewGuid();
                                //    LocationId = new Guid(LId);
                                //}

                                string teamcode;
                                if (workSheet.Cells[i, 1].Value == null)
                                {
                                    teamcode = null;
                                }
                                else
                                {
                                    teamcode = workSheet.Cells[i, 1].Value.ToString();
                                }
                                string teamname;
                                if (workSheet.Cells[i, 2].Value == null)
                                {
                                    teamname = null;
                                }
                                else
                                {
                                    teamname = workSheet.Cells[i, 2].Value.ToString();
                                }
                                string teamNameTh;
                                if (workSheet.Cells[i, 3].Value == null)
                                {
                                    teamNameTh = null;
                                }
                                else
                                {
                                    teamNameTh = workSheet.Cells[i, 3].Value.ToString();
                                }
                                string teamNameEn;
                                if (workSheet.Cells[i, 4].Value == null)
                                {
                                    teamNameEn = null;
                                }
                                else
                                {
                                    teamNameEn = workSheet.Cells[i, 4].Value.ToString();
                                }
                                string shipto;
                                if (workSheet.Cells[i, 5].Value == null)
                                {
                                    shipto = null;
                                }
                                else
                                {
                                    shipto = workSheet.Cells[i, 5].Value.ToString();
                                }
                                string stageLocal;
                                if (workSheet.Cells[i, 6].Value == null)
                                {
                                    stageLocal = null;
                                }
                                else
                                {
                                    stageLocal = workSheet.Cells[i, 6].Value.ToString();
                                }

                                string OosstorageLocation;
                                if (workSheet.Cells[i, 7].Value == null)
                                {
                                    OosstorageLocation = null;
                                }
                                else
                                {
                                    OosstorageLocation = workSheet.Cells[i, 7].Value.ToString();
                                }
                                string locationCode;
                                if (workSheet.Cells[i, 8].Value == null)
                                {
                                    locationCode = null;
                                }
                                else
                                {
                                    locationCode = workSheet.Cells[i, 8].Value.ToString();
                                }
                                string vendorcode;
                                if (workSheet.Cells[i, 9].Value == null)
                                {
                                    vendorcode = null;
                                }
                                else
                                {
                                    vendorcode = workSheet.Cells[i, 9].Value.ToString();
                                }
                                string jobType;
                                if (workSheet.Cells[i, 10].Value == null)
                                {
                                    jobType = null;
                                }
                                else
                                {
                                    jobType = workSheet.Cells[i, 10].Value.ToString();
                                }
                                string subcontractType;
                                if (workSheet.Cells[i, 11].Value == null)
                                {
                                    subcontractType = null;
                                }
                                else
                                {
                                    subcontractType = workSheet.Cells[i, 11].Value.ToString();
                                }
                                string subcontractSubType;
                                if (workSheet.Cells[i, 12].Value == null)
                                {
                                    subcontractSubType = null;
                                }
                                else
                                {
                                    subcontractSubType = workSheet.Cells[i, 12].Value.ToString();
                                }
                                string warrantyMa;
                                if (workSheet.Cells[i, 13].Value == null)
                                {
                                    warrantyMa = null;
                                }
                                else
                                {
                                    warrantyMa = workSheet.Cells[i, 13].Value.ToString();
                                }
                                string warrantyInstall;
                                if (workSheet.Cells[i, 14].Value == null)
                                {
                                    warrantyInstall = null;
                                }
                                else
                                {
                                    warrantyInstall = workSheet.Cells[i, 14].Value.ToString();
                                }
                                string serviceSkill;
                                if (workSheet.Cells[i, 15].Value == null)
                                {
                                    serviceSkill = null;
                                }
                                else
                                {
                                    serviceSkill = workSheet.Cells[i, 15].Value.ToString();
                                }
                                string installationsContractPhone;
                                if (workSheet.Cells[i, 16].Value == null)
                                {
                                    installationsContractPhone = null;
                                }
                                else
                                {
                                    installationsContractPhone = workSheet.Cells[i, 16].Value.ToString();
                                }
                                string maintenanceContractPhone;
                                if (workSheet.Cells[i, 17].Value == null)
                                {
                                    maintenanceContractPhone = null;
                                }
                                else
                                {
                                    maintenanceContractPhone = workSheet.Cells[i, 17].Value.ToString();
                                }
                                string etcContractPhone;
                                if (workSheet.Cells[i, 18].Value == null)
                                {
                                    etcContractPhone = null;
                                }
                                else
                                {
                                    etcContractPhone = workSheet.Cells[i, 18].Value.ToString();
                                }
                                string installationsContractEmail;
                                if (workSheet.Cells[i, 19].Value == null)
                                {
                                    installationsContractEmail = null;
                                }
                                else
                                {
                                    installationsContractEmail = workSheet.Cells[i, 19].Value.ToString();
                                }
                                string maintenanceContractEmail;
                                if (workSheet.Cells[i, 20].Value == null)
                                {
                                    maintenanceContractEmail = null;
                                }
                                else
                                {
                                    maintenanceContractEmail = workSheet.Cells[i, 20].Value.ToString();
                                }
                                string etcContractEmail;
                                if (workSheet.Cells[i, 21].Value == null)
                                {
                                    etcContractEmail = null;
                                }
                                else
                                {
                                    etcContractEmail = workSheet.Cells[i, 21].Value.ToString();
                                }
                                string status;
                                if (workSheet.Cells[i, 22].Value == null)
                                {
                                    status = null;
                                }
                                else
                                {
                                    status = workSheet.Cells[i, 22].Value.ToString();
                                }

                                string _creDate = workSheet.Cells[i, 23].Value.ToString();
                                DateTime creDate = new DateTime();
                                if (_creDate == null || _creDate == "NULL")
                                {
                                    creDate = ddNew;
                                }
                                else
                                {
                                    creDate = ddNew;
                                }
                                string _upDate = workSheet.Cells[i, 24].Value.ToString();
                                DateTime upDate = new DateTime();
                                if (_upDate == null || _upDate == "NULL")
                                {
                                    upDate = ddNew;
                                }
                                else
                                {
                                    upDate = ddNew;
                                }

                                string createby;
                                if (workSheet.Cells[i, 25].Value == null)
                                {
                                    createby = null;
                                }
                                else
                                {
                                    createby = workSheet.Cells[i, 25].Value.ToString();
                                }
                                string updateby;
                                if (workSheet.Cells[i, 26].Value == null)
                                {
                                    updateby = null;
                                }
                                else
                                {
                                    updateby = workSheet.Cells[i, 26].Value.ToString();
                                }


                                TeamList.Add(new SubcontractProfileTeamModel
                                {
                                    // TeamId = TeamId,
                                    TeamCode = teamcode,
                                    TeamName = teamname,
                                    TeamNameTh = teamNameTh,
                                    TeamNameEn = teamNameEn,
                                    ShipTo = shipto,
                                    StageLocal = stageLocal,
                                    OosStorageLocation = OosstorageLocation,
                                    LocationCode = locationCode,
                                    VendorCode = vendorcode,
                                    JobType = jobType,
                                    SubcontractType = subcontractType,
                                    SubcontractSubType = subcontractSubType,
                                    WarrantyMa = warrantyMa,
                                    WarrantyInstall = warrantyInstall,
                                    ServiceSkill = serviceSkill,
                                    InstallationsContractPhone = installationsContractPhone,
                                    MaintenanceContractPhone = maintenanceContractPhone,

                                    EtcContractPhone = etcContractPhone,
                                    InstallationsContractEmail = installationsContractEmail,
                                    MaintenanceContractEmail = maintenanceContractEmail,
                                    EtcContractEmail = etcContractEmail,
                                    Status = status,
                                    //  CompanyId = CompanyId,
                                    //  LocationId = LocationId,
                                    CreateDate = creDate,
                                    CreateBy = createby,
                                    UpdateBy = updateby,
                                    UpdateDate = upDate,
                                });

                            }
                            if (TeamList.Count > 0)
                            {
                                SessionHelper.SetObjectAsJson(HttpContext.Session, "ExcelTeamData", TeamList);
                                // AddDataTableTeam();

                            }
                        }
                        catch (Exception Ex)
                        {
                            string msg = Ex.Message.ToString();

                        }



                    }
                }
            }
            else if (TableName == "Engineer")
            {
                List<SubcontractProfileEngineerModel> EngineerList = new List<SubcontractProfileEngineerModel>();
                var newfile = new FileInfo(postedFile.FileName);
                var fileExtension = newfile.Extension;
                using (MemoryStream ms = new MemoryStream())
                {
                    await postedFile.CopyToAsync(ms);

                    using (ExcelPackage package = new ExcelPackage(ms))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Engineer"];
                        int totalRows = workSheet.Dimension.Rows;
                        Guid TeamId; Guid? CompanyId; Guid? LocationId;


                        for (int i = 2; i <= totalRows; i++)
                        {
                            DateTime ddNew = DateTime.Now;

                            string staff_code;
                            if (workSheet.Cells[i, 1].Value == null)
                            {
                                staff_code = null;
                            }
                            else
                            {
                                staff_code = workSheet.Cells[i, 1].Value.ToString();
                            }

                            string foa_code;
                            if (workSheet.Cells[i, 2].Value == null)
                            {
                                foa_code = null;
                            }
                            else
                            {
                                foa_code = workSheet.Cells[i, 2].Value.ToString();
                            }
                            string staff_name;
                            if (workSheet.Cells[i, 3].Value == null)
                            {
                                staff_name = null;
                            }
                            else
                            {
                                staff_name = workSheet.Cells[i, 3].Value.ToString();
                            }
                            string staff_name_th;
                            if (workSheet.Cells[i, 4].Value == null)
                            {
                                staff_name_th = null;
                            }
                            else
                            {
                                staff_name_th = workSheet.Cells[i, 4].Value.ToString();
                            }
                            string staff_name_en;
                            if (workSheet.Cells[i, 5].Value == null)
                            {
                                staff_name_en = null;
                            }
                            else
                            {
                                staff_name_en = workSheet.Cells[i, 5].Value.ToString();
                            }
                            string asc_code;
                            if (workSheet.Cells[i, 6].Value == null)
                            {
                                asc_code = null;
                            }
                            else
                            {
                                asc_code = workSheet.Cells[i, 6].Value.ToString();
                            }
                            string tshirt_size;
                            if (workSheet.Cells[i, 7].Value == null)
                            {
                                tshirt_size = null;
                            }
                            else
                            {
                                tshirt_size = workSheet.Cells[i, 7].Value.ToString();
                            }
                            string contract_phone1;
                            if (workSheet.Cells[i, 8].Value == null)
                            {
                                contract_phone1 = null;
                            }
                            else
                            {
                                contract_phone1 = workSheet.Cells[i, 8].Value.ToString();
                            }
                            string contract_phone2;
                            if (workSheet.Cells[i, 9].Value == null)
                            {
                                contract_phone2 = null;
                            }
                            else
                            {
                                contract_phone2 = workSheet.Cells[i, 9].Value.ToString();
                            }
                            string contract_email;
                            if (workSheet.Cells[i, 10].Value == null)
                            {
                                contract_email = null;
                            }
                            else
                            {
                                contract_email = workSheet.Cells[i, 10].Value.ToString();
                            }
                            string work_experience;
                            if (workSheet.Cells[i, 11].Value == null)
                            {
                                work_experience = null;
                            }
                            else
                            {
                                work_experience = workSheet.Cells[i, 11].Value.ToString();
                            }
                            string work_experience_attach_file;
                            if (workSheet.Cells[i, 12].Value == null)
                            {
                                work_experience_attach_file = null;
                            }
                            else
                            {
                                work_experience_attach_file = workSheet.Cells[i, 12].Value.ToString();
                            }
                            string work_type;
                            if (workSheet.Cells[i, 13].Value == null)
                            {
                                work_type = null;
                            }
                            else
                            {
                                work_type = workSheet.Cells[i, 13].Value.ToString();
                            }
                            string course_skill;
                            if (workSheet.Cells[i, 14].Value == null)
                            {
                                course_skill = null;
                            }
                            else
                            {
                                course_skill = workSheet.Cells[i, 14].Value.ToString();
                            }
                            string skill_level;
                            if (workSheet.Cells[i, 15].Value == null)
                            {
                                skill_level = null;
                            }
                            else
                            {
                                skill_level = workSheet.Cells[i, 15].Value.ToString();
                            }
                            string vehicle_type;
                            if (workSheet.Cells[i, 16].Value == null)
                            {
                                vehicle_type = null;
                            }
                            else
                            {
                                vehicle_type = workSheet.Cells[i, 16].Value.ToString();
                            }
                            string vehicle_brand;
                            if (workSheet.Cells[i, 17].Value == null)
                            {
                                vehicle_brand = null;
                            }
                            else
                            {
                                vehicle_brand = workSheet.Cells[i, 17].Value.ToString();
                            }
                            string vehicle_serise;
                            if (workSheet.Cells[i, 18].Value == null)
                            {
                                vehicle_serise = null;
                            }
                            else
                            {
                                vehicle_serise = workSheet.Cells[i, 18].Value.ToString();
                            }
                            string vehicle_color;
                            if (workSheet.Cells[i, 19].Value == null)
                            {
                                vehicle_color = null;
                            }
                            else
                            {
                                vehicle_color = workSheet.Cells[i, 19].Value.ToString();
                            }
                            string vehicle_year;
                            if (workSheet.Cells[i, 20].Value == null)
                            {
                                vehicle_year = null;
                            }
                            else
                            {
                                vehicle_year = workSheet.Cells[i, 20].Value.ToString();
                            }
                            string vehicle_license_plate;
                            if (workSheet.Cells[i, 21].Value == null)
                            {
                                vehicle_license_plate = null;
                            }
                            else
                            {
                                vehicle_license_plate = workSheet.Cells[i, 21].Value.ToString();
                            }
                            string vehicle_attach_file;
                            if (workSheet.Cells[i, 22].Value == null)
                            {
                                vehicle_attach_file = null;
                            }
                            else
                            {
                                vehicle_attach_file = workSheet.Cells[i, 22].Value.ToString();
                            }
                            string tool_otrd;
                            if (workSheet.Cells[i, 23].Value == null)
                            {
                                tool_otrd = null;
                            }
                            else
                            {
                                tool_otrd = workSheet.Cells[i, 23].Value.ToString();
                            }
                            string tool_splicing;
                            if (workSheet.Cells[i, 24].Value == null)
                            {
                                tool_splicing = null;
                            }
                            else
                            {
                                tool_splicing = workSheet.Cells[i, 24].Value.ToString();
                            }
                            string position;
                            if (workSheet.Cells[i, 25].Value == null)
                            {
                                position = null;
                            }
                            else
                            {
                                position = workSheet.Cells[i, 25].Value.ToString();
                            }
                            string location_code;
                            if (workSheet.Cells[i, 26].Value == null)
                            {
                                location_code = null;
                            }
                            else
                            {
                                location_code = workSheet.Cells[i, 26].Value.ToString();
                            }
                            string staff_id;
                            if (workSheet.Cells[i, 27].Value == null)
                            {
                                staff_id = null;
                            }
                            else
                            {
                                staff_id = workSheet.Cells[i, 27].Value.ToString();
                            }
                            string team_code;
                            if (workSheet.Cells[i, 28].Value == null)
                            {
                                team_code = null;
                            }
                            else
                            {
                                team_code = workSheet.Cells[i, 28].Value.ToString();
                            }
                            string citizen_id;
                            if (workSheet.Cells[i, 29].Value == null)
                            {
                                citizen_id = null;
                            }
                            else
                            {
                                citizen_id = workSheet.Cells[i, 29].Value.ToString();
                            }
                            string bank_code;
                            if (workSheet.Cells[i, 30].Value == null)
                            {
                                bank_code = null;
                            }
                            else
                            {
                                bank_code = workSheet.Cells[i, 30].Value.ToString();
                            }
                            string bank_name;
                            if (workSheet.Cells[i, 31].Value == null)
                            {
                                bank_name = null;
                            }
                            else
                            {
                                bank_name = workSheet.Cells[i, 31].Value.ToString();
                            }
                            string account_no;
                            if (workSheet.Cells[i, 32].Value == null)
                            {
                                account_no = null;
                            }
                            else
                            {
                                account_no = workSheet.Cells[i, 32].Value.ToString();
                            }
                            string account_name;
                            if (workSheet.Cells[i, 33].Value == null)
                            {
                                account_name = null;
                            }
                            else
                            {
                                account_name = workSheet.Cells[i, 33].Value.ToString();
                            }
                            string personal_attach_file;
                            if (workSheet.Cells[i, 34].Value == null)
                            {
                                personal_attach_file = null;
                            }
                            else
                            {
                                personal_attach_file = workSheet.Cells[i, 34].Value.ToString();
                            }
                            string staff_status;
                            if (workSheet.Cells[i, 35].Value == null)
                            {
                                staff_status = null;
                            }
                            else
                            {
                                staff_status = workSheet.Cells[i, 35].Value.ToString();
                            }
                            EngineerList.Add(new SubcontractProfileEngineerModel
                            {
                                StaffCode = staff_code,
                                FoaCode = foa_code,
                                StaffName = staff_name,
                                StaffNameTh = staff_name_th,
                                StaffNameEn = staff_name_en,
                                AscCode = asc_code,
                                TshirtSize = tshirt_size,
                                ContractPhone1 = contract_phone1,
                                ContractPhone2 = contract_phone2,
                                ContractEmail = contract_email,
                                WorkExperience = work_experience,
                                WorkExperienceAttachFile = work_experience_attach_file,
                                WorkType = work_type,
                                CourseSkill = course_skill,
                                SkillLevel = skill_level,
                                VehicleType = vehicle_type,
                                VehicleBrand = vehicle_brand,
                                VehicleSerise = vehicle_serise,
                                VehicleColor = vehicle_color,
                                VehicleYear = vehicle_year,
                                VehicleLicensePlate = vehicle_license_plate,
                                VehicleAttachFile = vehicle_attach_file,
                                ToolOtrd = tool_otrd,
                                ToolSplicing = tool_splicing,
                                Position = position,
                                LocationCode = location_code,
                                StaffId = staff_id,
                                TeamCode = team_code,
                                CitizenId = citizen_id,
                                BankCode = bank_code,
                                BankName = bank_name,
                                AccountNo = account_no,
                                AccountName = account_name,
                                PersonalAttachFile = personal_attach_file,
                                StaffStatus = staff_status,


                            });
                        }
                        if (EngineerList.Count > 0)
                        {
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "ExcelEngineerData", EngineerList);
                            // AddDataTableTeam();

                        }
                    }
                }
            }
            else if (TableName == "Personal")
            {
                List<SubcontractProfilePersonalModel> PersonalList = new List<SubcontractProfilePersonalModel>();
                var newfile = new FileInfo(postedFile.FileName);
                var fileExtension = newfile.Extension;
                using (MemoryStream ms = new MemoryStream())
                {
                    await postedFile.CopyToAsync(ms);

                    using (ExcelPackage package = new ExcelPackage(ms))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Personal"];
                        int totalRows = workSheet.Dimension.Rows;
            


                        for (int i = 2; i <= totalRows; i++)
                        {
                            DateTime ddNew = DateTime.Now;

                            string citizen_id;
                            if (workSheet.Cells[i, 1].Value == null)
                            {
                                citizen_id = null;
                            }
                            else
                            {
                                citizen_id = workSheet.Cells[i, 1].Value.ToString();
                            }

                            
                            string title_name;
                            if (workSheet.Cells[i, 2].Value == null)
                            {
                                title_name = null;
                            }
                            else
                            {
                                title_name = workSheet.Cells[i, 2].Value.ToString();
                            }
                            string full_name_en;
                            if (workSheet.Cells[i, 3].Value == null)
                            {
                                full_name_en = null;
                            }
                            else
                            {
                                full_name_en = workSheet.Cells[i, 3].Value.ToString();
                            }
                            string full_name_th;
                            if (workSheet.Cells[i, 4].Value == null)
                            {
                                full_name_th = null;
                            }
                            else
                            {
                                full_name_th = workSheet.Cells[i, 4].Value.ToString();
                            }
                            string birth_date;
                            if (workSheet.Cells[i, 5].Value == null)
                            {
                                birth_date = null;
                            }
                            else
                            {
                                birth_date = workSheet.Cells[i, 5].Value.ToString();
                            }
                            string gender;
                            if (workSheet.Cells[i, 6].Value == null)
                            {
                                gender = null;
                            }
                            else
                            {
                                gender = workSheet.Cells[i, 6].Value.ToString();
                            }
                            string race;
                            if (workSheet.Cells[i, 7].Value == null)
                            {
                                race = null;
                            }
                            else
                            {
                                race = workSheet.Cells[i, 7].Value.ToString();
                            }
                            string nationality;
                            if (workSheet.Cells[i, 8].Value == null)
                            {
                                nationality = null;
                            }
                            else
                            {
                                nationality = workSheet.Cells[i, 8].Value.ToString();
                            }
                            string religion;
                            if (workSheet.Cells[i, 9].Value == null)
                            {
                                religion = null;
                            }
                            else
                            {
                                religion = workSheet.Cells[i, 9].Value.ToString();
                            }
                            string passport_attach_file;
                            if (workSheet.Cells[i, 10].Value == null)
                            {
                                passport_attach_file = null;
                            }
                            else
                            {
                                passport_attach_file = workSheet.Cells[i, 10].Value.ToString();
                            }
                            string identity_by;
                            if (workSheet.Cells[i, 11].Value == null)
                            {
                                identity_by = null;
                            }
                            else
                            {
                                identity_by = workSheet.Cells[i, 11].Value.ToString();
                            }
                            string address_id;
                            if (workSheet.Cells[i, 12].Value == null)
                            {
                                address_id = null;
                            }
                            else
                            {
                                address_id = workSheet.Cells[i, 12].Value.ToString();
                            }
                            string identity_card_address;
                            if (workSheet.Cells[i, 13].Value == null)
                            {
                                identity_card_address = null;
                            }
                            else
                            {
                                identity_card_address = workSheet.Cells[i, 13].Value.ToString();
                            }
                            string contact_phone1;
                            if (workSheet.Cells[i, 14].Value == null)
                            {
                                contact_phone1 = null;
                            }
                            else
                            {
                                contact_phone1 = workSheet.Cells[i, 14].Value.ToString();
                            }
                            string contact_phone2;
                            if (workSheet.Cells[i, 15].Value == null)
                            {
                                contact_phone2 = null;
                            }
                            else
                            {
                                contact_phone2 = workSheet.Cells[i, 15].Value.ToString();
                            }
                            string contact_email;
                            if (workSheet.Cells[i, 16].Value == null)
                            {
                                contact_email = null;
                            }
                            else
                            {
                                contact_email = workSheet.Cells[i, 16].Value.ToString();
                            }
                            string work_permit_no;
                            if (workSheet.Cells[i, 17].Value == null)
                            {
                                work_permit_no = null;
                            }
                            else
                            {
                                work_permit_no = workSheet.Cells[i, 17].Value.ToString();
                            }
                            string work_permit_attach_file;
                            if (workSheet.Cells[i, 18].Value == null)
                            {
                                work_permit_attach_file = null;
                            }
                            else
                            {
                                work_permit_attach_file = workSheet.Cells[i, 18].Value.ToString();
                            }
                            string profile_img_attach_file;
                            if (workSheet.Cells[i, 19].Value == null)
                            {
                                profile_img_attach_file = null;
                            }
                            else
                            {
                                profile_img_attach_file = workSheet.Cells[i, 19].Value.ToString();
                            }
                            string education;
                            if (workSheet.Cells[i, 20].Value == null)
                            {
                                education = null;
                            }
                            else
                            {
                                education = workSheet.Cells[i, 20].Value.ToString();
                            }
                            string th_listening;
                            if (workSheet.Cells[i, 21].Value == null)
                            {
                                th_listening = null;
                            }
                            else
                            {
                                th_listening = workSheet.Cells[i, 21].Value.ToString();
                            }
                            string th_speaking;
                            if (workSheet.Cells[i, 22].Value == null)
                            {
                                th_speaking = null;
                            }
                            else
                            {
                                th_speaking = workSheet.Cells[i, 22].Value.ToString();
                            }
                            string th_reading;
                            if (workSheet.Cells[i, 23].Value == null)
                            {
                                th_reading = null;
                            }
                            else
                            {
                                th_reading = workSheet.Cells[i, 23].Value.ToString();
                            }
                            string th_writing;
                            if (workSheet.Cells[i, 24].Value == null)
                            {
                                th_writing = null;
                            }
                            else
                            {
                                th_writing = workSheet.Cells[i, 24].Value.ToString();
                            }
                            string en_listening;
                            if (workSheet.Cells[i, 25].Value == null)
                            {
                                en_listening = null;
                            }
                            else
                            {
                                en_listening = workSheet.Cells[i, 25].Value.ToString();
                            }
                            string en_speaking;
                            if (workSheet.Cells[i, 26].Value == null)
                            {
                                en_speaking = null;
                            }
                            else
                            {
                                en_speaking = workSheet.Cells[i, 26].Value.ToString();
                            }
                            string en_reading;
                            if (workSheet.Cells[i, 27].Value == null)
                            {
                                en_reading = null;
                            }
                            else
                            {
                                en_reading = workSheet.Cells[i, 27].Value.ToString();
                            }
                            string en_writing;
                            if (workSheet.Cells[i, 28].Value == null)
                            {
                                en_writing = null;
                            }
                            else
                            {
                                en_writing = workSheet.Cells[i, 28].Value.ToString();
                            }
                            string certificate_type;
                            if (workSheet.Cells[i, 29].Value == null)
                            {
                                certificate_type = null;
                            }
                            else
                            {
                                certificate_type = workSheet.Cells[i, 29].Value.ToString();
                            }
                            string certificate_no;
                            if (workSheet.Cells[i, 30].Value == null)
                            {
                                certificate_no = null;
                            }
                            else
                            {
                                certificate_no = workSheet.Cells[i, 30].Value.ToString();
                            }
                            string certificate_expire_date;
                            if (workSheet.Cells[i, 31].Value == null)
                            {
                                certificate_expire_date = null;
                            }
                            else
                            {
                                certificate_expire_date = workSheet.Cells[i, 31].Value.ToString();
                            }
                            string certificate_attach_file;
                            if (workSheet.Cells[i, 32].Value == null)
                            {
                                certificate_attach_file = null;
                            }
                            else
                            {
                                certificate_attach_file = workSheet.Cells[i, 32].Value.ToString();
                            }
                            string bank_code;
                            if (workSheet.Cells[i, 33].Value == null)
                            {
                                bank_code = null;
                            }
                            else
                            {
                                bank_code = workSheet.Cells[i, 33].Value.ToString();
                            }
                            string bank_name;
                            if (workSheet.Cells[i, 34].Value == null)
                            {
                                bank_name = null;
                            }
                            else
                            {
                                bank_name = workSheet.Cells[i, 34].Value.ToString();
                            }
                            string account_number;
                            if (workSheet.Cells[i, 35].Value == null)
                            {
                                account_number = null;
                            }
                            else
                            {
                                account_number = workSheet.Cells[i, 35].Value.ToString();
                            }
                            string account_name;
                            if (workSheet.Cells[i, 36].Value == null)
                            {
                                account_name = null;
                            }
                            else
                            {
                                account_name = workSheet.Cells[i, 36].Value.ToString();
                            }
                            string status;
                            if (workSheet.Cells[i, 37].Value == null)
                            {
                                status = null;
                            }
                            else
                            {
                                status = workSheet.Cells[i, 37].Value.ToString();
                            }
                          

                            PersonalList.Add(new SubcontractProfilePersonalModel
                            {
                                CitizenId = citizen_id,
                                TitleName = title_name,
                                FullNameEn = full_name_en,
                                FullNameTh = full_name_th,
                                _BirthDate = birth_date,
                                Gender = gender,
                                Race = race,
                                Nationality = nationality,
                                Religion = religion,
                                PassportAttachFile = passport_attach_file,
                                IdentityBy = identity_by,
                                AddressId = Convert.ToInt32(address_id),
                                IdentityCardAddress = identity_card_address,
                                ContactPhone1 = contact_phone1,
                                ContactPhone2 = contact_phone2,
                                ContactEmail = contact_email,
                                WorkPermitNo = work_permit_no,
                                WorkPermitAttachFile = work_permit_attach_file,
                                ProfileImgAttachFile = profile_img_attach_file,
                                Education = education,
                                ThListening = th_listening,
                                ThSpeaking = th_speaking,
                                ThReading = th_reading,
                                ThWriting = th_writing,
                                EnListening = en_listening,
                                EnSpeaking = en_speaking,
                                EnReading = en_reading,
                                EnWriting = en_writing,
                                CertificateType = certificate_type,
                                CertificateNo = certificate_no,
                                _CertificateExpireDate = certificate_expire_date,
                                CertificateAttachFile = certificate_attach_file,
                                BankCode = bank_code,
                                BankName = bank_name,
                                AccountNumber = account_number,
                                AccountName = account_name,
                                Status = status
                                


                            });
                        }
                        if (PersonalList.Count > 0)
                        {
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "ExcelPersonalData", PersonalList);
                            // AddDataTableTeam();

                        }
                    }
                }
            }
            else if (TableName == "Company")
            {
                List<SubcontractProfileCompanyModel> CompanyList = new List<SubcontractProfileCompanyModel>();
                var newfile = new FileInfo(postedFile.FileName);
                var fileExtension = newfile.Extension;
                using (MemoryStream ms = new MemoryStream())
                {
                    await postedFile.CopyToAsync(ms);

                    using (ExcelPackage package = new ExcelPackage(ms))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Company"];
                        int totalRows = workSheet.Dimension.Rows;
                       


                        for (int i = 2; i <= totalRows; i++)
                        {
                            DateTime ddNew = DateTime.Now;

                            string company_code;
                            if (workSheet.Cells[i, 1].Value == null)
                            {
                                company_code = null;
                            }
                            else
                            {
                                company_code = workSheet.Cells[i, 1].Value.ToString();
                            }
                           
                            string company_name;
                            if (workSheet.Cells[i, 2].Value == null)
                            {
                                company_name = null;
                            }
                            else
                            {
                                company_name = workSheet.Cells[i,2].Value.ToString();
                            }
                            string company_name_th;
                            if (workSheet.Cells[i, 3].Value == null)
                            {
                                company_name_th = null;
                            }
                            else
                            {
                                company_name_th = workSheet.Cells[i, 3].Value.ToString();
                            }
                            string company_name_en;
                            if (workSheet.Cells[i, 4].Value == null)
                            {
                                company_name_en = null;
                            }
                            else
                            {
                                company_name_en = workSheet.Cells[i, 4].Value.ToString();
                            }
                            string company_alias;
                            if (workSheet.Cells[i, 5].Value == null)
                            {
                                company_alias = null;
                            }
                            else
                            {
                                company_alias = workSheet.Cells[i, 5].Value.ToString();
                            }
                            string distribution_channel;
                            if (workSheet.Cells[i, 6].Value == null)
                            {
                                distribution_channel = null;
                            }
                            else
                            {
                                distribution_channel = workSheet.Cells[i, 6].Value.ToString();
                            }
                            string channel_sale_group;
                            if (workSheet.Cells[i, 7].Value == null)
                            {
                                channel_sale_group = null;
                            }
                            else
                            {
                                channel_sale_group = workSheet.Cells[i, 7].Value.ToString();
                            }
                            string vendor_code;
                            if (workSheet.Cells[i, 8].Value == null)
                            {
                                vendor_code = null;
                            }
                            else
                            {
                                vendor_code = workSheet.Cells[i, 8].Value.ToString();
                            }
                            string customer_code;
                            if (workSheet.Cells[i, 9].Value == null)
                            {
                                customer_code = null;
                            }
                            else
                            {
                                customer_code = workSheet.Cells[i, 9].Value.ToString();
                            }
                            string area_id;
                            if (workSheet.Cells[i, 10].Value == null)
                            {
                                area_id = null;
                            }
                            else
                            {
                                area_id = workSheet.Cells[i, 10].Value.ToString();
                            }
                            string tax_id;
                            if (workSheet.Cells[i, 11].Value == null)
                            {
                                tax_id = null;
                            }
                            else
                            {
                                tax_id = workSheet.Cells[i, 11].Value.ToString();
                            }
                            string wt_name;
                            if (workSheet.Cells[i, 12].Value == null)
                            {
                                wt_name = null;
                            }
                            else
                            {
                                wt_name = workSheet.Cells[i, 12].Value.ToString();
                            }
                            string vat_type;
                            if (workSheet.Cells[i, 13].Value == null)
                            {
                                vat_type = null;
                            }
                            else
                            {
                                vat_type = workSheet.Cells[i, 13].Value.ToString();
                            }
                            string company_certified_file;
                            if (workSheet.Cells[i, 14].Value == null)
                            {
                                company_certified_file = null;
                            }
                            else
                            {
                                company_certified_file = workSheet.Cells[i, 14].Value.ToString();
                            }
                            string commercial_registration_file;
                            if (workSheet.Cells[i, 15].Value == null)
                            {
                                commercial_registration_file = null;
                            }
                            else
                            {
                                commercial_registration_file = workSheet.Cells[i, 15].Value.ToString();
                            }
                            
                            string deposit_authorization_level;
                            if (workSheet.Cells[i, 16].Value == null)
                            {
                                deposit_authorization_level = null;
                            }
                            else
                            {
                                deposit_authorization_level = workSheet.Cells[i, 16].Value.ToString();
                            }
                            string deposit_payment_type;
                            if (workSheet.Cells[i, 17].Value == null)
                            {
                                deposit_payment_type = null;
                            }
                            else
                            {
                                deposit_payment_type = workSheet.Cells[i, 17].Value.ToString();
                            }
                            string contract_start_date;
                            if (workSheet.Cells[i, 18].Value == null)
                            {
                                contract_start_date = null;
                            }
                            else
                            {
                                contract_start_date = workSheet.Cells[i, 18].Value.ToString();
                            }
                            string contract_end_date;
                            if (workSheet.Cells[i, 19].Value == null)
                            {
                                contract_end_date = null;
                            }
                            else
                            {
                                contract_end_date = workSheet.Cells[i, 19].Value.ToString();
                            }
                            string over_draft_deposit;
                            if (workSheet.Cells[i, 20].Value == null)
                            {
                                over_draft_deposit = null;
                            }
                            else
                            {
                                over_draft_deposit = workSheet.Cells[i, 20].Value.ToString();
                            }
                            string balance_deposit;
                            if (workSheet.Cells[i, 21].Value == null)
                            {
                                balance_deposit = null;
                            }
                            else
                            {
                                balance_deposit = workSheet.Cells[i, 21].Value.ToString();
                            }
                            string company_status;
                            if (workSheet.Cells[i, 22].Value == null)
                            {
                                company_status = null;
                            }
                            else
                            {
                                company_status = workSheet.Cells[i, 22].Value.ToString();
                            }
                            string company_address;
                            if (workSheet.Cells[i, 23].Value == null)
                            {
                                company_address = null;
                            }
                            else
                            {
                                company_address = workSheet.Cells[i, 23].Value.ToString();
                            }
                            string vat_address;
                            if (workSheet.Cells[i, 24].Value == null)
                            {
                                vat_address = null;
                            }
                            else
                            {
                                vat_address = workSheet.Cells[i, 24].Value.ToString();
                            }
                            string company_email;
                            if (workSheet.Cells[i, 25].Value == null)
                            {
                                company_email = null;
                            }
                            else
                            {
                                company_email = workSheet.Cells[i, 25].Value.ToString();
                            }
                            string contract_name;
                            if (workSheet.Cells[i, 26].Value == null)
                            {
                                contract_name = null;
                            }
                            else
                            {
                                contract_name = workSheet.Cells[i, 26].Value.ToString();
                            }
                            string contract_phone;
                            if (workSheet.Cells[i, 27].Value == null)
                            {
                                contract_phone = null;
                            }
                            else
                            {
                                contract_phone = workSheet.Cells[i, 27].Value.ToString();
                            }
                            string contract_email;
                            if (workSheet.Cells[i, 28].Value == null)
                            {
                                contract_email = null;
                            }
                            else
                            {
                                contract_email = workSheet.Cells[i, 28].Value.ToString();
                            }
                            string bank_code;
                            if (workSheet.Cells[i, 29].Value == null)
                            {
                                bank_code = null;
                            }
                            else
                            {
                                bank_code = workSheet.Cells[i, 29].Value.ToString();
                            }
                            string bank_name;
                            if (workSheet.Cells[i, 30].Value == null)
                            {
                                bank_name = null;
                            }
                            else
                            {
                                bank_name = workSheet.Cells[i, 30].Value.ToString();
                            }
                            string account_number;
                            if (workSheet.Cells[i, 31].Value == null)
                            {
                                account_number = null;
                            }
                            else
                            {
                                account_number = workSheet.Cells[i, 31].Value.ToString();
                            }
                            string account_name;
                            if (workSheet.Cells[i, 32].Value == null)
                            {
                                account_name = null;
                            }
                            else
                            {
                                account_name = workSheet.Cells[i, 32].Value.ToString();
                            }
                            string attach_file;
                            if (workSheet.Cells[i, 33].Value == null)
                            {
                                attach_file = null;
                            }
                            else
                            {
                                attach_file = workSheet.Cells[i, 33].Value.ToString();
                            }
                            string branch_code;
                            if (workSheet.Cells[i, 34].Value == null)
                            {
                                branch_code = null;
                            }
                            else
                            {
                                branch_code = workSheet.Cells[i, 34].Value.ToString();
                            }
                            string branch_name;
                            if (workSheet.Cells[i, 35].Value == null)
                            {
                                branch_name = null;
                            }
                            else
                            {
                                branch_name = workSheet.Cells[i, 35].Value.ToString();
                            }
                            string dept_of_install_name;
                            if (workSheet.Cells[i, 36].Value == null)
                            {
                                dept_of_install_name = null;
                            }
                            else
                            {
                                dept_of_install_name = workSheet.Cells[i, 36].Value.ToString();
                            }
                            string dept_of_mainten_name;
                            if (workSheet.Cells[i, 37].Value == null)
                            {
                                dept_of_mainten_name = null;
                            }
                            else
                            {
                                dept_of_mainten_name = workSheet.Cells[i, 37].Value.ToString();
                            }
                            string dept_of_account_name;
                            if (workSheet.Cells[i, 38].Value == null)
                            {
                                dept_of_account_name = null;
                            }
                            else
                            {
                                dept_of_account_name = workSheet.Cells[i, 38].Value.ToString();
                            }
                            string dept_of_install_phone;
                            if (workSheet.Cells[i, 39].Value == null)
                            {
                                dept_of_install_phone = null;
                            }
                            else
                            {
                                dept_of_install_phone = workSheet.Cells[i, 39].Value.ToString();
                            }
                            string dept_of_mainten_phone;
                            if (workSheet.Cells[i, 40].Value == null)
                            {
                                dept_of_mainten_phone = null;
                            }
                            else
                            {
                                dept_of_mainten_phone = workSheet.Cells[i, 40].Value.ToString();
                            }
                            string dept_of_account_phone;
                            if (workSheet.Cells[i, 41].Value == null)
                            {
                                dept_of_account_phone = null;
                            }
                            else
                            {
                                dept_of_account_phone = workSheet.Cells[i, 41].Value.ToString();
                            }
                            string dept_of_install_email;
                            if (workSheet.Cells[i, 42].Value == null)
                            {
                                dept_of_install_email = null;
                            }
                            else
                            {
                                dept_of_install_email = workSheet.Cells[i, 42].Value.ToString();
                            }
                            string dept_of_mainten_email;
                            if (workSheet.Cells[i, 43].Value == null)
                            {
                                dept_of_mainten_email = null;
                            }
                            else
                            {
                                dept_of_mainten_email = workSheet.Cells[i, 43].Value.ToString();
                            }
                            string dept_of_account_email;
                            if (workSheet.Cells[i, 44].Value == null)
                            {
                                dept_of_account_email = null;
                            }
                            else
                            {
                                dept_of_account_email = workSheet.Cells[i, 44].Value.ToString();
                            }
                            string location_code;
                            if (workSheet.Cells[i, 45].Value == null)
                            {
                                location_code = null;
                            }
                            else
                            {
                                location_code = workSheet.Cells[i, 45].Value.ToString();
                            }
                            string location_name_th;
                            if (workSheet.Cells[i, 46].Value == null)
                            {
                                location_name_th = null;
                            }
                            else
                            {
                                location_name_th = workSheet.Cells[i, 46].Value.ToString();
                            }
                            string location_name_en;
                            if (workSheet.Cells[i, 47].Value == null)
                            {
                                location_name_en = null;
                            }
                            else
                            {
                                location_name_en = workSheet.Cells[i, 47].Value.ToString();
                            }
                            string bank_account_type_id;
                            if (workSheet.Cells[i, 48].Value == null)
                            {
                                bank_account_type_id = null;
                            }
                            else
                            {
                                bank_account_type_id = workSheet.Cells[i, 48].Value.ToString();
                            }
                            string subcontract_profile_type;
                            if (workSheet.Cells[i, 49].Value == null)
                            {
                                subcontract_profile_type = null;
                            }
                            else
                            {
                                subcontract_profile_type = workSheet.Cells[i, 49].Value.ToString();
                            }
                            string company_title_th_id;
                            if (workSheet.Cells[i, 50].Value == null)
                            {
                                company_title_th_id = null;
                            }
                            else
                            {
                                company_title_th_id = workSheet.Cells[i, 50].Value.ToString();
                            }
                            string company_title_en_id;
                            if (workSheet.Cells[i, 51].Value == null)
                            {
                                company_title_en_id = null;
                            }
                            else
                            {
                                company_title_en_id = workSheet.Cells[i, 51].Value.ToString();
                            }
                            string status;
                            if (workSheet.Cells[i, 52].Value == null)
                            {
                                status = null;
                            }
                            else
                            {
                                status = workSheet.Cells[i, 52].Value.ToString();
                            }
                            string activate_date;
                            if (workSheet.Cells[i, 53].Value == null)
                            {
                                activate_date = null;
                            }
                            else
                            {
                                activate_date = workSheet.Cells[i, 53].Value.ToString();
                            }
                            string register_date;
                            if (workSheet.Cells[i, 54].Value == null)
                            {
                                register_date = null;
                            }
                            else
                            {
                                register_date = workSheet.Cells[i, 54].Value.ToString();
                            }
                            string remark_for_sub;
                            if (workSheet.Cells[i, 55].Value == null)
                            {
                                remark_for_sub = null;
                            }
                            else
                            {
                                remark_for_sub = workSheet.Cells[i, 55].Value.ToString();
                            }
                            string verified_date;
                            if (workSheet.Cells[i, 56].Value == null)
                            {
                                verified_date = null;
                            }
                            else
                            {
                                verified_date = workSheet.Cells[i, 56].Value.ToString();
                            }
                            string remark;
                            if (workSheet.Cells[i, 57].Value == null)
                            {
                                remark = null;
                            }
                            else
                            {
                                remark = workSheet.Cells[i, 57].Value.ToString();
                            }


                            CompanyList.Add(new SubcontractProfileCompanyModel
                            {

                                CompanyCode = company_code,
                                CompanyName = company_name,
                                CompanyNameTh = company_name_th,
                                CompanyNameEn = company_name_en,
                                CompanyAlias = company_alias,
                                DistributionChannel = distribution_channel,
                                ChannelSaleGroup = channel_sale_group,
                                VendorCode = vendor_code,
                                CustomerCode = customer_code,
                                AreaId = area_id,
                                TaxId = tax_id,
                                WtName = wt_name,
                                VatType = vat_type,
                                CompanyCertifiedFile = company_certified_file,
                                CommercialRegistrationFile = commercial_registration_file,
                             //   vatRegistrationCertificateFile = vat_registration_certificate_file,
                              //  contract_agreement_file = contract_agreement_file,
                                DepositAuthorizationLevel = deposit_authorization_level,
                                DepositPaymentType = deposit_payment_type,
                                _ContractStartDate = contract_start_date,
                                _ContractEndDate = contract_end_date,
                                OverDraftDeposit = over_draft_deposit,
                                BalanceDeposit = Convert.ToInt32(balance_deposit),
                                CompanyStatus = company_status,
                                CompanyAddress = company_address,
                                VatAddress = vat_address,
                                CompanyEmail = company_email,
                                ContractName = contract_name,
                                ContractPhone = contract_phone,
                                ContractEmail = contract_email,
                                BankCode = bank_code,
                                BankName = bank_name,
                                AccountNumber = account_number,
                                AccountName = account_name,
                                AttachFile = attach_file,
                                BranchCode = branch_code,
                                BranchName = branch_name,
                                DeptOfInstallName = dept_of_install_name,
                                DeptOfMaintenName = dept_of_mainten_name,
                                DeptOfAccountName = dept_of_account_name,
                                DeptOfInstallPhone = dept_of_install_phone,
                                DeptOfMaintenPhone = dept_of_mainten_phone,
                                DeptOfAccountPhone = dept_of_account_phone,
                                DeptOfInstallEmail = dept_of_install_email,
                                DeptOfMaintenEmail = dept_of_mainten_email,
                                DeptOfAccountEmail = dept_of_account_email,
                                LocationCode = location_code,
                                LocationNameTh = location_name_th,
                                LocationNameEn = location_name_en,
                                BankAccountTypeId = bank_account_type_id,
                                SubcontractProfileType = subcontract_profile_type,
                                CompanyTitleThId = company_title_th_id,
                                CompanyTitleEnId = company_title_en_id,
                                Status = status,
                                _ActivateDate = activate_date,
                                _RegisterDate = register_date,
                                RemarkForSub = remark_for_sub,
                                _VerifiedDate = verified_date,
                                Remark = remark




                            });
                        }
                        if (CompanyList.Count > 0)
                        {
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "ExcelCompanyData", CompanyList);
                            // AddDataTableTeam();

                        }
                    }
                }

            }

                return null;

        }
        public ActionResult AddDataTablePersonal()
        {
            var sessiommodel = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelPersonalData");


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
            var ExcelPersonal = new List<SubcontractProfilePersonalModel>();

            //  DataTable dt = new DataTable();

            ExcelPersonal = (from DataRow dr in sessiommodel.Rows



                            select new SubcontractProfilePersonalModel()
                            {


                                CitizenId = Convert.ToString(dr["CitizenId"] ?? ""),
                                TitleName = Convert.ToString(dr["TitleName"] ?? ""),
                                FullNameEn = Convert.ToString(dr["FullNameEn"] ?? ""),
                                FullNameTh = Convert.ToString(dr["FullNameTh"] ?? ""),
                                _BirthDate = Convert.ToString(dr["_BirthDate"] ?? ""),
                                Gender = Convert.ToString(dr["Gender"] ?? ""),
                                Race = Convert.ToString(dr["Race"] ?? ""),
                                Nationality = Convert.ToString(dr["Nationality"] ?? ""),
                                Religion = Convert.ToString(dr["Religion"] ?? ""),
                                PassportAttachFile = Convert.ToString(dr["PassportAttachFile"] ?? ""),
                                IdentityBy = Convert.ToString(dr["IdentityBy"] ?? ""),
                                AddressId = Convert.ToInt32(dr["AddressId"] ?? ""),
                                IdentityCardAddress = Convert.ToString(dr["IdentityCardAddress"] ?? ""),
                                ContactPhone1 = Convert.ToString(dr["ContactPhone1"] ?? ""),
                                ContactPhone2 = Convert.ToString(dr["ContactPhone2"] ?? ""),
                                ContactEmail = Convert.ToString(dr["ContactEmail"] ?? ""),
                                WorkPermitNo = Convert.ToString(dr["WorkPermitNo"] ?? ""),
                                WorkPermitAttachFile = Convert.ToString(dr["WorkPermitAttachFile"] ?? ""),
                                ProfileImgAttachFile = Convert.ToString(dr["ProfileImgAttachFile"] ?? ""),
                                Education = Convert.ToString(dr["Education"] ?? ""),
                                ThListening = Convert.ToString(dr["ThListening"] ?? ""),
                                ThSpeaking = Convert.ToString(dr["ThSpeaking"] ?? ""),
                                ThReading = Convert.ToString(dr["ThReading"] ?? ""),
                                ThWriting = Convert.ToString(dr["ThWriting"] ?? ""),
                                EnListening = Convert.ToString(dr["EnListening"] ?? ""),
                                EnSpeaking = Convert.ToString(dr["EnSpeaking"] ?? ""),
                                EnReading = Convert.ToString(dr["EnReading"] ?? ""),
                                EnWriting = Convert.ToString(dr["EnWriting"] ?? ""),
                                CertificateType = Convert.ToString(dr["CertificateType"] ?? ""),
                                CertificateNo = Convert.ToString(dr["CertificateNo"] ?? ""),
                                _CertificateExpireDate = Convert.ToString(dr["_CertificateExpireDate"] ?? ""),
                                CertificateAttachFile = Convert.ToString(dr["CertificateAttachFile"] ?? ""),
                                BankCode = Convert.ToString(dr["BankCode"] ?? ""),
                                BankName = Convert.ToString(dr["BankName"] ?? ""),
                                AccountNumber = Convert.ToString(dr["AccountNumber"] ?? ""),
                                AccountName = Convert.ToString(dr["AccountName"] ?? ""),
                                Status = Convert.ToString(dr["Status"] ?? "")
        

                            }).ToList();



            //Sorting  


            recordsTotal = ExcelPersonal.Count();

            //Paging   
            var data = ExcelPersonal.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });


        }
        public ActionResult AddDataTableCompany()
        {
            var sessiommodel = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelCompanyData");


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
            var ExcelCompany = new List<SubcontractProfileCompanyModel>();

            //  DataTable dt = new DataTable();

            ExcelCompany = (from DataRow dr in sessiommodel.Rows



                             select new SubcontractProfileCompanyModel()
                             {
                                 CompanyCode = Convert.ToString(dr["CompanyCode"] ?? ""),
                                 CompanyName = Convert.ToString(dr["CompanyName"] ?? ""),
                                 CompanyNameTh = Convert.ToString(dr["CompanyNameTh"] ?? ""),
                                 CompanyNameEn = Convert.ToString(dr["CompanyNameEn"] ?? ""),
                                 CompanyAlias = Convert.ToString(dr["CompanyAlias"] ?? ""),
                                 DistributionChannel = Convert.ToString(dr["DistributionChannel"] ?? ""),
                                 ChannelSaleGroup = Convert.ToString(dr["ChannelSaleGroup"] ?? ""),
                                 VendorCode = Convert.ToString(dr["VendorCode"] ?? ""),
                                 CustomerCode = Convert.ToString(dr["CustomerCode"] ?? ""),
                                 AreaId = Convert.ToString(dr["AreaId"] ?? ""),
                                 TaxId = Convert.ToString(dr["TaxId"] ?? ""),
                                 WtName = Convert.ToString(dr["WtName"] ?? ""),
                                 VatType = Convert.ToString(dr["VatType"] ?? ""),
                                 CompanyCertifiedFile = Convert.ToString(dr["CompanyCertifiedFile"] ?? ""),
                                 CommercialRegistrationFile = Convert.ToString(dr["CommercialRegistrationFile"] ?? ""),
                                
                                 DepositAuthorizationLevel = Convert.ToString(dr["DepositAuthorizationLevel"] ?? ""),
                                 DepositPaymentType = Convert.ToString(dr["DepositPaymentType"] ?? ""),
                                 _ContractStartDate = Convert.ToString(dr["_ContractStartDate"] ?? ""),
                                 _ContractEndDate = Convert.ToString(dr["_ContractEndDate"] ?? ""),
                                 OverDraftDeposit = Convert.ToString(dr["OverDraftDeposit"] ?? ""),
                                 BalanceDeposit = Convert.ToInt32(dr["BalanceDeposit"] ?? ""),
                                 CompanyStatus = Convert.ToString(dr["CompanyStatus"] ?? ""),
                                 CompanyAddress = Convert.ToString(dr["CompanyAddress"] ?? ""),
                                 VatAddress = Convert.ToString(dr["VatAddress"] ?? ""),
                                 CompanyEmail = Convert.ToString(dr["CompanyEmail"] ?? ""),
                                 ContractName = Convert.ToString(dr["ContractName"] ?? ""),
                                 ContractPhone = Convert.ToString(dr["ContractPhone"] ?? ""),
                                 ContractEmail = Convert.ToString(dr["ContractEmail"] ?? ""),
                                 BankCode = Convert.ToString(dr["BankCode"] ?? ""),
                                 BankName = Convert.ToString(dr["BankName"] ?? ""),
                                 AccountNumber = Convert.ToString(dr["AccountNumber"] ?? ""),
                                 AccountName = Convert.ToString(dr["AccountName"] ?? ""),
                                 AttachFile = Convert.ToString(dr["AttachFile"] ?? ""),
                                 BranchCode = Convert.ToString(dr["BranchCode"] ?? ""),
                                 BranchName = Convert.ToString(dr["BranchName"] ?? ""),
                                 DeptOfInstallName = Convert.ToString(dr["DeptOfInstallName"] ?? ""),
                                 DeptOfMaintenName = Convert.ToString(dr["DeptOfMaintenName"] ?? ""),
                                 DeptOfAccountName = Convert.ToString(dr["DeptOfAccountName"] ?? ""),
                                 DeptOfInstallPhone = Convert.ToString(dr["DeptOfInstallPhone"] ?? ""),
                                 DeptOfMaintenPhone = Convert.ToString(dr["DeptOfMaintenPhone"] ?? ""),
                                 DeptOfAccountPhone = Convert.ToString(dr["DeptOfAccountPhone"] ?? ""),
                                 DeptOfInstallEmail = Convert.ToString(dr["DeptOfInstallEmail"] ?? ""),
                                 DeptOfMaintenEmail = Convert.ToString(dr["DeptOfMaintenEmail"] ?? ""),
                                 DeptOfAccountEmail = Convert.ToString(dr["DeptOfAccountEmail"] ?? ""),
                                 LocationCode = Convert.ToString(dr["LocationCode"] ?? ""),
                                 LocationNameTh = Convert.ToString(dr["LocationNameTh"] ?? ""),
                                 LocationNameEn = Convert.ToString(dr["LocationNameEn"] ?? ""),
                                 BankAccountTypeId = Convert.ToString(dr["BankAccountTypeId"] ?? ""),
                                 SubcontractProfileType = Convert.ToString(dr["SubcontractProfileType"] ?? ""),
                                 CompanyTitleThId = Convert.ToString(dr["CompanyTitleThId"] ?? ""),
                                 CompanyTitleEnId = Convert.ToString(dr["CompanyTitleEnId"] ?? ""),
                                 Status = Convert.ToString(dr["Status"] ?? ""),
                                 _ActivateDate = Convert.ToString(dr["_ActivateDate"] ?? ""),
                                 _RegisterDate = Convert.ToString(dr["_RegisterDate"] ?? ""),
                                 RemarkForSub = Convert.ToString(dr["RemarkForSub"] ?? ""),
                                 _VerifiedDate = Convert.ToString(dr["_VerifiedDate"] ?? ""),
                                 Remark = Convert.ToString(dr["Remark"] ?? "")


                             }).ToList();



            //Sorting  


            recordsTotal = ExcelCompany.Count();

            //Paging   
            var data = ExcelCompany.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });


        }
        public ActionResult AddDataTableEngineer()
        {

            var sessiommodel = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelEngineerData");


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
            var ExcelEngineer = new List<SubcontractProfileEngineerModel>();

            //  DataTable dt = new DataTable();

            ExcelEngineer = (from DataRow dr in sessiommodel.Rows



                            select new SubcontractProfileEngineerModel()
                            {


                                StaffCode = Convert.ToString(dr["StaffCode"] ?? ""),
                                FoaCode = Convert.ToString(dr["FoaCode"] ?? ""),
                                StaffName = Convert.ToString(dr["StaffName"] ?? ""),
                                StaffNameTh = Convert.ToString(dr["StaffNameTh"] ?? ""),
                                StaffNameEn = Convert.ToString(dr["StaffNameEn"] ?? ""),
                                AscCode = Convert.ToString(dr["AscCode"] ?? ""),
                                TshirtSize = Convert.ToString(dr["TshirtSize"] ?? ""),
                                ContractPhone1 = Convert.ToString(dr["ContractPhone1"] ?? ""),
                                ContractPhone2 = Convert.ToString(dr["ContractPhone2"] ?? ""),
                                ContractEmail = Convert.ToString(dr["ContractEmail"] ?? ""),
                                WorkExperience = Convert.ToString(dr["WorkExperience"] ?? ""),
                                WorkExperienceAttachFile = Convert.ToString(dr["WorkExperienceAttachFile"] ?? ""),
                                WorkType = Convert.ToString(dr["WorkType"] ?? ""),
                                CourseSkill = Convert.ToString(dr["CourseSkill"] ?? ""),
                                SkillLevel = Convert.ToString(dr["SkillLevel"] ?? ""),
                                VehicleType = Convert.ToString(dr["VehicleType"] ?? ""),
                                VehicleBrand = Convert.ToString(dr["VehicleBrand"] ?? ""),
                                VehicleSerise = Convert.ToString(dr["VehicleSerise"] ?? ""),
                                VehicleColor = Convert.ToString(dr["VehicleColor"] ?? ""),
                                VehicleYear = Convert.ToString(dr["VehicleYear"] ?? ""),
                                VehicleLicensePlate = Convert.ToString(dr["VehicleLicensePlate"] ?? ""),
                                VehicleAttachFile = Convert.ToString(dr["VehicleAttachFile"] ?? ""),
                                ToolOtrd = Convert.ToString(dr["ToolOtrd"] ?? ""),
                                ToolSplicing = Convert.ToString(dr["ToolSplicing"] ?? ""),
                                Position = Convert.ToString(dr["Position"] ?? ""),
                                LocationCode = Convert.ToString(dr["LocationCode"] ?? ""),
                                StaffId = Convert.ToString(dr["StaffId"] ?? ""),
                                TeamCode = Convert.ToString(dr["TeamCode"] ?? ""),
                                CitizenId = Convert.ToString(dr["CitizenId"] ?? ""),
                                BankCode = Convert.ToString(dr["BankCode"] ?? ""),
                                BankName = Convert.ToString(dr["BankName"] ?? ""),
                                AccountNo = Convert.ToString(dr["AccountNo"] ?? ""),
                                AccountName = Convert.ToString(dr["AccountName"] ?? ""),
                                PersonalAttachFile = Convert.ToString(dr["PersonalAttachFile"] ?? ""),
                                StaffStatus = Convert.ToString(dr["StaffStatus"] ?? ""),




                            }).ToList();



            //Sorting  


            recordsTotal = ExcelEngineer.Count();

            //Paging   
            var data = ExcelEngineer.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });



        }
        public ActionResult AddDataTableTeam()
        {
          
            var sessiommodel = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelTeamData");


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
            var Excelocation = new List<SubcontractProfileTeamModel>();
      
           //  DataTable dt = new DataTable();
         
            Excelocation = (from DataRow dr in sessiommodel.Rows
                             
                         
            
            select new SubcontractProfileTeamModel()
                            {

                                    
          
                                TeamCode = Convert.ToString(dr["TeamCode"] ?? ""),
                                TeamName = Convert.ToString(dr["TeamName"] ?? ""),
                               TeamNameTh = Convert.ToString(dr["TeamNameTh"] ?? ""),
               TeamNameEn = Convert.ToString(dr["TeamNameEn"] ?? ""),
                ShipTo = Convert.ToString(dr["ShipTo"] ?? ""),

                StageLocal = Convert.ToString(dr["StageLocal"] ?? ""),
                LocationCode = Convert.ToString(dr["LocationCode"] ?? ""),
                VendorCode = Convert.ToString(dr["VendorCode"] ?? ""),
                JobType = Convert.ToString(dr["JobType"] ?? ""),

                SubcontractType = Convert.ToString(dr["SubcontractType"] ?? ""),
                SubcontractSubType = Convert.ToString(dr["SubcontractSubType"] ?? ""),
                WarrantyMa = Convert.ToString(dr["WarrantyMa"] ?? ""),
                WarrantyInstall = Convert.ToString(dr["WarrantyInstall"] ?? ""),

                ServiceSkill = Convert.ToString(dr["ServiceSkill"] ?? ""),
                InstallationsContractPhone = Convert.ToString(dr["InstallationsContractPhone"] ?? ""),
                MaintenanceContractPhone = Convert.ToString(dr["MaintenanceContractPhone"] ?? ""),
                EtcContractPhone = Convert.ToString(dr["EtcContractPhone"] ?? ""),

                InstallationsContractEmail = Convert.ToString(dr["InstallationsContractEmail"] ?? ""),
                MaintenanceContractEmail = Convert.ToString(dr["MaintenanceContractEmail"] ?? ""),
                EtcContractEmail = Convert.ToString(dr["EtcContractEmail"] ?? ""),
                Status = Convert.ToString(dr["Status"] ?? ""),


                CreateDate = Convert.ToDateTime(dr["CreateDate"] ?? ""),
                CreateBy = Convert.ToString(dr["CreateBy"] ?? ""),
                UpdateBy = Convert.ToString(dr["UpdateBy"] ?? ""),
                UpdateDate = Convert.ToDateTime(dr["UpdateDate"] ?? ""),



            }).ToList();



            //Sorting  


            recordsTotal = Excelocation.Count();

            //Paging   
            var data = Excelocation.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });



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
            DateTime ddNew = DateTime.Now;
            var Excelocation = new List<SubcontractProfileLocationModel>();
                      

            Excelocation = (from DataRow dr in sessiommodel.Rows
                          



                            select new SubcontractProfileLocationModel()
                            {
                               //  LocationId = Guid.Parse(dr["LocationId"].ToString()),
                                LocationCode = Convert.ToString(dr["LocationCode"] ?? ""),
                                LocationName = Convert.ToString(dr["LocationName"] ?? ""),
                                LocationNameTh = Convert.ToString(dr["LocationNameTh"] ?? ""),
                                LocationNameEn = Convert.ToString(dr["LocationNameEn"] ?? ""),
                                LocationNameAlias = Convert.ToString(dr["LocationNameAlias"] ?? ""),
                                VendorCode = Convert.ToString(dr["VendorCode"] ?? ""),
                                StorageLocation = Convert.ToString(dr["StorageLocation"] ?? ""),
                                ShipTo = Convert.ToString(dr["ShipTo"] ?? ""),
                                OutOfServiceStorageLocation = Convert.ToString(dr["OutOfServiceStorageLocation"] ?? ""),
                                SubPhase = Convert.ToString(dr["SubPhase"] ?? ""),
                                _EffectiveDate = Convert.ToString(dr["EffectiveDate"] ?? ""),
                                ShopType = Convert.ToString(dr["ShopType"] ?? ""),
                                VatBranchNumber = Convert.ToString(dr["VatBranchNumber"] ?? ""),
                                Phone = Convert.ToString(dr["Phone"] ?? ""),
                                CompanyMainContractPhone = Convert.ToString(dr["CompanyMainContractPhone"] ?? ""),
                                InstallationsContractPhone = Convert.ToString(dr["InstallationsContractPhone"] ?? ""),
                                MaintenanceContractPhone = Convert.ToString(dr["MaintenanceContractPhone"] ?? ""),
                                InventoryContractPhone = Convert.ToString(dr["InventoryContractPhone"] ?? ""),
                                PaymentContractPhone = Convert.ToString(dr["PaymentContractPhone"] ?? ""),
                                EtcContractPhone = Convert.ToString(dr["EtcContractPhone"] ?? ""),
                                CompanyGroupMail = Convert.ToString(dr["CompanyGroupMail"] ?? ""),
                                InstallationsContractMail = Convert.ToString(dr["InstallationsContractMail"] ?? ""),
                                MaintenanceContractMail = Convert.ToString(dr["MaintenanceContractMail"] ?? ""),
                                InventoryContractMail = Convert.ToString(dr["InventoryContractMail"] ?? ""),
                                PaymentContractMail = Convert.ToString(dr["PaymentContractMail"] ?? ""),
                                EtcContractMail = Convert.ToString(dr["EtcContractMail"] ?? ""),
                                LocationAddress = Convert.ToString(dr["LocationAddress"] ?? ""),
                                PostAddress = Convert.ToString(dr["PostAddress"] ?? ""),
                                TaxAddress = Convert.ToString(dr["TaxAddress"] ?? ""),
                                WtAddress = Convert.ToString(dr["WtAddress"] ?? ""),
                                HouseNo = Convert.ToString(dr["HouseNo"] ?? ""),
                                AreaCode = Convert.ToString(dr["AreaCode"] ?? ""),
                                BankCode = Convert.ToString(dr["BankCode"] ?? ""),
                                BankName = Convert.ToString(dr["BankName"] ?? ""),
                                BankAccountNo = Convert.ToString(dr["BankAccountNo"] ?? ""),
                                BankAccountName = Convert.ToString(dr["BankAccountName"] ?? ""),
                                BankAttachFile = Convert.ToString(dr["BankAttachFile"] ?? ""),
                                Status = Convert.ToString(dr["Status"] ?? ""),
                                CreateDate = Convert.ToDateTime(dr["CreateDate"] ?? ""),
                                CreateBy = Convert.ToString(dr["CreateBy"] ?? ""),
                                UpdateBy = Convert.ToString(dr["UpdateBy"] ?? ""),
                                UpdateDate = Convert.ToDateTime(dr["UpdateDate"]  ?? ""),
                                BankBranchCode = Convert.ToString(dr["BankBranchCode"] ?? ""),
                                BankBranchName = Convert.ToString(dr["BankBranchName"] ?? ""),
                                PenaltyContractPhone = Convert.ToString(dr["PenaltyContractPhone"] ?? ""),
                                PenaltyContractMail = Convert.ToString(dr["PenaltyContractMail"] ?? ""),
                                ContractPhone = Convert.ToString(dr["ContractPhone"] ?? ""),
                                ContractMail = Convert.ToString(dr["ContractMail"] ?? ""),
                                //  CompanyId = Guid.Parse(dr["CompanyId"].ToString())

                            }).ToList();



            //Sorting  


            recordsTotal = Excelocation.Count();

            //Paging   
            var data = Excelocation.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });



        }
        public async Task<IActionResult> BulkInsert(string table)
        {
            string message = "";
            ResponseModel result = new ResponseModel();
            if (table == "Location")
            {

                var LocationList = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelLocationData");
                if (LocationList != null)
                {
                    var ExcelocationList = new List<SubcontractProfileLocationModel>();
                    ExcelocationList = (from DataRow dr in LocationList.Rows


                                        select new SubcontractProfileLocationModel()
                                        {
                                            LocationId = Guid.NewGuid(),
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
                                            _EffectiveDate = dr["_EffectiveDate"].ToString(),
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
                                            CompanyId = Guid.NewGuid()

                                        }).ToList();
                    SubcontractProfileLocationModel locationModel = new SubcontractProfileLocationModel();
                    foreach (var dd in ExcelocationList)
                    {
                        if (dd._EffectiveDate.ToString() == null || dd._EffectiveDate.ToString() == "")
                        {

                            locationModel.EffectiveDate = null;
                        }
                        else
                        {
                            locationModel.EffectiveDate = Convert.ToDateTime(dd._EffectiveDate);
                        }

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
                        // locationModel.EffectiveDate = dd._EffectiveDate;
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

                        var uriLocation = new Uri(Path.Combine(strpathAPI, "Location", "MigrationInsert"));

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
            else if (table == "Team")
            {
                var TeamList = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelTeamData");
                if (TeamList != null)
                {
                    var ExceTeamList = new List<SubcontractProfileTeamModel>();

                    ExceTeamList = (from DataRow dr in TeamList.Rows

                                    select new SubcontractProfileTeamModel()
                                    {



                                        TeamCode = Convert.ToString(dr["TeamCode"] ?? ""),
                                        TeamName = Convert.ToString(dr["TeamName"] ?? ""),
                                        TeamNameTh = Convert.ToString(dr["TeamNameTh"] ?? ""),
                                        TeamNameEn = Convert.ToString(dr["TeamNameEn"] ?? ""),
                                        ShipTo = Convert.ToString(dr["ShipTo"] ?? ""),

                                        StageLocal = Convert.ToString(dr["StageLocal"] ?? ""),
                                        LocationCode = Convert.ToString(dr["LocationCode"] ?? ""),
                                        VendorCode = Convert.ToString(dr["VendorCode"] ?? ""),
                                        JobType = Convert.ToString(dr["JobType"] ?? ""),

                                        SubcontractType = Convert.ToString(dr["SubcontractType"] ?? ""),
                                        SubcontractSubType = Convert.ToString(dr["SubcontractSubType"] ?? ""),
                                        WarrantyMa = Convert.ToString(dr["WarrantyMa"] ?? ""),
                                        WarrantyInstall = Convert.ToString(dr["WarrantyInstall"] ?? ""),

                                        ServiceSkill = Convert.ToString(dr["ServiceSkill"] ?? ""),
                                        InstallationsContractPhone = Convert.ToString(dr["InstallationsContractPhone"] ?? ""),
                                        MaintenanceContractPhone = Convert.ToString(dr["MaintenanceContractPhone"] ?? ""),
                                        EtcContractPhone = Convert.ToString(dr["EtcContractPhone"] ?? ""),

                                        InstallationsContractEmail = Convert.ToString(dr["InstallationsContractEmail"] ?? ""),
                                        MaintenanceContractEmail = Convert.ToString(dr["MaintenanceContractEmail"] ?? ""),
                                        EtcContractEmail = Convert.ToString(dr["EtcContractEmail"] ?? ""),
                                        Status = Convert.ToString(dr["Status"] ?? ""),


                                        //  CreateDate = Convert.ToDateTime(dr["CreateDate"] ?? ""),
                                        //  CreateBy = Convert.ToString(dr["CreateBy"] ?? ""),
                                        //  UpdateBy = Convert.ToString(dr["UpdateBy"] ?? ""),
                                        //   UpdateDate = Convert.ToDateTime(dr["UpdateDate"] ?? ""),



                                    }).ToList();
                    SubcontractProfileTeamModel TeamModel = new SubcontractProfileTeamModel();
                    foreach (var d in ExceTeamList)
                    {
                        TeamModel.TeamCode = d.TeamCode;
                        TeamModel.TeamName = d.TeamName;
                        TeamModel.TeamNameTh = d.TeamNameTh;
                        TeamModel.TeamNameEn = d.TeamNameEn;
                        TeamModel.ShipTo = d.ShipTo;
                        TeamModel.StageLocal = d.StageLocal;
                        TeamModel.LocationCode = d.LocationCode;
                        TeamModel.VendorCode = d.VendorCode;
                        TeamModel.JobType = d.JobType;
                        TeamModel.SubcontractType = d.SubcontractType;
                        TeamModel.SubcontractSubType = d.SubcontractSubType;
                        TeamModel.WarrantyMa = d.WarrantyMa;
                        TeamModel.WarrantyInstall = d.WarrantyInstall;
                        TeamModel.ServiceSkill = d.ServiceSkill;
                        TeamModel.InstallationsContractPhone = d.InstallationsContractPhone;
                        TeamModel.MaintenanceContractPhone = d.MaintenanceContractPhone;
                        TeamModel.EtcContractPhone = d.EtcContractPhone;
                        TeamModel.InstallationsContractEmail = d.InstallationsContractEmail;
                        TeamModel.MaintenanceContractEmail = d.MaintenanceContractEmail;
                        TeamModel.EtcContractEmail = d.EtcContractEmail;
                        TeamModel.Status = d.Status;
                        TeamModel.CreateBy = "GUI MigrationProfile";
                        TeamModel.CompanyId = new Guid();
                        TeamModel.LocationId = new Guid();
                        var LocationResult = new SubcontractProfileLocationModel();

                        var uriLocation = new Uri(Path.Combine(strpathAPI, "Team", "MigrationInsert"));

                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpContent = new StringContent(JsonConvert.SerializeObject(TeamModel), Encoding.UTF8, "application/json");

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
            }
            else if (table == "Engineer")
            {
                var EngineerData = SessionHelper.GetObjectFromJson<DataTable>(HttpContext.Session, "ExcelEngineerData");
                if (EngineerData != null)
                {
                    var ExcelEngineerList = new List<SubcontractProfileEngineerModel>();

                    ExcelEngineerList = (from DataRow dr in EngineerData.Rows

                                         select new SubcontractProfileEngineerModel()
                                         {


                                             StaffCode = Convert.ToString(dr["StaffCode"] ?? ""),
                                             FoaCode = Convert.ToString(dr["FoaCode"] ?? ""),
                                             StaffName = Convert.ToString(dr["StaffName"] ?? ""),
                                             StaffNameTh = Convert.ToString(dr["StaffNameTh"] ?? ""),
                                             StaffNameEn = Convert.ToString(dr["StaffNameEn"] ?? ""),
                                             AscCode = Convert.ToString(dr["AscCode"] ?? ""),
                                             TshirtSize = Convert.ToString(dr["TshirtSize"] ?? ""),
                                             ContractPhone1 = Convert.ToString(dr["ContractPhone1"] ?? ""),
                                             ContractPhone2 = Convert.ToString(dr["ContractPhone2"] ?? ""),
                                             ContractEmail = Convert.ToString(dr["ContractEmail"] ?? ""),
                                             WorkExperience = Convert.ToString(dr["WorkExperience"] ?? ""),
                                             WorkExperienceAttachFile = Convert.ToString(dr["WorkExperienceAttachFile"] ?? ""),
                                             WorkType = Convert.ToString(dr["WorkType"] ?? ""),
                                             CourseSkill = Convert.ToString(dr["CourseSkill"] ?? ""),
                                             SkillLevel = Convert.ToString(dr["SkillLevel"] ?? ""),
                                             VehicleType = Convert.ToString(dr["VehicleType"] ?? ""),
                                             VehicleBrand = Convert.ToString(dr["VehicleBrand"] ?? ""),
                                             VehicleSerise = Convert.ToString(dr["VehicleSerise"] ?? ""),
                                             VehicleColor = Convert.ToString(dr["VehicleColor"] ?? ""),
                                             VehicleYear = Convert.ToString(dr["VehicleYear"] ?? ""),
                                             VehicleLicensePlate = Convert.ToString(dr["VehicleLicensePlate"] ?? ""),
                                             VehicleAttachFile = Convert.ToString(dr["VehicleAttachFile"] ?? ""),
                                             ToolOtrd = Convert.ToString(dr["ToolOtrd"] ?? ""),
                                             ToolSplicing = Convert.ToString(dr["ToolSplicing"] ?? ""),
                                             Position = Convert.ToString(dr["Position"] ?? ""),
                                             LocationCode = Convert.ToString(dr["LocationCode"] ?? ""),
                                             StaffId = Convert.ToString(dr["StaffId"] ?? ""),
                                             TeamCode = Convert.ToString(dr["TeamCode"] ?? ""),
                                             CitizenId = Convert.ToString(dr["CitizenId"] ?? ""),
                                             BankCode = Convert.ToString(dr["BankCode"] ?? ""),
                                             BankName = Convert.ToString(dr["BankName"] ?? ""),
                                             AccountNo = Convert.ToString(dr["AccountNo"] ?? ""),
                                             AccountName = Convert.ToString(dr["AccountName"] ?? ""),
                                             PersonalAttachFile = Convert.ToString(dr["PersonalAttachFile"] ?? ""),
                                             StaffStatus = Convert.ToString(dr["StaffStatus"] ?? ""),




                                         }).ToList();






                    SubcontractProfileEngineerModel EngineerModel = new SubcontractProfileEngineerModel();
                    foreach (var d in ExcelEngineerList)
                    {
                        EngineerModel.StaffCode = d.StaffCode;

                        EngineerModel.FoaCode = d.FoaCode;
                        EngineerModel.StaffName = d.StaffName;
                        EngineerModel.StaffNameTh = d.StaffNameTh;
                        EngineerModel.StaffNameEn = d.StaffNameEn;
                        EngineerModel.AscCode = d.AscCode;
                        EngineerModel.TshirtSize = d.TshirtSize;
                        EngineerModel.ContractPhone1 = d.ContractPhone1;
                        EngineerModel.ContractPhone2 = d.ContractPhone2;
                        EngineerModel.ContractEmail = d.ContractEmail;
                        EngineerModel.WorkExperience = d.WorkExperience;
                        EngineerModel.WorkExperienceAttachFile = d.WorkExperienceAttachFile;
                        EngineerModel.WorkType = d.WorkType;
                        EngineerModel.CourseSkill = d.CourseSkill;
                        EngineerModel.SkillLevel = d.SkillLevel;
                        EngineerModel.VehicleType = d.VehicleType;
                        EngineerModel.VehicleBrand = d.VehicleBrand;
                        EngineerModel.VehicleSerise = d.VehicleSerise;
                        EngineerModel.VehicleColor = d.VehicleColor;
                        EngineerModel.VehicleYear = d.VehicleYear;
                        EngineerModel.VehicleLicensePlate = d.VehicleLicensePlate;
                        EngineerModel.VehicleAttachFile = d.VehicleAttachFile;
                        EngineerModel.ToolOtrd = d.ToolOtrd;
                        EngineerModel.ToolSplicing = d.ToolSplicing;
                        EngineerModel.Position = d.Position;
                        EngineerModel.LocationCode = d.LocationCode;
                        EngineerModel.StaffId = d.StaffId;
                        EngineerModel.TeamCode = d.TeamCode;
                        EngineerModel.CitizenId = d.CitizenId;
                        EngineerModel.BankCode = d.BankCode;
                        EngineerModel.BankName = d.BankName;
                        EngineerModel.AccountNo = d.AccountNo;
                        EngineerModel.AccountName = d.AccountName;
                        EngineerModel.PersonalAttachFile = d.PersonalAttachFile;
                        EngineerModel.StaffStatus = d.StaffStatus;







                        var LocationResult = new SubcontractProfileEngineerModel();

                        var uriLocation = new Uri(Path.Combine(strpathAPI, "Engineer", "MigrationInsert"));

                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpContent = new StringContent(JsonConvert.SerializeObject(EngineerModel), Encoding.UTF8, "application/json");

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

            }
            else if (table == "Personal") { }
            else if (table == "Company") { }
            else
            {


                result.Status = true;
                //  result.Message = "Please select MasterTable Before Upload.";
                result.Message = _localizer["MessageNullMasterUpload"];
                result.StatusError = "0";
            }


            return Json(result);


        }

    }
}