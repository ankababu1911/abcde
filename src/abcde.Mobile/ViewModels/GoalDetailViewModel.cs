using abcde.Mobile.Helpers;
using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Views;
using abcde.Model;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;

namespace abcde.Mobile.ViewModels
{
    [QueryProperty(nameof(WorkItem), "WorkItem")]
    public partial class GoalDetailViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IAppEnvironmentService _appEnvironmentService;

        #region Property

        [ObservableProperty] private List<WorkItem> _taskItems;
        [ObservableProperty] private WorkItem _newWorkItem;
        [ObservableProperty] private WorkItems _workItem;
        [ObservableProperty] public string _title;
        [ObservableProperty] public string _description;
        [ObservableProperty] private int _completedTasks;
        [ObservableProperty] private int _totalTasks;
        [ObservableProperty] private float _overallProgress;
        [ObservableProperty] private bool _isVisibleProgress;
        [ObservableProperty] private DateTime? _originalPlannedEndDateTime;
        [ObservableProperty] public bool _isLoading;
        [ObservableProperty] public string _priority;
        [ObservableProperty] private bool _saveAndCancel;
        [ObservableProperty] private string _noteDescription;
        [ObservableProperty] private bool _savedNotes;
        [ObservableProperty] private List<Note> _notesList;
        [ObservableProperty] private ObservableCollectionEx<NoteModel> _notesviewmodel;
        [ObservableProperty] public bool _isPriorityVisible;

        [ObservableProperty]
        public ObservableCollectionEx<Note> _notes = new();

        #region IsBusy

        private long _isBusy;

        public bool IsBusy => Interlocked.Read(ref _isBusy) > 0;

