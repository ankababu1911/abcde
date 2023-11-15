using abcde.Data.Interfaces;
using abcde.Data.Repositories.Base;
using abcde.Model;
using abcde.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace abcde.Data.Repositories
{
    public class LocalisationRepository:GenericAsyncRepository<Translation,BaseSummary,BaseFilter>,
        ILocalisationRepository
    {
        private readonly DataContext _context;
        public LocalisationRepository(DataContext context, IDbContextFactory<DataContext> contextFactory) : base(context, contextFactory)
        {
            _context = context;
        }

    }
}
