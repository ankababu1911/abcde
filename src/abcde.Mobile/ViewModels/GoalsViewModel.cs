using abcde.Mobile.Resx;
using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using abcde.Model;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace abcde.Mobile.ViewModels
{
    public partial class GoalsViewModel: ViewModelBase
    {
        private readonly IAppEnvironmentService _appEnvironmentService;

        [ObservableProperty] private ObservableCollectionEx<WorkItems> _workItems;

        [ObservableProperty] public bool _isLoading;

        [ObservableProperty] private bool _optionsListView;

        public GoalsViewModel(IAppEnvironmentService appEnvironmentService) : base(appEnvironmentService)
        {
            _appEnvironmentService = appEnvironmentService;
            _workItems = new ObservableCollectionEx<WorkItems>();
        }

        public override async Task InitializeAsync()
        {
            try
            {
                OptionsListView = false;
                if (IsLoading) return;
                IsLoading = true;
                await LoadGoals();
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
        private async Task FlyoutPage()
        {
            OptionsListView = !OptionsListView; // Toggle the value of OptionsListView
        }

        [RelayCommand]
        private async Task GotoNextPage(string buttonAction)
        {
            switch(buttonAction)
            {
                case "PlanMyDay":
                    if (DateTime.Now.Hour <= 18)
                    {
                        await Shell.Current.GoToAsync(nameof(PlanMyDayView));
                    }                    
                    else
                    {
                        await Application.Current.MainPage.DisplaySnackbar(AppResources.ResourceManager.GetString("YouCanPlanYourDayOnlyBefore12PM"), visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);                        
                    }
                    break;
                case "My Goals":
                    await Shell.Current.GoToAsync(nameof(AddGoalView), true, new Dictionary<string, object> { { "WorkItem", null } });
                    break;
                case "My Notes":
                    await Shell.Current.GoToAsync(nameof(AddGoalView), true, new Dictionary<string, object> { { "WorkItem", null } });
                    break;
                case "Settings":
                    await Shell.Current.GoToAsync(nameof(AddGoalView), true, new Dictionary<string, object> { { "WorkItem", null } });
                    break;
                case "About Me":
                    await Shell.Current.GoToAsync(nameof(AddGoalView), true, new Dictionary<string, object> { { "WorkItem", null } });
                    break;
            }
        }

        [RelayCommand]
        public async Task GotoAddGoal()
        {
            await Shell.Current.GoToAsync(nameof(AddGoalView),true, new Dictionary<string, object> { { "WorkItem", null } });
        }


        [RelayCommand]
        private async Task GoalSelected(WorkItem goal)
        {
            await Shell.Current.GoToAsync(nameof(GoalDetailsView), true, new Dictionary<string, object>
             {
                 { "WorkItem", goal }
             });
        }
    
        [RelayCommand]
        private async Task PinToTop(WorkItem goal)
        {
            goal.IsPinned = !goal.IsPinned;
            string res = await _appEnvironmentService.GoalsService.UpdateIsPinned(goal.Id, goal.IsPinned);
            if (res.Equals("UpdatedIsPinnedStatus"))
            {
                await LoadGoals();
            }
            else
            {
                if (res.Equals("YouCanOnlyPin3WorkItems"))
                    await Application.Current.MainPage.DisplaySnackbar(AppResources.ResourceManager.GetString(res), visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);                
            }
        }

        private async Task LoadGoals()
        {
            await IsBusyFor(async () =>
            {
                OptionsListView = false;
                var goals = await _appEnvironmentService.GoalsService.GetGoals();
                IsLoading = false;
                if (goals != null && goals.Any())
                {
                    goals = goals.Where(x => x.ParentId == null && x.Status != Model.ItemStatus.Completed)
                        .OrderBy(x => x.OriginalPlannedEndDateTime)
                        .OrderByDescending(x => x.IsPinned).ToList();
                    var goals2 = new List<WorkItems>();
                   

                    if (goals != null)
                    {
                        foreach (var item in goals)
                        {

                            goals2.Add(new WorkItems()
                            {
                                Title = item.Title,
                                Status = item.Status,
                                Description = item.Description,
                                OriginalPlannedEndDateTime = item.OriginalPlannedEndDateTime,
                                ParentId = item.ParentId,
                                IHaveToDo = item.IHaveToDo,
                                IWantToDo = item.IWantToDo,
                                Important = item.Important,
                                Urgent = item.Urgent,
                                Id = item.Id,
                                IsPinned = item.IsPinned,
                                Complexity = item.Complexity,
                                Priority = item.Priority,
                                CompletedTasks = item.Children==null?0:item.Children.Where(x => x.EndDateTime != null).Count(),
                                TotalTasks = item.Children==null?0:item.Children.Count(),
                                OverallProgress = (float)(item.Children == null ? 0 : item.Children.Where(x => x.EndDateTime != null).Count()) / (item.Children == null ? 0 : item.Children.Count()), 
                                IsVisibleProgress = item.Children==null?false:item.Children.Count() > 0,
                                Children = item.Children,
                                StartDateTime = item.StartDateTime,
                                EndDateTime = item.EndDateTime,
                            }) ;
                        }
                      
                        WorkItems.ReloadData(goals2);
                    }
                       
                }
            });
        }

    }
    public class WorkItems : WorkItem,INotifyPropertyChanged
    {

        private int _completedTasks;
        private int _totalTasks;
        private float _overallProgress;
        private bool _isVisibleProgress;

        public int CompletedTasks
        {
            get { return _completedTasks; }
            set
            {
                _completedTasks = value;
                OnPropertyChanged();
            }
        }
        public int TotalTasks
        {
            get { return _totalTasks; }
            set
            {
                _totalTasks = value;
                OnPropertyChanged();
            }
        }
        public float OverallProgress
       {
            get { return _overallProgress; }
            set
            {
                _overallProgress = value;
                OnPropertyChanged();
            }
        }
        public bool IsVisibleProgress
        {
            get { return _isVisibleProgress; }
            set
            {
                _isVisibleProgress = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
