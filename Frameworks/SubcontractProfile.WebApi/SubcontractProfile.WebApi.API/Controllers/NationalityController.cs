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
    [Route("api/NationalityController")]
    [ApiController]
    public class NationalityController : ControllerBase
    {
        private readonly ISubcontractProfileNationalityRepo _service;
        private readonly ILogger<NationalityController> _logger;

        public NationalityController(ISubcontractProfileNationalityRepo service,
            ILogger<NationalityController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NationalityController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NationalityController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>> GetAll()
        {

            _logger.LogInformation($"NationalityController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"NationalityController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileNationality))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileNationality))]
        [HttpGet("GetByNationalityId/{nationalityId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality> GetByNationalityId(string nationalityId)
        {
            _logger.LogInformation($"Start NationalityController::GetByNationalityId", nationalityId);

            var entities = _service.GetByNationalityId(nationalityId);

            if (entities == null)
            {
                _logger.LogWarning($"NationalityController::", "GetByNationalityId NOT FOUND", nationalityId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileNationality}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileNationality))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality subcontractProfileNationality)
        {
            _logger.LogInformation($"Start NationalityController::Insert", subcontractProfileNationality);

            if (subcontractProfileNationality == null)
                _logger.LogWarning($"Start NationalityController::Insert", subcontractProfileNationality);


            var result = _service.Insert(subcontractProfileNationality);

            if (result == null)
            {
                _logger.LogWarning($"NationalityController::", "Insert NOT FOUND", subcontractProfileNationality);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileNationalityList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileNationality))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality> subcontractProfileNationalityList)
        {
            _logger.LogInformation($"Start NationalityController::BulkInsert", subcontractProfileNationalityList);

            if (subcontractProfileNationalityList == null)
                _logger.LogWarning($"Start NationalityController::BulkInsert", subcontractProfileNationalityList);


            var result = _service.BulkInsert(subcontractProfileNationalityList);

            if (result == null)
            {
                _logger.LogWarning($"NationalityController::", "BulkInsert NOT FOUND", subcontractProfileNationalityList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileNationality}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality subcontractProfileNationality)
        {
            _logger.LogInformation($"Start NationalityController::Update", subcontractProfileNationality);

            if (subcontractProfileNationality == null)
                _logger.LogWarning($"Start NationalityController::Update", subcontractProfileNationality);

            var result = _service.Update(subcontractProfileNationality);

            if (result == null)
            {
                _logger.LogWarning($"NationalityController::", "Update NOT FOUND", subcontractProfileNationality);

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
            _logger.LogInformation($"Start NationalityController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start NationalityController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
