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
    [Route("api/TitleController")]
    [ApiController]
    public class TitleController : ControllerBase
    {

        private readonly ISubcontractProfileTitleRepo _service;
        private readonly ILogger<TitleController> _logger;
        public TitleController(ISubcontractProfileTitleRepo service,
          ILogger<TitleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region GET
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTitle))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTitle))]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>> GetAll()
        {
            _logger.LogInformation($"TitleController::GetALL");

            var entities = await _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"TitleController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileTitle))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileTitle))]
        [HttpGet("GetByTitleId/{titleId}")] // GET /api/GetByAddressId/addressId/787413D6-AA0B-4F20-A638-94FBFBF634C9
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle> GetByTitleId(string titleId)
        {
            _logger.LogInformation($"Start TitleController::GetByProvinceId", titleId);

            var entities = await _service.GetByTitleId(titleId);

            if (entities == null)
            {
                _logger.LogWarning($"TitleController::", "GetByProvinceId NOT FOUND", titleId);
                return null;
            }

            return entities;
        }
        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileTitle}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTitle))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle subcontractProfileTitle)
        {
            _logger.LogInformation($"Start TitleController::Insert", subcontractProfileTitle);

            if (subcontractProfileTitle == null)
                _logger.LogWarning($"Start TitleController::Insert", subcontractProfileTitle);


            var result = _service.Insert(subcontractProfileTitle);

            if (result == null)
            {
                _logger.LogWarning($"TitleController::", "Insert NOT FOUND", subcontractProfileTitle);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileTitleList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileTitle))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle> subcontractProfileTitleList)
        {
            _logger.LogInformation($"Start TitleController::BulkInsert", subcontractProfileTitleList);

            if (subcontractProfileTitleList == null)
                _logger.LogWarning($"Start TitleController::BulkInsert", subcontractProfileTitleList);


            var result = _service.BulkInsert(subcontractProfileTitleList);

            if (result == null)
            {
                _logger.LogWarning($"TitleController::", "BulkInsert NOT FOUND", subcontractProfileTitleList);
            }
            return result;

        }
        #endregion

        #region PUT

        [HttpPut("Update/{subcontractProfileTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle subcontractProfileTitle)
        {
            _logger.LogInformation($"Start TitleController::Update", subcontractProfileTitle);

            if (subcontractProfileTitle == null)
                _logger.LogWarning($"Start TitleController::Update", subcontractProfileTitle);

            var result = _service.Update(subcontractProfileTitle);

            if (result == null)
            {
                _logger.LogWarning($"TitleController::", "Update NOT FOUND", subcontractProfileTitle);

            }
            return result;
        }
        #endregion

        #region DELETE

        [HttpDelete("Delete/{titleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Delete(string titleId)
        {
            _logger.LogInformation($"Start TitleController::Delete", titleId);

            if (titleId == "")
                _logger.LogWarning($"Start TitleController::Delete", titleId);

            return _service.Delete(titleId);
        }
        #endregion
    }
}
