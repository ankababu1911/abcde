using abcde.Mobile.Services.Auth;
using abcde.Mobile.Services.Goals;
using abcde.Mobile.Services.Notes;
using abcde.Mobile.Services.Settings;

namespace abcde.Mobile.Services.AppEnvironment
{
    public class AppEnvironmentService : IAppEnvironmentService
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly INotesService _noteService;
        private readonly IGoalsService  _goalsService;
        private readonly INavigation _navigationService;
        private readonly ISettingsService _settingsService;

        public ILoginService LoginService { get; private set; }
        public IRegisterService RegisterService { get; private set; }
        public INotesService NotesService { get; private set; }
        public IGoalsService GoalsService { get; private set; }
        public ISettingsService SettingsService { get; private set; }
        public INavigation NavigationService { get; private set; }
        public AppEnvironmentService(ILoginService loginService,
            IRegisterService registerService,
            INotesService noteService,
            IGoalsService goalsService,
            ISettingsService settingsService,
            INavigation navigationService)
        {
            _loginService = loginService;
            _registerService = registerService;
            _noteService = noteService;
            _settingsService = settingsService;
            _navigationService = navigationService;
            _goalsService = goalsService;
        }

        public void UpdateDependencies(bool useMockServices)
        {
            LoginService = _loginService;
            RegisterService = _registerService;
            NotesService = _noteService;
            GoalsService = _goalsService;
            NavigationService = _navigationService;
            SettingsService = _settingsService;
        }

        public void SetToken(string token)
        {

        }
    }
}
