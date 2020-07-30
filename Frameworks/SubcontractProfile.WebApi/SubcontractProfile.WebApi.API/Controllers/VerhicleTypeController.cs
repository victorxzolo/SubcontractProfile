using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.API.Controllers
{
    [Route("api/VerhicleType")]
    [ApiController]
    public class VerhicleTypeController : ControllerBase
    {
        private readonly ISubcontractProfileVerhicleTypeRepo _service;
        private readonly ILogger<VerhicleTypeController> _logger;

        public VerhicleTypeController(ISubcontractProfileVerhicleTypeRepo service,
            ILogger<VerhicleTypeController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VerhicleTypeController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(VerhicleTypeController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>> GetAll()
        {

            _logger.LogInformation($"VerhicleTypeController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"VerhicleTypeController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileVerhicleType))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileVerhicleType))]
        [HttpGet("GetByVerhicleTypeId/{verhicleTypeId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType> GetByVerhicleTypeId(string verhicleTypeId)
        {
            _logger.LogInformation($"Start VerhicleTypeController::GetByVerhicleTypeId", verhicleTypeId);

            var entities = _service.GetByVerhicleTypeId(verhicleTypeId);

            if (entities == null)
            {
                _logger.LogWarning($"VerhicleTypeController::", "GetByVerhicleTypeId NOT FOUND", verhicleTypeId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileVerhicleType}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileVerhicleType))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType subcontractProfileVerhicleType)
        {
            _logger.LogInformation($"Start VerhicleTypeController::Insert", subcontractProfileVerhicleType);

            if (subcontractProfileVerhicleType == null)
                _logger.LogWarning($"Start VerhicleTypeController::Insert", subcontractProfileVerhicleType);


            var result = _service.Insert(subcontractProfileVerhicleType);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleTypeController::", "Insert NOT FOUND", subcontractProfileVerhicleType);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileVerhicleSeriseList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileVerhicleType))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType> subcontractProfileVerhicleTypeList)
        {
            _logger.LogInformation($"Start VerhicleTypeController::BulkInsert", subcontractProfileVerhicleTypeList);

            if (subcontractProfileVerhicleTypeList == null)
                _logger.LogWarning($"Start VerhicleTypeController::BulkInsert", subcontractProfileVerhicleTypeList);


            var result = _service.BulkInsert(subcontractProfileVerhicleTypeList);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleTypeController::", "BulkInsert NOT FOUND", subcontractProfileVerhicleTypeList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileVerhicleType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType subcontractProfileVerhicleType)
        {
            _logger.LogInformation($"Start VerhicleTypeController::Update", subcontractProfileVerhicleType);

            if (subcontractProfileVerhicleType == null)
                _logger.LogWarning($"Start VerhicleTypeController::Update", subcontractProfileVerhicleType);

            var result = _service.Update(subcontractProfileVerhicleType);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleTypeController::", "Update NOT FOUND", subcontractProfileVerhicleType);

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
            _logger.LogInformation($"Start VerhicleTypeController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start VerhicleTypeController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
