using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Views;
using abcde.Model;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Mobile.ViewModels
{
    [QueryProperty(nameof(WorkItem), "WorkItem")]
    public partial class TaskDetailViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IAppEnvironmentService _appEnvironmentService;


        [ObservableProperty] private List<WorkItem> _subTaskItems;
        [ObservableProperty] private WorkItem _workItem;
        [ObservableProperty] public string _title;
        [ObservableProperty] public string _goalTitle;
        [ObservableProperty] public string _description;
        [ObservableProperty] private DateTime? _originalPlannedEndDateTime;
        [ObservableProperty] public bool _isLoading;
        [ObservableProperty] public string _priority;

        public TaskDetailViewModel()
        {
            _appEnvironmentService = MauiProgram.GetService<IAppEnvironmentService>();
            _subTaskItems = new List<WorkItem>();
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            WorkItem workItem = query["WorkItem"] as WorkItem;
            WorkItem = workItem;
            InitAsync();
        }
        private async void InitAsync()
        {

            try
            {
                if (IsLoading) return;
                IsLoading = true;

                Title = WorkItem.Title;
                GoalTitle = WorkItem.Parent.Title;
                Description = WorkItem.Description;
                OriginalPlannedEndDateTime = WorkItem.OriginalPlannedEndDateTime;
                Priority = WorkItem.Priority.ToString();
                if (WorkItem.Children != null)
                {
                    SubTaskItems = WorkItem.Children.ToList();
                }
                else
                {
                    SubTaskItems = new List<WorkItem>();
                }

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
            await Shell.Current.GoToAsync(nameof(AddGoalView), new Dictionary<string, object> { { "WorkItem", WorkItem } });
        }
    }
}
