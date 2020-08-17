using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.API.Controllers
{
    [Route("api/CompanyType")]
    [ApiController]
    public class CompanyTypeController : ControllerBase
    {
        private readonly ISubcontractProfileCompanyTypeRepo _service;
        private readonly ILogger<CompanyTypeController> _logger;

        public CompanyTypeController(ISubcontractProfileCompanyTypeRepo service,
            ILogger<CompanyTypeController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyTypeController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CompanyTypeController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>> GetAll()
        {

            _logger.LogInformation($"CompanyTypeController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"CompanyTypeController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileCompanyType))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileCompanyType))]
        [HttpGet("GetByCompanyTypeId/{companyTypeId}")]
        public  Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType> GetByCompanyTypeId(string companyTypeId)
        {
            _logger.LogInformation($"Start CompanyTypeController::GetByCompanyTypeId", companyTypeId);

            var entities = _service.GetByCompanyTypeId(companyTypeId);

            if (entities == null)
            {
                _logger.LogWarning($"CompanyTypeController::", "GetByCompanyTypeId NOT FOUND", companyTypeId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileCompanyType))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType SubcontractProfileCompanyType)
        {
            _logger.LogInformation($"Start CompanyTypeController::Insert", SubcontractProfileCompanyType);

            if (SubcontractProfileCompanyType == null)
                _logger.LogWarning($"Start CompanyTypeController::Insert", SubcontractProfileCompanyType);


            var result = _service.Insert(SubcontractProfileCompanyType);

            if (result == null)
            {
                _logger.LogWarning($"CompanyTypeController::", "Insert NOT FOUND", SubcontractProfileCompanyType);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileCompanyType))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType> SubcontractProfileCompanyTypeList)
        {
            _logger.LogInformation($"Start CompanyTypeController::BulkInsert", SubcontractProfileCompanyTypeList);

            if (SubcontractProfileCompanyTypeList == null)
                _logger.LogWarning($"Start CompanyTypeController::BulkInsert", SubcontractProfileCompanyTypeList);


            var result = _service.BulkInsert(SubcontractProfileCompanyTypeList);

            if (result == null)
            {
                _logger.LogWarning($"CompanyTypeController::", "BulkInsert NOT FOUND", SubcontractProfileCompanyTypeList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType SubcontractProfileCompanyType)
        {
            _logger.LogInformation($"Start CompanyTypeController::Update", SubcontractProfileCompanyType);

            if (SubcontractProfileCompanyType == null)
                _logger.LogWarning($"Start CompanyTypeController::Update", SubcontractProfileCompanyType);

            var result = _service.Update(SubcontractProfileCompanyType);

            if (result == null)
            {
                _logger.LogWarning($"CompanyTypeController::", "Update NOT FOUND", SubcontractProfileCompanyType);

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
            _logger.LogInformation($"Start CompanyTypeController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start CompanyTypeController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
