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
    [Route("api/Address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ISubcontractProfileAddressRepo _service;
        private readonly ILogger<AddressController> _logger;

        public AddressController(ISubcontractProfileAddressRepo service, 
            ILogger<AddressController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAddress))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAddress))]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<SubcontractProfileAddress>> GetAll()
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
        [HttpGet("GetByAddressId/{addressId}")] // GET /api/GetByAddressId/addressId/787413D6-AA0B-4F20-A638-94FBFBF634C9
        public async Task<SubcontractProfileAddress> GetByAddressId(string addressId)
        {
            _logger.LogInformation($"Start AddressController::GetByAddressId", addressId);

            var entities = await _service.GetByAddressId(addressId);

            if (entities == null)
            {
                _logger.LogWarning($"AddressController::", "GetByAddressId NOT FOUND", addressId);
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileAddress))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileAddress))]
        [HttpPost("GetByCompanyId")]
        public async Task<IEnumerable<SubcontractProfileAddress>> GetByCompanyId(SubcontractProfileAddress modal)
        {
            _logger.LogInformation($"Start AddressController::GetByCompanyId", modal.CompanyId);

            var entities = await _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"AddressController::", "GetByCompanyId NOT FOUND", modal.CompanyId);
                return null;
            }
            else
            {
                if(modal.CompanyId !=null)
                {
                    var returnentities = entities.Where(x => x.CompanyId == modal.CompanyId).ToList();
                    return returnentities;
                }
                else
                {
                    return null;
                }
              
            }

 

        }

        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAddress))]
        public Task<bool> Insert(SubcontractProfileAddress subcontractProfileAddress)
        {
            _logger.LogInformation($"Start AddressController::Insert", subcontractProfileAddress);

            if (subcontractProfileAddress == null)
                _logger.LogWarning($"Start AddressController::Insert", subcontractProfileAddress);


            var result = _service.Insert(subcontractProfileAddress);

            if (result == null)
            {
                _logger.LogWarning($"AddressController::", "Insert NOT FOUND", subcontractProfileAddress);
             
            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAddress))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfileAddress> subcontractProfileAddressList)
        {
            _logger.LogInformation($"Start AddressController::BulkInsert", subcontractProfileAddressList);

            if (subcontractProfileAddressList == null)
                _logger.LogWarning($"Start AddressController::BulkInsert", subcontractProfileAddressList);


            var result = _service.BulkInsert(subcontractProfileAddressList);

            if (result == null)
            {
                _logger.LogWarning($"AddressController::", "BulkInsert NOT FOUND", subcontractProfileAddressList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfileAddress subcontractProfileAddress)
        {
            _logger.LogInformation($"Start AddressController::Update", subcontractProfileAddress);

            if (subcontractProfileAddress == null)
                _logger.LogWarning($"Start AddressController::Update", subcontractProfileAddress);

            var result =  _service.Update(subcontractProfileAddress);

            if (result == null)
            {
                _logger.LogWarning($"AddressController::", "Update NOT FOUND", subcontractProfileAddress);

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
            _logger.LogInformation($"Start AddressController::Delete", id);

            if (id == null)
                _logger.LogWarning($"Start AddressController::Delete", id);

            return await _service.Delete(id);
        }
        #endregion

        #region Excepions
        [HttpGet("exception/{message}")]
        [ProducesErrorResponseType(typeof(Exception))]
        public async Task RaiseException(string message)
        {
            _logger.LogDebug($"UserControllers::RaiseException::{message}");

            throw new Exception(message);
        }
        #endregion
    }
}


