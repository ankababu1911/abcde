using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Services.Settings;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace abcde.Mobile.ViewModels
{
    public partial class InitialSetUpViewModel : ViewModelBase
    {
        private IAppEnvironmentService _appEnvironmentService;
        private ISettingsService _settingsService;
        private INavigation _navigationService;

        [ObservableProperty] public bool _isErrorMsgVisible;

        [ObservableProperty] public string _orgCode;

        public InitialSetUpViewModel(IAppEnvironmentService appEnvironmentService, ISettingsService settingsService, INavigation navigation) : base(appEnvironmentService)
        {
            _appEnvironmentService = appEnvironmentService;
            _settingsService = settingsService;
            _navigationService = navigation;
            IsErrorMsgVisible = false;
        }

        [RelayCommand]
        public async Task SaveOrgCode()
        {
            if (string.IsNullOrWhiteSpace(OrgCode))
            {
                OrgCode = Helpers.Constants.DefaultOrgCode;
            }
            await SecureStorage.SetAsync(Helpers.Constants.OrganisationConnectionStringCode, OrgCode);
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
    }
}