using abcde.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace abcde.Mobile.ViewModels
{
    public class NoteModel: INotifyPropertyChanged
    {
        public NoteModel(Note note)
        {
            Id=note.Id;
            NoteText = note.NoteText;
            Date = note.Date;
            UserId = note.UserId;
            WorkItemId = note.WorkItemId;
        }
        public Guid Id { get; set; }
        public string NoteText { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public Guid WorkItemId { get; set; }

        private bool _isSelected ;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
