using abcde.Mobile.Helpers;
using CommunityToolkit.Maui.Core;

namespace abcde.Mobile.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private const string AccessToken = "access_token";
        private const string userID = "userID";
        private const string tenantID = "tenantID";
        private readonly string AccessTokenDefault = string.Empty;
        private readonly string UserIDDefault = string.Empty;
        private readonly string TenantIDDefault = string.Empty;
        private SnackbarOptions snackbarOptions { get; set; }

        public SnackbarOptions SnackbarOptions
        {
            get => snackbarOptions;
            set
            {
                snackbarOptions = new SnackbarOptions { TextColor = Colors.White, BackgroundColor = Colors.Black };
            }
        }

        public string AuthAccessToken
        {
            get => Preferences.Get(AccessToken, AccessTokenDefault);
            set => Preferences.Set(AccessToken, value);
        }

        public async Task<Guid> GetUserIDAsync()
        {
            return await LocalStorage.Get<Guid>(userID);
        }

        public async Task SetUserIDAsync(Guid value)
        {
            await LocalStorage.Insert(userID, value);
        }
        public string TenantID
        {
            get => Preferences.Get(tenantID, TenantIDDefault);
            set => Preferences.Set(tenantID, value);
        }
    }
}