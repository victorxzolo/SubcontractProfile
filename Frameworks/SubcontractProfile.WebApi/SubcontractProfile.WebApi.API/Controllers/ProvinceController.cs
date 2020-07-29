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
    [Route("api/ProvinceController")]
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
        #endregion

    }
}
