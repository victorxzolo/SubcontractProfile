using AutoMapper;
//using SubcontractProfile.WebApi.API.DataContracts.Requests;
using SubcontractProfile.WebApi.API.DataContracts;
using SubcontractProfile.WebApi.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using SubcontractProfile.WebApi.API.Controllers;

namespace SubcontractProfile.WebApi.API.Tests.Controllers.ControllerTests
{
    public class registercompanyTest: TestBase
    {
        RegisterCompanyController _comp_controller;
        public registercompanyTest() : base()
        {
            var businessService = _serviceProvider.GetRequiredService<ISubcontractProfileCompanyRepo>();
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<RegisterCompanyController>();

            _comp_controller = new RegisterCompanyController(businessService, mapper, logger);
        }

        [TestMethod]
        public async Task CreateUser_Nominal_OK()
        {
            //Simple test
            var result = await _comp_controller.GetALL();

            Assert.IsNotNull(result);
        }

    }
}
