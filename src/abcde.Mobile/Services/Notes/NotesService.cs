using abcde.Client.Interfaces;
using abcde.Mobile.Services.Settings;
using abcde.Model;

namespace abcde.Mobile.Services.Notes
{
    public class NotesService : INotesService
    {
        private IAPIGateway _apiGateway;
        private ISettingsService _settingsService;

        public NotesService(IAPIGateway aPIGateway, ISettingsService settingsService)
        {
            _apiGateway = aPIGateway;
            _settingsService = settingsService;
        }

        public async Task<Note> AddNote(Note note)
        {
            return await _apiGateway.NoteService.AddAsync(note);
        }

        public async Task DeleteNote(Note note)
        {
            await _apiGateway.NoteService.DeleteAsync(note.Id);
        }

        public async Task<Note> GetNoteById(Guid id)
        {
            if (_settingsService.AuthAccessToken != null)
            {
                return await _apiGateway.NoteService.GetAsync(id);
            }
            else return default;
        }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            return await _apiGateway.NoteService.GetFilteredAsync(new Model.Filters.NoteFilter
            {
                UserId = await _settingsService.GetUserIDAsync()
            });
        }

        public async Task<Note> UpdateNote(Note note)
        {
            return await _apiGateway.NoteService.UpdateAsync(note);
        }
    }
}