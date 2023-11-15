using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace abcde.Mobile.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly IAppEnvironmentService _appEnvironmentServices;

        [ObservableProperty]
        public string _email;

        [ObservableProperty]
        public string _password;

        [ObservableProperty]
        public string _confirmPassword;

        [ObservableProperty]
        public bool _isInProgress;

        public RegisterViewModel(IAppEnvironmentService appEnvironmentService) : base(appEnvironmentService)
        {
            _appEnvironmentServices = appEnvironmentService;
        }

        [RelayCommand]
        private async Task Register()
        {
            try
            {
                if (IsInProgress) return;
                IsInProgress = true;
                await IsBusyFor(async () =>
                {
                    var resgiterResult = await _appEnvironmentServices.RegisterService.RegisterAsync(new Model.Identity.RegisterModel
                    {
                        Email = Email.Trim(),
                        Password = Password,
                        ConfirmPassword = ConfirmPassword,
                    });

                    IsInProgress = false;
                    if (resgiterResult.Successful)
                    {
                        Email = string.Empty;
                        Password = string.Empty;
                        ConfirmPassword = string.Empty;
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            App.Current.MainPage = new NavigationPage(new LoginView(App.Current.Handler.MauiContext.Services.GetService<LoginViewModel>()));
                        });
                    }
                    else
                    {
                        await App.Current.MainPage.DisplaySnackbar("Unable to create account");
                    }
                });
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                {
                    await App.Current.MainPage.DisplaySnackbar("Please provide the valid info", visualOptions: _appEnvironmentServices.SettingsService.SnackbarOptions);
                }
                else
                {
                    await App.Current.MainPage.DisplaySnackbar(ex.Message, visualOptions: _appEnvironmentServices.SettingsService.SnackbarOptions);
                }
            }
            finally
            {
                IsInProgress = false;
            }
        }
    }
}
