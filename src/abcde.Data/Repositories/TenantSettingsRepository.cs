using abcde.Data.Interfaces;
using abcde.Data.Repositories.Base;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;
using abcde.Model.Summary;
using System;

namespace abcde.Data.Repositories
{
    public class TenantSettingsRepository : GenericTenantAsyncRepository<TenantSettings, BaseTenantSummary, BaseTenantFilter>, ITenantSettingsRepository
    {
        private readonly DataContext _context;

        #region ctor

        public TenantSettingsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        /// <summary>
        /// Get tenant culture
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<string> GetTenantCulture(string tenantId)
        {
            var result = (from ov in DbSet.Where(t => t.TenantId == tenantId.ToUpper()) select ov.TenantCulture).FirstOrDefault();

            return await Task.Run(() => result);
        }
    }
}
