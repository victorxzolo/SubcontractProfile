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
    [Route("api/Location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ISubcontractProfileLocationRepo _service;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ISubcontractProfileLocationRepo service,
            ILogger<LocationController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(LocationController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>> GetAll()
        {

            _logger.LogInformation($"LocationController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileLocation))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileLocation))]
        [HttpGet("GetByLocationId/{locationId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> GetByLocationId(System.Guid locationId)
        {
            _logger.LogInformation($"Start LocationController::GetByLocationId", locationId);

            var entities = _service.GetByLocationId(locationId);

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "GetByLocationId NOT FOUND", locationId);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileLocation))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileLocation))]
        [HttpGet("GetLocationByCompany/{company_id}")]

        public Task<IEnumerable<SubcontractProfileLocation>> GetLocationByCompany(Guid company_id)
        {
            _logger.LogInformation($"Start LocationController::GetLocationByCompany", company_id);
            var entities = _service.GetLocationByCompany(company_id);

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "GetLocationByCompany NOT FOUND", company_id);
                return null;
            }

            return entities;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileLocation))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileLocation))]
        [HttpGet("SearchLocation/{company_id}/{location_code}/{location_name}/{location_name_en}/{storage_location}/{phone}/{location_name_alias}")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>> SearchLocation(Guid company_id, string location_code,
                 string location_name, string location_name_en, string storage_location, string phone
            , string location_name_alias)
        {
            _logger.LogInformation($"Start LocationController::SearchLocation", company_id, location_name, location_name_en, phone, location_name_alias);

            if (location_code.ToUpper() == "NULL")
            {
                location_code = string.Empty;
            }

            if (location_name.ToUpper() == "NULL")
            {
                location_name = string.Empty;
            }

            if (location_name_en.ToUpper() == "NULL")
            {
                location_name_en = string.Empty;
            }

            if (storage_location.ToUpper() == "NULL")
            {
                storage_location = string.Empty;
            }

            if (phone.ToUpper() == "NULL")
            {
                phone = string.Empty;
            }

            if (location_name_alias.ToUpper() == "NULL")
            {
                location_name_alias = string.Empty;
            }

            var entities = _service.SearchLocation(company_id, location_code, location_name, location_name_en, storage_location, phone, location_name_alias);

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "SearchLocation NOT FOUND", company_id, company_id, location_name, location_name_en, phone, location_name_alias);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileLocationOutput))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileLocationOutput))]
        [HttpPost("GetListLocation")]
        public SubcontractProfileLocationOutput GetListLocation(SearchSubcontractProfileLocationQuery data)
        {

            _logger.LogInformation($"LocationController::GetALL");

            SubcontractProfileLocationOutput Output = new SubcontractProfileLocationOutput();

            var entities =  _service.SearchListLocation(data).Result.ToList();
           

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "GetALL NOT FOUND");
                return null;
            }
            else
            {
                Output.ListResult = entities;
                Output.filteredResultsCount = Output.ListResult.Count();
                Output.TotalResultsCount = Output.ListResult[0].row_total;

              
            }
            return Output;
        }

        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileLocation))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation subcontractProfileLocation)
        {
            _logger.LogInformation($"Start LocationController::Insert", subcontractProfileLocation);

            if (subcontractProfileLocation == null)
                _logger.LogWarning($"Start LocationController::Insert", subcontractProfileLocation);


            var result = _service.Insert(subcontractProfileLocation);

            if (result == null)
            {
                _logger.LogWarning($"LocationController::", "Insert NOT FOUND", subcontractProfileLocation);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileLocation))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> subcontractProfileLocationList)
        {
            _logger.LogInformation($"Start LocationController::BulkInsert", subcontractProfileLocationList);

            if (subcontractProfileLocationList == null)
                _logger.LogWarning($"Start LocationController::BulkInsert", subcontractProfileLocationList);


            var result = _service.BulkInsert(subcontractProfileLocationList);

            if (result == null)
            {
                _logger.LogWarning($"LocationController::", "BulkInsert NOT FOUND", subcontractProfileLocationList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation subcontractProfileLocation)
        {
            _logger.LogInformation($"Start LocationController::Update", subcontractProfileLocation);

            if (subcontractProfileLocation == null)
                _logger.LogWarning($"Start LocationController::Update", subcontractProfileLocation);

            var result = _service.Update(subcontractProfileLocation);

            if (result == null)
            {
                _logger.LogWarning($"LocationController::", "Update NOT FOUND", subcontractProfileLocation);

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
        public Task<bool> Delete(Guid id)
        {
            _logger.LogInformation($"Start LocationController::Delete", id);

            if (id == Guid.Empty)
                _logger.LogWarning($"Start LocationController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
