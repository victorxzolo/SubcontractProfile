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
    [Route("api/Region")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ISubcontractProfileRegionRepo _service;
        private readonly ILogger<RegionController> _logger;

        public RegionController(ISubcontractProfileRegionRepo service,
            ILogger<RegionController> logger)
        {
            _service = service;
            _logger = logger;
        }


        #region GET


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileRegion))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileRegion))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>> GetAll()
        {

            _logger.LogInformation($"RegionController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"RegionController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileRegion))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileRegion))]
        [HttpGet("GetByRegionId/{regionId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion> GetByRegionId(int regionId)
        {
            _logger.LogInformation($"Start RegionController::GetByRegionId", regionId);

            var entities = _service.GetByRegionId(regionId);

            if (entities == null)
            {
                _logger.LogWarning($"RegionController::", "GetByRegionId NOT FOUND", regionId);
                return null;
            }

            return entities;

        }

        #endregion


        #region POST

        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileRegion))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion subcontractProfileRegion)
        {
            _logger.LogInformation($"Start RegionController::Insert", subcontractProfileRegion);

            if (subcontractProfileRegion == null)
                _logger.LogWarning($"Start RegionController::Insert", subcontractProfileRegion);


            var result = _service.Insert(subcontractProfileRegion);

            if (result == null)
            {
                _logger.LogWarning($"RegionController::", "Insert NOT FOUND", subcontractProfileRegion);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileRegion))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion> subcontractProfileRegionList)
        {
            _logger.LogInformation($"Start RegionController::BulkInsert", subcontractProfileRegionList);

            if (subcontractProfileRegionList == null)
                _logger.LogWarning($"Start RegionController::BulkInsert", subcontractProfileRegionList);


            var result = _service.BulkInsert(subcontractProfileRegionList);

            if (result == null)
            {
                _logger.LogWarning($"RegionController::", "BulkInsert NOT FOUND", subcontractProfileRegionList);
            }
            return result;

        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion subcontractProfileRegion)
        {
            _logger.LogInformation($"Start RegionController::Update", subcontractProfileRegion);

            if (subcontractProfileRegion == null)
                _logger.LogWarning($"Start RegionController::Update", subcontractProfileRegion);

            var result = _service.Update(subcontractProfileRegion);

            if (result == null)
            {
                _logger.LogWarning($"RegionController::", "Update NOT FOUND", subcontractProfileRegion);

            }
            return result;
        }

        #endregion

        #region DELETE

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Delete(int id)
        {
            _logger.LogInformation($"Start RegionController::Delete", id);

            if (id == 0)
                _logger.LogWarning($"Start RegionController::Delete", id);

            return _service.Delete(id);
        }

        #endregion
    }
}
