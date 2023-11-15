using abcde.Mobile.ViewModels;

namespace abcde.Mobile.Views;

public partial class EmailVerificationView : ContentPage
{
	public EmailVerificationView(EmailVerificationViewModel emailVerificationViewModel)
	{
        BindingContext = emailVerificationViewModel;
        InitializeComponent();
		
    }
}