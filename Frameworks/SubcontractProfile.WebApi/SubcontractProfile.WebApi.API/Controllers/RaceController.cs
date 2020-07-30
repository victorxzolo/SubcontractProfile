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
    [Route("api/Race")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly ISubcontractProfileRaceRepo _service;
        private readonly ILogger<RaceController> _logger;

        public RaceController(ISubcontractProfileRaceRepo service,
            ILogger<RaceController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RaceController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(RaceController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>> GetAll()
        {

            _logger.LogInformation($"RaceController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"RaceController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileRace))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileRace))]
        [HttpGet("GetByRaceId/{raceId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace> GetByRaceId(string raceId)
        {
            _logger.LogInformation($"Start RaceController::GetByRaceId", raceId);

            var entities = _service.GetByRaceId(raceId);

            if (entities == null)
            {
                _logger.LogWarning($"RaceController::", "GetByRaceId NOT FOUND", raceId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileRace}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileRace))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace subcontractProfileRace)
        {
            _logger.LogInformation($"Start RaceController::Insert", subcontractProfileRace);

            if (subcontractProfileRace == null)
                _logger.LogWarning($"Start RaceController::Insert", subcontractProfileRace);


            var result = _service.Insert(subcontractProfileRace);

            if (result == null)
            {
                _logger.LogWarning($"RaceController::", "Insert NOT FOUND", subcontractProfileRace);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileRaceList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileRace))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace> subcontractProfileRaceList)
        {
            _logger.LogInformation($"Start RaceController::BulkInsert", subcontractProfileRaceList);

            if (subcontractProfileRaceList == null)
                _logger.LogWarning($"Start RaceController::BulkInsert", subcontractProfileRaceList);


            var result = _service.BulkInsert(subcontractProfileRaceList);

            if (result == null)
            {
                _logger.LogWarning($"RaceController::", "BulkInsert NOT FOUND", subcontractProfileRaceList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileRace}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace subcontractProfileRace)
        {
            _logger.LogInformation($"Start RaceController::Update", subcontractProfileRace);

            if (subcontractProfileRace == null)
                _logger.LogWarning($"Start RaceController::Update", subcontractProfileRace);

            var result = _service.Update(subcontractProfileRace);

            if (result == null)
            {
                _logger.LogWarning($"RaceController::", "Update NOT FOUND", subcontractProfileRace);

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
            _logger.LogInformation($"Start RaceController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start RaceController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
