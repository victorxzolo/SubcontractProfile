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

namespace SubcontractProfile.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private SubcontractProfileUserModel dataUser = new SubcontractProfileUserModel();
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string strpathAPI;
        private string Lang = "";
        private string strpathASCProfile;
        private readonly string strpathUpload;
        private const int MegaBytes = 1024 * 1024;
        private const int TMegaBytes = 1024 *1024;
        public RegistrationController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
            strpathUpload = _configuration.GetValue<string>("PathUploadfile:Local").ToString();
            strpathASCProfile = _configuration.GetValue<string>("PathASCProfile:DEV").ToString();
            //******set language******
            #region GET SESSION

            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");

            #endregion
        }
        public IActionResult ActivateProfile()
        {
            ViewData["Controller"] = "Registration";
            ViewData["View"] = "Activate Profile";
            return View();
        }
        public IActionResult SearchCompanyVerify()
        {
            ViewData["Controller"] = "Registration";
            ViewData["View"] = "Search Company Verify";
            return View();
        }
        public IActionResult CompanyVerify(string companyid)
        {
            //ViewData["Controller"] = "Registration";
            //ViewData["View"] = "Company Verify";
            ViewBag.Companyid = companyid;
            return View();
        }

        [HttpPost]
        public IActionResult DDLStatus()
        {
            var output = new List<SubcontractProfileRequestStatusModel>();
            List<SelectListItem> getAllStatusList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "RequestStatus/GetALL");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileRequestStatusModel>>(v);
            }
            if (Lang == null || Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileRequestStatusModel
                {
                    request_status = "",

                });

                foreach(var r in output)
                {
                    if(r.request_status=="")
                    {
                        getAllStatusList.Add(new SelectListItem
                        {
                            Text = "กรุณาเลือกสถานะ",
                            Value = r.request_status
                        });
                    }
                   else
                    {
                        getAllStatusList.Add(new SelectListItem
                        {
                            Text = r.request_status,
                            Value = r.request_status
                        });
                    }
                }
               
            }
            else
            {
                output.Add(new SubcontractProfileRequestStatusModel
                {
                    request_status = "",

                });

                foreach (var r in output)
                {
                    if (r.request_status == "")
                    {
                        getAllStatusList.Add(new SelectListItem
                        {
                            Text = "Select Status",
                            Value = r.request_status
                        });
                    }
                    else
                    {
                        getAllStatusList.Add(new SelectListItem
                        {
                            Text = r.request_status,
                            Value = r.request_status
                        });
                    }
                }

            }
          var result=  getAllStatusList.OrderBy(c => c.Value).ToList();

            return Json(new { response = result });
        }



        [HttpPost]
        public ActionResult Search(SubcontractProfileCompanyViewModel searchmodel)
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

            SubcontractProfileCompanyModel model = new SubcontractProfileCompanyModel();

            if(searchmodel.RegisterDateFrom !=null)
            {
                DateTime datefrom = DateTime.ParseExact(searchmodel.RegisterDateFrom, "dd/MM/yyyy", null);
                model.ContractStartDate = datefrom;
            }
           if(searchmodel.RegisterDateTo !=null)
            {
                DateTime dateto = DateTime.ParseExact(searchmodel.RegisterDateTo, "dd/MM/yyyy", null);
                model.ContractEndDate = dateto;
            }
            model.CompanyName = searchmodel.CompanyName;
            model.SubcontractProfileType = searchmodel.SubcontractProfileType;
            model.TaxId = searchmodel.TaxId;
            model.DistributionChannel = searchmodel.DistributionChannel;
            model.ChannelSaleGroup = searchmodel.ChannelSaleGroup;
            model.VendorCode = searchmodel.VendorCode;
            model.Status = searchmodel.Status;


            var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "SearchCompanyVerify"));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            

            var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(uriCompany, httpContentCompany).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    //data
                    companyResult = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(result);
                }
            }


            //total number of rows count   
            recordsTotal = companyResult.Count();

            //Paging   
            var data = companyResult.Skip(skip).Take(pageSize).ToList();


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

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


                if (companyResult.CompanyCertifiedFile != null)
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
                if (companyResult.CommercialRegistrationFile != null)
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
                if (companyResult.VatRegistrationCertificateFile != null)
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
                if (companyResult.AttachFile != null)
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
                if (L_File.Count != 0)
                {
                    if(GetFile(companyId, ref L_File))
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompanySSO", L_File);
                    }
                    
                   
                }


            }

            return Json(companyResult);
        }




        [HttpPost]
        public IActionResult GetAddress(string company)
        {
            var addressResult = new List<SubcontractProfileAddressModel>();
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault() == null ? "0" : Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault() == null ? "10" : Request.Form["length"].FirstOrDefault();
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
                    if (addressResult.Count() != 0)
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
                                if (f.AddressTypeId != null)
                                {
                                    f.address_type_name = L_addresstype.Where(x => x.AddressTypeId == f.AddressTypeId).Select(x => x.AddressTypeNameTh).FirstOrDefault().ToString();

                                }
                                if (f.SubDistrictId != null)
                                {
                                    f.sub_district_name = L_subdistirct.Where(x => x.SubDistrictId == f.SubDistrictId).Select(x => x.SubDistrictNameTh).FirstOrDefault().ToString();

                                }
                                if (f.DistrictId != null)
                                {
                                    f.district_name = L_distirct.Where(e => e.DistrictId == f.DistrictId).Select(x => x.DistrictNameTh).FirstOrDefault().ToString();

                                }
                                if (f.ProvinceId != null)
                                {
                                    f.province_name = L_province.Where(x => x.ProvinceId == f.ProvinceId).Select(x => x.ProvinceNameTh).FirstOrDefault().ToString();

                                }
                            }
                            else
                            {
                                if (f.AddressTypeId != null)
                                {
                                    f.address_type_name = L_addresstype.Where(x => x.AddressTypeId == f.AddressTypeId && f.AddressTypeId != null).Select(x => x.AddressTypeNameEn).FirstOrDefault().ToString();

                                }
                                if (f.SubDistrictId != null)
                                {
                                    f.sub_district_name = L_subdistirct.Where(x => x.SubDistrictId == f.SubDistrictId).Select(x => x.SubDistrictNameEn).FirstOrDefault().ToString();

                                }
                                if (f.DistrictId != null)
                                {
                                    f.district_name = L_distirct.Where(e => e.DistrictId == f.DistrictId).Select(x => x.DistrictNameEn).FirstOrDefault().ToString();

                                }
                                if (f.ProvinceId != null)
                                {
                                    f.province_name = L_province.Where(x => x.ProvinceId == f.ProvinceId).Select(x => x.ProvinceNameEn).FirstOrDefault().ToString();

                                }
                            }

                        }
                    }




                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompanySSO", addressResult);
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
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompanySSO");
                if (data != null && data.Count != 0)
                {
                    foreach (var e in daftdata)
                    {
                        if (e.AddressId == null)
                        {
                            if (e.location_code != "")//มาจาก dealer
                            {
                                data.RemoveAll(x => x.location_code == e.location_code && x.AddressTypeId == e.AddressTypeId);
                            }
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
                            data.RemoveAll(x => x.AddressId == e.AddressId && x.CompanyId == e.CompanyId);
                            data.Add(e);
                        }

                    }

                }
                else
                {
                    data = SaveAddressSession(daftdata);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompanySSO", data);
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
                var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompanySSO").ToList();

                data.RemoveAll(x => x.AddressId.ToString().Contains(AddressId));

                SessionHelper.SetObjectAsJson(HttpContext.Session, "userAddressDaftCompanySSO", data);
                var data_delete = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompanySSO");
                if (data_delete == null)
                {
                    SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaftCompanySSO");
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
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompanySSO");
                    var result = data.Where(x => x.AddressId.ToString() == addressID).FirstOrDefault();
                    return Json(new { response = result, status = true });
                }
                else
                {
                    var data = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompanySSO");
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
                var data = SessionHelper.GetObjectFromJson<List<LocationListModel>>(HttpContext.Session, "LocationDealerCompanySSO");
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
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "LocationDealerCompanySSO", locate);
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
                data = ListResult.Count() != 0 ? (ListResult[0].vHouseNumber != null ? ListResult : new List<VATModal>()) : new List<VATModal>()
            });
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


        [HttpPost]
        public async Task<IActionResult> OnSave(SubcontractProfileCompanyModel model,string status,string? contractstart,string? contractend)
        {
            bool resultGetFile = true;
            ResponseModel res = new ResponseModel();
            string user = "";
            //SubcontractProfileCompanyModel model = new SubcontractProfileCompanyModel();
            if (dataUser == null)
            {
                getsession();

            }
           
            try
            {

               // model.CompanyId= new Guid(companyId);

                #region Verify
                user = dataUser.UserId.ToString();
                model.UpdateBy = user;

                #endregion

                if (contractstart != null && contractstart != "")
                {
                    DateTime datefrom = DateTime.ParseExact(contractstart, "dd/MM/yyyy", null);
                    model.ContractStartDate = datefrom;
                }
                if (contractend != null && contractend != "")
                {
                    DateTime dateto = DateTime.ParseExact(contractend, "dd/MM/yyyy", null);
                    model.ContractEndDate = dateto;
                }

                #region Copy File to server
                //var dataUploadfile = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftCompanySSO");
                //if (dataUploadfile != null && dataUploadfile.Count != 0)
                //{
                //   

                //    System.IO.DirectoryInfo di = new DirectoryInfo(Path.Combine(strpathUpload, model.CompanyId.ToString()));

                //foreach (FileInfo finfo in di.GetFiles())
                //{
                //    finfo.Delete();
                //}


                //foreach (var e in dataUploadfile)
                //{
                //    resultGetFile = await CopyFile(e, model.CompanyId.ToString());

                //    string filename = ContentDispositionHeaderValue.Parse(e.ContentDisposition).FileName.Trim('"');
                //    filename = EnsureCorrectFilename(filename);

                //    switch (e.typefile)
                //    {
                //        case "CompanyCertifiedFile":
                //            model.CompanyCertifiedFile = filename;
                //            break;
                //        case "CommercialRegistrationFile":
                //            model.CommercialRegistrationFile = filename;
                //            break;
                //        case "VatRegistrationCertificateFile":
                //            model.VatRegistrationCertificateFile = filename;
                //            break;
                //        case "bookbankfile":
                //            model.AttachFile = filename;
                //            break;
                //    }
                //}

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
                
                #endregion

                if (resultGetFile)
                {
                    SessionHelper.RemoveSession(HttpContext.Session, "userUploadfileDaftCompanySSO");
                //SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaftCompanySSO");

                #region Update Company for AIS

                if (status=="Approve")
                {
                    model.Status = "Y";
                }
                else if(status=="NotApprove")
                {
                    model.Status = "N";
                }

                        //var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "UpdateVerify"));
                        var uriCompany = new Uri(Path.Combine(strpathAPI, "Company", "Update"));

                HttpClient clientCompany = new HttpClient();
                clientCompany.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string str = JsonConvert.SerializeObject(model);
                var httpContentCompany = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                HttpResponseMessage responseCompany = clientCompany.PutAsync(uriCompany, httpContentCompany).Result;
                
                if (responseCompany.IsSuccessStatusCode)
                {
                    var result = responseCompany.Content.ReadAsStringAsync().Result;
                    //data
                   var output = JsonConvert.DeserializeObject<bool>(result);
                    //if(output)
                    //{
                    //    res.Status = true;
                    //    res.Message = "Update Success";
                    //    res.StatusError = "0";
                    //}
                    //else
                    //{
                    //    res.Status = false;
                    //    res.Message = "Update not sccess";
                    //    res.StatusError = "0";
                    //}
                    if(output)
                            {
                                #region Insert Address

                                var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileAddressModel>>(HttpContext.Session, "userAddressDaftCompanySSO");

                                if (dataaddr != null && dataaddr.Count != 0)
                                {
                                    var uriAddrDelete = new Uri(Path.Combine(strpathAPI, "Address", "DeleteByCompanyID"));
                                    HttpClient clientAddrDelete = new HttpClient();
                                    clientAddrDelete.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    var httpContentAddrDelete = new StringContent(JsonConvert.SerializeObject(model.CompanyId.ToString()), Encoding.UTF8, "application/json");

                                    var responseDelete = await clientAddrDelete.PostAsync(uriAddrDelete, httpContentAddrDelete);
                                    if(responseDelete.IsSuccessStatusCode)
                                    {
                                        var r = responseDelete.Content.ReadAsStringAsync().Result;
                                        bool statusDelete = JsonConvert.DeserializeObject<bool>(r);
                                        if (statusDelete)
                                        {
                                            SessionHelper.RemoveSession(HttpContext.Session, "userAddressDaftCompanySSO");
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
                                                //addr.CompanyId = companyId.ToString();
                                                addr.CompanyId = model.CompanyId.ToString();
                                                addr.ModifiedBy = user;
                                                addr.ModifiedDate = DateTime.Now;
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

                                                // string rr = JsonConvert.SerializeObject(addr);

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

                                            if (statusAddAddr)
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
                                res.Message = "Company Data is not correct, Please Check Data or Contact System Admin";
                                res.StatusError = "-1";
                            }
                        }
                else
                {
                     res.Status = false;
                     res.Message = "Company is not correct, Please Check Data or Contact System Admin";
                     res.StatusError = "-1";
                }
                #endregion
            //}
            //    else
            //{
            //    res.Status = false;
            //    res.Message = "Upload file is not correct, Please Check Data or Contact System Admin";
            //    res.StatusError = "-1";
            //}
                }
                else
                {
                    res.Status = false;
                    res.Message = "Upload file is not correct, Please Check Data or Contact System Admin";
                    res.StatusError = "-1";
                }

            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
                res.StatusError = "-1";
            }
            return Json(new { Response = res });
        }

        #region Tab Location

        [HttpPost]
        public ActionResult SearchListLocation(string companyid)
        {

            var Result = new List<SubcontractProfileLocationModel>();
            var ResultCompany = new SubcontractProfileCompanyModel();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault() == null ? "0" : Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault() == null ? "10" : Request.Form["length"].FirstOrDefault();
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

            //var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

           // Guid strCompanyId = userProfile.companyid;

            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetLocationByCompany", companyid);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(result);

            }

            string uriStringcompany = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", companyid);

           response = client.GetAsync(uriStringcompany).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                ResultCompany = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();

            foreach(var d in data)
            {
                d.SubcontractProfileType = ResultCompany.SubcontractProfileType;
            }


            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data,companynameth=ResultCompany.CompanyNameTh });
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

        [HttpPost]
        public JsonResult GetDataLocationById(string locationId)
        {
            var locationResult = new SubcontractProfileLocationModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetByLocationId", locationId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                locationResult = JsonConvert.DeserializeObject<SubcontractProfileLocationModel>(result);

            }

            return Json(locationResult);
        }

        #endregion

        #region Team

        [HttpPost]
        public IActionResult SearchListTeam(string locationId, string companyid)
        {

            var Result = new List<SubcontractProfileTeamModel>();
            var ResultLocation = new SubcontractProfileLocationModel();
            var ResultCompany = new SubcontractProfileCompanyModel();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault() == null ? "0" : Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault() == null ? "10" : Request.Form["length"].FirstOrDefault();
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

            //var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            //Guid companyId = userProfile.companyid;

            if (locationId == "-1" || locationId == null)
            {
                locationId = "-1";
            }
            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", strpathAPI + "Team/SearchTeam"
                                            , companyid, locationId, "null", "null", "null", "null", "null");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(result);

            }

            string uriStringcompany = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", companyid);

            response = client.GetAsync(uriStringcompany).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                ResultCompany = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);

            }

            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();

            foreach (var d in data)
            {
                d.SubcontractProfileType = ResultCompany.SubcontractProfileType;
            }

            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        [HttpPost]
        public JsonResult GetDataTeamById(string teamId)
        {
            var result = new SubcontractProfileTeamModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Team/GetByTeamId", teamId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfileTeamModel>(resultAsysc);

            }

            return Json(result);
        }

        //[HttpPost]
        //public IActionResult DDLLocation(string companyid)
        //{
        //    var output = new List<SubcontractProfileLocationModel>();
        //    List<SelectListItem> getAllLocationList = new List<SelectListItem>();

        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));

        //    string uriString = string.Format("{0}/{1}", strpathAPI + "Team/GetLocationByCompany", companyid);
        //    HttpResponseMessage response = client.GetAsync(uriString).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var v = response.Content.ReadAsStringAsync().Result;
        //        output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
        //    }
        //    if (Lang == null || Lang == "")
        //    {
        //        getsession();
        //    }
        //    if (Lang == "TH")
        //    {
        //        output.Add(new SubcontractProfileLocationModel
        //        {
        //            LocationCode = ""

        //        }); ;

        //        foreach (var r in output)
        //        {
        //            if (r.LocationCode == "")
        //            {
        //                getAllLocationList.Add(new SelectListItem
        //                {
        //                    Text = "กรุณาเลือก Location",
        //                    Value =""
        //                }) ;
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
        //            LocationCode = ""

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

        //    return Json(new { response = result });
        //}

        [HttpPost]
        public JsonResult GetLocationByCompany(string companyid)
        {
            //var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");
            var output = new List<SubcontractProfileLocationModel>();
            List<SelectListItem> getAllLocationList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

           // var companyId = userProfile.companyid;


            string uriString = string.Format("{0}/{1}", strpathAPI + "Location/GetLocationByCompany", companyid);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsync = response.Content.ReadAsStringAsync().Result;
                //data
                output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(resultAsync);

            }
            if (Lang == null || Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileLocationModel
                {
                    LocationCode = ""

                }); ;

                foreach (var r in output)
                {
                    if (r.LocationCode == "")
                    {
                        getAllLocationList.Add(new SelectListItem
                        {
                            Text = "กรุณาเลือก Location",
                            Value = ""
                        });
                    }
                    else
                    {
                        getAllLocationList.Add(new SelectListItem
                        {
                            Text = r.LocationNameTh,
                            Value = r.LocationId.ToString()
                        });
                    }
                }

            }
            else
            {
                output.Add(new SubcontractProfileLocationModel
                {
                    LocationCode = ""

                });

                foreach (var r in output)
                {
                    if (r.LocationCode == "")
                    {
                        getAllLocationList.Add(new SelectListItem
                        {
                            Text = "Select Location",
                            Value = ""
                        });
                    }
                    else
                    {
                        getAllLocationList.Add(new SelectListItem
                        {
                            Text = r.LocationNameEn,
                            Value = r.LocationId.ToString()
                        });
                    }
                }

            }
            var result = getAllLocationList.OrderBy(c => c.Value).ToList();

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetTitleName()
        {
            var result = new List<SubcontractDropdownModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Dropdown/GetByDropDownName", "title_name");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractDropdownModel>>(resultAsysc);

            }

            return Json(result);
        }
        #endregion

        #region Engineer

        [HttpPost]
        public ActionResult SearchListEngineer(string locationId, string teamId,string companyid)
        {

            var Result = new List<SubcontractProfileEngineerModel>();
            var ResultCompany = new SubcontractProfileCompanyModel();

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

           // var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            //Guid companyId = userProfile.companyid;
            Guid gLocationId;
            Guid gTeamId;

            if (locationId == null || locationId == "")
            {
                gLocationId = Guid.Empty;
            }
            else
            {
                gLocationId = new Guid(locationId);
            }


            if (teamId == null || teamId == "")
            {
                gTeamId = Guid.Empty;
            }
            else
            {
                gTeamId = new Guid(teamId);
            }



            string uriString = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", strpathAPI + "Engineer/SearchEngineer", companyid, gLocationId
               , gTeamId, "null", "null", "null");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                Result = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(result);

            }

            string uriStringcompany = string.Format("{0}/{1}", strpathAPI + "Company/GetByCompanyId", companyid);

            response = client.GetAsync(uriStringcompany).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                //data
                ResultCompany = JsonConvert.DeserializeObject<SubcontractProfileCompanyModel>(result);

            }


            //total number of rows count   
            recordsTotal = Result.Count();

            //Paging   
            var data = Result.Skip(skip).Take(pageSize).ToList();
            foreach (var d in data)
            {
                d.SubcontractProfileType = ResultCompany.SubcontractProfileType;
            }

            // Returning Json Data
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = data });
        }

        public JsonResult GetDataEngineerById(string engineerId)
        {
            var result = new SubcontractProfileEngineerModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Engineer/GetByEngineerId", engineerId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfileEngineerModel>(resultAsysc);

            }

            return Json(result);
        }

        public JsonResult GetDataByLocationId(string locationId, string companyid)
        {
            //var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            //Guid companyId = userProfile.companyid;

            var result = new List<SubcontractProfileTeamModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            Guid gLocationId;

            if (locationId == null || locationId == "-1")
            {
                gLocationId = Guid.Empty;
            }
            else
            {
                gLocationId = new Guid(locationId);
            }

            string uriString = string.Format("{0}/{1}/{2}", strpathAPI + "Team/GetByLocationId", companyid, gLocationId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(resultAsysc);

            }

            return Json(result);
        }

        public JsonResult GetDataPersonalById(Guid personalId)
        {
            var result = new SubcontractProfilePersonalModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "Personal/GetByPersonalId", personalId);

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsysc = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<SubcontractProfilePersonalModel>(resultAsysc);

            }

            return Json(result);
        }

        public JsonResult GetBankName()
        {
            //var userProfile = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(HttpContext.Session, "userLogin");

            var result = new List<SubcontractProfileBankingModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //var companyId = userProfile.companyid;


            string uriString = string.Format("{0}", strpathAPI + "Banking/GetAll");

            HttpResponseMessage response = client.GetAsync(uriString).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultAsync = response.Content.ReadAsStringAsync().Result;
                //data
                result = JsonConvert.DeserializeObject<List<SubcontractProfileBankingModel>>(resultAsync);

            }


            return Json(result);
        }

        [HttpPost]
        public IActionResult DDLTeam(string companyid,string locationId)
        {
            var output = new List<SubcontractProfileTeamModel>();
            List<SelectListItem> getAllTeamList = new List<SelectListItem>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString="";

                uriString = string.Format("{0}/{1}/{2}", strpathAPI + "Team/GetByLocationId", companyid, locationId);

           
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
            }
            if (Lang == null || Lang == "")
            {
                getsession();
            }
            if (Lang == "TH")
            {
                output.Add(new SubcontractProfileTeamModel
                {
                    TeamCode = ""

                }); ;

                foreach (var r in output)
                {
                    if (r.TeamCode == "")
                    {
                        getAllTeamList.Add(new SelectListItem
                        {
                            Text = "กรุณาเลือก Team",
                            Value = ""
                        });
                    }
                    else
                    {
                        getAllTeamList.Add(new SelectListItem
                        {
                            Text = r.TeamNameTh,
                            Value = r.TeamId.ToString()
                        }) ;
                    }
                }

            }
            else
            {
                output.Add(new SubcontractProfileTeamModel
                {
                    LocationCode = ""

                });

                foreach (var r in output)
                {
                    if (r.LocationCode == "")
                    {
                        getAllTeamList.Add(new SelectListItem
                        {
                            Text = "Select Team",
                            Value = ""
                        });
                    }
                    else
                    {
                        getAllTeamList.Add(new SelectListItem
                        {
                            Text = r.TeamNameEn,
                            Value = r.TeamId.ToString()
                        });
                    }
                }

            }
            var result = getAllTeamList.OrderBy(c => c.Value).ToList();

            return Json(new { response = result });
        }
        #endregion


        #region Upload File


        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Uploadfile(IList<IFormFile> files, string fid, string type_file, string Company)
        {
            bool statusupload = true;
            List<FileUploadModal> L_File = new List<FileUploadModal>();
            //FileStream output;
            string strmess = "";

            //string[] arr = { "application/pdf", "image/png", "image/jpeg", "image/jpeg", "image/bmp", "image/gif", "image/tif", "image/tiff" };


            try
            {
                foreach (FormFile source in files)
                {
                    if (source.Length > 0)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);

                        if(type_file== "bookbankfile")
                        {

                            if (source.ContentType.ToLower() != "image/jpg" &&
                                source.ContentType.ToLower() != "image/jpeg" &&
                                source.ContentType.ToLower() != "image/pjpeg" &&
                                source.ContentType.ToLower() != "image/gif" &&
                                source.ContentType.ToLower() != "image/x-png" &&
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
                                    var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftCompanySSO");

                                    if (data != null)
                                    {

                                        data.RemoveAll(x => x.file_id.ToString() == fid);
                                        data.Add(L_File[0]);
                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompanySSO", data);

                                    }
                                    else
                                    {

                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompanySSO", L_File);
                                    }
                                    strmess = "Upload file success";
                                }

                                
                            }
                        }
                        else
                        {
                            if (source.ContentType.ToLower() != "application/pdf")
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
                                    var data = SessionHelper.GetObjectFromJson<List<FileUploadModal>>(HttpContext.Session, "userUploadfileDaftCompanySSO");

                                    if (data != null)
                                    {

                                        data.RemoveAll(x => x.file_id.ToString() == fid);
                                        data.Add(L_File[0]);
                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompanySSO", data);

                                    }
                                    else
                                    {

                                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userUploadfileDaftCompanySSO", L_File);
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


            return Json(new { status = statusupload, message = strmess, response = (statusupload ? L_File[0].file_id.ToString() : "") });

            // return Json(new { status = statusupload, message = strmess });
        }

        private bool DeleteFile(string companyid, string filename)
        {
            bool result = true;
            try
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(Path.Combine(strpathUpload, companyid, filename)))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(Path.Combine(strpathUpload, companyid, filename));

                }
            }
            catch (IOException ioExp)
            {
                result = false;
            }
            return result;
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
            }
            return result;
        }
        private async Task<bool> CopyFile(FileUploadModal file, string companyid)
        {
            FileStream output;
            try
            {
                var stream = new MemoryStream(file.Fileupload);
                FormFile files = new FormFile(stream, 0, file.Fileupload.Length, "name", "fileName");

                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                filename = EnsureCorrectFilename(file.Filename);
                using (output = System.IO.File.Create(this.GetPathAndFilename(companyid, filename)))
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

        private void getsession()
        {
            Lang = SessionHelper.GetObjectFromJson<string>(_httpContextAccessor.HttpContext.Session, "language");
            if (Lang == null || Lang == "")
            {
                Lang = "TH";
            }
            dataUser = SessionHelper.GetObjectFromJson<SubcontractProfileUserModel>(_httpContextAccessor.HttpContext.Session, "userLogin");
        }
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
        #endregion
       
    }
}
