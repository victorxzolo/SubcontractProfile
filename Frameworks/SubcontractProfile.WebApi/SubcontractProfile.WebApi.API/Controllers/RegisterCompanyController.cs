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
       
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyRegister))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CompanyRegister))]
        [HttpGet("GetALL/{id}")]
        public async Task<IEnumerable<SubcontractProfileCompany>> GetALL()
        {
            //_logger.LogDebug($"UserControllers::GetALL::{id}");
            _logger.LogDebug($"UserControllers::GetALL");

            var data = await _service.GetAll();

            if (data != null)
                return data.ToList(); //_mapper.Map<SubcontractProfileCompany>(data);
            else
                return null;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        #endregion


        #region POST
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

    }
}
