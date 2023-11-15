using abcde.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using abcde.Model.Filters;
using abcde.Data.Interfaces;
using abcde.Model;
using abcde.Model.Summary;

namespace abcde.Data.Repositories
{
    public class LoginAuditRepository : GenericAsyncRepository<LoginAudit, LoginAuditSummary, LoginAuditFilter>, ILoginAuditRepository
    {
        private readonly DataContext _context;

        #region ctor

        public LoginAuditRepository(DataContext context, IDbContextFactory<DataContext> contextFactory) : base(context, contextFactory)
        {
            _context = context;
        }

        #endregion
    }
}
