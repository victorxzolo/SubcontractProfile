using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SubcontractProfile.Web.Extension;
using SubcontractProfile.Web.Model;

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace SubcontractProfile.Web.Controllers
{
    public class CompanyProfileController : Controller
    {
        private readonly string strpathAPI;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Lang = "";
        private readonly string strpathUpload;
        private SubcontractProfileUserModel datauser = new SubcontractProfileUserModel();
        private string strpathASCProfile;
        private const int MegaBytes = 1024*1024;
        private const int TMegaBytes = 3*1024*1024;

        private string PathNas = "";
        // private readonly IHtmlLocalizer<CompanyProfileController> _localizer;

        private readonly IStringLocalizer<CompanyProfileController> _localizer;

        public CompanyProfileController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor//, IHtmlLocalizer<CompanyProfileController> localizer
           ,IStringLocalizer<CompanyProfileController> localizer
            )
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _localizer = localizer;
            // _localizer = localizer;

            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();
            strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();

            //Session["UserID"] = "";


            //******set language******
            #region GET SESSION

            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            datauser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");

            #endregion

            //strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();


            //NSA
            //PathNas = _configuration.GetValue<string>("PathUploadfile:NAS").ToString();
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
        public ActionResult Index()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if(string.IsNullOrEmpty(userProfile.Username))
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            getsession();
            if (Lang == "TH")
            {
                ViewData["Controller"] = "ข้อมูลโปรไฟล์";
                ViewData["View"] = "ข้อมูลบริษัท";
            }
            else
            {
                ViewData["Controller"] = "Profile";
                ViewData["View"] = "Company Profile";
            }
            return View();
        }

            


        public ActionResult Profile()
        {
            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // GET: CompanyProfileController/Search/5
        public ActionResult Search(string companyNameTh, string companyNameEn, string companyAilas
            , string taxId,string SubcontractProfileType)
        {

            var companyResult = new List<SubcontractProfileCompanyModel>();

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

            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            Guid strCompanyId = userProfile.companyid;


            if (companyNameTh == null)
            {
                companyNameTh = "null";
            }

            if (companyNameEn == null)
            {
                companyNameEn = "null";
            }

            if (companyAilas == null)
            {
                companyAilas = "null";
            }

            if (taxId == null)
            {
                taxId = "null";
            }

            if(SubcontractProfileType==null)
            {
                SubcontractProfileType = "null";
            }

            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Company/SearchCompany", strCompanyId
                , companyNameTh, companyNameEn, companyAilas, taxId, SubcontractProfileType);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                companyResult = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(result);

            }


            //total number of rows count   
            recordsTotal = companyResult.Count();

            //Paging   
            var data = companyResult.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        public JsonResult GetCompanyDataById()
        {
            var companyResult = new SubcontractProfileCompanyModel();

            var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", userProfile.companyid);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                companyResult = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);
                companyResult.RegisterDateStr = companyResult.RegisterDate.Value.ToString("dd/MM/yyyy HH:mm");


            }

            return Json(companyResult);
        }


        [HttpPost]
        public async Task<JsonResult> GetDataById(string companyId)
        {
            var companyResult = new SubcontractProfileCompanyModel();

            // Getting all company data  
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", companyId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                companyResult = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);

                List<FileUploadModal> L_File = new List<FileUploadModal>();


                if(companyResult.CompanyCertifiedFile !=null)
                {
                    Guid file_id_Company = Guid.NewGuid();
                    L_File.Add(new FileUploadModal
                    {
                        file_id = file_id_Company,
                        //Fileupload = fileBytes,
                        typefile = "CompanyCertifiedFile",
                        //ContentDisposition = source.ContentDisposition,
                        //ContentType = source.ContentType,
                        Filename = companyResult.CompanyCertifiedFile,
                        CompanyId = companyId

                    });
                    companyResult.file_id_CompanyCertifiedFile = file_id_Company;
                }
                if(companyResult.CommercialRegistrationFile !=null)
                {
                    Guid file_id_Commercial = Guid.NewGuid();
                    L_File.Add(new FileUploadModal
                    {
                        file_id = file_id_Commercial,
                        //Fileupload = fileBytes,
                        typefile = "CommercialRegistrationFile",
                        //ContentDisposition = source.ContentDisposition,
                        //ContentType = source.ContentType,
                        Filename = companyResult.CommercialRegistrationFile,
                        CompanyId = companyId

                    });
                    companyResult.file_id_CommercialRegistrationFile = file_id_Commercial;
                }
                if(companyResult.VatRegistrationCertificateFile !=null)
                {
                    Guid file_id_Vat = Guid.NewGuid();
                    L_File.Add(new FileUploadModal
                    {
                        file_id = file_id_Vat,
                        //Fileupload = fileBytes,
                        typefile = "VatRegistrationCertificateFile",
                        //ContentDisposition = source.ContentDisposition,
                        //ContentType = source.ContentType,
                        Filename = companyResult.VatRegistrationCertificateFile,
                        CompanyId = companyId

                    });
                    companyResult.file_id_VatRegistrationCertificateFile = file_id_Vat;
                }
                if(companyResult.AttachFile !=null)
                {
                    Guid file_id_bookbank = Guid.NewGuid();
                    L_File.Add(new FileUploadModal
                    {
                        file_id = file_id_bookbank,
                        //Fileupload = fileBytes,
                        typefile = "bookbankfile",
                        //ContentDisposition = source.ContentDisposition,
                        //ContentType = source.ContentType,
                        Filename = companyResult.AttachFile,
                        CompanyId = companyId

                    });
                    companyResult.file_id_bookbank = file_id_bookbank;
                }
                //if(L_File.Count != 0)
                //{
                //    if (GetFile(companyId, ref L_File))
                //    {
                //        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompany", L_File);
                //    }

                //}
            

            }

            return Json(companyResult);
        }

        // GET: CompanyProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OnSave(SubcontractProfileCompanyModel model)
        {
            bool resultGetFile = true;
            ResponseModel res = new ResponseModel();
            try
            {
                if (datauser == null)
                {
                    getsession();
                }

                var companyId = model.CompanyId;

                model.UpdateDate = DateTime.Now;
                model.UpdateBy = datauser.Username;

                //var dataUploadfile = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftCompany");
                //if (dataUploadfile != null && dataUploadfile.Count != 0)
                //{
                    #region Copy File to server

                    //if (!DeleteFile(model.CompanyId.ToString()))
                    //{
                        //resultGetFile = false;
                    //}
                    //if(resultGetFile)
                    //{
                        //foreach (var e in dataUploadfile)
                        //{
                if (model.FileBookBank != null && model.FileBookBank.Length > 0)
                {
                    resultGetFile = await UploadfileOnSave(model.FileBookBank, model.CompanyId.ToString());
                    model.AttachFile = model.FileBookBank.FileName;
                }

                if (model.FileCompanyCertified != null && model.FileCompanyCertified.Length > 0)
                {
                    resultGetFile = await UploadfileOnSave(model.FileCompanyCertified, model.CompanyId.ToString());
                    model.CompanyCertifiedFile = model.FileCompanyCertified.FileName;
                }


                if (model.FileCommercialRegistration != null && model.FileCommercialRegistration.Length > 0)
                {
                    resultGetFile = await UploadfileOnSave(model.FileCommercialRegistration, model.CompanyId.ToString());
                    model.CommercialRegistrationFile = model.FileCommercialRegistration.FileName;
                }


                if (model.FileVatRegistrationCertificate != null && model.FileVatRegistrationCertificate.Length > 0)
                {
                    resultGetFile = await UploadfileOnSave(model.FileVatRegistrationCertificate, model.CompanyId.ToString());
                    model.VatRegistrationCertificateFile = model.FileVatRegistrationCertificate.FileName;
                }




                //string filename = ContentDispositionHeaderValue.Parse(e.ContentDisposition).FileName.Trim('"');
                //            filename = EnsureCorrectFilename(filename);

                //            switch (e.typefile)
                //            {
                //                case "CompanyCertifiedFile":
                //                    model.CompanyCertifiedFile = filename;
                //                    break;
                //                case "CommercialRegistrationFile":
                //                    model.CommercialRegistrationFile = filename;
                //                    break;
                //                case "VatRegistrationCertificateFile":
                //                    model.VatRegistrationCertificateFile = filename;
                //                    break;
                //                case "bookbankfile":
                //                    model.AttachFile = filename;
                //                    break;
                //            }


                        //}
                    //}
                    
                    #endregion
                    if (resultGetFile)
                    {
                        SessionHelper.RemoveSession(HttpContext.Session, "userUploadfileDaftCompany");

                        #region Insert Company

                        model.Status = "W";// Waiting

                        var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "Update"));

                        HttpClient clientCompany = new HttpClient();
                        clientCompany.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                        HttpResponseMessage responseCompany = clientCompany.PutAsync(uriCompany, httpContentCompany).Result;

                        if (responseCompany.IsSuccessStatusCode)
                        {
                            #region Insert Address
                            var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany");

                            if (dataaddr != null && dataaddr.Count != 0)
                            {
                                var uriAddrDelete = new Uri(Path.Combine(strpathAPI, "Address", "DeleteByCompanyID"));
                                HttpClient clientAddrDelete= new HttpClient();
                                clientAddrDelete.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var httpContentAddrDelete = new StringContent(JsonConvert.SerializeObject(companyId.ToString()), Encoding.UTF8, "application/json");

                                var responseDelete = await clientAddrDelete.PostAsync(uriAddrDelete, httpContentAddrDelete);
                                if(responseDelete.IsSuccessStatusCode)
                                {
                                    var r= responseDelete.Content.ReadAsStringAsync().Result;
                                  bool  statusDelete = JsonConvert.DeserializeObject<bool>(r);
                                    if(statusDelete)
                                    {
                                        SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaftCompany");
                                        bool statusAddAddr = true;

                                        foreach (var d in dataaddr)
                                        {
                                            SubcontractProfileAddressModel addr = new SubcontractProfileAddressModel();
                                            addr.AddressId = d.AddressId;
                                            addr.AddressTypeId = d.AddressTypeId;
                                            addr.Building = d.Building;
                                            addr.City = d.City;
                                            addr.Country = d.Country;
                                            addr.DistrictId = d.DistrictId;
                                            addr.Floor = d.Floor;
                                            addr.HouseNo = d.HouseNo;
                                            addr.Moo = d.Moo;
                                            addr.ProvinceId = d.ProvinceId;
                                            addr.CompanyId = companyId.ToString();
                                            addr.CreateBy = datauser.UserId.ToString();
                                            addr.CreateDate = DateTime.Now;
                                            addr.RegionId = d.RegionId;
                                            addr.Road = d.Road;
                                            addr.Soi = d.Soi;
                                            addr.RoomNo = d.RoomNo;
                                            addr.SubDistrictId = d.SubDistrictId;
                                            addr.VillageName = d.VillageName;
                                            addr.ZipCode = d.ZipCode;

                                            var uriAddress = new Uri(Path.Combine(strpathAPI, "Address", "Insert"));
                                            HttpClient clientAddress = new HttpClient();
                                            clientAddress.DefaultRequestHeaders.Accept.Add(
                                            new MediaTypeWithQualityHeaderValue("application/json"));

                                            string rr = JsonConvert.SerializeObject(addr);

                                            var httpContent = new StringContent(JsonConvert.SerializeObject(addr), Encoding.UTF8, "application/json");
                                            HttpResponseMessage responseAddress = clientAddress.PostAsync(uriAddress, httpContent).Result;
                                            if (responseAddress.IsSuccessStatusCode)
                                            {
                                                statusAddAddr = true;
                                            }
                                            else
                                            {
                                                statusAddAddr = false; break;
                                            }
                                        }

                                        if(statusAddAddr)
                                        {
                                            res.Status = true;
                                            res.Message = "Update Success";
                                            res.StatusError = "0";
                                        }
                                        else
                                        {
                                            res.Status = false;
                                            res.Message = "Address Data is not correct, Please Check Data or Contact System Admin";
                                            res.StatusError = "-1";
                                        }
                                    }
                                    else
                                    {
                                        res.Status = false;
                                        res.Message = "Address Data is not correct, Please Check Data or Contact System Admin";
                                        res.StatusError = "-1";
                                    }
                                }
                                else
                                {
                                    res.Status = false;
                                    res.Message = "Address Data is not correct, Please Check Data or Contact System Admin";
                                    res.StatusError = "-1";
                                }

                                
                            }
                            else
                            {
                                res.Status = false;
                                res.Message = "Address Data is not correct, Please Check Data or Contact System Admin";
                                res.StatusError = "-1";
                            }
                            #endregion
                        }
                        else
                        {
                            res.Status = false;
                            res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                            res.StatusError = "-1";
                        }
                        #endregion
                    }
                    else
                    {
                        res.Status = false;
                        res.Message = "Data is not correct, Please Check Data or Contact System Admin";
                        res.StatusError = "-1";
                    }
                //}
                //else
                //{
                //    res.Status = false;
                //    res.Message = "Seesion file is not correct, Please Upload file.";
                //    res.StatusError = "-1";
                //}

                //return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
                res.StatusError = "-1";
            }
            return Json(new { Response = res });
        }

        [HttpPost]
        public IActionResult GetAddress(string company)
        {
            var addressResult = new List<SubcontractProfileAddressModel>();
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault()==null? "0": Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault()==null? "10": Request.Form["length"].FirstOrDefault();
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
                SubcontractProfileAddressModel input = new SubcontractProfileAddressModel();
                input.AddressId = Guid.NewGuid();
                input.CompanyId = company;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "Address/GetByCompanyId");

                //string jj = JsonConvert.SerializeObject(input);

                var httpContentCompany = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uriString, httpContentCompany).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    addressResult = JsonConvert.DeserializeObject<List<SubcontractProfileAddressModel>>(v);
                    if(addressResult.Count()!=0)
                    {
                        string uriStringSubdistrict = string.Format("{0}", strpathAPI + "SubDistrict/GetAll");
                        string uriStringDistirct = string.Format("{0}", strpathAPI + "District/GetAll");
                        string uriStringProvince = string.Format("{0}", strpathAPI + "Province/GetAll");
                        string uriStringAddresstype = string.Format("{0}", strpathAPI + "AddressType/GetALL");

                        response = client.GetAsync(uriStringSubdistrict).Result;
                        var s = response.Content.ReadAsStringAsync().Result;
                        var L_subdistirct = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(s);

                        response = client.GetAsync(uriStringDistirct).Result;
                        var d = response.Content.ReadAsStringAsync().Result;
                        var L_distirct = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(d);

                        response = client.GetAsync(uriStringProvince).Result;
                        var p = response.Content.ReadAsStringAsync().Result;
                        var L_province = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(p);

                        response = client.GetAsync(uriStringAddresstype).Result;
                        var a = response.Content.ReadAsStringAsync().Result;
                        var L_addresstype = JsonConvert.DeserializeObject<List<SubcontractProfileAddressTypeModel>>(a);

                        foreach (var f in addressResult)
                        {
                            if (Lang == "TH")
                            {
                                if(f.AddressTypeId != null)
                                {
                                    f.address_type_name = L_addresstype.Where(x => x.AddressTypeId == f.AddressTypeId).Select(x => x.AddressTypeNameTh).FirstOrDefault().ToString();

                                }
                                if(f.SubDistrictId !=null)
                                {
                                    f.sub_district_name = L_subdistirct.Where(x => x.SubDistrictId == f.SubDistrictId).Select(x => x.SubDistrictNameTh).FirstOrDefault().ToString();

                                }
                                if(f.DistrictId !=null)
                                {
                                    f.district_name = L_distirct.Where(e => e.DistrictId == f.DistrictId).Select(x => x.DistrictNameTh).FirstOrDefault().ToString();

                                }
                                if(f.ProvinceId !=null)
                                {
                                    f.province_name = L_province.Where(x => x.ProvinceId == f.ProvinceId).Select(x => x.ProvinceNameTh).FirstOrDefault().ToString();

                                }
                            }
                            else
                            {
                                if(f.AddressTypeId != null)
                                {
                                    f.address_type_name = L_addresstype.Where(x => x.AddressTypeId == f.AddressTypeId && f.AddressTypeId != null).Select(x => x.AddressTypeNameEn).FirstOrDefault().ToString();

                                }
                                if(f.SubDistrictId != null)
                                {
                                    f.sub_district_name = L_subdistirct.Where(x => x.SubDistrictId == f.SubDistrictId).Select(x => x.SubDistrictNameEn).FirstOrDefault().ToString();

                                }
                                if(f.DistrictId != null)
                                {
                                    f.district_name = L_distirct.Where(e => e.DistrictId == f.DistrictId).Select(x => x.DistrictNameEn).FirstOrDefault().ToString();

                                }
                                if(f.ProvinceId != null)
                                {
                                    f.province_name = L_province.Where(x => x.ProvinceId == f.ProvinceId).Select(x => x.ProvinceNameEn).FirstOrDefault().ToString();

                                }
                            }

                            string straddr = "";
                            straddr = string.Concat(f.HouseNo != null && f.HouseNo != "" ? f.HouseNo : "", " ",
                                                  f.Building != null && f.Building != "" ? "อาคาร " + f.Building : "", " ",
                                                  f.Floor != null && f.Floor != "" ? "ชั้นที่ " + f.Floor : "", " ",
                                                  f.RoomNo != null && f.RoomNo != "" ? "ห้องที่ " + f.RoomNo : "", " ",
                                                  f.VillageName != null && f.VillageName != "" ? "หมู่บ้าน " + f.VillageName : "", " ",
                                                  f.Moo != null ? "หมู่ที่ " + f.Moo : "", " ",
                                                  f.Soi != null && f.Soi != "" ? "ซอย " + f.Soi : "", " ",
                                                  f.Road != null && f.Road != "" ? "ถนน " + f.Road : "", " ",
                                                  f.SubDistrictId != 0 ? "ตำบล/แขวง " + f.sub_district_name : "", " ",
                                                  f.DistrictId != 0 ? "อำเภอ/เขต " + f.district_name : "", " ",
                                                  f.ProvinceId != 0 ? "จังหวัด " + f.province_name : "", " ",
                                                  f.ZipCode != "" ? f.ZipCode : "");
                            f.outFullAddress = straddr;

                        }
                    }

                   

                    
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompany", addressResult);
                recordsTotal = addressResult.Count();

                //Paging   
                var data = addressResult.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = new List<SubcontractProfileAddressModel>() });
                throw;
            }
           
        }

        [HttpPost]
        public IActionResult SaveDaftAddress(List<SubcontractProfileAddressModel> daftdata)
        {
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany");
                if (data != null && data.Count != 0)
                {
                    foreach (var e in daftdata)
                    {
                        if (e.AddressId == null)
                        {
                            //if (e.location_code != "")//มาจาก dealer
                            //{
                            //    data.RemoveAll(x => x.location_code == e.location_code && x.AddressTypeId == e.AddressTypeId);
                            //}
                            data.RemoveAll(x => x.AddressTypeId == e.AddressTypeId);
                            List<SubcontractProfileAddressModel> newaddr = new List<SubcontractProfileAddressModel>();
                            newaddr.Add(new SubcontractProfileAddressModel
                            {
                                AddressTypeId = e.AddressTypeId,
                                address_type_name = e.address_type_name,
                                Country = e.Country,
                                ZipCode = e.ZipCode,
                                HouseNo = e.HouseNo,
                                Moo = e.Moo,
                                VillageName = e.VillageName,
                                Building = e.Building,
                                Floor = e.Floor,
                                RoomNo = e.RoomNo,
                                Soi = e.Soi,
                                Road = e.Road,
                                SubDistrictId = e.SubDistrictId,
                                sub_district_name = e.sub_district_name,
                                DistrictId = e.DistrictId,
                                district_name = e.district_name,
                                ProvinceId = e.ProvinceId,
                                province_name = e.province_name,
                                RegionId = e.RegionId,
                                outFullAddress = e.outFullAddress,
                                location_code = e.location_code,
                                CompanyId = e.CompanyId
                            });
                            foreach (var r in SaveAddressSession(newaddr))
                            {
                                data.Add(r);
                            }

                        }
                        else
                        {

                            data.RemoveAll(x => x.AddressTypeId== e.AddressTypeId);
                            data.Add(e);
                        }

                    }

                }
                else
                {
                    data = SaveAddressSession(daftdata);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompany", data);
                return Json(new { response = data, status = true });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
            }

        }

        [HttpPost]
        public IActionResult DeleteDaftAddress(string AddressId)
        {
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany").ToList();

                data.RemoveAll(x => x.AddressId.ToString().Contains(AddressId));

                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompany", data);
                var data_delete = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany");
                if (data_delete == null)
                {
                    SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaftCompany");
                }
                return Json(new { response = data_delete, status = true });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
                throw;
            }

        }

        [HttpPost]
        public IActionResult GetAddressById(string addressID)
        {
            try
            {
                if (addressID != null && addressID != "")
                {
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany");
                    var result = data.Where(x => x.AddressId.ToString() == addressID).FirstOrDefault();
                    return Json(new { response = result, status = true });
                }
                else
                {
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompany");
                    return Json(new { response = data, status = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = false });
            }
            //var result = new SubcontractProfileAddressModel();
            //try
            //{
            //    HttpClient client = new HttpClient();
            //    client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //    string uriString = string.Format("{0}/{1}", strpathAPI + "Address/GetByAddressId", addressID);

            //    //string jj = JsonConvert.SerializeObject(input);

            //    HttpResponseMessage response = client.GetAsync(uriString).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var v = response.Content.ReadAsStringAsync().Result;
            //        result = JsonConvert.DeserializeObject<SubcontractProfileAddressModel>(v);
            //    }
            //    return Json(new { response = result, status = true, message= response.StatusCode });
            //}
            //catch (Exception e)
            //{
            //    return Json(new { message = e.Message, status = false });
            //    throw;
            //}

        }

        [HttpPost]
        public IActionResult GetLocationSession(string location_id)
        {
            List<LocationListModel> resultLocation = new List<LocationListModel>();
            ResponseModel res = new ResponseModel();
            try
            {
                var data = SessionHelper.GetObjectFromJson<List<LocationListModel>>(HttpContext.Session, "LocationDealerCompany");
                if (data != null)
                {
                    resultLocation = data.Where(x => x.location_id == location_id).ToList();
                    res.Status = true;
                }

            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
                throw;
            }
            return Json(new
            {
                LocationListModel = resultLocation,
                Response = res

            });
        }



        [HttpPost]
        public IActionResult SearchLocation(SearchSubcontractProfileLocationViewModel model)
        {
            ASCProfileModel result = new ASCProfileModel();
            List<LocationListModel> locate = new List<LocationListModel>();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                //string uriString = string.Format("http://10.138.34.60:8080/phxPartner/v1/partner/ChannelASCProfile.json?filter=(&(inSource={0})(inEvent={1})" +
                //                                 "(inASCCode={2})(inASCMobileNo={3})(inIdNo={4})(inLocationCode={5})(inSAPCode={6})(inUserID={7}))"
                //                                , "FBB", "evLocationInfo",model.asc_code,model.asc_mobile_no,model.id_Number,model.location_code,model.sap_code,
                //                                model.user_id);
                string uriString = string.Format(strpathASCProfile, "FBB", "evLocationInfo", model.asc_code, model.asc_mobile_no, model.id_Number
                                            , model.location_code, model.sap_code, model.user_id);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<ASCProfileModel>(v);
                    if (result.outStatus == "0000")
                    {
                        foreach (var e in result.resultData)
                        {
                            foreach (var r in e.LocationList)
                            {
                                Guid l_id = Guid.NewGuid();
                                locate.Add(new LocationListModel
                                {
                                    outTitle = r.outTitle,
                                    outCompanyName = r.outCompanyName,
                                    outPartnerName = r.outPartnerName,
                                    outCompanyShortName = r.outCompanyShortName,
                                    outTaxId = r.outTaxId,
                                    outWTName = r.outWTName,
                                    outDistChn = r.outDistChn,
                                    outChnSales = r.outChnSales,
                                    outType = r.outType,
                                    outSubType = r.outSubType,
                                    outBusinessType = r.outBusinessType,
                                    outCharacteristic = r.outCharacteristic,
                                    outLocationCode = r.outLocationCode,
                                    outLocationName = r.outLocationName,
                                    outShopArea = r.outShopArea,
                                    outShopType = r.outShopType,
                                    outOperatorClass = r.outOperatorClass,
                                    outLocationPhoneNo = r.outLocationPhoneNo,
                                    outLocationFax = r.outLocationFax,
                                    outContractName = r.outContractName,
                                    outContractPhoneNo = r.outContractPhoneNo,
                                    outLocationStatus = r.outLocationStatus,
                                    outRetailShop = r.outRetailShop,
                                    outBusinessRegistration = r.outBusinessRegistration,
                                    outVatType = r.outVatType,
                                    outEffectiveDt = r.outEffectiveDt,
                                    outIdType = r.outIdType,
                                    outHQFlag = r.outHQFlag,
                                    outChnName = r.outChnName,
                                    outSAPVendorCode = r.outSAPVendorCode,
                                    outMobileForService = r.outMobileForService,
                                    outLocationRegion = r.outLocationRegion,
                                    outLocationSubRegion = r.outLocationSubRegion,
                                    outPaymentChannelCode = r.outPaymentChannelCode,
                                    outPaymentChannelName = r.outPaymentChannelName,
                                    outLocationRemark = r.outLocationRemark,
                                    outBusinessName = r.outBusinessName,
                                    outPubid = r.outPubid,
                                    outLocationAbbr = r.outLocationAbbr,
                                    addressLocationList = r.addressLocationList,
                                    SAPCustomerList = r.SAPCustomerList,
                                    ASCList = r.ASCList,
                                    location_id = l_id.ToString()
                                });



                            }


                        }
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "LocationDealerCompany", locate);
                    }
                    return Json(new
                    {
                        draw = model.draw,
                        recordsTotal = locate.Count(),
                        recordsFiltered = locate.Count(),
                        data = locate
                    });

                }
                else
                {
                    return Json(new
                    {
                        draw = model.draw,
                        recordsTotal = 0,
                        recordsFiltered = 0,
                        data = locate
                    });
                }


            }
            catch (Exception e)
            {

                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = locate
                });
                throw;
            }
        }


        [HttpPost]
        public IActionResult GetRevenue(SearchVATModel model)
        {
            List<VATModal> ListResult = new List<VATModal>();
            VATModal Vmodel = new VATModal();


            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            try
            {

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower() == "asc";
                }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                string uriString = string.Format("{0}/{1}", strpathAPI + "VATService/Get", model.tIN);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    var outputresponse = JsonConvert.DeserializeObject<VATResultModel>(v);
                    if (outputresponse.StatusCode == "200")
                    {
                        if (outputresponse.Value != null)
                        {
                            string straddr = "";
                            straddr = string.Concat(outputresponse.Value.vHouseNumber != null && outputresponse.Value.vHouseNumber != "-" ? outputresponse.Value.vHouseNumber : "", " ",
                                                      outputresponse.Value.vBuildingName != null && outputresponse.Value.vBuildingName != "-" ? "อาคาร " + outputresponse.Value.vBuildingName : "", " ",
                                                      outputresponse.Value.vFloorNumber != null && outputresponse.Value.vFloorNumber != "-" ? "ชั้นที่ " + outputresponse.Value.vFloorNumber : "", " ",
                                                      outputresponse.Value.vRoomNumber != null && outputresponse.Value.vRoomNumber != "-" ? "ห้องที่ " + outputresponse.Value.vRoomNumber : "", " ",
                                                      outputresponse.Value.vVillageName != null && outputresponse.Value.vVillageName != "-" ? "หมู่บ้าน " + outputresponse.Value.vVillageName : "", " ",
                                                      outputresponse.Value.vMooNumber != null && outputresponse.Value.vMooNumber != "-" ? "หมู่ที่ " + outputresponse.Value.vMooNumber : "", " ",
                                                      outputresponse.Value.vSoiName != null && outputresponse.Value.vSoiName != "-" ? "ซอย " + outputresponse.Value.vSoiName : "", " ",
                                                      outputresponse.Value.vStreetName != null && outputresponse.Value.vStreetName != "-" ? "ถนน " + outputresponse.Value.vStreetName : "", " ",
                                                      outputresponse.Value.vThambol != null && outputresponse.Value.vThambol != "-" ? "ตำบล/แขวง " + outputresponse.Value.vThambol : "", " ",
                                                      outputresponse.Value.vAmphur != null && outputresponse.Value.vAmphur != "-" ? "อำเภอ/เขต " + outputresponse.Value.vAmphur : "", " ",
                                                      outputresponse.Value.vProvince != null && outputresponse.Value.vProvince != "-" ? "จังหวัด " + outputresponse.Value.vProvince : "", " ",
                                                      outputresponse.Value.vPostCode != null && outputresponse.Value.vPostCode != "-" ? outputresponse.Value.vPostCode : "");
                            ListResult.Add(new VATModal
                            {
                                vAmphur = outputresponse.Value.vAmphur,
                                vBranchName = outputresponse.Value.vBranchName,
                                vBranchNumber = outputresponse.Value.vBranchNumber,
                                vBranchTitleName = outputresponse.Value.vBranchTitleName,
                                vBuildingName = outputresponse.Value.vBuildingName,
                                vBusinessFirstDate = outputresponse.Value.vBusinessFirstDate,
                                vFloorNumber = outputresponse.Value.vFloorNumber,
                                vHouseNumber = outputresponse.Value.vHouseNumber,
                                vMooNumber = outputresponse.Value.vMooNumber,
                                vmsgerr = outputresponse.Value.vmsgerr,
                                vName = outputresponse.Value.vName,
                                vNID = outputresponse.Value.vNID,
                                vPostCode = outputresponse.Value.vPostCode,
                                vProvince = outputresponse.Value.vProvince,
                                vRoomNumber = outputresponse.Value.vRoomNumber,
                                vSoiName = outputresponse.Value.vSoiName,
                                vStreetName = outputresponse.Value.vStreetName,
                                vSurname = outputresponse.Value.vSurname,
                                vThambol = outputresponse.Value.vThambol,
                                vtin = outputresponse.Value.vtin,
                                vtitleName = outputresponse.Value.vtitleName,
                                vVillageName = outputresponse.Value.vVillageName,
                                outConcataddr = straddr
                            });


                            if (sortDir) //asc
                            {
                                if (sortBy == "vBranchName")
                                {
                                    ListResult = ListResult.OrderBy(c => c.vBranchName).ToList();
                                }
                                else if (sortBy == "vBranchNumber")
                                {
                                    ListResult = ListResult.OrderBy(c => c.vBranchNumber).ToList();
                                }
                            }
                            else //desc
                            {
                                if (sortBy == "vBranchName")
                                {
                                    ListResult = ListResult.OrderByDescending(c => c.vBranchName).ToList();
                                }
                                else if (sortBy == "vBranchNumber")
                                {
                                    ListResult = ListResult.OrderByDescending(c => c.vBranchNumber).ToList();
                                }
                            }

                            filteredResultsCount = ListResult.Count();
                            totalResultsCount = ListResult.Count();
                        }
                    }

                }

            }
            catch (Exception e)
            {
                throw;
            }

            return Json(new
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = ListResult.Count()!=0? (ListResult[0].vHouseNumber !=null? ListResult: new List<VATModal>()): new List<VATModal>()
            });
        }

        [HttpPost]
        public IActionResult GetDataBankAccountType()
        {
            var output = new List<SubcontractDropdownModel>();
            List<SelectListItem> getAllBankAccList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "Dropdown/GetByDropDownName/bank_account_type");
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
                    dropdown_text = "กรุณาเลือกประเภทบัญชี",
                    dropdown_value = ""

                });

                getAllBankAccList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();
            }
            else
            {
                output.Add(new SubcontractDropdownModel
                {
                    dropdown_text = "Select Acount Type",
                    dropdown_value = ""
                });
                getAllBankAccList = output.Select(a => new SelectListItem
                {
                    Text = a.dropdown_text,
                    Value = a.dropdown_value
                }).OrderBy(c => c.Value).ToList();

            }
            return Json(new { response = getAllBankAccList });
        }




        #region Upload File


        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Uploadfile(IList<IFormFile> files, string fid, string type_file,string Company)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            //FileStream output;
            string strmess = "";

            string[] arr = { "application/pdf", "image/png", "image/jpeg", "image/jpeg", "image/bmp", "image/gif", "image/tif", "image/tiff" };


            try
            {
                foreach (FormFile source in files)
                {
                    if (source.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);

                            if (source.ContentType.ToLower() != "image/jpg" &&
                            source.ContentType.ToLower() != "image/jpeg" &&
                            source.ContentType.ToLower() != "image/pjpeg" &&
                            source.ContentType.ToLower() != "image/gif" &&
                            source.ContentType.ToLower() != "image/png" &&
                            source.ContentType.ToLower() != "image/bmp" &&
                            source.ContentType.ToLower() != "image/tiff" &&
                            source.ContentType.ToLower() != "image/tif" &&
                            source.ContentType.ToLower() != "application/pdf"
                            )
                            {
                                statusupload = false;
                                strmess = "Upload type file miss match.";
                            }
                        else
                        {
                            var fileSize = source.Length;
                            if(source.ContentType.ToLower() == "application/pdf")
                            {
                                if (fileSize > MegaBytes)
                                {
                                    statusupload = false;
                                    strmess = "Upload file is too large.";
                                }
                                else
                                {
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
                                            Filename = filename,
                                            CompanyId = Company
                                        });
                                    }
                                    var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftCompany");

                                    if (data != null)
                                    {

                                        data.RemoveAll(x => x.file_id.ToString() == fid);
                                        data.Add(L_File[0]);
                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompany", data);

                                    }
                                    else
                                    {

                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompany", L_File);
                                    }
                                    strmess = "Upload file success";
                                }
                            }
                            else
                            {
                                if (fileSize > TMegaBytes)
                                {
                                    statusupload = false;
                                    strmess = "Upload file is too large.";
                                }
                                else
                                {
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
                                            Filename = filename,
                                            CompanyId = Company
                                        });
                                    }
                                    var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftCompany");

                                    if (data != null)
                                    {

                                        data.RemoveAll(x => x.file_id.ToString() == fid);
                                        data.Add(L_File[0]);
                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompany", data);

                                    }
                                    else
                                    {

                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompany", L_File);
                                    }
                                    strmess = "Upload file success";
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
                throw;
            }


            return Json(new { status = statusupload, message = strmess, response = (statusupload? L_File[0].file_id.ToString():"") });

            // return Json(new { status = statusupload, message = strmess });
        }

        private bool DeleteFile(string companyid)
        {
            bool result = true;
            var output = new List<SubcontractDropdownModel>();
            try
            {
                #region NAS
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                //string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "nas_subcontract");

                //HttpResponseMessage response = client.GetAsync(uriString).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    var v = response.Content.ReadAsStringAsync().Result;
                //    output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                //}
                //if (output != null & output.Count() > 0)
                //{
                //    using (var impersonator = new Impersonator(output[0].value1, output[0].value2, output[0].dropdown_text, false))
                //    {
                //        if (Directory.GetFiles(output[0].dropdown_text + @"\SubContractProfile\" + companyid).Count() > 0)
                //        {
                //            System.IO.File.Delete(output[0].dropdown_text + @"\SubContractProfile\" + companyid);

                //        }
                //    }
                //}
                #endregion

                string pathdir = Path.Combine(strpathUpload, companyid);
                if (Directory.GetFiles(pathdir, "*", SearchOption.AllDirectories).Length > 0)
                {

                    System.IO.DirectoryInfo di = new DirectoryInfo(pathdir);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                }

            }
            catch (IOException ioExp)
            {
                result = false;
            }
            return result;
        }

        [HttpPost]
        public IActionResult TestNAS()
        {
            string str = "";
            var output = new List<SubcontractDropdownModel>();

            

            try
            {
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                //string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "nas_subcontract");

                //HttpResponseMessage response = client.GetAsync(uriString).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    var v = response.Content.ReadAsStringAsync().Result;
                //    output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                //}
                //if (output != null)
                //{


                string username = "nas_fixedbb";
                string password = "Ais2018fixedbb";
                string destNAS = @"\\10.137.32.9\fbb_idcard_ndev001b";

                //string username = "PF0QMBH6";
                //string password = "1234";
                //string NAS = @"DESKTOP-MMCKBRE";
                //string destNAS = @"D:\NasPath";

                using (var impersonator = new Impersonator(username, password, destNAS, false))
                    // using (var impersonator = new Impersonator(output[0].value1, output[0].value2, output[0].dropdown_text, false))
                    {
                    string strdir = destNAS + @"\SubContractProfile" + @"\f2423a7a-ed2c-4c9b-b766-c37ada227b6d";
                    if (Directory.Exists(strdir))
                    {
                        //string[] filePaths = Directory.GetFiles(Doc, "*.*");
                        //str = filePaths.Count().ToString() + " , username:" + username + " ,password:" + password + " ,domainOrServerName: " + Doc;
                        //int existFile = Directory.GetFiles(destNAS + @"\f2423a7a-ed2c-4c9b-b766-c37ada227b6d\").Count();
                        str = "Found";
                    }
                    else
                    {
                        str = "Doc Not Found :" + strdir;
                    }


                }
                //}

               
            }
            catch (Exception e)
            {
                str = e.Message;
            }
            return Json(str);
        }
        private bool GetFile(string companyid,ref List<FileUploadModal> L_File)
        {
            bool result = true;
            var output = new List<SubcontractDropdownModel>();
            try
            {
                #region NAS
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                //string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "nas_subcontract");

                //HttpResponseMessage response = client.GetAsync(uriString).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    var v = response.Content.ReadAsStringAsync().Result;
                //    output = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                //}
                //if (output != null & output.Count() > 0)
                //{
                //    using (var impersonator = new Impersonator(output[0].value1, output[0].value2, output[0].dropdown_text, false))
                //    {
                //        if (Directory.GetFiles(output[0].dropdown_text + @"\SubContractProfile\" + companyid).Count() > 0)
                //        {
                //            string pathdir = output[0].dropdown_text + @"\SubContractProfile\" + companyid;
                //            string[] filePaths = Directory.GetFiles(pathdir, "*.*");
                //            foreach (string file in filePaths)
                //            {
                //                using (var ms = new MemoryStream(System.IO.File.ReadAllBytes(file)))
                //                {
                //                    foreach (var e in L_File)
                //                    {
                //                        string filename = Path.GetFileName(file);
                //                        filename = EnsureCorrectFilename(filename);
                //                        var fileBytes = ms.ToArray();
                //                        if (e.Filename == filename)
                //                        {
                //                            e.Fileupload = fileBytes;
                //                            e.ContentType = Path.GetExtension(Path.GetExtension(file));
                //                            e.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "files", FileName = filename }.ToString();
                //                        }
                //                    }

                //                }
                //            }
                //        }
                //        else
                //        {
                //            result = false;
                //        }
                //    }
                //}
                #endregion

                string pathdir = Path.Combine(strpathUpload, companyid);
                if (Directory.GetFiles(pathdir, "*", SearchOption.AllDirectories).Length > 0)
                {
                    string[] filePaths = Directory.GetFiles(pathdir, "*.*");
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
                else
                {
                    result = false;
                }


            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
        private async Task<bool> CopyFile(FileUploadModal file,string companyid)
        {
            FileStream output;
            var outputNas = new List<SubcontractDropdownModel>();
            try
            {
                #region NAS
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                //string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "nas_subcontract");

                //HttpResponseMessage response = client.GetAsync(uriString).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    var v = response.Content.ReadAsStringAsync().Result;
                //    outputNas = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(v);
                //} 
               
                //if (outputNas != null & outputNas.Count() > 0)
                //{

                //    using (var impersonator = new Impersonator(outputNas[0].value1, outputNas[0].value2, outputNas[0].dropdown_text, false))
                //    {
                //        if (!Directory.Exists(outputNas[0].dropdown_text + @"\SubContractProfile\" + companyid))
                //        {
                //            Directory.CreateDirectory(outputNas[0].dropdown_text + @"\SubContractProfile\" + companyid);
                //        }

                //        var stream = new MemoryStream(file.Fileupload);
                //        FormFile files = new FormFile(stream, 0, file.Fileupload.Length, "name", "fileName");

                //        string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                //        filename = EnsureCorrectFilename(file.Filename);
                //        using (output = System.IO.File.Create(Path.Combine(outputNas[0].dropdown_text + @"\SubContractProfile\" + companyid, filename)))
                //        {
                //            await files.CopyToAsync(output);
                //        }

                //    }

                //}
                #endregion

                
                    var stream = new MemoryStream(file.Fileupload);
                    FormFile files = new FormFile(stream, 0, file.Fileupload.Length, "name", "fileName");

                    string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    filename = EnsureCorrectFilename(file.Filename);
                    using (output = System.IO.File.Create(GetPathAndFilename(companyid, filename)))
                    {
                        await files.CopyToAsync(output);
                    }



            }
            catch (Exception e)
            {
                return false;
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
            string pathdir = Path.Combine(strpathUpload, guid);
            string PathOutput = "";
            if (!Directory.Exists(pathdir))
            {
                Directory.CreateDirectory(pathdir);
            }
            PathOutput = Path.Combine(pathdir, filename);
            return PathOutput;
        }

        private async Task<bool> UploadfileOnSave(IFormFile files, string CompanyId)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            FileStream output;
            string strmess = "";
            try
            {

                if (files != null && files.Length > 0)
                {

                    string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                    filename = EnsureCorrectFilename(filename);
                    using (output = System.IO.File.Create(this.GetPathAndFilename(CompanyId, filename)))
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

        #endregion


        #region Private
        private List<SubcontractProfileAddressModel> SaveAddressSession(List<SubcontractProfileAddressModel> daftdata)
        {
            List<SubcontractProfileAddressModel> data = new List<SubcontractProfileAddressModel>();
            try
            {
                var outputprovince = new List<SubcontractProfileProvinceModel>();
                var outputdistrict = new List<SubcontractProfileDistrictModel>();
                var outputsubdistrict = new List<SubcontractProfileSubDistrictModel>();

                string uriprovice = string.Format("{0}", strpathAPI + "Province/GetAll");
                string uridistrict = string.Format("{0}", strpathAPI + "District/GetDistrictByProvinceId");
                string urisubdistrict = string.Format("{0}", strpathAPI + "SubDistrict/GetSubDistrictByDistrict");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;

                foreach (var e in daftdata)
                {
                    Guid addr_id = Guid.NewGuid();
                    e.AddressId = addr_id;


                    if (e.ProvinceId == 0)
                    {
                        response = client.GetAsync(uriprovice).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputprovince = JsonConvert.DeserializeObject<List<SubcontractProfileProvinceModel>>(v);
                            string[] s_provice = e.province_name.Split(" ");
                            if (Lang == "TH")
                            {
                                var w = outputprovince.First(x => x.ProvinceNameTh.Contains(s_provice[1].ToString()));
                                e.ProvinceId = w.ProvinceId;
                                e.RegionId = w.RegionId;

                            }
                            else
                            {
                                var w = outputprovince.First(x => x.ProvinceNameEn.Contains(s_provice[1].ToString()));
                                e.ProvinceId = w.ProvinceId;
                                e.RegionId = w.RegionId;
                            }
                        }
                    }
                    if (e.DistrictId == 0)
                    {
                        response = client.GetAsync(uridistrict + "/" + e.ProvinceId).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputdistrict = JsonConvert.DeserializeObject<List<SubcontractProfileDistrictModel>>(v);
                            string[] s_district = e.district_name.Split(" ");
                            if (Lang == "TH")
                            {
                                var w = outputdistrict.First(d => d.DistrictNameTh.Contains(s_district[1].ToString()));
                                e.DistrictId = w.DistrictId;
                            }
                            else
                            {
                                var w = outputdistrict.First(d => d.DistrictNameEn.Contains(s_district[1].ToString()));
                                e.DistrictId = w.DistrictId;
                            }

                        }

                    }
                    if (e.SubDistrictId == 0)
                    {
                        response = client.GetAsync(urisubdistrict + "/" + e.DistrictId).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var v = response.Content.ReadAsStringAsync().Result;
                            outputsubdistrict = JsonConvert.DeserializeObject<List<SubcontractProfileSubDistrictModel>>(v);

                            string[] s_subdistrict = e.sub_district_name.Split(" ");
                            if (Lang == "TH")
                            {
                                var w = outputsubdistrict.First(d => d.SubDistrictNameTh.Contains(s_subdistrict[1].ToString()));
                                e.SubDistrictId = w.SubDistrictId;
                            }
                            else
                            {
                                var w = outputsubdistrict.First(d => d.SubDistrictNameEn.Contains(s_subdistrict[1].ToString()));
                                e.SubDistrictId = w.SubDistrictId;
                            }

                        }
                    }
                }

                data = daftdata;


            }
            catch (Exception e)
            {

                throw;
            }
            return data;
        }



        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if (Lang == null || Lang == "")
            {
                Lang = "TH";
            }
            datauser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }
        #endregion
    }
}
namespace Localization.LocSample
{
    public class SharedResource
    {
    }
}