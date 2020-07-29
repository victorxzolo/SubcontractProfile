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
    [Route("api/BankingController")]
    [ApiController]
    public class BankingController: ControllerBase
    {
        private readonly ISubcontractProfileBankingRepo _service;
        private readonly ILogger<BankingController> _logger;

        public BankingController(ISubcontractProfileBankingRepo service,
            ILogger<BankingController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankingController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(BankingController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>> GetAll()
        {

            _logger.LogInformation($"BankingController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"BankingController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }
        //Example multiple // GET api/user/firstname/lastname/address
        // [HttpGet("{firstName}/{lastName}/{address}")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileBanking))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileBanking))]
        [HttpGet("GetByBankId/{addressTypeId}")] // GET /api/GetByAddressId/addressId/787413D6-AA0B-4F20-A638-94FBFBF634C9
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking> GetByBankId(System.Guid bankId)
        {
            _logger.LogInformation($"Start BankingController::GetByBankId", bankId);

            var entities =  _service.GetByBankId(bankId);

            if (entities == null)
            {
                _logger.LogWarning($"BankingController::", "GetByBankId NOT FOUND", bankId);
                return null;
            }

            return entities;

        }


        #endregion

        #region POST
        [HttpPost("Insert/{subcontractProfileBanking}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAddressType))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking subcontractProfileBanking)
        {
            _logger.LogInformation($"Start BankingController::Insert", subcontractProfileBanking);

            if (subcontractProfileBanking == null)
                _logger.LogWarning($"Start BankingController::Insert", subcontractProfileBanking);


            var result = _service.Insert(subcontractProfileBanking);

            if (result == null)
            {
                _logger.LogWarning($"BankingController::", "Insert NOT FOUND", subcontractProfileBanking);

            }
            return result;

        }

        [HttpPost("BulkInsert/{subcontractProfileBankingList}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileAddressType))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking> subcontractProfileBankingList)
        {
            _logger.LogInformation($"Start BankingController::BulkInsert", subcontractProfileBankingList);

            if (subcontractProfileBankingList == null)
                _logger.LogWarning($"Start BankingController::BulkInsert", subcontractProfileBankingList);


            var result = _service.BulkInsert(subcontractProfileBankingList);

            if (result == null)
            {
                _logger.LogWarning($"BankingController::", "BulkInsert NOT FOUND", subcontractProfileBankingList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update/{subcontractProfileBanking}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking subcontractProfileBanking)
        {
            _logger.LogInformation($"Start BankingController::Update", subcontractProfileBanking);

            if (subcontractProfileBanking == null)
                _logger.LogWarning($"Start BankingController::Update", subcontractProfileBanking);

            var result = _service.Update(subcontractProfileBanking);

            if (result == null)
            {
                _logger.LogWarning($"BankingController::", "Update NOT FOUND", subcontractProfileBanking);

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
            _logger.LogInformation($"Start BankingController::Delete", id);

            if (id == 0)
                _logger.LogWarning($"Start BankingController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
