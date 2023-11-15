using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Filters;
using abcde.Model.Summary;

namespace abcde.Client.Services
{
    public class NoteService : BaseService<Note, NoteSummary, NoteFilter>, INoteService
    {
        public NoteService(HttpClient httpClient) : base(httpClient) => BaseResource = "Notes";

        public Task<Note> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetInstanceAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
