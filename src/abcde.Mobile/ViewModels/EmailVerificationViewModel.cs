using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Services.Settings;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace abcde.Mobile.ViewModels
{
    public partial class EmailVerificationViewModel : ViewModelBase
    {
        private IAppEnvironmentService _appEnvironmentService;
        private ISettingsService _settingsService;
        private INavigation _navigationService;

        [ObservableProperty] private bool _verificationCodePopupVisible;
        [ObservableProperty] public string _email;

        [ObservableProperty] public string _verificationCode;
        public EmailVerificationViewModel(IAppEnvironmentService appEnvironmentService, ISettingsService settingsService, INavigation navigation) :base(appEnvironmentService)
        {
            VerificationCodePopupVisible = false;
            _navigationService = navigation;
        }

        [RelayCommand]
        private async Task VerificationCodeAsync()
        {
            VerificationCodePopupVisible = true;
        }

        [RelayCommand]
        private async Task ForgotPasswordAsync()
        {
            await Shell.Current.GoToAsync(nameof(ForgotPasswordView));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}