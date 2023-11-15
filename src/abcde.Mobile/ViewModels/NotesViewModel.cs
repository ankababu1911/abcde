using abcde.Mobile.Helpers;
using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Services.Settings;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using abcde.Model;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static Akavache.Sqlite3.Internal.SQLite3;

namespace abcde.Mobile.ViewModels
{
    public partial class NotesViewModel : ViewModelBase
    {
        private readonly IAppEnvironmentService _appEnvironmentService;
        private readonly ISettingsService _settingService;

        [ObservableProperty]
        public ObservableCollectionEx<Note> _notes = new();

        [ObservableProperty]
        public bool _isLoading;

        [ObservableProperty]
        public string _noteText;

        public NotesViewModel(IAppEnvironmentService appEnvironmentService, ISettingsService settingsService) : base(appEnvironmentService)
        {
            _appEnvironmentService = appEnvironmentService;
            _settingService = settingsService;
        }

        public override async Task InitializeAsync()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                await IsBusyFor(async () =>
                {
                    var notes = await _appEnvironmentService.NotesService.GetNotes();                    
                    IsLoading = false;
                    if (notes != null)
                    {
                        Notes.ReloadData(notes);
                    }
                });
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplaySnackbar(ex.Message, visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task AddNote()
        {
             try
             {
                 if (IsLoading) return;
                 IsLoading = true;
                 var notes = new List<Note>(Notes);
                 Notes.Clear();
                var note = new Note
                {
                    NoteText = this.NoteText,
                    Date = DateTime.Now,
                    UserId = await _settingService.GetUserIDAsync()
                 };
                 notes.Insert(0, note);
                 this.NoteText = string.Empty;
                 Notes.ReloadData(notes);
                 IsLoading = false;
                _ = Task.Run(async () =>
                {
                    await _appEnvironmentService.NotesService.AddNote(note);
                });
             }
             catch (Exception)
             {
                 await App.Current.MainPage.DisplaySnackbar("Invalid credentials", visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
             }
        }

        [RelayCommand]
        private async void Logout()
        {
            await LocalStorage.RemoveAll();
            _settingService.AuthAccessToken = null;
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
    }
}
