using abcde.Data.Interfaces;
using abcde.Data.Repositories.Base;
using abcde.Model;
using abcde.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace abcde.Data.Repositories
{
    public class DomainRepository : GenericAsyncRepository<Domain, BaseSummary, BaseFilter>, IDomainRepository
    {
        private readonly IDbContextFactory<DataContext> _context;

        public DomainRepository(IDbContextFactory<DataContext> context) : base(context)
        {
            _context = context;
        }

        /// <see cref="IDomainRepository.GetAllAsync()"/>
        public override async Task<IEnumerable<Domain>> GetAllAsync()
        {
            await Task.CompletedTask;
            var domains = DbSet.Include(x => x.DomainUsers).ToList();
            return domains;
        }

        /// <see cref="IDomainRepository.GetRootDomainAsync(Guid)"/>
        public async Task<IEnumerable<Domain>> GetAllDomainsAsync(Guid userId)
        {
            await Task.CompletedTask;
            return DbSet.Include(x => x.DomainUsers)
                .Include(x => x.Children)
                .Where(d => d.DomainUsers.Any(du => du.UserID == userId))                
                .ToList();
        }

        /// <see cref="IDomainRepository.AddUserToDomainAsync(Guid, Guid)"/>
        public async Task<bool> AddUserToDomainAsync(Guid userId, Guid domainId)
        {
            if (!DataContext.Set<DomainUser>().Any(x => x.UserID == userId && x.DomainID == domainId))
            {
                var domainUser = new DomainUser
                {
                    DomainID = domainId,
                    UserID = userId
                };
                DataContext.Set<DomainUser>().Add(domainUser);
                await DataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <see cref="IDomainRepository.GetDomainAsync(Guid)"/>
        public async Task<Domain> GetDomainAsync(Guid domainId)
        {            
            return await DbSet.Include(x => x.DomainUsers)
                .Include(x => x.Children)
                .FirstOrDefaultAsync(d => d.Id == domainId);
        }

        /// <see cref="IDomainRepository.GetAllDomainsAsync(Guid)"/>
        public async Task<Domain> GetRootDomainAsync(Guid tenantId)
        {            
            return await DbSet.FirstOrDefaultAsync(d => d.TenantId == tenantId.ToString() && d.ParentId == null);
        }

        /// <see cref="IDomainRepository.GetDomainByNameAsync(string, Guid)"/>
        public async Task<Domain> GetDomainByNameAsync(string domainName, Guid tenantId)
        {
            return await DbSet.FirstOrDefaultAsync(d => d.Name == domainName && d.TenantId == tenantId.ToString());
        }
    }
}