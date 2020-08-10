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
    [Route("api/Personal")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly ISubcontractProfilePersonalRepo _service;
        private readonly ILogger<PersonalController> _logger;

        public PersonalController(ISubcontractProfilePersonalRepo service,
            ILogger<PersonalController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonalController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PersonalController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>> GetAll()
        {

            _logger.LogInformation($"PersonalController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"PersonalController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfilePersonal))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfilePersonal))]
        [HttpGet("GetByPersonalId/{personalId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal> GetByPersonalId(System.Guid personalId)
        {
            _logger.LogInformation($"Start PersonalController::GetByPersonalId", personalId);

            var entities = _service.GetByPersonalId(personalId);

            if (entities == null)
            {
                _logger.LogWarning($"PersonalController::", "GetByPaymentId NOT FOUND", personalId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfilePersonal))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal subcontractProfilePersonal)
        {
            _logger.LogInformation($"Start PersonalController::Insert", subcontractProfilePersonal);

            if (subcontractProfilePersonal == null)
                _logger.LogWarning($"Start PersonalController::Insert", subcontractProfilePersonal);


            var result = _service.Insert(subcontractProfilePersonal);

            if (result == null)
            {
                _logger.LogWarning($"PersonalController::", "Insert NOT FOUND", subcontractProfilePersonal);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfilePersonal))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal> subcontractProfilePersonalList)
        {
            _logger.LogInformation($"Start PersonalController::BulkInsert", subcontractProfilePersonalList);

            if (subcontractProfilePersonalList == null)
                _logger.LogWarning($"Start PersonalController::BulkInsert", subcontractProfilePersonalList);


            var result = _service.BulkInsert(subcontractProfilePersonalList);

            if (result == null)
            {
                _logger.LogWarning($"PersonalController::", "BulkInsert NOT FOUND", subcontractProfilePersonalList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal subcontractProfilePersonal)
        {
            _logger.LogInformation($"Start PersonalController::Update", subcontractProfilePersonal);

            if (subcontractProfilePersonal == null)
                _logger.LogWarning($"Start PersonalController::Update", subcontractProfilePersonal);

            var result = _service.Update(subcontractProfilePersonal);

            if (result == null)
            {
                _logger.LogWarning($"PersonalController::", "Update NOT FOUND", subcontractProfilePersonal);

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
            _logger.LogInformation($"Start PersonalController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start PersonalController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
