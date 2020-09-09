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


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTrainingRequest))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTrainingRequest))]
        [HttpGet("GetByTrainingId/{trainingId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingRequest> GetByTrainingId(System.Guid trainingId)
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
        [HttpGet("SearchTraining/{company_Id}/{status}/{date_from}/{date_to}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> SearchTraining(Guid company_id,
            string status, string date_from, string date_to)

        {
            _logger.LogInformation($"Start TrainingController::SearchTraining", company_id, status, date_from, date_to);

          
            if (status.ToUpper()== "NULL")
            {
                status = string.Empty;
            }

            if (date_from.ToUpper() == "NULL")
            {
                date_from = string.Empty;
            }

            if (date_to.ToUpper() == "NULL")
            {
                date_to = string.Empty;
            }

           
            var entities = _service.SearchTraining(company_id, status, date_from, date_to);

            if (entities == null)
            {
                _logger.LogWarning($"TrainingController::", "SearchTraining NOT FOUND", company_id,status, date_from, date_to);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTraining))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTraining))]
        [HttpGet("SearchTrainingForApprove/{company_name_th}/{tax_id}/{status}/{date_from}/{date_to}/{bookingdate_from}/{bookingdate_to}")]

        public  Task<IEnumerable<SubcontractProfileTraining>> SearchTrainingForApprove(string company_name_th,
           string tax_id, string status, string date_from, string date_to,
           string bookingdate_from, string bookingdate_to)
        {

            _logger.LogInformation($"Start TrainingController::SearchTrainingForApprove", company_name_th, tax_id,
                 status, date_from, date_to, bookingdate_from, bookingdate_to);

            if (company_name_th.ToUpper() == "NULL")
            {
                company_name_th = string.Empty;
            }

            if (tax_id.ToUpper() == "NULL")
            {
                tax_id = string.Empty;
            }

            if (status.ToUpper() == "NULL")
            {
                status = string.Empty;
            }

            if (date_from.ToUpper() == "NULL")
            {
                date_from = string.Empty;
            }

            if (date_to.ToUpper() == "NULL")
            {
                date_to = string.Empty;
            }

            if (bookingdate_from.ToUpper() == "NULL")
            {
                bookingdate_from = string.Empty;
            }

            if (bookingdate_to.ToUpper() == "NULL")
            {
                bookingdate_to = string.Empty;
            }

            var entities = _service.SearchTrainingForApprove(company_name_th, tax_id,
                 status, date_from, date_to, bookingdate_from, bookingdate_to);

            if (entities == null)
            {
                _logger.LogWarning($"TrainingController::", "SearchTrainingForApprove NOT FOUND", company_name_th, tax_id,
                 status, date_from, date_to, bookingdate_from, bookingdate_to);
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTraining))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTraining))]
        [HttpGet("SearchTrainingForTest/{company_name_th}/{tax_id}/{training_date_fr}/{training_date_to}/{test_date_fr}/{test_date_to}")]

        public Task<IEnumerable<SubcontractProfileTraining>> SearchTrainingForTest(string company_name_th,
           string tax_id, string training_date_fr, string training_date_to, string test_date_fr, string test_date_to)
        {
            _logger.LogInformation($"Start TrainingController::SearchTrainingForTest", company_name_th, tax_id,
            training_date_fr, training_date_to, test_date_fr, test_date_to);

            if (company_name_th.ToUpper() == "NULL")
            {
                company_name_th = string.Empty;
            }

            if (tax_id.ToUpper() == "NULL")
            {
                tax_id = string.Empty;
            }

            if (training_date_fr.ToUpper() == "NULL")
            {
                training_date_fr = string.Empty;
            }

            if (training_date_to.ToUpper() == "NULL")
            {
                training_date_to = string.Empty;
            }

            if (test_date_fr.ToUpper() == "NULL")
            {
                test_date_fr = string.Empty;
            }

            if (test_date_to.ToUpper() == "NULL")
            {
                test_date_to = string.Empty;
            }


            var entities = _service.SearchTrainingForTest(company_name_th, tax_id,
                training_date_fr, training_date_to, test_date_fr, test_date_to);

            if (entities == null)
            {
                _logger.LogWarning($"TrainingController::", "SearchTrainingForTest NOT FOUND", company_name_th, tax_id,
            training_date_fr, training_date_to, test_date_fr, test_date_to);
                return null;
            }

            return entities;
        }
        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTrainingRequest))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingRequest subcontractProfileTraining)
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

        [HttpPost("BulkInsert")]
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
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingRequest subcontractProfileTraining)
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

        [HttpPut("UpdateByVerified")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public  Task<bool> UpdateByVerified(SubcontractProfileTrainingRequest subcontractProfileTraining)
        {
            _logger.LogInformation($"Start TrainingController::UpdateByVerified", subcontractProfileTraining);


            if (subcontractProfileTraining == null)
                _logger.LogWarning($"Start TrainingController::UpdateByVerified", subcontractProfileTraining);

            var result = _service.UpdateByVerified(subcontractProfileTraining);

            if (result == null)
            {
                _logger.LogWarning($"TrainingController::", "UpdateByVerified NOT FOUND", subcontractProfileTraining);

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
