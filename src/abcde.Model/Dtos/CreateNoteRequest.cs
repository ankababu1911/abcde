namespace abcde.Model.Dtos
{
    public class CreateNoteRequest
    {
        public Guid Id { get; set; }
        public string NoteText { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public Guid WorkItemId { get; set; }
    }
}