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
    [Route("api/File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ISubcontractProfileFileRepo _service;
        private readonly ILogger<FileController> _logger;
        public FileController(ISubcontractProfileFileRepo service,
          ILogger<FileController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(FileController))]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<SubcontractProfileFile>> GetAll()
        {

            _logger.LogInformation($"AddressController::GetALL");

            var entities = await _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"AddressController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(FileController))]
        [HttpPost("GetByPaymentId")] // GET /api/GetByAddressId/addressId/787413D6-AA0B-4F20-A638-94FBFBF634C9
        public async Task<IEnumerable<SubcontractProfileFile>> GetByPaymentId(SubcontractProfilePayment model)
        {
            _logger.LogInformation($"Start FileController::GetByPaymentId", model.PaymentId);

            var entities = await _service.GetByPaymentId(model.PaymentId);

            if (entities == null)
            {
                _logger.LogWarning($"FileController::", "PaymentId NOT FOUND", model.PaymentId);
                return null;
            }

            return entities;

        }

        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FileController))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile subcontractProfileFile)
        {
            _logger.LogInformation($"Start FileController::Insert", subcontractProfileFile);

            if (subcontractProfileFile == null)
                _logger.LogWarning($"Start FileController::Insert", subcontractProfileFile);


            var result = _service.Insert(subcontractProfileFile);

            if (result == null)
            {
                _logger.LogWarning($"FileController::", "Insert NOT FOUND", subcontractProfileFile);

            }
            return result;

        }

        [HttpPost("DeleteByPaymentId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> DeleteByPaymentId([FromBody] string id)
        {
            _logger.LogInformation($"Start FileController::Delete", id);

            if (id == null)
                _logger.LogWarning($"Start FileController::Delete", id);

            return await _service.DeleteByPaymentId(id);
        }
    }
}
