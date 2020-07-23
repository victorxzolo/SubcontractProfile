﻿using System;
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

    [ApiVersion("1.0")]
    [Route("api/SubcontractProfileCompany")]//required for default versioning
    [Route("api/v{version:apiVersion}/SubcontractProfileCompany")]
    [ApiController]
    public class RegisterCompanyController : Controller
    {
        private readonly ISubcontractProfileCompanyRepo _service;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterCompanyController> _logger;

        public RegisterCompanyController(ISubcontractProfileCompanyRepo service, IMapper mapper, ILogger<RegisterCompanyController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        #region GET
        /// <summary>
        /// Returns a user entity according to the provided Id.
        /// </summary>
        /// <remarks>
        /// XML comments included in controllers will be extracted and injected in Swagger/OpenAPI file.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Returns a user entity according to the provided Id.
        /// </returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="204">If the item is null.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyRegister))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CompanyRegister))]
        [HttpGet("{id}")]
        public async Task<IEnumerable<SubcontractProfileCompany>> GetALL()
        {
            //_logger.LogDebug($"UserControllers::GetALL::{id}");
            _logger.LogDebug($"UserControllers::GetALL");

            var data = await _service.GetAll();

            if (data != null)
                return data; //_mapper.Map<SubcontractProfileCompany>(data);
            else
                return null;
        }
        #endregion


    }
}