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
    [Route("api/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly ISubcontractProfilePaymentRepo _service;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ISubcontractProfilePaymentRepo service,
            ILogger<PaymentController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PaymentController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>> GetAll()
        {

            _logger.LogInformation($"PaymentController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"PaymentController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfilePayment))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfilePayment))]
        [HttpGet("GetByPaymentId/{paymentId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment> GetByPaymentId(string paymentId)
        {
            _logger.LogInformation($"Start PaymentController::GetByPaymentId", paymentId);

            var entities = _service.GetByPaymentId(paymentId);

            if (entities == null)
            {
                _logger.LogWarning($"PaymentController::", "GetByPaymentId NOT FOUND", paymentId);
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfilePayment))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfilePayment))]
        [HttpGet("SearchPayment/{payment_no}/{request_training_no}/{request_date_from}/{request_date_to}/{payment_date_from}/{payment_date_to}/{payment_status}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>> SearchPayment(string payment_no,
           string request_training_no, string request_date_from, string request_date_to, string payment_date_from,
           string payment_date_to, string payment_status)
        {
            _logger.LogInformation($"Start PaymentController::SearchPayment", payment_no, request_training_no, request_date_from,
                request_date_to, payment_date_from, payment_date_to, payment_status);
        
            if(payment_no =="NULL")
            {
                payment_no = "";
            }

            if (request_training_no == "NULL")
            {
                request_training_no = "";
            }

            if (payment_status == "NULL")
            {
                payment_status = "";
            }
            var entities = _service.searchPayment(payment_no, request_training_no, request_date_from, request_date_to, payment_date_from, payment_date_to, payment_status);

            if (entities == null)
            {
                _logger.LogWarning($"PaymentController::", "GetByPaymentId NOT FOUND", payment_no, request_training_no, request_date_from,
                request_date_to, payment_date_from, payment_date_to, payment_status);
                return null;
            }

            return entities;

        }

        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfilePayment))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment subcontractProfilePayment)
        {
            _logger.LogInformation($"Start PaymentController::Insert", subcontractProfilePayment);

            if (subcontractProfilePayment == null)
                _logger.LogWarning($"Start PaymentController::Insert", subcontractProfilePayment);


            var result = _service.Insert(subcontractProfilePayment);

            if (result == null)
            {
                _logger.LogWarning($"PaymentController::", "Insert NOT FOUND", subcontractProfilePayment);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfilePayment))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment> subcontractProfilePaymentList)
        {
            _logger.LogInformation($"Start PaymentController::BulkInsert", subcontractProfilePaymentList);

            if (subcontractProfilePaymentList == null)
                _logger.LogWarning($"Start PaymentController::BulkInsert", subcontractProfilePaymentList);


            var result = _service.BulkInsert(subcontractProfilePaymentList);

            if (result == null)
            {
                _logger.LogWarning($"PaymentController::", "BulkInsert NOT FOUND", subcontractProfilePaymentList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment subcontractProfilePayment)
        {
            _logger.LogInformation($"Start PaymentController::Update", subcontractProfilePayment);

            if (subcontractProfilePayment == null)
                _logger.LogWarning($"Start PaymentController::Update", subcontractProfilePayment);

            var result = _service.Update(subcontractProfilePayment);

            if (result == null)
            {
                _logger.LogWarning($"PaymentController::", "Update NOT FOUND", subcontractProfilePayment);

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
            _logger.LogInformation($"Start PaymentController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start PaymentController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
