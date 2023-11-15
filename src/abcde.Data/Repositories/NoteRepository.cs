using abcde.Data.Interfaces;
using abcde.Data.Predicates;
using abcde.Data.Repositories.Base;
using abcde.Model;
using abcde.Model.Filters;
using abcde.Model.Summary;

namespace abcde.Data.Repositories
{
    public class NoteRepository : GenericTenantAsyncRepository<Note, NoteSummary, NoteFilter>, INoteRepository
    {
        public NoteRepository(DataContext context) : base(context)
        { }

        public override async Task<IEnumerable<Note>> GetFilteredAsync(NoteFilter filter)
        {
            var predicate = await new NotePredicate().GetPredicate(filter);

            var result = DbSet.Where(predicate).ToList();

            return await Task.Run(() => result);
        }
    }
}
