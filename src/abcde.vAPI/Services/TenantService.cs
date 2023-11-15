using abcde.Data.Interfaces;
using abcde.Model.Identity;
using abcde.Model;

namespace abcde.vAPI.Services
{
    public interface ITenantService
    {
        List<ApplicationUser> GetTenants(Guid tenantId);
        /// <summary>
        /// Get Tenant based on connectionStringCode
        /// </summary>
        /// <param name="connectionStringCode">A connectionStringCode may contain code </param>
        /// <returns>return Tenant</returns>
        Task<List<Tenant>> GetTenantByConnectionStringCodeAsync(string connectionStringCode);
        /// <summary>
        /// Register tenant
        /// </summary>
        /// <param name="organisation"></param>
        /// <param name="tenantId"></param>
        Task<Domain> RegisterTenant(Organisation organisation, string tenantId);
        /// <summary>
        /// Add DomainUser 
        /// </summary>
        /// <param name="registerModel">A registerModel may a contain Details of the User</param>
        /// <param name="domain">A domain may a contain Domain data </param>
        void AddDomainUser(UserModel registerModel, Domain domain);
        /// <summary>
        /// UpdateDatabase
        /// </summary>
        void UpdateDatabase();
    }

    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantSettingsRepository _tenantSettingsRepository;
        private readonly IDomainRepository _domainRepository;
        private readonly IDomainUserRepository _domainUserRepository;

        public TenantService(ITenantRepository tenantRepository, ITenantSettingsRepository tenantSettingsRepository, IDomainRepository domainRepository, IDomainUserRepository domainUserRepository)
        {
            _tenantRepository = tenantRepository;
            _tenantSettingsRepository = tenantSettingsRepository;
            _domainRepository = domainRepository;
            _domainUserRepository = domainUserRepository;
        }

        public List<ApplicationUser> GetTenants(Guid tenantId)
        {
            return _tenantRepository.GetUsersForTenant(tenantId);
        }
        ///<see cref="ITenantService.GetTenantByConnectionStringCode(string)"/>
        public async void AddDomainUser(UserModel registerModel, Domain domain)
        {
            if (domain != null)
            {
                var domainUser = new DomainUser()
                {
                    Id = Guid.NewGuid(),
                    DomainID = domain.Id,
                    IsDomainHead = true,
                    UserID = registerModel.Id
                };
                await _domainUserRepository.InsertAsync(domainUser);
            }
        }

        ///<see cref="ITenantService.GetTenantByConnectionStringCode(string)"/>
        public Task<List<Tenant>> GetTenantByConnectionStringCodeAsync(string connectionStringCode)
        {
            return _tenantRepository.GetTenantByConnectionStringCodeAsync(connectionStringCode);

        }
        ///<see cref="ITenantService.RegisterTenant(Organisation, string)"/>
        public async Task<Domain> RegisterTenant(Organisation organisation, string tenantId)
        {
            var tenant = new Tenant()
            {
                Id = organisation.Id,
                Name = organisation.Name,
                Domain = organisation.Name,
                TenantId = tenantId,
                CreatedBy = "System",
                Datestamp = DateTime.UtcNow,
                ConnectionStringCode = organisation.ConnectionStringCode
            };

            await _tenantRepository.InsertAsync(tenant);

            var tenantSettings = new TenantSettings()
            {
                Name = organisation.Name,
                ContactEmail = organisation.ContactEmail,
                ContactName = organisation.ContactName,
                LicenseCount = organisation.LicenseCount,
                PhoneNumber = organisation.PhoneNumber,
                Addresses = organisation.Addresses,
                TenantId = tenantId,
                LastCheckedDateTime = DateTime.UtcNow,
                LicenseValidFrom = organisation.LicenseValidFrom,
                LicenseValidTo = organisation.LicenseValidTo
            };

            await _tenantSettingsRepository.InsertAsync(tenantSettings);
            if (organisation != null && tenantId != null)
            {
                var domain = new Domain()
                {
                    Name = organisation.Name + "_root",
                    TenantId = tenantId
                };
                var data = await _domainRepository.InsertAsync(domain);
                return data;
            }

            return null;
        }
        ///<see cref="ITenantService.UpdateDatabase"/>
        public void UpdateDatabase()
        {
            _tenantRepository.UpdateDatabase();
        }
    }
}