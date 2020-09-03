using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubcontractProfile.WebApi.API.DataContracts;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.API.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly ISubcontractProfileCompanyRepo _service;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ISubcontractProfileCompanyRepo service,
            ILogger<CompanyController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CompanyController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> GetAll()
        {

            _logger.LogInformation($"CompanyController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"CompanyController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }
        //Example multiple // GET api/user/firstname/lastname/address
        // [HttpGet("{firstName}/{lastName}/{address}")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAddress))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAddress))]
        [HttpGet("GetByCompanyId/{companyId}")] // GET /api/GetByAddressId/addressId/787413D6-AA0B-4F20-A638-94FBFBF634C9
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> GetByCompanyId(string companyId)
        {
            _logger.LogInformation($"Start CompanyController::GetByCompanyId", companyId);

            System.Guid company_id = new Guid(companyId);

            var entities = _service.GetByCompanyId(company_id);

            if (entities == null)
            {
                _logger.LogWarning($"CompanyController::", "GetByCompanyId NOT FOUND", companyId);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CompanyController))]
        [HttpGet("SearchActivateProfile/{subcontract_profile_type}/{company_name_th}/{tax_id}/{activate_date_fr}/{activate_date_to}/{activate_status}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> SearchActivateProfile(string subcontract_profile_type
           , string company_name_th, string tax_id, string activate_date_fr, string activate_date_to, 
            string activate_status
          )
        {

            _logger.LogInformation($"Start CompanyController::SearchActivateProfile", subcontract_profile_type, company_name_th, tax_id, activate_date_fr
                , activate_date_to, activate_status);

           
            if (subcontract_profile_type.ToUpper() == "NULL")
            {
                subcontract_profile_type = string.Empty;
            }

            if (company_name_th.ToUpper() == "NULL")
            {
                company_name_th = string.Empty;
            }

            if (tax_id.ToUpper() == "NULL")
            {
                tax_id = string.Empty;
            }

            if (activate_date_fr.ToUpper() == "NULL")
            {
                activate_date_fr = string.Empty;
            }

            if (activate_date_to.ToUpper() == "NULL")
            {
                activate_date_to = string.Empty;
            }

            if (activate_status.ToUpper() == "NULL")
            {
                activate_status = string.Empty;
            }

            var entities = _service.SearchActivateProfile(subcontract_profile_type, company_name_th, tax_id, activate_date_fr, activate_date_to, activate_status);

            if (entities == null)
            {
                _logger.LogWarning($"CompanyController::", "SearchActivateProfile NOT FOUND", subcontract_profile_type, company_name_th, tax_id, activate_date_fr
                , activate_date_to, activate_status);
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAddress))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAddress))]
        [HttpGet("SearchCompany/{companyId}/{company_th}/{company_en}/{company_alias}/{tax_id}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> SearchCompany(string companyId
            , string company_th, string company_en, string company_alias, string tax_id)
        {
            _logger.LogInformation($"Start CompanyController::SearchCompany", companyId, company_th, company_en, company_alias
                , tax_id);

            //if(subcontract_profile_type.ToUpper()=="NULL")
            //{
            //    subcontract_profile_type = string.Empty;
            //}

            //if (location_code.ToUpper() == "NULL")
            //{
            //    location_code = string.Empty;
            //}

            //if (vendor_code.ToUpper() == "NULL")
            //{
            //    vendor_code = string.Empty;
            //}
            

            if (company_th.ToUpper() == "NULL")
            {
                company_th = string.Empty;
            }

            if (company_en.ToUpper() == "NULL")
            {
                company_en = string.Empty;
            }

            if (company_alias.ToUpper() == "NULL")
            {
                company_alias = string.Empty;
            }

            if (tax_id.ToUpper() == "NULL")
            {
                tax_id = string.Empty;
            }

            //if (company_code.ToUpper() == "NULL")
            //{
            //    company_code = string.Empty;
            //}

            //if (distibution_channel.ToUpper() == "NULL")
            //{
            //    distibution_channel = string.Empty;
            //}

            //if (channel_sale_group.ToUpper() == "NULL")
            //{
            //    channel_sale_group = string.Empty;
            //}

            System.Guid strCompanyId = new Guid(companyId);

            var entities = _service.SearchCompany(strCompanyId, company_th, company_en, company_alias, tax_id);

            if (entities == null)
            {
                _logger.LogWarning($"CompanyController::", "SearchCompany NOT FOUND", companyId, company_th, company_en, company_alias
                , tax_id);
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAddress))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAddress))]
        [HttpPost("SearchCompanyVerify")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> SearchCompanyVerify(SubcontractProfileCompany search)
        {
            _logger.LogInformation($"Start CompanyController::SearchCompanyVerify", search.SubcontractProfileType,search.TaxId,search.CompanyName
                ,search.DistributionChannel,search.ChannelSaleGroup,search.ContractStartDate,search.ContractEndDate,search.VendorCode,search.Status);


            var entities = _service.SearchCompanyVerify(search);

            if (entities == null)
            {
                _logger.LogWarning($"CompanyController::", "SearchCompanyVerify NOT FOUND",  search.SubcontractProfileType, search.TaxId, search.CompanyName
                , search.DistributionChannel, search.ChannelSaleGroup, search.ContractStartDate, search.ContractEndDate, search.VendorCode, search.Status);
                return null;
            }

            return entities;
        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileCompany))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany)
        {
            _logger.LogInformation($"Start CompanyController::Insert", subcontractProfileCompany);

            if (subcontractProfileCompany == null)
                _logger.LogWarning($"Start CompanyController::Insert", subcontractProfileCompany);


            var result = _service.Insert(subcontractProfileCompany);

            if (result == null)
            {
                _logger.LogWarning($"CompanyController::", "Insert NOT FOUND", subcontractProfileCompany);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileCompany))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> subcontractProfileCompanyList)
        {
            _logger.LogInformation($"Start CompanyController::BulkInsert", subcontractProfileCompanyList);

            if (subcontractProfileCompanyList == null)
                _logger.LogWarning($"Start CompanyController::BulkInsert", subcontractProfileCompanyList);


            var result = _service.BulkInsert(subcontractProfileCompanyList);

            if (result == null)
            {
                _logger.LogWarning($"CompanyController::", "BulkInsert NOT FOUND", subcontractProfileCompanyList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany)
        {
            _logger.LogInformation($"Start CompanyController::Update", subcontractProfileCompany);

            if (subcontractProfileCompany == null)
                _logger.LogWarning($"Start CompanyController::Update", subcontractProfileCompany);

            var result = _service.Update(subcontractProfileCompany);

            if (result == null)
            {
                _logger.LogWarning($"CompanyController::", "Update NOT FOUND", subcontractProfileCompany);

            }
            return result;
        }
        [HttpPut("UpdateByActivate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> UpdateByActivate(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany)
        {
            _logger.LogInformation($"Start CompanyController::Update", subcontractProfileCompany);

            if (subcontractProfileCompany == null)
                _logger.LogWarning($"Start CompanyController::Update", subcontractProfileCompany);

            var result = _service.UpdateByActivate(subcontractProfileCompany);

            if (result == null)
            {
                _logger.LogWarning($"CompanyController::", "Update NOT FOUND", subcontractProfileCompany);

            }
            return result;
        }

        [HttpPut("UpdateVerify")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> UpdateVerify(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany)
        {
            _logger.LogInformation($"Start CompanyController::UpdateVerify", subcontractProfileCompany);

            if (subcontractProfileCompany == null)
                _logger.LogWarning($"Start CompanyController::UpdateVerify", subcontractProfileCompany);

            var result = _service.UpdateVerify(subcontractProfileCompany);

            if (result == null)
            {
                _logger.LogWarning($"CompanyController::", "UpdateVerify NOT FOUND", subcontractProfileCompany);

            }
            return result;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Deletes an user entity.
        /// </summary>
        /// <remarks>
        /// No remarks.
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <returns>
        /// Boolean notifying if the user has been deleted properly.
        /// </returns>
        /// <response code="200">Boolean notifying if the user has been deleted properly.</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Delete(string id)
        {
            _logger.LogInformation($"Start CompanyController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start CompanyController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
