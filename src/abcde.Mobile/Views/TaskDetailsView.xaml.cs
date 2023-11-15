using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class TaskDetailsView : ContentPage
{
	public TaskDetailsView(TaskDetailViewModel taskDetailViewModel)
	{
		InitializeComponent();
		BindingContext = taskDetailViewModel;
	}
    protected override bool OnBackButtonPressed()
    {
		Shell.Current.GoToAsync(nameof(GoalDetailsView ));
		return true;
    }
}