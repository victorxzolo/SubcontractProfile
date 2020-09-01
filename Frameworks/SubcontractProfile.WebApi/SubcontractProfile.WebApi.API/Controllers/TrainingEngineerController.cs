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
    [Route("api/TrainingEngineer")]
    [ApiController]
   
    public class TrainingEngineerController : ControllerBase
    {
        private readonly ISubcontractProfileTrainingEngineerRepo  _service;
        private readonly ILogger<TrainingEngineerController> _logger;

        public TrainingEngineerController(ISubcontractProfileTrainingEngineerRepo service,
            ILogger<TrainingEngineerController> logger)
        {
            _service = service;
            _logger = logger;
        }


        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainingEngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TrainingEngineerController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>> GetAll()
        {

            _logger.LogInformation($"TrainingEngineerController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"TrainingEngineerController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainingEngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TrainingEngineerController))]
        [HttpGet("GetByTrainingEngineerId/{trainingEngineerId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer> GetByTrainingEngineerId(System.Guid trainingEngineerId)
        {
            _logger.LogInformation($"Start TrainingEngineerController::GetByTrainingEngineerId", trainingEngineerId);

            var entities = _service.GetByTrainingEngineerId(trainingEngineerId);

            if (entities == null)
            {
                _logger.LogWarning($"TrainingEngineerController::", "GetByTrainingEngineerId NOT FOUND", trainingEngineerId);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainingEngineerController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TrainingEngineerController))]
        [HttpGet("GetTrainingEngineerByTrainingId/{training_Id}")]

        public  Task<IEnumerable<SubcontractProfileTrainingEngineer>> GetTrainingEngineerByTrainingId(Guid training_Id)
        {

            _logger.LogInformation($"Start TrainingEngineerController::GetTrainingEngineerByTrainingId", training_Id);

            if (training_Id == Guid.Empty)
            {
                training_Id = Guid.Empty;
            }

            var entities = _service.GetTrainingEngineerByTrainingId(training_Id);

            if (entities == null)
            {
                _logger.LogWarning($"TrainingEngineerController::", "GetTrainingEngineerByTrainingId NOT FOUND", training_Id);
                return null;
            }

            return entities;
        }

        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrainingEngineerController))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer SubcontractProfileTrainingEngineer)
        {
            _logger.LogInformation($"Start TrainingEngineerController::Insert", SubcontractProfileTrainingEngineer);

            if (SubcontractProfileTrainingEngineer == null)
                _logger.LogWarning($"Start TrainingEngineerController::Insert", SubcontractProfileTrainingEngineer);


            var result = _service.Insert(SubcontractProfileTrainingEngineer);

            if (result == null)
            {
                _logger.LogWarning($"TrainingEngineerController::", "Insert NOT FOUND", SubcontractProfileTrainingEngineer);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrainingEngineerController))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer> SubcontractProfileTrainingEngineerList)
        {
            _logger.LogInformation($"Start TrainingEngineerController::BulkInsert", SubcontractProfileTrainingEngineerList);

            if (SubcontractProfileTrainingEngineerList == null)
                _logger.LogWarning($"Start TrainingEngineerController::BulkInsert", SubcontractProfileTrainingEngineerList);


            var result = _service.BulkInsert(SubcontractProfileTrainingEngineerList);

            if (result == null)
            {
                _logger.LogWarning($"TrainingEngineerController::", "BulkInsert NOT FOUND", SubcontractProfileTrainingEngineerList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer SubcontractProfileTrainingEngineer)
        {
            _logger.LogInformation($"Start TrainingEngineerController::Update", SubcontractProfileTrainingEngineer);

            if (SubcontractProfileTrainingEngineer == null)
                _logger.LogWarning($"Start TrainingEngineerController::Update", SubcontractProfileTrainingEngineer);

            var result = _service.Update(SubcontractProfileTrainingEngineer);

            if (result == null)
            {
                _logger.LogWarning($"TrainingEngineerController::", "Update NOT FOUND", SubcontractProfileTrainingEngineer);

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
        public Task<bool> Delete(System.Guid id)
        {
            _logger.LogInformation($"Start TrainingEngineerController::Delete", id);

            if (id == Guid.Empty)
                _logger.LogWarning($"Start TrainingEngineerController::Delete", id);

            return _service.Delete(id);
        }
        #endregion

    }
}
