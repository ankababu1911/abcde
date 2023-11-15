using abcde.Data.Repositories.Base;
using abcde.Data.Interfaces;
using abcde.Model;
using Microsoft.EntityFrameworkCore;
using abcde.Model.Filters;
using abcde.Data.Predicates.Base;
using abcde.Model.Summary;

namespace abcde.Data.Repositories
{
    public class TenantAdminRepository : GenericAsyncRepository<Tenant, TenantAdminSummary, TenantAdminFilter>, ITenantAdminRepository
    {
        #region ctor

        public TenantAdminRepository(DataContext context, IDbContextFactory<DataContext> contextFactory) : base(context, contextFactory)
        { }

        #endregion

        public override async Task<IEnumerable<TenantAdminSummary>> GetAllSummaryAsync()
        {
            var result = (from t in base.Get()
                          select new TenantAdminSummary
                          {
                              Id = t.Id,
                              TenantId = t.TenantId,
                              Name = t.Name,
                              Status = t.Status
                          }).ToList();

            return await Task.Run((() => result));
        }

        public override async Task<IEnumerable<Tenant>> GetFilteredAsync(TenantAdminFilter filter)
        {
            var predicate = await new GenericFilterPredicate<Tenant, TenantAdminFilter>().GetPredicate(filter);

            return Get(predicate, q => q.OrderBy(d => d.Datestamp));
        }

        public override async Task<IEnumerable<TenantAdminSummary>> GetFilteredSummaryAsync(TenantAdminFilter filter)
        {
            var predicate = await new GenericFilterPredicate<Tenant, TenantAdminFilter>().GetPredicate(filter);

            var query = from ev in Get(predicate, q => q.OrderBy(d => d.Datestamp))
                        select
                        new TenantAdminSummary()
                        {
                            Id = ev.Id,
                            Name = ev.Name,
                            TenantId = ev.TenantId,
                            Status = ev.Status
                        };

            return await Task.Run(() => query);

        }
    }
}
