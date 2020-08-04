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
        [HttpGet("SearchLocation/{company_id}/{location_code}/{location_name}/{location_name_en}/{phone}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> SearchLocation(Guid company_id, string location_code,
                 string location_name, string location_name_en, string phone)
        {
            _logger.LogInformation($"Start LocationController::SearchLocation", company_id, location_name, location_name_en, phone);

            var entities = _service.SearchLocation(company_id, location_code, location_name, location_name_en, phone);

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "SearchLocation NOT FOUND", company_id, company_id, location_name, location_name_en, phone);
                return null;
            }

            return entities;

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(LocationController))]
        [HttpGet("GetListLocation/{data}")]
        public async Task<SubcontractProfileLocationOutput> GetAll(Search_SubcontractProfileLocationQuery data)
        {

            _logger.LogInformation($"LocationController::GetALL");

            SubcontractProfileLocationOutput Output = new SubcontractProfileLocationOutput();

            var entities = await _service.GetAll();
           

            if (entities == null)
            {
                _logger.LogWarning($"LocationController::", "GetALL NOT FOUND");
                return null;
            }
            else
            {
                if(data.dir.ToLower()=="asc")
                {
                    switch(data.ordercolumn)
                    {
                        case "company_name_th":break;
                        case "LocationCode":
                            Output.ListResult = entities.Where(x => x.LocationCode.Contains(data.location_code != null ? data.location_code : "")
                                        || x.LocationNameTh.Contains(data.location_name_th != null ? data.location_name_th : "")
                                        || x.LocationNameEn.Contains(data.location_name_en != null ? data.location_name_en : "")
                                        ).OrderBy(r => r.LocationCode).ToList();
                            break;
                        case "LocationNameTh":
                            Output.ListResult = entities.Where(x => x.LocationCode.Contains(data.location_code != null ? data.location_code : "")
                                        || x.LocationNameTh.Contains(data.location_name_th != null ? data.location_name_th : "")
                                        || x.LocationNameEn.Contains(data.location_name_en != null ? data.location_name_en : "")
                                        ).OrderBy(r => r.LocationNameTh).ToList();
                            break;
                        case "distribution_channel":break;
                        case "channel_sale_group":break;

                    }
                   
                }
                else if(data.dir.ToLower()=="desc")
                {
                    switch (data.ordercolumn)
                    {
                        case "company_name_th": break;
                        case "LocationCode":
                            Output.ListResult = entities.Where(x => x.LocationCode.Contains(data.location_code != null ? data.location_code : "")
                                        || x.LocationNameTh.Contains(data.location_name_th != null ? data.location_name_th : "")
                                        || x.LocationNameEn.Contains(data.location_name_en != null ? data.location_name_en : "")
                                        ).OrderByDescending(r => r.LocationCode).ToList();
                            break;
                        case "LocationNameTh":
                            Output.ListResult = entities.Where(x => x.LocationCode.Contains(data.location_code != null ? data.location_code : "")
                                        || x.LocationNameTh.Contains(data.location_name_th != null ? data.location_name_th : "")
                                        || x.LocationNameEn.Contains(data.location_name_en != null ? data.location_name_en : "")
                                        ).OrderByDescending(r => r.LocationNameTh).ToList();
                            break;
                        case "distribution_channel": break;
                        case "channel_sale_group": break;

                    }
                }
               
                Output.TotalResultsCount = Output.ListResult.Count();
                Output.ListResult = Output.ListResult.Skip(data.PAGE_INDEX).Take(data.PAGE_SIZE).ToList();

                return Output;
            }

           

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
        public Task<bool> Delete(string id)
        {
            _logger.LogInformation($"Start LocationController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start LocationController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
