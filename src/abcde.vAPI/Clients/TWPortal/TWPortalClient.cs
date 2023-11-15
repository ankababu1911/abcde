using abcde.Model;
using abcde.Model.Identity;
using abcde.Model.Exceptions;

namespace abcde.vAPI.Clients.TWPortal
{
    public class TWPortalClient : ITWPortalService
    {
        #region Variables

        private const string validateResource = "Api/Organisation/ValidateOrg";
        private const string addTmUserResource = "Api/TmUser/AddTmUser";
        private const string GetConnectionStringCodeById = "Api/TmUser/GetConnectionStringCodeById/{0}";
        private const string GetConnectionStringByEmailId = "Api/TmUser/GetConnectionStringByEmailId/?emailId={0}";
        private const string TWPortalUri = "Settings:API:TWBaseUri";
        private readonly HttpClient _httpClient;

        #endregion Variables

        #region ctor

        public TWPortalClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration[TWPortalUri]);
        }

        #endregion ctor

        #region Methods

        /// <see cref="ITWPortalService.ValidateOrganisationAdministrator(VerifyOrganisation)"
        public async Task<Organisation> ValidateOrganisationAdministrator(VerifyOrganisation model)
        {
            var response = await _httpClient.PostAsJsonAsync(validateResource, model);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();

                throw new ClientException(response.StatusCode, errorResponse);
            }

            return await response.Content.ReadFromJsonAsync<Organisation>();
        }

        /// <see cref="ITWPortalService.AddTmUserAsync(AddTmUserRequest)"/>
        public async Task<bool> AddTmUserAsync(AddTmUserRequest addTmUserRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(addTmUserResource, addTmUserRequest);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
            catch
            {
            }
            return false;
        }

        /// <see cref="ITWPortalService.GetConnectionStringCodeAsync(Guid)"/>
        public async Task<string> GetConnectionStringCodeAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(string.Format(GetConnectionStringCodeById, userId));

                return response;
            }
            catch
            {
            }
            return string.Empty;
        }

        /// <see cref="ITWPortalService.GetConnectionStringCodeByEmailAsync(string)"/>
        public async Task<string> GetConnectionStringCodeByEmailAsync(string emailId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(string.Format(GetConnectionStringByEmailId, emailId));

                return response;
            }
            catch
            {
            }
            return string.Empty;
        }

        #endregion Methods
    }
}