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
    [Route("api/Province")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly ISubcontractProfileProvinceRepo _service;
        private readonly ILogger<ProvinceController> _logger;
        public ProvinceController(ISubcontractProfileProvinceRepo service,
          ILogger<ProvinceController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region GET
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileProvince))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileProvince))]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>> GetAll()
        {
            _logger.LogInformation($"ProvinceController::GetALL");

            var entities = await _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"ProvinceController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileProvince))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileProvince))]
        [HttpGet("GetByProvinceId/{provinceId}")] // GET /api/GetByAddressId/addressId/787413D6-AA0B-4F20-A638-94FBFBF634C9
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince> GetByProvinceId(int provinceId)
        {
            _logger.LogInformation($"Start ProvinceController::GetByProvinceId", provinceId);

            var entities = await _service.GetByProvinceId(provinceId);

            if (entities == null)
            {
                _logger.LogWarning($"ProvinceController::", "GetByProvinceId NOT FOUND", provinceId);
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileProvince))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileProvince))]
        [HttpGet("GetProvinceByRegionId/{regionId}")]
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>> GetAll(int regionId)
        {
            _logger.LogInformation($"ProvinceController::GetALL");

            var entities = await _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"ProvinceController::", "GetALL NOT FOUND");
                return null;
            }
            else
            {
                var result = entities.Where(x => x.RegionId == regionId).ToList();
                return result;
            }
        }

        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileProvince}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileProvince))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince subcontractProfileProvince)
        {
            _logger.LogInformation($"Start ProvinceController::Insert", subcontractProfileProvince);

            if (subcontractProfileProvince == null)
                _logger.LogWarning($"Start ProvinceController::Insert", subcontractProfileProvince);


            var result = _service.Insert(subcontractProfileProvince);

            if (result == null)
            {
                _logger.LogWarning($"ProvinceController::", "Insert NOT FOUND", subcontractProfileProvince);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileProvinceList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileProvince))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince> subcontractProfileProvinceList)
        {
            _logger.LogInformation($"Start ProvinceController::BulkInsert", subcontractProfileProvinceList);

            if (subcontractProfileProvinceList == null)
                _logger.LogWarning($"Start ProvinceController::BulkInsert", subcontractProfileProvinceList);


            var result = _service.BulkInsert(subcontractProfileProvinceList);

            if (result == null)
            {
                _logger.LogWarning($"ProvinceController::", "BulkInsert NOT FOUND", subcontractProfileProvinceList);
            }
            return result;

        }
        #endregion

        #region PUT

        [HttpPut("Update/{subcontractProfileProvince}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince subcontractProfileProvince)
        {
            _logger.LogInformation($"Start ProvinceController::Update", subcontractProfileProvince);

            if (subcontractProfileProvince == null)
                _logger.LogWarning($"Start ProvinceController::Update", subcontractProfileProvince);

            var result = _service.Update(subcontractProfileProvince);

            if (result == null)
            {
                _logger.LogWarning($"ProvinceController::", "Update NOT FOUND", subcontractProfileProvince);

            }
            return result;
        }

        #endregion

        #region DELETE

        [HttpDelete("Delete/{provinceId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Delete(int provinceId)
        {
            _logger.LogInformation($"Start ProvinceController::Delete", provinceId);

            if (provinceId == 0)
            {
                _logger.LogWarning($"Start ProvinceController::Delete", provinceId);
            }

            return _service.Delete(provinceId);
        }

        #endregion

    }
}
