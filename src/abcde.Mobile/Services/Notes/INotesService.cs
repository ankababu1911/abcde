using abcde.Model;
namespace abcde.Mobile.Services.Notes
{
    public interface INotesService
    {
        Task<Note> AddNote(Note note);
        Task<IEnumerable<Note>> GetNotes();
        Task<Note> GetNoteById(Guid id);
        Task<Note> UpdateNote(Note note);
        Task DeleteNote(Note note);
    }
}
