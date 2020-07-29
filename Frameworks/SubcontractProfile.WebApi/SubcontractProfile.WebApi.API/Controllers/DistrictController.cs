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
    [Route("api/DistrictController")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly ISubcontractProfileDistrictRepo _service;
        private readonly ILogger<DistrictController> _logger;

        public DistrictController(ISubcontractProfileDistrictRepo service,
            ILogger<DistrictController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistrictController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(DistrictController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>> GetAll()
        {

            _logger.LogInformation($"DistrictController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"DistrictController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }
       

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileDistrict))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileDistrict))]
        [HttpGet("GetByDistrictId/{districtId}")] 
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict> GetByDistrictId(int districtId)
        {
            _logger.LogInformation($"Start DistrictController::GetByDistrictId", districtId);

            var entities = _service.GetByDistrictId(districtId);

            if (entities == null)
            {
                _logger.LogWarning($"DistrictController::", "GetByDistrictId NOT FOUND", districtId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileDistrict}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileDistrict))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict subcontractProfileDistrict)
        {
            _logger.LogInformation($"Start DistrictController::Insert", subcontractProfileDistrict);

            if (subcontractProfileDistrict == null)
                _logger.LogWarning($"Start DistrictController::Insert", subcontractProfileDistrict);


            var result = _service.Insert(subcontractProfileDistrict);

            if (result == null)
            {
                _logger.LogWarning($"DistrictController::", "Insert NOT FOUND", subcontractProfileDistrict);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileDistrictList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileDistrict))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict> subcontractProfileDistrictList)
        {
            _logger.LogInformation($"Start DistrictController::BulkInsert", subcontractProfileDistrictList);

            if (subcontractProfileDistrictList == null)
                _logger.LogWarning($"Start DistrictController::BulkInsert", subcontractProfileDistrictList);


            var result = _service.BulkInsert(subcontractProfileDistrictList);

            if (result == null)
            {
                _logger.LogWarning($"DistrictController::", "BulkInsert NOT FOUND", subcontractProfileDistrictList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileDistrict}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict subcontractProfileDistrict)
        {
            _logger.LogInformation($"Start DistrictController::Update", subcontractProfileDistrict);

            if (subcontractProfileDistrict == null)
                _logger.LogWarning($"Start DistrictController::Update", subcontractProfileDistrict);

            var result = _service.Update(subcontractProfileDistrict);

            if (result == null)
            {
                _logger.LogWarning($"DistrictController::", "Update NOT FOUND", subcontractProfileDistrict);

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
            _logger.LogInformation($"Start DistrictController::Delete", id);

            if (id == 0)
                _logger.LogWarning($"Start DistrictController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
