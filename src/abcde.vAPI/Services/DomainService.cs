using abcde.Data.Interfaces;
using abcde.Model;

namespace abcde.vAPI.Services
{
    public interface IDomainService
    {
        /// <summary>
        /// Get all domains for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Domain>> GetAllDomains(Guid userId);

        /// <summary>
        /// Add user to domain
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="domainId"></param>
        /// <returns></returns>

        Task<bool> AddUserToDomain(Guid userId, Guid domainId);

        /// <summary>
        /// Get domain by id
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>

        Task<Domain> GetDomainAsync(Guid domainId);

        /// <summary>
        /// Get Root Domain
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Domain> GetRootDomainAsync(Guid tenantId);

        /// <summary>
        /// get domain by name
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<Domain> GetDomainByNameAsync(string domainName, Guid tenantId);
    }

    public class DomainService : IDomainService
    {
        private readonly IDomainRepository _domainRepository;        
        public DomainService(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;            
        }

        /// <see cref="IDomainService.GetAllDomains(Guid)"/>
        public async Task<IEnumerable<Domain>> GetAllDomains(Guid userId)
        {
            var result = await _domainRepository.GetAllDomainsAsync(userId);
            foreach (var domain in result)
            {
                var childern = result.Where(x => x.ParentId == domain.Id);
                if (childern != null && childern.Any())
                {
                    foreach (var subDomain in childern)
                    {
                        var subDomains = result.Where(x => x.ParentId == subDomain.Id);
                        if (subDomains != null && subDomains.Any())
                        {
                            if (subDomain.Children == null)
                            {
                                subDomain.Children = new List<Domain>();
                            }
                            subDomain.Children = subDomains.ToList();
                        }
                    }
                    if (domain.Children == null)
                    {
                        domain.Children = new List<Domain>();
                    }
                    domain.Children = childern.ToList();
                }
            }
            var domains = result.Where(x => !x.ParentId.HasValue).ToList();
            if (domains != null && domains.Any())
            {
                var rootDomain = domains.FirstOrDefault(x => x.ParentId == null && x.Name.Contains("_root"));
                if (rootDomain != null)
                    return rootDomain.Children;
            }
            return result;
        }

        /// <see cref="IDomainService.AddUserToDomain(Guid, Guid)"/>
        public async Task<bool> AddUserToDomain(Guid userId, Guid domainId)
        {
            var result = await _domainRepository.AddUserToDomainAsync(userId, domainId);
            return result;
        }

        /// <see cref="IDomainService.GetDomainAsync(Guid)"/>
        public async Task<Domain> GetDomainAsync(Guid domainId)
        {
            return await _domainRepository.GetDomainAsync(domainId);
        }


        /// <see cref="IDomainService.GetRootDomainAsync(Guid)"/>
        public async Task<Domain> GetRootDomainAsync(Guid tenantId)
        {
            return await _domainRepository.GetRootDomainAsync(tenantId);
        }

        ///<see cref="IDomainService.GetDomainByNameAsync(string, Guid)"/>
        public async Task<Domain> GetDomainByNameAsync(string domainName, Guid tenantId)
        {
            return await _domainRepository.GetDomainByNameAsync(domainName, tenantId);
        }
    }
}