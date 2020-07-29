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
    [Route("api/CompanyController")]
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
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> GetByCompanyId(System.Guid companyId)
        {
            _logger.LogInformation($"Start CompanyController::GetByCompanyId", companyId);

            var entities = _service.GetByCompanyId(companyId);

            if (entities == null)
            {
                _logger.LogWarning($"CompanyController::", "GetByCompanyId NOT FOUND", companyId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileCompany}")]
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

        [HttpPost("BulkInsert/{subcontractProfileCompanyList}")]
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
        [HttpPut("Update/{subcontractProfileCompany}")]
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

            if (id == 0)
                _logger.LogWarning($"Start CompanyController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
