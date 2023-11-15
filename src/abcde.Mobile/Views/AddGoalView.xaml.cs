using abcde.Mobile.ViewModels;
using abcde.Model;

namespace abcde.Mobile.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class AddGoalView : ContentPageBase
{
    AddGoalViewModel _vm;
    public AddGoalView(AddGoalViewModel addGoalViewModel)
	{
		BindingContext = _vm =  addGoalViewModel;
		InitializeComponent();
	}

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.EndsWith(" "))
        {
            string tag = e.NewTextValue.Trim();
            _vm.Tags.Add(tag);
            MyEntry.Text = ""; 

            // Update the CollectionView
            TagCollectionView.ItemsSource = _vm.Tags;
        }
    }

    protected override bool OnBackButtonPressed()
    {
        _vm.OnBackButtonPressed();
        return true;
       
    }

    private void UrgentSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        _vm.UrgentValue = e.NewValue;
    }

    private void ImportantSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        _vm.ImportantValue = e.NewValue;
    }
}