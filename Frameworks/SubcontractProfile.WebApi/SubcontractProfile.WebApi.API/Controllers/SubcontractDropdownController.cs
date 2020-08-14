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
    [Route("api/Dropdown")]
    [ApiController]
    public class SubcontractDropdownController : ControllerBase
    {
        private readonly ISubcontractDropdownRepo _service;
        private readonly ILogger<SubcontractDropdownController> _logger;

        public SubcontractDropdownController(ISubcontractDropdownRepo service,
            ILogger<SubcontractDropdownController> logger)
        {
            _service = service;
            _logger = logger;
        }


        #region GET
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractDropdown))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractDropdown))]
        [HttpGet("GetByDropDownName/{dropdownName}")]
        public Task<IEnumerable<SubcontractDropdown>> GetByDropDownName(string dropdownName)
        {
            _logger.LogInformation($"Start SubcontractDropdownController::GetByDropDownName", dropdownName);

            var entities = _service.GetByDropDownName(dropdownName);


            if (entities == null)
            {
                _logger.LogWarning($"SubDistrictController::", "GetByDropDownName NOT FOUND", dropdownName);
                return null;
            }

            return entities;

           

        }
        #endregion
    }
}
