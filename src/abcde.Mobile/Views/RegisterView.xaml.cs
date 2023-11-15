using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

public partial class RegisterView : ContentPageBase
{
    public RegisterView(RegisterViewModel registerViewModel)
    {
        BindingContext = registerViewModel;
        InitializeComponent();
    }

    private void LoginTapped(object sender, TappedEventArgs e)
    {
        Navigation.PopToRootAsync();
    }
}