        public async Task IsBusyFor(Func<Task> unitOfWork)
        {
            Interlocked.Increment(ref _isBusy);
            OnPropertyChanged(nameof(IsBusy));

            try
            {
                await unitOfWork();
            }
            finally
            {
                Interlocked.Decrement(ref _isBusy);
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        #endregion IsBusy

        #endregion Property

        public GoalDetailViewModel()
        {
            _appEnvironmentService = MauiProgram.GetService<IAppEnvironmentService>();
            _taskItems = new List<WorkItem>();
            NewWorkItem = new WorkItem();
            Notesviewmodel = new ObservableCollectionEx<NoteModel>();
            SaveAndCancel = false;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            WorkItems workItem = query["WorkItem"] as WorkItems;
            WorkItem = workItem;
            InitAsync();
        }

        private List<NoteModel> originalNotesViewmodel;

        private async void InitAsync()
        {
            try
            {
                NoteDescription = string.Empty;
                SaveAndCancel = true;
                if (IsLoading) return;
                IsLoading = true;

                Title = WorkItem.Title;
                Description = WorkItem.Description;
                TotalTasks = WorkItem.TotalTasks;
                CompletedTasks = WorkItem.CompletedTasks;
                OverallProgress = WorkItem.OverallProgress;
                IsVisibleProgress = WorkItem.IsVisibleProgress;
                OriginalPlannedEndDateTime = WorkItem.OriginalPlannedEndDateTime;
                Priority = WorkItem.Priority.ToString();
                if(Priority == "Notset")
                {
                    IsPriorityVisible = false;
                }
                else
                {
                    IsPriorityVisible = true;
                }
                if(WorkItem.Children != null)
                {
                    TaskItems = WorkItem.Children.ToList();
                   
                }
                else
                {
                    TaskItems = new List<WorkItem>();
                }
                SavedNotes = true;

                await LoadNotes();
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
        private async Task EditGoal()
        {
            NewWorkItem.Id = WorkItem.Id;
            NewWorkItem.Title = WorkItem.Title;
            NewWorkItem.Children = WorkItem.Children;
            NewWorkItem.StartDateTime = WorkItem.StartDateTime;
            NewWorkItem.EndDateTime = WorkItem.EndDateTime;
            NewWorkItem.OriginalPlannedEndDateTime = WorkItem.OriginalPlannedEndDateTime;
            NewWorkItem.Parent = WorkItem.Parent;
            NewWorkItem.Status = WorkItem.Status;
            NewWorkItem.ParentId = WorkItem.ParentId;
            NewWorkItem.Description = WorkItem.Description;
            NewWorkItem.IWantToDo = WorkItem.IWantToDo;
            NewWorkItem.IHaveToDo = WorkItem.IHaveToDo;
            NewWorkItem.Important = WorkItem.Important;
            NewWorkItem.Priority = WorkItem.Priority;
            NewWorkItem.Complexity = WorkItem.Complexity;
            NewWorkItem.Urgent = WorkItem.Urgent;
            NewWorkItem.EndDateTime = WorkItem.EndDateTime;
            await Shell.Current.GoToAsync(nameof(AddGoalView), new Dictionary<string, object> { { "WorkItem", NewWorkItem } });
        }

        [RelayCommand]
        public async Task SaveNote()
        {
            try
            {
                var result = await _appEnvironmentService.NotesService.AddNote(new Note
                {
                    UserId = await _appEnvironmentService.SettingsService.GetUserIDAsync(),
                    Date = DateTime.Now,
                    NoteText = NoteDescription,
                    WorkItemId = WorkItem.Id
                });

                NoteDescription = string.Empty;
                if (result != null)
                {
                    SavedNotes = true;
                    await LoadNotes();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        [RelayCommand]
        public async Task EditNote(NoteModel note)
        {
            try
            {
                await Task.CompletedTask;
                if (Notesviewmodel.Any(x => x.NoteText == note.NoteText))
                {
                    Notesviewmodel.FirstOrDefault(x => x.NoteText == note.NoteText).IsSelected = true;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        [RelayCommand]
        public async Task CancelNote(NoteModel note)
        {
            try
            {
                await LoadNotes();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        [RelayCommand]
        public async Task UpdateNote(NoteModel note)
        {
            try
            {
                IsLoading = true;
                Note updatedNote = new Note
                {
                    Id = note.Id,
                    UserId = note.UserId,
                    Date = DateTime.Now,
                    NoteText = note.NoteText,
                    WorkItemId = WorkItem.Id
                };

                var result = await _appEnvironmentService.NotesService.UpdateNote(updatedNote);

                if (result != null)
                {
                    IsLoading = false;
                    await LoadNotes();
                }
                else
                {
                    IsLoading = false;
                    await Shell.Current.DisplayAlert("Update Failed", "Please check again", "OK");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        [RelayCommand]
        public async Task DeleteNote(NoteModel note)
        {
            try
            {
                IsLoading = true;
                await _appEnvironmentService.NotesService.DeleteNote(new Note
                {
                    Id = note.Id
                });
                IsLoading = false;
                await LoadNotes();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async Task LoadNotes()
        {
           
                try
                {
                    IsLoading = true;
                    NotesList = (await _appEnvironmentService.NotesService.GetNotes()).ToList();

                    Notesviewmodel.Clear();

                    var notesview = new List<NoteModel>();
                    var Listnotes = NotesList.Where(y => y.WorkItemId == WorkItem.Id).OrderByDescending(x=>x.Created);
                    if (Listnotes.Any())
                    {
                        
                            foreach (var note in Listnotes)
                            {
                                notesview.Add(new NoteModel(note)
                                {
                                    Date = note.Date,
                                    NoteText = note.NoteText,
                                });
                            }
                    Notesviewmodel.ReloadData(notesview);
                        
                    }
                    else
                    {
                        Notesviewmodel.Clear();
                        Notes.Clear();
                        SavedNotes = false;
                    }
                
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                finally
                {
                    IsLoading = false;
                }
           
        }

        [RelayCommand]
        private async Task NewTask()
        {
            await Shell.Current.GoToAsync(nameof(AddGoalView), new Dictionary<string, object> { { "WorkItem", 
                    new WorkItem{
                      ParentId = WorkItem.Id,
                      Parent = WorkItem,
            } } });
        }

        [RelayCommand]
        private async Task TaskView(WorkItem workItem)
        {
            workItem.Parent = WorkItem;
            await Shell.Current.GoToAsync(nameof(TaskDetailsView), true, new Dictionary<string, object>
             {
                 { "WorkItem", workItem }
             });
        }
    }
}