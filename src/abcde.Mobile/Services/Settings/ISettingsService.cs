using CommunityToolkit.Maui.Core;

namespace abcde.Mobile.Services.Settings
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
        Task<Guid> GetUserIDAsync();
        Task SetUserIDAsync(Guid value);
        string TenantID { get; set; }
        SnackbarOptions SnackbarOptions { get; set; }
    }
}
