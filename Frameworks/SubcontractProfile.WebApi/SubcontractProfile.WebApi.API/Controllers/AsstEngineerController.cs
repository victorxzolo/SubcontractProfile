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
    [Route("api/AsstEngineer")]
    [ApiController]
    public class AsstEngineerController : ControllerBase
    {
        private readonly ISubcontractProfileAsstEngineerRepo _service;
        private readonly ILogger<AsstEngineerController> _logger;

        public AsstEngineerController(ISubcontractProfileAsstEngineerRepo service,
            ILogger<AsstEngineerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAsstEngineer))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAsstEngineer))]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<SubcontractProfileAsstEngineer>> GetAll()
        {

            _logger.LogInformation($"AsstEngineerController::GetALL");

            var entities = await _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"AsstEngineerController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAsstEngineer))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAsstEngineer))]
        [HttpGet("GetByAsstEngineerId/{asstEngineerId}")]
        public async Task<SubcontractProfileAsstEngineer> GetByAsstEngineerId(string asstEngineerId)
        {
            _logger.LogInformation($"Start AsstEngineerController::GetByAsstEngineerId", asstEngineerId);

            var entities = await _service.GetByAsstEngineerId(asstEngineerId);

            if (entities == null)
            {
                _logger.LogWarning($"AsstEngineerController::", "GetByAsstEngineerId NOT FOUND", asstEngineerId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAsstEngineer))]
        public Task<bool> Insert(SubcontractProfileAsstEngineer SubcontractProfileAsstEngineer)
        {
            _logger.LogInformation($"Start AsstEngineerController::Insert", SubcontractProfileAsstEngineer);

            if (SubcontractProfileAsstEngineer == null)
                _logger.LogWarning($"Start AsstEngineerController::Insert", SubcontractProfileAsstEngineer);


            var result = _service.Insert(SubcontractProfileAsstEngineer);

            if (result == null)
            {
                _logger.LogWarning($"AsstEngineerController::", "Insert NOT FOUND", SubcontractProfileAsstEngineer);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAsstEngineer))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfileAsstEngineer> subcontractProfileAddressList)
        {
            _logger.LogInformation($"Start AsstEngineerController::BulkInsert", subcontractProfileAddressList);

            if (subcontractProfileAddressList == null)
                _logger.LogWarning($"Start AsstEngineerController::BulkInsert", subcontractProfileAddressList);


            var result = _service.BulkInsert(subcontractProfileAddressList);

            if (result == null)
            {
                _logger.LogWarning($"AsstEngineerController::", "BulkInsert NOT FOUND", subcontractProfileAddressList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfileAsstEngineer SubcontractProfileAsstEngineer)
        {
            _logger.LogInformation($"Start AsstEngineerController::Update", SubcontractProfileAsstEngineer);

            if (SubcontractProfileAsstEngineer == null)
                _logger.LogWarning($"Start AsstEngineerController::Update", SubcontractProfileAsstEngineer);

            var result = _service.Update(SubcontractProfileAsstEngineer);

            if (result == null)
            {
                _logger.LogWarning($"AsstEngineerController::", "Update NOT FOUND", SubcontractProfileAsstEngineer);

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
        public async Task<bool> Delete(string id)
        {
            _logger.LogInformation($"Start AsstEngineerController::Delete", id);

            if (id == null)
                _logger.LogWarning($"Start AsstEngineerController::Delete", id);

            return await _service.Delete(id);
        }
        #endregion

        #region Excepions
        [HttpGet("exception/{message}")]
        [ProducesErrorResponseType(typeof(Exception))]
        public async Task RaiseException(string message)
        {
            _logger.LogDebug($"UserControllers::RaiseException::{message}");

            throw new Exception(message);
        }
        #endregion
    }
}
