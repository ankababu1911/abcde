using abcde.Data.Repositories.Base;
using abcde.Data.Interfaces;
using abcde.Model;
using abcde.Model.Base;
using Microsoft.EntityFrameworkCore;
using abcde.Model.Filters;
using abcde.Data.Predicates;
using Serilog;

namespace abcde.Data.Repositories
{
    public class AuditRepository : GenericTenantAsyncRepository<Audit, BaseSummary, AuditFilter>, IAuditRepository
    {
        private readonly IDbContextFactory<DataContext> _context;
        #region ctor

        public AuditRepository(IDbContextFactory<DataContext> context, IDbContextFactory<DataContext> contextFactory) : base(context, contextFactory)
        {
            _context = context;
        }

        #endregion

        public override async Task<IEnumerable<Audit>> GetFilteredAsync(AuditFilter filter)
        {
            try
            {
                var predicate = await new AuditPredicate().GetPredicate(filter);

                var result = _context.Audits.Where(predicate);

                return await Task.Run(() => result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }
    }
}
