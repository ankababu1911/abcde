using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class GoalsView : ContentPageBase
{
	public GoalsView(GoalsViewModel goalsViewModel)
	{
		BindingContext = goalsViewModel;
		InitializeComponent();
	}
}