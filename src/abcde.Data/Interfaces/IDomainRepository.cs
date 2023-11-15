using abcde.Data.Interfaces.Base;
using abcde.Model;
using abcde.Model.Base;

namespace abcde.Data.Interfaces
{
    public interface IDomainRepository : IGenericAsyncRepository<Domain, BaseSummary, BaseFilter>
    {
        /// <summary>
        /// Get all domains for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Domain>> GetAllDomainsAsync(Guid userId);

        /// <summary>
        /// Add a user to a domain
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="domainId"></param>
        /// <returns></returns>
        Task<bool> AddUserToDomainAsync(Guid userId, Guid domainId);

        /// <summary>
        /// Get a domain by id
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        Task<Domain> GetDomainAsync(Guid domainId);


        /// <summary>
        /// get root domain for a tenant
        /// </summary>
        /// <param name="tenantId"></param>
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
}