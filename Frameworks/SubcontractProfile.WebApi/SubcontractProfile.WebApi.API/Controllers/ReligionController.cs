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
    [Route("api/ReligionController")]
    [ApiController]
    public class ReligionController : ControllerBase
    {
        private readonly ISubcontractProfileReligionRepo _service;
        private readonly ILogger<ReligionController> _logger;

        public ReligionController(ISubcontractProfileReligionRepo service,
            ILogger<ReligionController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReligionController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ReligionController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>> GetAll()
        {

            _logger.LogInformation($"ReligionController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"ReligionController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileReligion))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileReligion))]
        [HttpGet("GetByReligionId/{religionId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion> GetByReligionId(string religionId)
        {
            _logger.LogInformation($"Start ReligionController::GetByReligionId", religionId);

            var entities = _service.GetByReligionId(religionId);

            if (entities == null)
            {
                _logger.LogWarning($"ReligionController::", "GetByReligionId NOT FOUND", religionId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileReligion}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileReligion))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion subcontractProfileReligion)
        {
            _logger.LogInformation($"Start ReligionController::Insert", subcontractProfileReligion);

            if (subcontractProfileReligion == null)
                _logger.LogWarning($"Start ReligionController::Insert", subcontractProfileReligion);


            var result = _service.Insert(subcontractProfileReligion);

            if (result == null)
            {
                _logger.LogWarning($"ReligionController::", "Insert NOT FOUND", subcontractProfileReligion);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileReligionList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileReligion))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion> subcontractProfileReligionList)
        {
            _logger.LogInformation($"Start ReligionController::BulkInsert", subcontractProfileReligionList);

            if (subcontractProfileReligionList == null)
                _logger.LogWarning($"Start ReligionController::BulkInsert", subcontractProfileReligionList);


            var result = _service.BulkInsert(subcontractProfileReligionList);

            if (result == null)
            {
                _logger.LogWarning($"ReligionController::", "BulkInsert NOT FOUND", subcontractProfileReligionList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileReligion}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion subcontractProfileReligion)
        {
            _logger.LogInformation($"Start ReligionController::Update", subcontractProfileReligion);

            if (subcontractProfileReligion == null)
                _logger.LogWarning($"Start ReligionController::Update", subcontractProfileReligion);

            var result = _service.Update(subcontractProfileReligion);

            if (result == null)
            {
                _logger.LogWarning($"ReligionController::", "Update NOT FOUND", subcontractProfileReligion);

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
            _logger.LogInformation($"Start ReligionController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start ReligionController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
