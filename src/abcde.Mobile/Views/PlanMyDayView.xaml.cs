using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;
[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class PlanMyDayView : ContentPageBase
{
	private readonly PlanMyDayViewModel _viewModel;
	public PlanMyDayView(PlanMyDayViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}
}