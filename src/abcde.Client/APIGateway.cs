using abcde.Client.Services;
using abcde.Client.Services.Interfaces;
using abcde.Client.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using abcde.Model.Constants;
using abcde.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace abcde.Client
{
    public class APIGateway : IAPIGateway
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;

        private const string httpRequestHeaderTenantId = "tenantId";
        private const string httpRequestHeaderSubTenantId = "subtenantId";
        private const string httpRequestHeaderDepartmentId = "departmentId";
        private const string httpRequestHeaderAuthorization = "Authorization";
        private static string ConnectionStringCode = "";
        private static string _token;
        private static string _tenantId;
        private static string _subTenantId;

        #region ctor

        //public APIGateway(HttpClient httpClient, IMemoryCache memoryCache)
        //{
        //    _httpClient = httpClient;
        //    _memoryCache = memoryCache;
        //}
        public APIGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (!string.IsNullOrEmpty(ConnectionStringCode))
            {
                SetConnectionStringCode(ConnectionStringCode);
            }
        }

        #endregion ctor

        public string Token
        {
            set
            {
                _httpClient.DefaultRequestHeaders.Remove(httpRequestHeaderAuthorization);
                _httpClient.DefaultRequestHeaders.Add(httpRequestHeaderAuthorization, $"Bearer {value}");
                _token = value;
            }
            get
            {
                return _token;
            }
        }

        public string TenantId
        {
            set
            {
                _httpClient.DefaultRequestHeaders.Remove(httpRequestHeaderTenantId);
                _httpClient.DefaultRequestHeaders.Add(httpRequestHeaderTenantId, value);
                _tenantId = value;
            }
            get
            {
                return _tenantId;
            }
        }

        public string SubTenantId
        {
            set
            {
                _httpClient.DefaultRequestHeaders.Add(httpRequestHeaderSubTenantId, value);
                _subTenantId = value;
            }
            get
            {
                return _subTenantId;
            }
        }

        public string DepartmentId
        {
            set
            {
                _httpClient.DefaultRequestHeaders.Add(httpRequestHeaderDepartmentId, value);
            }
        }

        /// <summary>
        /// Sets header required to get the connection string for the tenant
        /// </summary>
        /// <param name="value"></param>
        public void SetConnectionStringCode(string value)
        {
            ConnectionStringCode = value;
            _httpClient.DefaultRequestHeaders.Add(Headers.HttpRequestHeaderConnectionStringCode, value);
        }

        public void SetHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove(httpRequestHeaderAuthorization);
            _httpClient.DefaultRequestHeaders.Add(httpRequestHeaderAuthorization, $"Bearer {_token}");
            _httpClient.DefaultRequestHeaders.Remove(httpRequestHeaderTenantId);
            _httpClient.DefaultRequestHeaders.Add(httpRequestHeaderTenantId, TenantId);
            _httpClient.DefaultRequestHeaders.Remove(httpRequestHeaderSubTenantId);
            _httpClient.DefaultRequestHeaders.Add(httpRequestHeaderSubTenantId, SubTenantId);
            _httpClient.DefaultRequestHeaders.Remove(httpRequestHeaderDepartmentId);
        }

        public IAuditService AuditService => new AuditService(_httpClient);

        public ILoginAuditService LoginAuditService => new LoginAuditService(_httpClient);

        public ITenantService TenantService => new TenantService(_httpClient);

        public ITenantSettingsService TenantSettingsService => new TenantSettingsService(_httpClient);

        public IIdentityService IdentityService => new IdentityService(_httpClient);

        public INoteService NoteService => new NoteService(_httpClient);

        public ITenantAdminService TenantAdminService => new TenantAdminService(_httpClient);

        public IWorkItemService WorkItemService => new WorkItemService(_httpClient);

        public IDomainService DomainService => new DomainService(_httpClient);

        public void SetToken(string token)
        {
            Token = token;
            _httpClient.DefaultRequestHeaders.Authorization =
            !string.IsNullOrEmpty(token)
                ? new AuthenticationHeaderValue("Bearer", token)
                : null;
        }
    }
}