using abcde.Mobile.Services.Auth;
using abcde.Mobile.Services.Goals;
using abcde.Mobile.Services.Notes;
using abcde.Mobile.Services.Settings;

namespace abcde.Mobile.Services.AppEnvironment
{
    public interface IAppEnvironmentService
    {
        ILoginService LoginService { get; }
        IRegisterService RegisterService { get; }
        INotesService NotesService { get; }
        IGoalsService GoalsService { get; }
        INavigation NavigationService { get; }
        ISettingsService SettingsService { get; }
        void UpdateDependencies(bool useMockServices);
        void SetToken(string token);
    }
}
