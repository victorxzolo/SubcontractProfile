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
    [Route("api/VerhicleSerise")]
    [ApiController]
    public class VerhicleSeriseController : ControllerBase
    {
        private readonly ISubcontractProfileVerhicleSeriseRepo _service;
        private readonly ILogger<VerhicleSeriseController> _logger;

        public VerhicleSeriseController(ISubcontractProfileVerhicleSeriseRepo service,
            ILogger<VerhicleSeriseController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VerhicleSeriseController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(VerhicleSeriseController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>> GetAll()
        {

            _logger.LogInformation($"VerhicleSeriseController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"VerhicleSeriseController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileVerhicleSerise))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileVerhicleSerise))]
        [HttpGet("GetByVerhicleSeriseId/{verhicleSeriseId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> GetByVerhicleSeriseId(string verhicleSeriseId)
        {
            _logger.LogInformation($"Start VerhicleSeriseController::GetByVerhicleSeriseId", verhicleSeriseId);

            var entities = _service.GetByVerhicleSeriseId(verhicleSeriseId);

            if (entities == null)
            {
                _logger.LogWarning($"VerhicleSeriseController::", "GetByVerhicleSeriseId NOT FOUND", verhicleSeriseId);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileVerhicleSerise))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileVerhicleSerise))]
        [HttpGet("GetByVerhicleBrandId/{verhicleBrandId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> GetByVerhicleBrandId(string verhicleBrandId)
        {
            _logger.LogInformation($"Start VerhicleSeriseController::GetByVerhicleBrandId", verhicleBrandId);

            var entities = _service.GetByVerhicleSeriseId(verhicleBrandId);

            if (entities == null)
            {
                _logger.LogWarning($"VerhicleSeriseController::", "GetByVerhicleBrandId NOT FOUND", verhicleBrandId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileVerhicleSerise))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise subcontractProfileVerhicleSerise)
        {
            _logger.LogInformation($"Start VerhicleSeriseController::Insert", subcontractProfileVerhicleSerise);

            if (subcontractProfileVerhicleSerise == null)
                _logger.LogWarning($"Start VerhicleSeriseController::Insert", subcontractProfileVerhicleSerise);


            var result = _service.Insert(subcontractProfileVerhicleSerise);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleSeriseController::", "Insert NOT FOUND", subcontractProfileVerhicleSerise);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileVerhicleSerise))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> subcontractProfileVerhicleSeriseList)
        {
            _logger.LogInformation($"Start VerhicleSeriseController::BulkInsert", subcontractProfileVerhicleSeriseList);

            if (subcontractProfileVerhicleSeriseList == null)
                _logger.LogWarning($"Start VerhicleSeriseController::BulkInsert", subcontractProfileVerhicleSeriseList);


            var result = _service.BulkInsert(subcontractProfileVerhicleSeriseList);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleSeriseController::", "BulkInsert NOT FOUND", subcontractProfileVerhicleSeriseList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise subcontractProfileVerhicleSerise)
        {
            _logger.LogInformation($"Start VerhicleSeriseController::Update", subcontractProfileVerhicleSerise);

            if (subcontractProfileVerhicleSerise == null)
                _logger.LogWarning($"Start VerhicleSeriseController::Update", subcontractProfileVerhicleSerise);

            var result = _service.Update(subcontractProfileVerhicleSerise);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleSeriseController::", "Update NOT FOUND", subcontractProfileVerhicleSerise);

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
            _logger.LogInformation($"Start VerhicleSeriseController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start VerhicleSeriseController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
