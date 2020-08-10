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
    [Route("api/SubDistrict")]
    [ApiController]
    public class SubDistrictController : ControllerBase
    {
        private readonly ISubcontractProfileSubDistrictRepo _service;
        private readonly ILogger<SubDistrictController> _logger;

        public SubDistrictController(ISubcontractProfileSubDistrictRepo service,
            ILogger<SubDistrictController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region GET
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileSubDistrict))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileSubDistrict))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>> GetAll()
        {

            _logger.LogInformation($"SubDistrictController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"SubDistrictController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileSubDistrict))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileSubDistrict))]
        [HttpGet("GetBySubDistrictId/{subDistrictId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> GetBySubDistrictId(int subDistrictId)
        {
            _logger.LogInformation($"Start SubDistrictController::GetByDistrictId", subDistrictId);

            var entities = _service.GetBySubDistrictId(subDistrictId);

            if (entities == null)
            {
                _logger.LogWarning($"SubDistrictController::", "GetBySubDistrictId NOT FOUND", subDistrictId);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileSubDistrict))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileSubDistrict))]
        [HttpGet("GetSubDistrictByDistrict/{districtId}")]
        public List<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> GetSubDistrictByDistrict(int districtId)
        {

            _logger.LogInformation($"SubDistrictController::GetALL");

            var entities = _service.GetAll().Result;

            if (entities == null)
            {
                _logger.LogWarning($"SubDistrictController::", "GetALL NOT FOUND");
                return null;
            }
            else
            {
                var result = entities.Where(x => x.DistrictId == districtId).ToList();
                return result;
            }

        }

        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileDistrict))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict subcontractProfileSubDistrict)
        {
            _logger.LogInformation($"Start SubDistrictController::Insert", subcontractProfileSubDistrict);

            if (subcontractProfileSubDistrict == null)
                _logger.LogWarning($"Start SubDistrictController::Insert", subcontractProfileSubDistrict);


            var result = _service.Insert(subcontractProfileSubDistrict);

            if (result == null)
            {
                _logger.LogWarning($"SubDistrictController::", "Insert NOT FOUND", subcontractProfileSubDistrict);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileSubDistrict))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> subcontractProfileSubDistrictList)
        {
            _logger.LogInformation($"Start SubDistrictController::BulkInsert", subcontractProfileSubDistrictList);

            if (subcontractProfileSubDistrictList == null)
                _logger.LogWarning($"Start SubDistrictController::BulkInsert", subcontractProfileSubDistrictList);


            var result = _service.BulkInsert(subcontractProfileSubDistrictList);

            if (result == null)
            {
                _logger.LogWarning($"SubDistrictController::", "BulkInsert NOT FOUND", subcontractProfileSubDistrictList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict subcontractProfileSubDistrict)
        {
            _logger.LogInformation($"Start SubDistrictController::Update", subcontractProfileSubDistrict);

            if (subcontractProfileSubDistrict == null)
                _logger.LogWarning($"Start SubDistrictController::Update", subcontractProfileSubDistrict);

            var result = _service.Update(subcontractProfileSubDistrict);

            if (result == null)
            {
                _logger.LogWarning($"SubDistrictController::", "Update NOT FOUND", subcontractProfileSubDistrict);

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
        public Task<bool> Delete(int id)
        {
            _logger.LogInformation($"Start SubDistrictController::Delete", id);

            if (id == 0)
                _logger.LogWarning($"Start SubDistrictController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
