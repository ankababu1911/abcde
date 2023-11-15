using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel loginViewModel)
    {
        BindingContext = loginViewModel;
        InitializeComponent();
    }

    private void CreateAccountTapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new RegisterView(Handler.MauiContext.Services.GetService<RegisterViewModel>()));
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}