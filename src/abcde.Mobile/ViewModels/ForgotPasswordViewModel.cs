using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Services.Settings;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace abcde.Mobile.ViewModels
{
    public partial class ForgotPasswordViewModel : ViewModelBase
    {
        [ObservableProperty] private bool _loginPopupVisible;

        [ObservableProperty] public string _newPassword;

        [ObservableProperty] public string _confirmPassword;
        public ForgotPasswordViewModel(IAppEnvironmentService appEnvironmentService, ISettingsService settingsService, INavigation navigation) : base(appEnvironmentService)
        {
            LoginPopupVisible = false;
        }

        [RelayCommand]
        private async Task ChangePasswordAsync()
        {
            LoginPopupVisible = true;
        }

        [RelayCommand]
        private async Task LoginViewAsync()
        {
            LoginPopupVisible = false;
            await Shell.Current.GoToAsync(nameof(LoginView));
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