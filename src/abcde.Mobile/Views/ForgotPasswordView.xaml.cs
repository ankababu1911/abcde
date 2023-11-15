using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

public partial class ForgotPasswordView : ContentPage
{
	public ForgotPasswordView(ForgotPasswordViewModel forgotPasswordViewModel)
	{
        BindingContext = forgotPasswordViewModel;
        InitializeComponent();
    }
}