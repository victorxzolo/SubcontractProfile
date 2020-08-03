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
    [Route("api/Team")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ISubcontractProfileTeamRepo _service;
        private readonly ILogger<TeamController> _logger;

        public TeamController(ISubcontractProfileTeamRepo service,
            ILogger<TeamController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TeamController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> GetAll()
        {

            _logger.LogInformation($"TeamController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"TeamController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTeam))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTeam))]
        [HttpGet("GetByTeamId/{teamId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> GetByTeamId(System.Guid teamId)
        {
            _logger.LogInformation($"Start TeamController::GetByTeamId", teamId);

            var entities = _service.GetByTeamId(teamId);

            if (entities == null)
            {
                _logger.LogWarning($"TeamController::", "GetByTeamId NOT FOUND", teamId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTeam))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam)
        {
            _logger.LogInformation($"Start TeamController::Insert", subcontractProfileTeam);

            if (subcontractProfileTeam == null)
                _logger.LogWarning($"Start TeamController::Insert", subcontractProfileTeam);


            var result = _service.Insert(subcontractProfileTeam);

            if (result == null)
            {
                _logger.LogWarning($"TeamController::", "Insert NOT FOUND", subcontractProfileTeam);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTeam))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> subcontractProfileTeamList)
        {
            _logger.LogInformation($"Start TeamController::BulkInsert", subcontractProfileTeamList);

            if (subcontractProfileTeamList == null)
                _logger.LogWarning($"Start TeamController::BulkInsert", subcontractProfileTeamList);


            var result = _service.BulkInsert(subcontractProfileTeamList);

            if (result == null)
            {
                _logger.LogWarning($"TeamController::", "BulkInsert NOT FOUND", subcontractProfileTeamList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam)
        {
            _logger.LogInformation($"Start TeamController::Update", subcontractProfileTeam);

            if (subcontractProfileTeam == null)
                _logger.LogWarning($"Start TeamController::Update", subcontractProfileTeam);

            var result = _service.Update(subcontractProfileTeam);

            if (result == null)
            {
                _logger.LogWarning($"TeamController::", "Update NOT FOUND", subcontractProfileTeam);

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
            _logger.LogInformation($"Start TeamController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start TeamController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
