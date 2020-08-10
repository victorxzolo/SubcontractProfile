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
    [Route("api/Engineer")]
    [ApiController]
    public class EngineerController : ControllerBase
    {
        private readonly ISubcontractProfileEngineerRepo _service;
        private readonly ILogger<EngineerController> _logger;

        public EngineerController(ISubcontractProfileEngineerRepo service,
            ILogger<EngineerController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EngineerController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetAll()
        {

            _logger.LogInformation($"EngineerController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileEngineer))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileEngineer))]
        [HttpGet("GetByEngineerId/{engineerId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> GetByEngineerId(System.Guid engineerId)
        {
            _logger.LogInformation($"Start EngineerController::GetByEngineerId", engineerId);

            var entities = _service.GetByEngineerId(engineerId);

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "GetByEngineerId NOT FOUND", engineerId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileEngineer))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer)
        {
            _logger.LogInformation($"Start EngineerController::Insert", subcontractProfileEngineer);

            if (subcontractProfileEngineer == null)
                _logger.LogWarning($"Start EngineerController::Insert", subcontractProfileEngineer);


            var result = _service.Insert(subcontractProfileEngineer);

            if (result == null)
            {
                _logger.LogWarning($"EngineerController::", "Insert NOT FOUND", subcontractProfileEngineer);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileEngineer))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> subcontractProfileEngineerList)
        {
            _logger.LogInformation($"Start EngineerController::BulkInsert", subcontractProfileEngineerList);

            if (subcontractProfileEngineerList == null)
                _logger.LogWarning($"Start EngineerController::BulkInsert", subcontractProfileEngineerList);


            var result = _service.BulkInsert(subcontractProfileEngineerList);

            if (result == null)
            {
                _logger.LogWarning($"EngineerController::", "BulkInsert NOT FOUND", subcontractProfileEngineerList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer)
        {
            _logger.LogInformation($"Start EngineerController::Update", subcontractProfileEngineer);

            if (subcontractProfileEngineer == null)
                _logger.LogWarning($"Start EngineerController::Update", subcontractProfileEngineer);

            var result = _service.Update(subcontractProfileEngineer);

            if (result == null)
            {
                _logger.LogWarning($"EngineerController::", "Update NOT FOUND", subcontractProfileEngineer);

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
            _logger.LogInformation($"Start EngineerController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start EngineerController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
