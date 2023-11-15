using abcde.Data;
using abcde.Data.Interfaces;
using abcde.Data.Repositories;
using abcde.Data.Services;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Dtos;
using abcde.Model.Exceptions;
using abcde.Model.Filters;
using abcde.Model.Identity;
using abcde.vAPI.Clients.TWPortal;
using abcde.vAPI.Controllers.Base;
using abcde.vAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace abcde.vAPI.Controllers.v1
{
    public class IdentityController : GenericsTenantController<User, BaseTenantSummary, UserFilter>
    {
        private readonly ITenantService _tenantService;
        private readonly ITWPortalService tWPortalService;
        private readonly IIdentityService identityService;
        private readonly ITenantHandlerService tenantHandlerService;
        private readonly IDomainService domainService;

        #region ctor

        public IdentityController(
            IUserRepository userRepository,
            ITenantService tenantService,
            ITWPortalService tWPortalService,
            IIdentityService identityService, IOptions<Model.Configuration.Options> appSettings, DataContext dataContext,
            IDomainService domainService) : base(userRepository)
        {
            _tenantService = tenantService;
            this.tWPortalService = tWPortalService;
            this.identityService = identityService;
            this.domainService = domainService;
            tenantHandlerService = new TenantHandlerService(null, appSettings, dataContext);
        }

        #endregion ctor

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ValidateOrg")]
        public async Task<IActionResult> Validate([FromBody] VerifyOrganisation model)
        {
            try
            {
                var organisation = await tWPortalService.ValidateOrganisationAdministrator(model);

                if (organisation == null)
                {
                    return BadRequest("UnableToValidate");
                }
                //Now set the database connection if it has to go to a different one
                tenantHandlerService.VerifyAndSetConnectionString(organisation.ConnectionStringCode);
                _tenantService.UpdateDatabase();
                var data = await _tenantService.RegisterTenant(organisation, organisation.Id.ToString());

                var registerModel = new RegisterModel()
                {
                    Email = model.EmailId,
                    Password = model.Password,
                    TenantId = organisation.Id.ToString(),
                    Role = Roles.Admin.ToString()
                };

                var result = await identityService.Register(registerModel);
                if (result.Successful)
                {
                    registerModel.Id = result.UserId;
                    _tenantService.AddDomainUser(registerModel, data);
                }

                return result.Successful ? Ok(result) : BadRequest(result);
            }
            catch (ClientException cex)
            {
                Log.Error(cex._validationError.message);

                return BadRequest(cex._validationError.message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(TenantId))
                {
                    model.TenantId = TenantId;
                }

                var result = await identityService.Register(model);

                return result.Successful ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login user with Identity
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var result = await identityService.Login(model);

                return result.Successful ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Change forgotten password
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                var result = await identityService.ChangePassword(model);

                return result.Succeeded ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest model)
        {
            try
            {
                RegisterModel registerModel = new RegisterModel
                {
                    Email = model.Email,
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    TenantId = TenantId
                };
                var result = await identityService.Register(registerModel);

                if (result.Successful && model.DomainId.HasValue)
                {
                    await domainService.AddUserToDomain(result.UserId, model.DomainId.Value);
                }

                return result.Successful
                    ? Ok(result)
                    : BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}