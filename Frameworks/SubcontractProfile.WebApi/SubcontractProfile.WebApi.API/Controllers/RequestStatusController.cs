using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using SubcontractProfile.WebApi.API.DataContracts;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.API.Controllers
{
    [Route("api/RequestStatus")]
    [ApiController]
    public class RequestStatusController : ControllerBase
    {
        private readonly ISubcontractProfileRequestStatusRepo _service;
        private readonly ILogger<RequestStatusController> _logger;
        public RequestStatusController(ISubcontractProfileRequestStatusRepo service,
           ILogger<RequestStatusController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequestStatusController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(RequestStatusController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRequestStatus>> GetAll()
        {

            _logger.LogInformation($"RequestStatusController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"RequestStatusController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }

        #endregion
    }
}
