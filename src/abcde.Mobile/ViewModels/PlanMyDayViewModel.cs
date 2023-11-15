using abcde.Mobile.Resx;
using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.ViewModels.Base;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace abcde.Mobile.ViewModels
{
    public partial class PlanMyDayViewModel: ViewModelBase
    {
        private readonly IAppEnvironmentService _appEnvironmentService;

        [ObservableProperty]
        private ObservableCollectionEx<WorkItems> _workItems;
        [ObservableProperty]
        public ObservableCollection<WorkItems> items1 = new ObservableCollection<WorkItems>();
        [ObservableProperty]
        public bool _isLoading;
        [ObservableProperty]
        public WorkItems dragItem;
        [ObservableProperty]
        public WorkItems dragItem1;
        [ObservableProperty]
        public bool itemVisible;
        [ObservableProperty]
        public bool dropitem;
        [ObservableProperty]
        public bool allowDrop = false;
        [ObservableProperty]
        public int indexValue;

        public PlanMyDayViewModel(IAppEnvironmentService appEnvironmentService) : base(appEnvironmentService)
        {
            _appEnvironmentService = appEnvironmentService;
            _workItems = new ObservableCollectionEx<WorkItems>();
        }
        public override async Task InitializeAsync()
        {
            try
            {
                // Check if data is already loaded
                if (IsLoading || WorkItems.Count > 0) return; 
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

        private async Task LoadGoals()
        {

            await IsBusyFor(async () =>
            {
                // Get all un completed tasks
                var tasks = await _appEnvironmentService.GoalsService.GetAllIncompletedTasks();
                
                if (tasks != null && tasks.Any())
                {
                    tasks = tasks.OrderBy(x => x.OriginalPlannedEndDateTime)
                                .OrderByDescending(x => x.IsPinned).ToList();

                    var tasks2 = new List<WorkItems>();

                    if (tasks != null)
                    {
                        foreach (var task in tasks)
                        {
                            tasks2.Add(new WorkItems()
                            {
                                Title = task.Title,
                                OriginalPlannedEndDateTime = task.OriginalPlannedEndDateTime,
                                Id = task.Id,
                                TenantId = task.TenantId                                
                            });
                        }
                        WorkItems.ReloadData(tasks2);
                    }
                }

                //get all prioritized tasks
                var prioritizedTasks = await _appEnvironmentService.GoalsService.GetPrioritizedTasks();
                IsLoading = false;
                if (prioritizedTasks != null && prioritizedTasks.Any())
                {
                    prioritizedTasks = prioritizedTasks.OrderBy(x => x.OriginalPlannedEndDateTime)
                                .OrderByDescending(x => x.IsPinned).ToList();

                    var tasks2 = new List<WorkItems>();

                    if (prioritizedTasks != null)
                    {
                        foreach (var task in prioritizedTasks)
                        {
                            tasks2.Add(new WorkItems()
                            {
                                Title = task.Title,
                                OriginalPlannedEndDateTime = task.OriginalPlannedEndDateTime,
                                Id = task.Id,
                                TenantId = task.TenantId
                            });
                        }
                        Items1 = new ObservableCollection<WorkItems>(tasks2);
                    }
                }
            });
        }

        [RelayCommand]
        private async Task DragStarting(WorkItems item)
        {
            if (Items1 != null && Items1.Count > 3)
            {
                await App.Current.MainPage.DisplaySnackbar(AppResources.ResourceManager.GetString("OnlyFourWorkItemsCanBePrioritized"), visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                return;
            }
            DragItem = item;
            ItemVisible = true;
            Dropitem = false;
            IndexValue = WorkItems.IndexOf(item);
            WorkItems.Remove(DragItem);

        }

        [RelayCommand]
        private void DragStarting2(WorkItems item)
        {            
            DragItem = item; 
            ItemVisible = false; 
            Dropitem = true;
            IndexValue = Items1.IndexOf(item); 
            Items1.Remove(DragItem);
        }

        [RelayCommand]
        private void Drop()
        {
            if (Items1 != null &&  Items1.Count > 3)
            {
                return;
            }
            if (ItemVisible)
            {
                Items1.Add(DragItem);
            }
            else
            {
                Items1.Insert(IndexValue, DragItem);
            }
        }

        [RelayCommand]
        private void DropBack()
        {
            if (Dropitem)
            {
                WorkItems.Add(DragItem);
            }
            else
            {
                WorkItems.Insert(IndexValue, DragItem);
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            ItemVisible = false;
            Dropitem = false;
            await Shell.Current.GoToAsync($"//GoalsPage");
        }

        [RelayCommand]
        private async Task SavePrioritizedTasks()
        {
            try
            {
                List<Guid> workItems = new List<Guid>();
                foreach (var item in Items1)
                {
                    workItems.Add(item.Id);
                }
                //save prioritized tasks
                var result = await _appEnvironmentService.GoalsService.SavePrioritizedTasks(workItems);
                if(result == "true")
                {
                    await Application.Current.MainPage.DisplaySnackbar(AppResources.ResourceManager.GetString("PrioritizedTasksSavedSuccessfully"), visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                    await Shell.Current.GoToAsync($"//GoalsPage");
                }
                else
                {
                    await Application.Current.MainPage.DisplaySnackbar(AppResources.ResourceManager.GetString("ErrorWhileSavingPrioritizedTasks"), visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplaySnackbar(ex.Message, visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
            }
        }
    }
}
