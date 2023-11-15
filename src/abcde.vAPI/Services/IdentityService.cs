using abcde.Data;
using abcde.Data.Interfaces;
using abcde.Data.Services;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Identity;
using abcde.Model.Identity.ViewModels;
using abcde.vAPI.Clients.TWPortal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace abcde.vAPI.Services
{
    public interface IIdentityService
    {
        Task<IdentityResult> AddUserToRole(string email, string roleName);

        Task AddRoleClaim(PermissionClaimViewModel model, string role);

        Task<IdentityResult> UpdateUser(UserModel user);

        Task<RegisterResult> Register(RegisterModel model);

        Task<LoginResult> Login(LoginModel model);

        Task<IdentityResult> ChangePassword(ChangePasswordModel model);

        Task<IdentityResult> ResetPassword(ChangePasswordModel model);
    }

    public class IdentityService : IIdentityService
    {
        private const string ClaimTypePermission = "Permission";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILoginAuditRepository _loginAuditRepository;
        private readonly IOptions<Model.Configuration.Options> _appSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITenantRepository _tenantRepository;
        private readonly ITWPortalService _tWPortalService;
        private readonly ITenantHandlerService setter;

        #region ctor

        public IdentityService(
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILoginAuditRepository loginAuditRepository,
            IOptions<Model.Configuration.Options> appSettings,
            DataContext context,
            ITenantRepository tenantRepository,
            ITWPortalService tWPortalService)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            this.userManager = userManager;
            _loginAuditRepository = loginAuditRepository;
            _appSettings = appSettings;
            _tenantRepository = tenantRepository;
            _tWPortalService = tWPortalService;
            this.setter = new TenantHandlerService(null, appSettings, context);
        }

        #endregion ctor

        /// <summary>
        /// Register user add to role if specified
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<RegisterResult> Register(RegisterModel model)
        {
            try
            {
                var existingUser = await userManager.FindByEmailAsync(model.Email);

                if (existingUser == null)
                {
                    var newUser = await MapUserRegisterModel(model);
                    if (string.IsNullOrEmpty(model.Password))
                    {
                        //generate random password dynamic for each user
                        model.Password = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
                    }
                    var user = await userManager.CreateAsync(newUser, model.Password);
                    var tenantId = new Guid(model.TenantId);
                    if (user.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.Role))
                        {
                            await userManager.AddToRoleAsync(newUser, model.Role);
                        }
                        await _tWPortalService.AddTmUserAsync(new AddTmUserRequest { OrganisationId = tenantId, TmPortalUserId = newUser.Id, TmUserName = newUser.Email });
                    }

                    return new RegisterResult { Successful = user.Succeeded, UserId = newUser.Id, TenantId = tenantId };
                }
                else
                {
                    return new RegisterResult()
                    {
                        Errors = new List<string> { "UserAlreadyExists" },
                        Successful = false
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return new RegisterResult()
                {
                    Errors = new List<string> { ex.Message },
                    Successful = false
                };
            }
        }

        /// <summary>
        /// Update (application) user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateUser(UserModel model)
        {
            var updateUser = await userManager.FindByIdAsync(model.Id.ToString());

            if (updateUser != null)
            {
                updateUser = await MapUserViewModel(updateUser, model);

                return await userManager.UpdateAsync(updateUser);
            }

            return new IdentityResult();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<LoginResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                //now let's try if this belongs to a different organisation
                var code = await _tWPortalService.GetConnectionStringCodeByEmailAsync(model.Email);
                if (!string.IsNullOrEmpty(code))
                {
                    setter.VerifyAndSetConnectionString(code);
                    result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                }
                if (!result.Succeeded)
                {
                    return new LoginResult { Successful = false, Error = "Invalid email or password - please try again" };
                }
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null && user.IsActive)
            {
                //if (user.TenantId != Guid.Empty)
                //{
                //    await AddLoginAudit(user);
                //}
                //if (user.Tenant == null)
                //{
                //    user.Tenant = await _tenantRepository.GetAsync(user.TenantId.Value);
                //}
                var token = await GetToken(user);

                var loginResult = new LoginResult
                {
                    Successful = true,
                    Token = token,
                    UserId = user.Id,
                    HasChangedPassword = user.HasChangedPassword,
                    TenantId = user.TenantId.ToString(),
                    //TenantConnCode = user.Tenant?.ConnectionStringCode
                };

                return loginResult;
            }

            return new LoginResult { Successful = false, Error = "User is not active" };
        }

        /// <summary>
        /// Add login for non admin users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task AddLoginAudit(ApplicationUser user)
        {
            var loginAduit = new LoginAudit()
            {
                UserId = user.Id.ToString(),
                UserName = $"{user.FirstName} {user.LastName}",
                UserEmail = user.Email,
                TenantId = user.TenantId.ToString(),
                Datestamp = DateTime.Now,
            };
            await _loginAuditRepository.InsertAsync(loginAduit);
        }

        /// <summary>
        /// Add individual user claim
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public async Task AddUserClaim(PermissionClaimViewModel model, ClaimsPrincipal user)
        {
            model.Identifier = user.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationUser = await userManager.FindByIdAsync(model.Identifier);

            if (applicationUser != null)
            {
                await userManager.AddClaimAsync(applicationUser, new Claim(model.Type, model.Value));
            }
        }

        /// <summary>
        /// Add individual role claim
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public async Task AddRoleClaim(PermissionClaimViewModel model, string roleName)
        {
            var applicationRole = await _roleManager.FindByNameAsync(roleName);

            if (applicationRole != null)
            {
                await _roleManager.AddClaimAsync(applicationRole, new Claim(model.Type, model.Value));
            }
        }

        /// <summary>
        /// Map model to application user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ApplicationUser> MapUserViewModel(ApplicationUser user, UserModel model)
        {
            user.FirstName = model.Firstname;
            user.LastName = model.Lastname;
            user.IsActive = model.IsActive;
            //user.TenantId = new Guid(model.TenantId);
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.UserName = model.Email;
                user.Email = model.Email;
            }

            return await Task.Run(() => user);
        }

        /// <summary>
        /// Map model to application user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ApplicationUser> MapUserRegisterModel(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                IsActive = true,
                //TenantId = new Guid(model.TenantId),
                FirstName = model.Firstname,
                LastName = model.Lastname,
                UserName = model.Email,
                Email = model.Email
            };

            return await Task.Run(() => user);
        }

        /// <summary>
        /// Get token helper method
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private async Task<string> GetToken(ApplicationUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Value.ApplicationSettings.JWTSettings.Key);

            DateTime currentAdjustedDateTime = DateTime.Now; // Replace with your actual DateTime variable
            DateTime nextDayOneAm = currentAdjustedDateTime.AddDays(1).Date.AddHours(1); // Add 1 day, set time to 00:00:00, and add 1 hour

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(await GetClaimsAsync(user)),
                Issuer = _appSettings.Value.ApplicationSettings.JWTSettings.Issuer,
                Audience = _appSettings.Value.ApplicationSettings.JWTSettings.Audience,
                Expires = nextDayOneAm,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object>
                {
                    { Constants.TenantID, user.TenantId },
                    { Constants.UserID, user.Id.ToString() },
                    { ClaimTypes.Email, user.Email },
                    { ClaimTypes.Name, user.FirstName },
                    //{ Constants.TenantConnCode, user.Tenant?.ConnectionStringCode }
                }
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.Run(() => tokenHandler.WriteToken(token));
        }

        /// <summary>
        /// Get claims helper method
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var userPermissionClaims = userClaims.Where(x => x.Type == ClaimTypePermission);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                var thisRole = await _roleManager.FindByNameAsync(role);
                var allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);

                var permissionClaimTypes = allPermissionsForThisRoles.Where(x => x.Type == ClaimTypePermission);
                permissionClaims.AddRange(permissionClaimTypes);
            }

            //if (user.TenantId == Guid.Empty)
            //{
            //    user.TenantId = Guid.NewGuid();
            //}
            if (string.IsNullOrEmpty(user.DepartmentId))
            {
                user.DepartmentId = string.Empty;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.TenantId.ToString()),
                new(ClaimTypes.PrimarySid, user.TenantId.ToString()),
                new(Constants.TenantID,user.TenantId.ToString())
            }
            .Union(userPermissionClaims)
            .Union(permissionClaims)
            .Union(roleClaims);

            return claims;
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId.ToString());

            if (user != null)
            {
                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

                if (result.Succeeded)
                {
                    user.HasChangedPassword = true;
                    await userManager.UpdateAsync(user);
                }

                return result;
            }

            return new IdentityResult();
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ResetPassword(ChangePasswordModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId.ToString());
            await userManager.RemovePasswordAsync(user);
            return await userManager.AddPasswordAsync(user, model.Password);
        }

        /// <summary>
        /// Add user to role
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AddUserToRole(string email, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                await _roleManager.CreateAsync(new ApplicationRole(roleName));
            }

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new IdentityResult();
            }

            return await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}