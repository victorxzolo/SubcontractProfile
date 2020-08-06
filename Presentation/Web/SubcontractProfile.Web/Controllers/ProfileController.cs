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

namespace SubcontractProfile.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strpathAPI;

        public ProfileController(IConfiguration configuration)
        {
            _configuration = configuration;


            //เรียก appsetting.json path api
            strpathAPI = _configuration.GetValue<string>("Pathapi:Local").ToString();
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
        public IActionResult GetCompany(string CompanyID)
        {
            var output = new List<SubcontractProfileCompanyModel>();
            var resultZipCode = new List<SubcontractProfileCompanyModel>();

            if (CompanyID != "0")
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "CompanyController/GetByCompanyId", CompanyID);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
                    //resultZipCode = output.GroupBy(c => c.CompanyId).Select(g => g.First()).ToList();
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "CompanyController/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
                    //resultZipCode = output.GroupBy(c => c.CompanyId).Select(g => g.First()).ToList();
                }
            }
            return Json(new { response = output});
            //return Json(new { response = output, responsezipcode = resultZipCode });
        }

        [HttpPost]
        public IActionResult AddCompany(SubcontractProfileCompanyModel Company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileCompanyModel>>(HttpContext.Session, "Company");
                    var model = new List<SubcontractProfileCompanyModel>();

                    if (Company != null)
                    {

                        model.Add(new SubcontractProfileCompanyModel
                        {
                            CompanyId = Guid.NewGuid(),
                            CompanyCode = " ",
                            CompanyName = " ",
                            CompanyNameTh = Company.CompanyNameTh,
                            CompanyNameEn = Company.CompanyNameEn,
                            CompanyAlias = Company.CompanyAlias,
                            DistributionChannel = " ",
                            ChannelSaleGroup = " ",
                            VendorCode = " ",
                            CustomerCode = " ",
                            AreaId = " ",
                            TaxId = " ",
                            WtName = " ",
                            VatType = " ",
                            CompanyCertifiedFile = " ",
                            CommercialRegistrationFile = " ",
                            VatRegistrationCertificateFile = " ",
                            ContractAgreementFile = " ",
                            DepositAuthorizationLevel = " ",
                            DepositPaymentType = " ",
                            ContractStartDate = DateTime.Now,  //Company.ContractStartDate,
                            ContractEndDate = DateTime.Now,
                            OverDraftDeposit = " ",
                            BalanceDeposit = 1,
                            CompanyStatus = " ",
                            CompanyAddress = " ",
                            VatAddress = " ",
                            CreateBy = " ",
                            CreateDate = DateTime.Now,
                            UpdateBy = " ",
                            UpdateDate = DateTime.Now,
                            CompanyEmail = " ",
                            ContractName = " ",
                            ContractPhone = " ",
                            ContractEmail = " ",
                            BankCode = " ",
                            BankName = " ",
                            AccountNumber = " ",
                            AccountName = " ",
                            AttachFile = " ",
                            BranchCode = " ",
                            BranchName = " ",
                            DeptOfInstallName = " ",
                            DeptOfMaintenName = " ",
                            DeptOfAccountName = " ",
                            DeptOfInstallPhone = " ",
                            DeptOfMaintenPhone = " ",
                            DeptOfAccountPhone = " ",
                            DeptOfInstallEmail = " ",
                            DeptOfMaintenEmail = " ",
                            DeptOfAccountEmail = " ",
                            LocationCode = " ",
                            LocationNameTh = " ",
                            LocationNameEn = " ",
                            BankAccountTypeId = " ",
                            SubcontractProfileType = " ",
                            CompanyTitleThId = " ",
                            CompanyTitleEnId = " ",
                            Status = " ",
                            ActivateDate = DateTime.Now

                        }); 
                        //model.Add(new SubcontractProfileCompanyModel
                        //{
                        //    CompanyId = Guid.NewGuid(),
                        //    CompanyCode = Company.CompanyCode,
                        //    CompanyName = Company.CompanyName,
                        //    CompanyNameTh = Company.CompanyNameTh,
                        //    CompanyNameEn = Company.CompanyNameEn,
                        //    CompanyAlias = Company.CompanyAlias,
                        //    DistributionChannel = Company.DistributionChannel,
                        //    ChannelSaleGroup = Company.ChannelSaleGroup,
                        //    VendorCode = Company.VendorCode,
                        //    CustomerCode = Company.CustomerCode,
                        //    AreaId = Company.AreaId,
                        //    TaxId = Company.TaxId,
                        //    WtName = Company.WtName,
                        //    VatType = Company.VatType,
                        //    CompanyCertifiedFile = Company.CompanyCertifiedFile,
                        //    CommercialRegistrationFile = Company.CommercialRegistrationFile,
                        //    VatRegistrationCertificateFile = Company.VatRegistrationCertificateFile,
                        //    ContractAgreementFile = Company.ContractAgreementFile,
                        //    DepositAuthorizationLevel = Company.DepositAuthorizationLevel,
                        //    DepositPaymentType = Company.DepositPaymentType,
                        //    ContractStartDate = DateTime.Now,  //Company.ContractStartDate,
                        //    ContractEndDate = DateTime.Now,
                        //    OverDraftDeposit = Company.OverDraftDeposit,
                        //    BalanceDeposit = 1,
                        //    CompanyStatus = Company.CompanyStatus,
                        //    CompanyAddress = Company.CompanyAddress,
                        //    VatAddress = Company.VatAddress,
                        //    CreateBy = Company.CreateBy,
                        //    CreateDate = DateTime.Now,
                        //    UpdateBy = Company.UpdateBy,
                        //    UpdateDate = DateTime.Now,
                        //    CompanyEmail = Company.CompanyEmail,
                        //    ContractName = Company.ContractName,
                        //    ContractPhone = Company.ContractPhone,
                        //    ContractEmail = Company.ContractEmail,
                        //    BankCode = Company.BankCode,
                        //    BankName = Company.BankName,
                        //    AccountNumber = Company.AccountNumber,
                        //    AccountName = Company.AccountName,
                        //    AttachFile = Company.AttachFile,
                        //    BranchCode = Company.BranchCode,
                        //    BranchName = Company.BranchName,
                        //    DeptOfInstallName = Company.DeptOfInstallName,
                        //    DeptOfMaintenName = Company.DeptOfMaintenName,
                        //    DeptOfAccountName = Company.DeptOfAccountName,
                        //    DeptOfInstallPhone = Company.DeptOfInstallPhone,
                        //    DeptOfMaintenPhone = Company.DeptOfMaintenPhone,
                        //    DeptOfAccountPhone = Company.DeptOfAccountPhone,
                        //    DeptOfInstallEmail = Company.DeptOfInstallEmail,
                        //    DeptOfMaintenEmail = Company.DeptOfMaintenEmail,
                        //    DeptOfAccountEmail = Company.DeptOfAccountEmail,
                        //    LocationCode = Company.LocationCode,
                        //    LocationNameTh = Company.LocationNameTh,
                        //    LocationNameEn = Company.LocationNameEn,
                        //    BankAccountTypeId = Company.BankAccountTypeId,
                        //    SubcontractProfileType = Company.SubcontractProfileType,
                        //    CompanyTitleThId = Company.CompanyTitleThId,
                        //    CompanyTitleEnId = Company.CompanyTitleEnId,
                        //    Status = Company.Status,
                        //    ActivateDate = DateTime.Now

                        //});;

                        //Command.Handle(model)
                        //output ret_code,ret
                        //ret_msg
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                        string uriString = string.Format("{0}/{1}", strpathAPI + "CompanyController/GetByCompanyId", model);
                        HttpResponseMessage response = client.GetAsync(uriString).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            //var v = response.Content.ReadAsStringAsync().Result;
                            //output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
                            //resultZipCode = output.GroupBy(c => c.CompanyId).Select(g => g.First()).ToList();
                        }
                        else {
                            //var x = response.IsSuccessStatusCode;
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            status = "-1",
                            message = "Data isnot correct, Please Check Data or Contact System Admin"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "Success"
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
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateCompany()
        {
            var output = new List<SubcontractProfileCompanyModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "CompanyController/Update");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
            }

            return Json(new { response = output });
        }

        [HttpPost]
        public IActionResult DeleteCompany(string CompanyId)
        {
            var output = new List<SubcontractProfileCompanyModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "CompanyController/Delete", CompanyId);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileCompanyModel>>(v);
            }

            return Json(new { response = output });
        }

        #endregion

        #region Engineer
        [HttpPost]
        public IActionResult GetEngineer(string EngineerId)
        {
            var output = new List<SubcontractProfileEngineerModel>();
            var resultZipCode = new List<SubcontractProfileEngineerModel>();

            if (EngineerId != "0")
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "EngineerController/GetByEngineerId", EngineerId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(v);
                    resultZipCode = output.GroupBy(c => c.EngineerId).Select(g => g.First()).ToList();
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "EngineerController/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(v);
                    resultZipCode = output.GroupBy(c => c.EngineerId).Select(g => g.First()).ToList();
                }
            }

            return Json(new { response = output, responsezipcode = resultZipCode });
        }


        [HttpPost]
        public IActionResult AddEngineer(subcontract_profile_New_Company model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileEngineerModel>>(HttpContext.Session, "Engineer");
                    model.ListEngineer = new List<SubcontractProfileEngineerModel>();

                    if (dataaddr != null && dataaddr.Count != 0)
                    {

                        foreach (var d in dataaddr)
                        {
                            model.ListEngineer.Add(new SubcontractProfileEngineerModel
                            {
                                EngineerId=d.EngineerId,
                                StaffCode=d.StaffCode,
                                FoaCode=d.FoaCode,
                                StaffName=d.StaffName,
                                StaffNameTh=d.StaffNameTh,
                                StaffNameEn=d.StaffNameEn,
                                AscCode=d.AscCode,
                                TshirtSize=d.TshirtSize,
                                ContractPhone1=d.ContractPhone1,
                                ContractPhone2=d.ContractPhone2,
                                ContractEmail=d.ContractEmail,
                                WorkExperience=d.WorkExperience,
                                WorkExperienceAttachFile=d.WorkExperienceAttachFile,
                                WorkType=d.WorkType,
                                CourseSkill=d.CourseSkill,
                                SkillLevel=d.SkillLevel,
                                VehicleType=d.VehicleType,
                                VehicleBrand=d.VehicleBrand,
                                VehicleSerise=d.VehicleSerise,
                                VehicleColor=d.VehicleColor,
                                VehicleYear=d.VehicleYear,
                                VehicleLicensePlate=d.VehicleLicensePlate,
                                VehicleAttachFile=d.VehicleAttachFile,
                                ToolOtrd=d.ToolOtrd,
                                ToolSplicing=d.ToolSplicing,
                                Position=d.Position,
                                LocationCode=d.LocationCode,
                                StaffId=d.StaffId,
                                TeamCode=d.TeamCode,
                                CitizenId=d.CitizenId,
                                BankCode=d.BankCode,
                                BankName=d.BankName,
                                AccountNo= d.AccountNo,
                                AccountName=d.AccountName,
                                PersonalAttachFile=d.PersonalAttachFile,
                                StaffStatus=d.StaffStatus,
                                CreateDate=d.CreateDate,
                                CreateBy=d.CreateBy,
                                UpdateBy=d.UpdateBy,
                                UpdateDate=d.UpdateDate


                            });
                        }

                        //Command.Handle(model)
                        //output ret_code,ret
                        //ret_msg
                    }
                    else
                    {
                        return Json(new
                        {
                            status = "-1",
                            message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "Success"
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
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateEngineer()
        {
            var output = new List<SubcontractProfileEngineerModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "EngineerController/Update");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileEngineerModel>>(v);
            }

            return Json(new { response = output });
        }

        [HttpPost]
        public IActionResult DeleteEngineer(string EngineerId)
        {
            var output = new List<SubcontractProfileEngineerModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "EngineerController/Delete", EngineerId);
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
        public IActionResult GetLocation(string LocationId)
        {
            var output = new List<SubcontractProfileLocationModel>();
            var resultZipCode = new List<SubcontractProfileLocationModel>();

            if (LocationId != "0")
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "LocationController/GetByLocationId", LocationId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
                    resultZipCode = output.GroupBy(c => c.LocationId).Select(g => g.First()).ToList();
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "LocationController/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
                    resultZipCode = output.GroupBy(c => c.LocationId).Select(g => g.First()).ToList();
                }
            }

            return Json(new { response = output, responsezipcode = resultZipCode });
        }

        [HttpPost]
        public IActionResult AddLocation(subcontract_profile_New_Company model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileLocationModel>>(HttpContext.Session, "Engineer");
                    model.ListLocation = new List<SubcontractProfileLocationModel>();

                    if (dataaddr != null && dataaddr.Count != 0)
                    {

                        foreach (var d in dataaddr)
                        {
                            model.ListLocation.Add(new SubcontractProfileLocationModel
                            {
                                LocationId = d.LocationId,
                                LocationCode = d.LocationCode,
                                LocationName = d.LocationName,
                                LocationNameTh = d.LocationNameTh,
                                LocationNameEn = d.LocationNameEn,
                                LocationNameAlias = d.LocationNameAlias,
                                VendorCode = d.VendorCode,
                                StorageLocation = d.StorageLocation,
                                ShipTo = d.ShipTo,
                                OutOfServiceStorageLocation = d.OutOfServiceStorageLocation,
                                SubPhase = d.SubPhase,
                                EffectiveDate = d.EffectiveDate,
                                ShopType = d.ShopType,
                                VatBranchNumber = d.VatBranchNumber,
                                Phone = d.Phone,
                                CompanyMainContractPhone = d.CompanyMainContractPhone,
                                InstallationsContractPhone = d.InstallationsContractPhone,
                                MaintenanceContractPhone = d.MaintenanceContractPhone,
                                InventoryContractPhone = d.InventoryContractPhone,
                                PaymentContractPhone = d.PaymentContractPhone,
                                EtcContractPhone = d.EtcContractPhone,
                                CompanyGroupMail = d.CompanyGroupMail,
                                InstallationsContractMail = d.InstallationsContractMail,
                                MaintenanceContractMail = d.MaintenanceContractMail,
                                InventoryContractMail = d.InventoryContractMail,
                                PaymentContractMail = d.PaymentContractMail,
                                EtcContractMail = d.EtcContractMail,
                                LocationAddress = d.LocationAddress,
                                PostAddress = d.PostAddress,
                                TaxAddress = d.TaxAddress,
                                WtAddress = d.WtAddress,
                                HouseNo = d.HouseNo,
                                AreaCode = d.AreaCode,
                                BankCode = d.BankCode,
                                BankName = d.BankName,
                                BankAccountNo = d.BankAccountNo,
                                BankAccountName = d.BankAccountName,
                                BankAttachFile = d.BankAttachFile,
                                Status = d.Status,
                                CreateDate = d.CreateDate,
                                CreateBy = d.CreateBy,
                                UpdateBy = d.UpdateBy,
                                UpdateDate = d.UpdateDate,
                                BankBranchCode = d.BankBranchCode,
                                BankBranchName = d.BankBranchName,
                                PenaltyContractPhone = d.PenaltyContractPhone,
                                PenaltyContractMail = d.PenaltyContractMail,
                                ContractPhone = d.ContractPhone,
                                ContractMail = d.ContractMail,
                                CompanyId = d.CompanyId

                            });
                        }

                        //Command.Handle(model)
                        //output ret_code,ret
                        //ret_msg
                    }
                    else
                    {
                        return Json(new
                        {
                            status = "-1",
                            message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "Success"
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

            string uriString = string.Format("{0}", strpathAPI + "LocationController/Update");
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
            var output = new List<SubcontractProfileLocationModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "LocationController/Delete", LocationId);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileLocationModel>>(v);
            }

            return Json(new { response = output });
        }

        #endregion

        #region team

        [HttpPost]
        public IActionResult GetTeam(string TeamId)
        {
            var output = new List<SubcontractProfileTeamModel>();
            var resultZipCode = new List<SubcontractProfileTeamModel>();

            if (TeamId != "0")
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}/{1}", strpathAPI + "TeamController/GetByTeamId", TeamId);
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
                    resultZipCode = output.GroupBy(c => c.TeamId).Select(g => g.First()).ToList();
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                string uriString = string.Format("{0}", strpathAPI + "TeamController/GetALL");
                HttpResponseMessage response = client.GetAsync(uriString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var v = response.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
                    resultZipCode = output.GroupBy(c => c.TeamId).Select(g => g.First()).ToList();
                }
            }

            return Json(new { response = output, responsezipcode = resultZipCode });
        }

        [HttpPost]
        public IActionResult AddTeam(subcontract_profile_New_Company model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataaddr = SessionHelper.GetObjectFromJson<List<SubcontractProfileLocationModel>>(HttpContext.Session, "Engineer");
                    model.ListLocation = new List<SubcontractProfileLocationModel>();

                    if (dataaddr != null && dataaddr.Count != 0)
                    {

                        foreach (var d in dataaddr)
                        {
                            model.ListLocation.Add(new SubcontractProfileLocationModel
                            {
                                LocationId = d.LocationId,
                                LocationCode = d.LocationCode,
                                LocationName = d.LocationName,
                                LocationNameTh = d.LocationNameTh,
                                LocationNameEn = d.LocationNameEn,
                                LocationNameAlias = d.LocationNameAlias,
                                VendorCode = d.VendorCode,
                                StorageLocation = d.StorageLocation,
                                ShipTo = d.ShipTo,
                                OutOfServiceStorageLocation = d.OutOfServiceStorageLocation,
                                SubPhase = d.SubPhase,
                                EffectiveDate = d.EffectiveDate,
                                ShopType = d.ShopType,
                                VatBranchNumber = d.VatBranchNumber,
                                Phone = d.Phone,
                                CompanyMainContractPhone = d.CompanyMainContractPhone,
                                InstallationsContractPhone = d.InstallationsContractPhone,
                                MaintenanceContractPhone = d.MaintenanceContractPhone,
                                InventoryContractPhone = d.InventoryContractPhone,
                                PaymentContractPhone = d.PaymentContractPhone,
                                EtcContractPhone = d.EtcContractPhone,
                                CompanyGroupMail = d.CompanyGroupMail,
                                InstallationsContractMail = d.InstallationsContractMail,
                                MaintenanceContractMail = d.MaintenanceContractMail,
                                InventoryContractMail = d.InventoryContractMail,
                                PaymentContractMail = d.PaymentContractMail,
                                EtcContractMail = d.EtcContractMail,
                                LocationAddress = d.LocationAddress,
                                PostAddress = d.PostAddress,
                                TaxAddress = d.TaxAddress,
                                WtAddress = d.WtAddress,
                                HouseNo = d.HouseNo,
                                AreaCode = d.AreaCode,
                                BankCode = d.BankCode,
                                BankName = d.BankName,
                                BankAccountNo = d.BankAccountNo,
                                BankAccountName = d.BankAccountName,
                                BankAttachFile = d.BankAttachFile,
                                Status = d.Status,
                                CreateDate = d.CreateDate,
                                CreateBy = d.CreateBy,
                                UpdateBy = d.UpdateBy,
                                UpdateDate = d.UpdateDate,
                                BankBranchCode = d.BankBranchCode,
                                BankBranchName = d.BankBranchName,
                                PenaltyContractPhone = d.PenaltyContractPhone,
                                PenaltyContractMail = d.PenaltyContractMail,
                                ContractPhone = d.ContractPhone,
                                ContractMail = d.ContractMail,
                                CompanyId = d.CompanyId

                            });
                        }

                        //Command.Handle(model)
                        //output ret_code,ret
                        //ret_msg
                    }
                    else
                    {
                        return Json(new
                        {
                            status = "-1",
                            message = "Address Data isnot correct, Please Check Data or Contact System Admin"
                        });
                    }
                    return Json(new
                    {
                        status = "1",
                        message = "Success"
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
            catch (Exception e)
            {
                string _msg = string.Empty;
                _msg = "Please Contact System Admin";
                return Json(new { status = "-1", message = _msg });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateTeam()
        {
            var output = new List<SubcontractProfileTeamModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "TeamController/Update");
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
            }

            return Json(new { response = output });
        }

        [HttpPost]
        public IActionResult DeleteTeam(string TeamId)
        {
            var output = new List<SubcontractProfileTeamModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}/{1}", strpathAPI + "TeamController/Delete", TeamId);
            HttpResponseMessage response = client.GetAsync(uriString).Result;
            if (response.IsSuccessStatusCode)
            {
                var v = response.Content.ReadAsStringAsync().Result;
                output = JsonConvert.DeserializeObject<List<SubcontractProfileTeamModel>>(v);
            }

            return Json(new { response = output });
        }

        #endregion

        public IActionResult _PartialCompany()
        {
            return PartialView("_PartialCompanyEdit");
        }
        public IActionResult _PartialLocation()
        {
            return PartialView("_PartialLocationEdit");
        }
        public IActionResult _PartialEngineer()
        {
            return PartialView("_PartialEngineerEdit");
        }

        [HttpGet]
        public DataTable Loaddata()
        {
            DataTable table = new DataTable("TestTable");
            DataColumn CompanyID = new DataColumn("CompanyId", typeof(Guid));
            //DataColumn RegisterType = new DataColumn("CompanyNameTh", typeof(string));
            DataColumn CompanyCode = new DataColumn("CompanyCode", typeof(int));
            DataColumn CompanyNameT = new DataColumn("CompanyNameTh", typeof(string));
            DataColumn CompanyNameE = new DataColumn("CompanyNameEn", typeof(string));
            DataColumn Distribution = new DataColumn("DistributionChannel", typeof(string));
            DataColumn Channel = new DataColumn("ChannelSaleGroup", typeof(string));
            DataColumn Address = new DataColumn("CompanyAddress", typeof(string));

            table.Columns.Add(CompanyID);
            //table.Columns.Add(RegisterType);
            table.Columns.Add(CompanyCode);
            table.Columns.Add(CompanyNameT);
            table.Columns.Add(CompanyNameE);
            table.Columns.Add(Distribution);
            table.Columns.Add(Channel);
            table.Columns.Add(Address);

            DataRow newRow = table.NewRow();
            newRow["CompanyId"] = Guid.NewGuid();
            //newRow["RegisterType"] = "A";
            newRow["CompanyCode"] = 1;
            newRow["CompanyNameTh"] = "Test1T";
            newRow["CompanyNameEn"] = "Test1E";
            newRow["DistributionChannel"] = "Test1";
            newRow["ChannelSaleGroup"] = "Test1";
            newRow["CompanyAddress"] = "AddressTest1";
            table.Rows.Add(newRow);

            newRow = table.NewRow();
            newRow["CompanyId"] = Guid.NewGuid();
            //newRow["RegisterType"] = "B";
            newRow["CompanyCode"] = 2;
            newRow["CompanyNameTh"] = "Test2T";
            newRow["CompanyNameEn"] = "Test2E";
            newRow["DistributionChannel"] = "Test2";
            newRow["ChannelSaleGroup"] = "Test2";
            newRow["CompanyAddress"] = "AddressTest2";
            table.Rows.Add(newRow);

            newRow = table.NewRow();
            newRow["CompanyId"] = Guid.NewGuid();
            //newRow["RegisterType"] = "C";
            newRow["CompanyCode"] = 3;
            newRow["CompanyNameTh"] = "Test3T";
            newRow["CompanyNameEn"] = "Test3E";
            newRow["DistributionChannel"] = "Test3";
            newRow["ChannelSaleGroup"] = "Test3";
            newRow["CompanyAddress"] = "AddressTest3";
            table.Rows.Add(newRow);


            return table;

        }

        public IActionResult SearchCompany(DataTableAjaxModel model)
        {
            try
            {

                int filteredResultsCount;
                int totalResultsCount;

                //var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

                //Guid id = Guid.NewGuid();


                var take = model.length;
                var skip = model.start;

                string sortBy = "";
                bool sortDir = true;

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower() == "asc";
                }

                List<SubcontractProfileCompanyModel> result;
                //if (result != null && result.Count != 0)
                //{
                //    if (sortDir) //asc
                //    {
                //        if (sortBy == "address_type")
                //        {
                //            result = result.OrderBy(c => c.CompanyNameTh).ToList();
                //        }
                //        else if (sortBy == "address")
                //        {
                //            result = result.OrderBy(c => c.HouseNo).ToList();
                //        }
                //    }
                //    else //desc
                //    {
                //        if (sortBy == "address_type")
                //        {
                //            result = result.OrderByDescending(c => c.address_type_name).ToList();
                //        }
                //        else if (sortBy == "address")
                //        {
                //            result = result.OrderByDescending(c => c.HouseNo).ToList();
                //        }
                //    }

                //    filteredResultsCount = result.Count(); //output from Database
                //    totalResultsCount = result.Count(); //output from Database
                //}
                //else
                //{
                //    filteredResultsCount = 0; //output from Database
                //    totalResultsCount = 0; //output from Database
                //}

                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = result
                });
            }
            catch (Exception e)
            {

                throw;
            }
        }

        #region DDL
        [HttpPost]
        public IActionResult DDLTitle()
        {
            var output = new List<SubcontractProfileTitleModel>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string uriString = string.Format("{0}", strpathAPI + "TitleController/GetALL");
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

            string uriString = string.Format("{0}", strpathAPI + "ProvinceController/GetALL");
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

                string uriString = string.Format("{0}/{1}", strpathAPI + "DistrictController/GetDistrictByProvinceId", province_id);
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

                string uriString = string.Format("{0}", strpathAPI + "DistrictController/GetAll");
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

                string uriString = string.Format("{0}/{1}", strpathAPI + "SubDistrictController/GetSubDistrictByDistrict", district_id);
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

                string uriString = string.Format("{0}", strpathAPI + "SubDistrictController/GetAll");
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
        #endregion
    }


    public class subcontract_profile_New_Company 
    {

        public List<SubcontractProfileCompanyModel> ListCompany{ get; set; }

        public List<SubcontractProfileEngineerModel> ListEngineer { get; set; }

        public List<SubcontractProfileLocationModel> ListLocation { get; set; }

        public List<SubcontractProfileTeamModel> ListTeam { get; set; }

    }
}