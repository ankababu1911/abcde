using abcde.Client.Interfaces;
using abcde.Mobile.Helpers;
using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Services.Settings;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace abcde.Mobile.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        #region Properties

        private IAppEnvironmentService _appEnvironmentService;
        private ISettingsService _settingsService;
        private INavigation _navigationService;

        [ObservableProperty] public bool _isErrorMsgVisible;

        [ObservableProperty] public string _email;

        [ObservableProperty] public string _password;

        [ObservableProperty] public bool _isInProgress;

        #endregion Properties

        #region Constructor

        public LoginViewModel(IAppEnvironmentService appEnvironmentService, ISettingsService settingsService, INavigation navigation) : base(appEnvironmentService)
        {
            _appEnvironmentService = appEnvironmentService;
            _settingsService = settingsService;
            _navigationService = navigation;
            IsErrorMsgVisible = false;
        }

        #endregion Constructor

        #region Methods

        [RelayCommand]
        private async Task OnForgotPasswordAsync()
        {
            await Shell.Current.GoToAsync(nameof(EmailVerificationView));
        }

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                if (IsInProgress) return;
                IsInProgress = true;

                // Check for null or whitespace in email and password
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    IsErrorMsgVisible = true;
                    IsInProgress = false;
                    return;
                }

                // Validate email using regex
                if (!IsValidEmail(Email))
                {
                    IsErrorMsgVisible = true;
                    IsInProgress = false;
                    return;
                }

                await IsBusyFor(async () =>
                {
                    var result = await _appEnvironmentService.LoginService.LoginAsync(new Model.Identity.LoginModel
                    {
                        Email = Email.Trim(),
                        Password = Password
                    });

                    IsInProgress = false;
                    if (result.Successful)
                    {
                        _settingsService.AuthAccessToken = result.Token;
                        await _settingsService.SetUserIDAsync(result.UserId);
                        _settingsService.TenantID = result.TenantId;
                        Email = string.Empty;
                        Password = string.Empty;
                        IsErrorMsgVisible = false;
                        var apiGateway = App.Current.Handler.MauiContext.Services.GetService<IAPIGateway>();
                        apiGateway.SetToken(result.Token);
                        if (result.HasChangedPassword)
                        {
                            await Shell.Current.GoToAsync($"//GoalsPage");
                        }
                        else
                        {
                            await Shell.Current.GoToAsync(nameof(ChangePasswordView));
                        }
                    }
                    else
                    {
                        IsErrorMsgVisible = true;
                    }
                });
            }
            catch (Exception ex)
            {
                IsErrorMsgVisible = true;
            }
            finally
            {
                IsInProgress = false;
            }
        }

        // Validating email using regex
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, Constants.EmailRegex);
        }

        #endregion Methods
    }
}