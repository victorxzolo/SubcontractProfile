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
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISubcontractProfileUserRepo _service;
        private readonly ILogger<UserController> _logger;

        public UserController(ISubcontractProfileUserRepo service,
            ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UserController))]
        [HttpGet("GetAll")]
        public Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>> GetAll()
        {

            _logger.LogInformation($"UserController::GetALL");

            var entities = _service.GetAll();

            if (entities == null)
            {
                _logger.LogWarning($"UserController::", "GetALL NOT FOUND");
                return null;
            }

            return entities;

        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileUser))]
        [HttpGet("GetByUserId/{userId}")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> GetByUserId(System.Guid userId)
        {
            _logger.LogInformation($"Start UserController::GetByUserId", userId);

            var entities = _service.GetByUserId(userId);

            if (entities == null)
            {
                _logger.LogWarning($"UserController::", "GetByUserId NOT FOUND", userId);
                return null;
            }

            return entities;

        }



        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcontractProfileUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SubcontractProfileUser))]
        [HttpPost("LoginUser")]
        public Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> LoginUser(SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser user)
        {
            _logger.LogInformation($"Start UserController::LoginUser", user.Username, user.password);

            var entities = _service.LoginUser(user.Username, user.password);

            if (entities == null)
            {
                _logger.LogWarning($"UserController::", "LoginUser NOT FOUND", user.Username, user.password);
                return null;
            }

            return entities;

        }



        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserController))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UserController))]
        [HttpGet("CheckUsername/{username}")]
        public List<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> CheckUsername(string username)
        {

            _logger.LogInformation($"UserController::CheckUsername");

            var entities = _service.GetAll().Result;

            if (entities == null)
            {
                _logger.LogWarning($"UserController::", "CheckUsername NOT FOUND");
                return null;
            }
            else
            {
                var result = entities.Where(x => x.Username !=null && x.Username.Contains(username)).ToList();
            
                return result;

            }
            

        }
        #endregion

        #region POST
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileUser))]
        public Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser subcontractProfileUser)
        {
            _logger.LogInformation($"Start UserController::Insert", subcontractProfileUser);

            if (subcontractProfileUser == null)
                _logger.LogWarning($"Start UserController::Insert", subcontractProfileUser);


            var result = _service.Insert(subcontractProfileUser);

            if (result == null)
            {
                _logger.LogWarning($"UserController::", "Insert NOT FOUND", subcontractProfileUser);

            }
            return result;

        }

        [HttpPost("BulkInsert")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileUser))]
        public Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> subcontractProfileUserList)
        {
            _logger.LogInformation($"Start UserController::BulkInsert", subcontractProfileUserList);

            if (subcontractProfileUserList == null)
                _logger.LogWarning($"Start UserController::BulkInsert", subcontractProfileUserList);


            var result = _service.BulkInsert(subcontractProfileUserList);

            if (result == null)
            {
                _logger.LogWarning($"UserController::", "BulkInsert NOT FOUND", subcontractProfileUserList);
            }
            return result;

        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser subcontractProfileUser)
        {
            _logger.LogInformation($"Start UserController::Update", subcontractProfileUser);

            if (subcontractProfileUser == null)
                _logger.LogWarning($"Start UserController::Update", subcontractProfileUser);

            var result = _service.Update(subcontractProfileUser);

            if (result == null)
            {
                _logger.LogWarning($"UserController::", "Update NOT FOUND", subcontractProfileUser);

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
            _logger.LogInformation($"Start UserController::Delete", id);

            if (id == "")
                _logger.LogWarning($"Start UserController::Delete", id);

            return _service.Delete(id);
        }
        #endregion
    }
}
