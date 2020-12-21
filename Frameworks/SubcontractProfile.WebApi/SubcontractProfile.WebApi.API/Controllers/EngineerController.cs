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
        [HttpGet("SearchEngineer/{companyId}/{locationId}/{teamId}/{staffName}/{citizenId}/{position}")]
        public  Task<IEnumerable<SubcontractProfileEngineer>> SearchEngineer(Guid companyId, Guid locationId,
           Guid teamId, string staffName, string citizenId, string position)
        {
            _logger.LogInformation($"Start EngineerController::SearchEngineer", companyId, locationId,
                teamId, staffName, citizenId, position);


            if (staffName.ToUpper() == "NULL")
            {
                staffName = string.Empty;
            }

            if (citizenId.ToUpper() == "NULL")
            {
                citizenId = string.Empty;
            }

            if (position.ToUpper() == "NULL")
            {
                position = string.Empty;
            }


            var entities = _service.SearchEngineer(companyId, locationId, teamId,
                staffName, citizenId, position);

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "SearchEngineer NOT FOUND", companyId, locationId,
                teamId, staffName, citizenId, position);
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EngineerController))]
        [HttpGet("GetEngineerByTeam/{companyId}/{locationId}/{teamId}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetEngineerByTeam(System.Guid companyId,
            System.Guid locationId, System.Guid teamId)
        {
            _logger.LogInformation($"Start EngineerController::GetEngineerByTeam", companyId, locationId,
               teamId);

          
            var entities = _service.GetEngineerByTeam(companyId, locationId, teamId);

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "GetEngineerByTeam NOT FOUND", companyId, locationId,
                teamId);
                return null;
            }

            return entities;
        }




        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EngineerController))]
        [HttpGet("selectEngineer/{citizen_id}/{staff_name}/{contact_phone}/{date_from}/{date_to}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> selectEngineer(
                 string citizen_id, string staff_name, string contact_phone, string date_from, string date_to)
        {
            _logger.LogInformation($"Start EngineerController::selectEngineer", citizen_id, staff_name, contact_phone, date_from, date_to);

            if (citizen_id.ToUpper() == "NULL")
            {
                citizen_id = string.Empty;
            }

            if (staff_name.ToUpper() == "NULL")
            {
                staff_name = string.Empty;
            }

            if (contact_phone.ToUpper() == "NULL")
            {
                contact_phone = string.Empty;
            }

            if (date_from.ToUpper() == "NULL")
            {
                date_from = string.Empty;
            }

            if (date_to.ToUpper() == "NULL")
            {
                date_to = string.Empty;
            }


            var entities = _service.selectEngineer(citizen_id, staff_name, contact_phone, date_from, date_to);

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "selectEngineer NOT FOUND", citizen_id, staff_name, contact_phone, date_from, date_to);
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EngineerController))]
        [HttpGet("selectEngineerAll/{citizen_id}/{staff_name}/{contract_phone}/{date_from}/{date_to}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> selectEngineerAll(
          string citizen_id ,string staff_name ,string contract_phone , string date_from ,string date_to)
        {
            _logger.LogInformation($"Start EngineerController::selectEngineerAll", citizen_id, staff_name,
               contract_phone, date_from , date_to);


            var entities = _service.selectEngineerAll(citizen_id, staff_name, contract_phone,date_from, date_to);

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "GetEngineerByTeam NOT FOUND", citizen_id, staff_name,
               contract_phone, date_from, date_to);
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EngineerController))]
        [HttpGet("GetEngineerByCompany/{companyId}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetEngineerByCompany(System.Guid companyId)
        {
            _logger.LogInformation($"Start EngineerController::GetEngineerByTeam", companyId);


            var entities = _service.GetEngineerByCompany(companyId);

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "GetEngineerByCompany NOT FOUND", companyId);
                return null;
            }

            return entities;
        }

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


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileEngineer))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileEngineer))]
        [HttpPost("CheckBlackList")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineerBlacklist>> CheckBlackList(SubcontractProfileEngineerBlacklist model)
        {
            _logger.LogInformation($"Start EngineerController::CheckBackList", model.id_card);

            var entities = _service.CheckBlacklist(model.id_card);

            if (entities == null)
            {
                _logger.LogWarning($"EngineerController::", "CheckBackList NOT FOUND", model.id_card);
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





        [HttpPost("MigrationInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileEngineer))]
        public Task<bool> MigrationInsert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer)
        {
            _logger.LogInformation($"Start EngineerController::MigrationInsert", subcontractProfileEngineer);

            if (subcontractProfileEngineer == null)
                _logger.LogWarning($"Start EngineerController::MigrationInsert", subcontractProfileEngineer);


            var result = _service.MigrationInsert(subcontractProfileEngineer);

            if (result == null)
            {
                _logger.LogWarning($"EngineerController::", "Insert NOT FOUND", subcontractProfileEngineer);

            }
            return result;

        }

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
