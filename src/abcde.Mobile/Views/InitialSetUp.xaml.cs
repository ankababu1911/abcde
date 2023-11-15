using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class InitialSetUp : ContentPage
{
    public InitialSetUp(InitialSetUpViewModel initialSetUpViewModel)
    {
        BindingContext = initialSetUpViewModel;
        InitializeComponent();
    }
}