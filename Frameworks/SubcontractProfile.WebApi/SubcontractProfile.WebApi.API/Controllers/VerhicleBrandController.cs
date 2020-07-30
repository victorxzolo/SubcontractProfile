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
    [Route("api/VerhicleBrandController")]
    [ApiController]
    public class VerhicleBrandController : ControllerBase
    {
        private readonly ISubcontractProfileVerhicleBrandRepo _service;
        private readonly ILogger<VerhicleBrandController> _logger;

        public VerhicleBrandController(ISubcontractProfileVerhicleBrandRepo service,
            ILogger<VerhicleBrandController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VerhicleBrandController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(VerhicleBrandController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>> GetAll()
        {

            _logger.LogInformation($"VerhicleBrandController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"VerhicleBrandController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileVerhicleBrand))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileVerhicleBrand))]
        [HttpGet("GetByVerhicleBrandId/{verhicleBrandId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand> GetByVerhicleBrandId(string verhicleBrandId)
        {
            _logger.LogInformation($"Start VerhicleBrandController::GetByVerhicleBrandId", verhicleBrandId);

            var entities = _service.GetByVerhicleBrandId(verhicleBrandId);

            if (entities == null)
            {
                _logger.LogWarning($"VerhicleBrandController::", "GetByVerhicleBrandId NOT FOUND", verhicleBrandId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileVerhicleBrand}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileVerhicleBrand))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand subcontractProfileVerhicleBrand)
        {
            _logger.LogInformation($"Start VerhicleBrandController::Insert", subcontractProfileVerhicleBrand);

            if (subcontractProfileVerhicleBrand == null)
                _logger.LogWarning($"Start VerhicleBrandController::Insert", subcontractProfileVerhicleBrand);


            var result = _service.Insert(subcontractProfileVerhicleBrand);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleBrandController::", "Insert NOT FOUND", subcontractProfileVerhicleBrand);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileVerhicleBrandList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileVerhicleBrand))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand> subcontractProfileVerhicleBrandList)
        {
            _logger.LogInformation($"Start VerhicleBrandController::BulkInsert", subcontractProfileVerhicleBrandList);

            if (subcontractProfileVerhicleBrandList == null)
                _logger.LogWarning($"Start VerhicleBrandController::BulkInsert", subcontractProfileVerhicleBrandList);


            var result = _service.BulkInsert(subcontractProfileVerhicleBrandList);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleBrandController::", "BulkInsert NOT FOUND", subcontractProfileVerhicleBrandList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileVerhicleBrand}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand subcontractProfileVerhicleBrand)
        {
            _logger.LogInformation($"Start VerhicleBrandController::Update", subcontractProfileVerhicleBrand);

            if (subcontractProfileVerhicleBrand == null)
                _logger.LogWarning($"Start VerhicleBrandController::Update", subcontractProfileVerhicleBrand);

            var result = _service.Update(subcontractProfileVerhicleBrand);

            if (result == null)
            {
                _logger.LogWarning($"VerhicleBrandController::", "Update NOT FOUND", subcontractProfileVerhicleBrand);

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
            _logger.LogInformation($"Start VerhicleBrandController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start VerhicleBrandController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
