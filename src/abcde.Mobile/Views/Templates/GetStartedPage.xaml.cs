using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views.Templates;

public partial class GetStartedPage : ContentPage
{
	public GetStartedPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            MainThread.BeginInvokeOnMainThread(() => { App.Current.MainPage = new NavigationPage(new LoginView(App.Current.MainPage.Handler.MauiContext.Services.GetService<LoginViewModel>())); });
        });
    }
}