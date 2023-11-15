using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

public partial class NotesView : ContentPageBase
{
    public NotesView(NotesViewModel notesViewModel)
    {
        BindingContext = notesViewModel;
        InitializeComponent();
    }
}