using abcde.Mobile.Resx;
using abcde.Mobile.ViewModels;
using abcde.Model;
using System.Web;

namespace abcde.Mobile.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class GoalDetailsView : ContentPageBase
{
    private GoalDetailViewModel viewModel;
    private ViewCell lastCell;

    public GoalDetailsView(GoalDetailViewModel goalDetailViewModel)
    {
        InitializeComponent();
        viewModel = goalDetailViewModel;
        viewModel.SaveAndCancel = false;
        saveandedit.IsVisible = false;
        EditorforNotes.Unfocus();
        BindingContext = goalDetailViewModel;
    }

    private void ViewCell_Tapped(object sender, EventArgs e)
    {
        if (lastCell != null)
            lastCell.View.BackgroundColor = Colors.Transparent;
        var viewCell = (ViewCell)sender;
        if (viewCell.View != null)
        {
            viewCell.View.BackgroundColor = Colors.LightGray;
            lastCell = viewCell;
        }
    }

    private void OnClicktoSave(object sender, EventArgs e)
    {
        EditorforNotes.Unfocus();
        viewModel.SaveNote();
    }

    private void OnClickToCancel(object sender, EventArgs e)
    {
        EditorforNotes.Text = string.Empty;
        saveandedit.IsVisible=false;
        EditorforNotes.Unfocus();
    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync($"//GoalsPage");
        return true;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        EditorforNotes.Focus();
        if (saveandedit.IsVisible == false)
        {
            saveandedit.IsVisible = true;
        }
    }
}