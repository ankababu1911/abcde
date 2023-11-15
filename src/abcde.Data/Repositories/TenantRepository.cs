using abcde.Data.Repositories.Base;
using abcde.Data.Interfaces;
using abcde.Model;
using abcde.Model.Base;
using Microsoft.EntityFrameworkCore;
using abcde.Model.Filters;
using abcde.Data.Predicates.Base;
using abcde.Model.Identity;

namespace abcde.Data.Repositories
{
    public class TenantRepository : GenericTenantAsyncRepository<Tenant, BaseTenantSummary, TenantFilter>, ITenantRepository
    {
        #region ctor

        public TenantRepository(DataContext context, IDbContextFactory<DataContext> contextFactory) : base(context, contextFactory)
        { }

        #endregion ctor

        //public override async Task<Tenant> GetAsync(string entityId)
        //{
        //    return await DbSet.Where(t => t.TenantId == entityId).FirstOrDefaultAsync();
        //}

        public override Task<BaseTenantSummary> GetSummaryAsync(string tenantId)
        {
            return base.GetSummaryAsync(tenantId);
        }
        public async Task<List<Tenant>> GetTenantByConnectionStringCodeAsync(string connectionStringCode)
        {

            var result = DbSet.Where(x => x.ConnectionStringCode == connectionStringCode).ToList();
            return await Task.Run(() => result);

        }
        //public override async Task<IEnumerable<BaseTenantSummary>> GetFilteredSummaryAsync(TenantFilter filter)
        //{
        //    var predicate = await new GenericTenantFilterPredicate<Tenant, TenantFilter>().GetPredicate(filter);

        //    var query = from ev in Get(predicate, q => q.OrderBy(d => d.Datestamp))
        //                select
        //                new BaseTenantSummary()
        //                {
        //                    Id = ev.Id,
        //                    Name = ev.Name,
        //                    TenantId = ev.TenantId
        //                };

        //    return await Task.Run(() => query);
        //}

        /// <see cref="ITenantRepository.GetUsersForTenant(Guid)"/>"/>
        public List<ApplicationUser> GetUsersForTenant(Guid tenantId)
        {
            var users = DbSet.Include(t => t.Users).Where(x => x.Id == tenantId).SelectMany(x => x.Users).ToList();
            return users;
        }
    }
}