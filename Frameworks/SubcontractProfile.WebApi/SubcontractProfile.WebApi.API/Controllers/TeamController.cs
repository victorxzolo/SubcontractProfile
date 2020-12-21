using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly ISubcontractProfileLocationRepo _serviceLoc;
        private readonly ILogger<TeamController> _logger;

        public TeamController(ISubcontractProfileTeamRepo service, ISubcontractProfileLocationRepo serviceLoc,
            ILogger<TeamController> logger)
        {
            _service = service;
            _serviceLoc = serviceLoc;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TeamController))]
        [HttpGet("SearchTeam/{companyId}/{locationId}/{teamcode}/{teamNameTh}/{teamNameEn}/{storageLocation}/{shipto}")]
        public  Task<IEnumerable<SubcontractProfileTeam>> SearchTeam(Guid companyId, string locationId
          , string teamcode, string teamNameTh, string teamNameEn,
          string storageLocation, string shipto)
        {
            _logger.LogInformation($"Start TeamController::SearchTeam", companyId, locationId,
                teamcode, teamNameEn, storageLocation, shipto);

            Guid gLocationId;
            if (locationId.ToUpper() == "-1")
            {
                gLocationId = Guid.Empty;
            }
            else
            {
                gLocationId = new Guid(locationId);
            }

            if (teamcode.ToUpper() == "NULL")
            {
                teamcode = string.Empty;
            }


            if (teamNameTh.ToUpper() == "NULL")
            {
                teamNameTh = string.Empty;
            }

            if (teamNameEn.ToUpper() == "NULL")
            {
                teamNameEn = string.Empty;
            }

            if (storageLocation.ToUpper() == "NULL")
            {
                storageLocation = string.Empty;
            }

            if (shipto.ToUpper() == "NULL")
            {
                shipto = string.Empty;
            }
       

            var entities = _service.SearchTeam(companyId, gLocationId, teamcode, 
                teamNameTh, teamNameEn, storageLocation, shipto);

            if (entities == null)
            {
                _logger.LogWarning($"TeamController::", "SearchTeam NOT FOUND", companyId, locationId,
                teamcode, teamNameEn, storageLocation, shipto);
                return null;
            }

            return entities;

        }

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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileLocation))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileLocation))]
        [HttpGet("GetLocationByCompany/{companyId}")]
        public Task<IEnumerable<SubcontractProfileLocation>> GetLocationByCompany(System.Guid companyId)
        {
            _logger.LogInformation($"Start TeamController::GetLocationByCompany");

            var entities = _serviceLoc.GetLocationByCompany(companyId);

            if (entities == null)
            {
                _logger.LogWarning($"TeamController::", "GetLocationByCompany NOT FOUND");
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTeam))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTeam))]
        [HttpGet("selectTeamAll/{team_code}/{location_code}/{vendor_code}/{date_from}/{date_to}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> selectLocationAll(
           string team_code,     string location_code, string vendor_code, string date_from, string date_to)
        {
            _logger.LogInformation($"Start TeamController::selectTeamAll", team_code, location_code, vendor_code, date_from, date_to);

            if (team_code.ToUpper() == "NULL")
            {
                team_code = string.Empty;
            }

            if (location_code.ToUpper() == "NULL")
            {
                location_code = string.Empty;
            }

            if (vendor_code.ToUpper() == "NULL")
            {
                vendor_code = string.Empty;
            }

            if (date_from.ToUpper() == "NULL")
            {
                date_from = string.Empty;
            }

            if (date_to.ToUpper() == "NULL")
            {
                date_to = string.Empty;
            }


            var entities = _service.selectTeamAll(team_code, vendor_code, location_code, date_from, date_to);

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "selectLocationAll NOT FOUND", team_code  , location_code, vendor_code, date_from, date_to);
                return null;
            }

            return entities;

        }



        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileLocation))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileLocation))]
        [HttpGet("GetByLocationId/{companyId}/{locationId}")]
        public  Task<IEnumerable<SubcontractProfileTeam>> GetByLocationId(Guid companyId, Guid locationId)
        {
            _logger.LogInformation($"Start TeamController::GetByLocationId");

            var entities = _service.GetByLocationId(companyId, locationId);

            if (entities == null)
            {
                _logger.LogWarning($"TeamController::", "GetByLocationId NOT FOUND");
                return null;
            }

            return entities;
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTeamServiceSkill))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTeamServiceSkill))]
        [HttpGet("GetServiceSkillByTeamId/{teamId}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeamServiceSkill>> GetServiceSkillByTeamId(System.Guid teamId)
        {
            _logger.LogInformation($"Start TeamController::GetServiceSkillByTeamId", teamId);

            var entities = _service.GetServiceSkillByTeamId(teamId);

            if (entities == null)
            {
                _logger.LogWarning($"TeamController::", "GetServiceSkillByTeamId NOT FOUND", teamId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTeam))]
        public Task<System.Guid> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam)
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


        [HttpPost("InsertTeamServiceSkill")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTeam))]
        public Task<bool> InsertTeamServiceSkill(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeamServiceSkill skill)
        {
            _logger.LogInformation($"Start TeamController::InsertTeamServiceSkill", skill);

            if (skill == null)
                _logger.LogWarning($"Start TeamController::InsertTeamServiceSkill", skill);


            var result = _service.InsertTeamServiceSkill(skill);

            if (result == null)
            {
                _logger.LogWarning($"TeamController::", "Insert NOT FOUND", skill);

            }
            return result;

        }

        #endregion

        #region PUT


        [HttpPost("MigrationInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTeam))]
        public Task<bool> MigrationInsert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam)
        {
            _logger.LogInformation($"Start LocationController::MigrationInsert", subcontractProfileTeam);

            if (subcontractProfileTeam == null)
                _logger.LogWarning($"Start LocationController::MigrationInsert", subcontractProfileTeam);


            var result = _service.MigrationInsert(subcontractProfileTeam);

            if (result == null)
            {
                _logger.LogWarning($"LocationController::", "Insert NOT FOUND", subcontractProfileTeam);

            }
            return result;

        }


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

        [HttpDelete("DeleteTeamServiceSkill/{teamid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> DeleteTeamServiceSkill(string teamid)
        {
            _logger.LogInformation($"Start TeamController::DeleteTeamServiceSkill", teamid);

            if (teamid == "")
                _logger.LogWarning($"Start TeamController::DeleteTeamServiceSkill", teamid);

            return _service.DeleteTeamServiceSkill(teamid);
        }
        #endregion
    }
}
