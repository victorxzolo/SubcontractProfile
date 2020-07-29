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
    [Route("api/AddressTypeController")]
    [ApiController]
    public class AddressTypeController : ControllerBase
    {
        private readonly ISubcontractProfileAddressTypeRepo _service;
        private readonly ILogger<AddressTypeController> _logger;

        public AddressTypeController(ISubcontractProfileAddressTypeRepo service,
            ILogger<AddressTypeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressTypeController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(AddressTypeController))]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<SubcontractProfileAddressType>> GetAll()
        {

            _logger.LogInformation($"AddressController::GetALL");

            var entities = await _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"AddressController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }
        //Example multiple // GET api/user/firstname/lastname/address
        // [HttpGet("{firstName}/{lastName}/{address}")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAddress))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAddress))]
        [HttpGet("GetByAddressTypeId/{addressTypeId}")] // GET /api/GetByAddressId/addressId/787413D6-AA0B-4F20-A638-94FBFBF634C9
        public async Task<SubcontractProfileAddressType> GetByAddressTypeId(string addressTypeId)
        {
            _logger.LogInformation($"Start AddressTypeController::GetByAddressId", addressTypeId);

            var entities = await _service.GetByAddressTypeId(addressTypeId);

            if (entities == null)
            {
                _logger.LogWarning($"AddressTypeController::", "addressTypeId NOT FOUND", addressTypeId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileAddressType}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAddressType))]
       public  Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType subcontractProfileAddressType)
        {
            _logger.LogInformation($"Start AddressTypeController::Insert", subcontractProfileAddressType);

            if (subcontractProfileAddressType == null)
                _logger.LogWarning($"Start AddressTypeController::Insert", subcontractProfileAddressType);


            var result = _service.Insert(subcontractProfileAddressType);

            if (result == null)
            {
                _logger.LogWarning($"AddressTypeController::", "Insert NOT FOUND", subcontractProfileAddressType);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileAddressList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAddressType))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfileAddressType> subcontractProfileAddressList)
        {
            _logger.LogInformation($"Start AddressTypeController::BulkInsert", subcontractProfileAddressList);

            if (subcontractProfileAddressList == null)
                _logger.LogWarning($"Start AddressTypeController::BulkInsert", subcontractProfileAddressList);


            var result = _service.BulkInsert(subcontractProfileAddressList);

            if (result == null)
            {
                _logger.LogWarning($"AddressTypeController::", "BulkInsert NOT FOUND", subcontractProfileAddressList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfileAddressType subcontractProfileAddress)
        {
            _logger.LogInformation($"Start AddressTypeController::Update", subcontractProfileAddress);

            if (subcontractProfileAddress == null)
                _logger.LogWarning($"Start AddressTypeController::Update", subcontractProfileAddress);

            var result = _service.Update(subcontractProfileAddress);

            if (result == null)
            {
                _logger.LogWarning($"AddressTypeController::", "Update NOT FOUND", subcontractProfileAddress);

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
        public async Task<bool> Delete(string id)
        {
            _logger.LogInformation($"Start AddressTypeController::Delete", id);

            if (id == null)
                _logger.LogWarning($"Start AddressTypeController::Delete", id);

            return await _service.Delete(id);
        }
        #endregion

        #region Excepions
        [HttpGet("exception/{message}")]
        [ProducesErrorResponseType(typeof(Exception))]
        public async Task RaiseException(string message)
        {
            _logger.LogDebug($"AddressTypeController::RaiseException::{message}");

            throw new Exception(message);
        }
        #endregion
    }
}
