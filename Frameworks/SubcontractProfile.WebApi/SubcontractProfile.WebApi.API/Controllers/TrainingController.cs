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
    [Route("api/Training")]
    [ApiController]
    public class TrainingController : ControllerBase
    {

        private readonly ISubcontractProfileTrainingRepo _service;
        private readonly ILogger<TrainingController> _logger;

        public TrainingController(ISubcontractProfileTrainingRepo service,
            ILogger<TrainingController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainingController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TrainingController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> GetAll()
        {

            _logger.LogInformation($"TrainingController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"TrainingController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTraining))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTraining))]
        [HttpGet("GetByTrainingId/{trainingId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> GetByTrainingId(System.Guid trainingId)
        {
            _logger.LogInformation($"Start TrainingController::GetByTrainingId", trainingId);

            var entities = _service.GetByTrainingId(trainingId);

            if (entities == null)
            {
                _logger.LogWarning($"TrainingController::", "GetByTrainingId NOT FOUND", trainingId);
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTraining))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTraining))]
        [HttpGet("SearchTraining/{trainingId}/{location_code}/{team_id}/{staff_name_th}/{position_id}/{status}/{date_from}/{date_to}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> SearchTraining(string company_id, string location_code,
            string team_id, string staff_name_th, string position_id, string status,
            string date_from, string date_to)
        {
            _logger.LogInformation($"Start TrainingController::SearchTraining", company_id);

            var entities = _service.SearchTraining(company_id, location_code, 
                team_id, staff_name_th, position_id, status, date_from, date_to);

            if (entities == null)
            {
                _logger.LogWarning($"TrainingController::", "SearchTraining NOT FOUND", company_id);
                return null;
            }

            return entities;

        }
        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileTraining}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTraining))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining subcontractProfileTraining)
        {
            _logger.LogInformation($"Start TrainingController::Insert", subcontractProfileTraining);

            if (subcontractProfileTraining == null)
                _logger.LogWarning($"Start TrainingController::Insert", subcontractProfileTraining);


            var result = _service.Insert(subcontractProfileTraining);

            if (result == null)
            {
                _logger.LogWarning($"TrainingController::", "Insert NOT FOUND", subcontractProfileTraining);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileTrainingList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTraining))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> subcontractProfileTrainingList)
        {
            _logger.LogInformation($"Start TrainingController::BulkInsert", subcontractProfileTrainingList);

            if (subcontractProfileTrainingList == null)
                _logger.LogWarning($"Start TrainingController::BulkInsert", subcontractProfileTrainingList);


            var result = _service.BulkInsert(subcontractProfileTrainingList);

            if (result == null)
            {
                _logger.LogWarning($"TrainingController::", "BulkInsert NOT FOUND", subcontractProfileTrainingList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileTraining}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining subcontractProfileTraining)
        {
            _logger.LogInformation($"Start TrainingController::Update", subcontractProfileTraining);

            if (subcontractProfileTraining == null)
                _logger.LogWarning($"Start TrainingController::Update", subcontractProfileTraining);

            var result = _service.Update(subcontractProfileTraining);

            if (result == null)
            {
                _logger.LogWarning($"TrainingController::", "Update NOT FOUND", subcontractProfileTraining);

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
            _logger.LogInformation($"Start TrainingController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start TrainingController::Delete", id);

            return _service.Delete(id);
        }
        #endregion

    }
}
