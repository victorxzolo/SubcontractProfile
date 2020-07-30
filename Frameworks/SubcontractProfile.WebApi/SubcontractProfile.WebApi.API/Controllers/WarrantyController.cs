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
    [Route("api/Warranty")]
    [ApiController]
    public class WarrantyController : ControllerBase
    {
        private readonly ISubcontractProfileWarrantyRepo _service;
        private readonly ILogger<WarrantyController> _logger;

        public WarrantyController(ISubcontractProfileWarrantyRepo service,
            ILogger<WarrantyController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarrantyController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(WarrantyController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>> GetAll()
        {

            _logger.LogInformation($"WarrantyController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"WarrantyController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileWarranty))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileWarranty))]
        [HttpGet("GetByWarrantyId/{warrantyId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty> GetByWarrantyId(string warrantyId)
        {
            _logger.LogInformation($"Start WarrantyController::GetByWarrantyId", warrantyId);

            var entities = _service.GetByWarrantyId(warrantyId);

            if (entities == null)
            {
                _logger.LogWarning($"WarrantyController::", "GetByWarrantyId NOT FOUND", warrantyId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileWarranty}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileWarranty))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty subcontractProfileWarranty)
        {
            _logger.LogInformation($"Start WarrantyController::Insert", subcontractProfileWarranty);

            if (subcontractProfileWarranty == null)
                _logger.LogWarning($"Start WarrantyController::Insert", subcontractProfileWarranty);


            var result = _service.Insert(subcontractProfileWarranty);

            if (result == null)
            {
                _logger.LogWarning($"WarrantyController::", "Insert NOT FOUND", subcontractProfileWarranty);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileVerhicleSeriseList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileWarranty))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty> subcontractProfileWarrantyList)
        {
            _logger.LogInformation($"Start WarrantyController::BulkInsert", subcontractProfileWarrantyList);

            if (subcontractProfileWarrantyList == null)
                _logger.LogWarning($"Start WarrantyController::BulkInsert", subcontractProfileWarrantyList);


            var result = _service.BulkInsert(subcontractProfileWarrantyList);

            if (result == null)
            {
                _logger.LogWarning($"WarrantyController::", "BulkInsert NOT FOUND", subcontractProfileWarrantyList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileWarranty}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty subcontractProfileWarranty)
        {
            _logger.LogInformation($"Start WarrantyController::Update", subcontractProfileWarranty);

            if (subcontractProfileWarranty == null)
                _logger.LogWarning($"Start WarrantyController::Update", subcontractProfileWarranty);

            var result = _service.Update(subcontractProfileWarranty);

            if (result == null)
            {
                _logger.LogWarning($"WarrantyController::", "Update NOT FOUND", subcontractProfileWarranty);

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
            _logger.LogInformation($"Start WarrantyController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start WarrantyController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
