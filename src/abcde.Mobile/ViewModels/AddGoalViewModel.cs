using abcde.Mobile.Resx;
using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.ViewModels.Base;
using abcde.Mobile.Views;
using abcde.Model;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace abcde.Mobile.ViewModels
{
    [QueryProperty(nameof(WorkItem), "WorkItem")]
    public partial class AddGoalViewModel : ObservableObject, IQueryAttributable
    {

        private readonly IAppEnvironmentService _appEnvironmentService;

        #region Properties
        [ObservableProperty] public bool _firstScreenVisible;

        [ObservableProperty] public bool _secondScreenVisible;

        [ObservableProperty] public bool _isGoalTitleVisible;

        [ObservableProperty] public bool _saveVisible;

        [ObservableProperty] public bool _isInProgress;

        [ObservableProperty] public bool _isLoading;

        [ObservableProperty] public bool _isErrorMsgVisible;

        [ObservableProperty] public string _errorMessage;

        [ObservableProperty] private WorkItem _workItem;

        [ObservableProperty] private List<string> _priority;

        [ObservableProperty] private List<string> _status;

        [ObservableProperty] private List<string> _yesNo;

        [ObservableProperty] private List<string> _complexity;

        [ObservableProperty] private ObservableCollection<string> _tags;

        [ObservableProperty] private DateTime? _originalEndDate;

        [ObservableProperty] private string _selectedPriority;

        [ObservableProperty]
        private string _selectedUrgentValue;

        [ObservableProperty]
        private string _selectedImportantValue;

        private double _importantValue;
        public double ImportantValue
        {
            get => _importantValue;
            set
            {
                SetProperty(ref _importantValue, value);
                UpdateImportanceLabel();
            }
        }
        private double _urgentValue;
        public double UrgentValue
        {
            get => _urgentValue;
            set
            {
                SetProperty(ref _urgentValue, value);
                UpdateUrgentLabel();
            }
        }
        [ObservableProperty]
        private string _selectedComplexity;


        [ObservableProperty] private string _selectedIHaveToDoValue;

        [ObservableProperty] private string _selectedIWantToDoValue;

        [ObservableProperty] private string _title;

        [ObservableProperty] private string _goalTitle;

        [ObservableProperty] private string _description;
        #endregion

        #region Constructor
        public AddGoalViewModel()
        {
            _appEnvironmentService = MauiProgram.GetService<IAppEnvironmentService>();
            FirstScreenVisible = true;
            SecondScreenVisible = false;
            SaveVisible = false;
            _ = InitializeAsync();
        }
        #endregion

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            WorkItem workItem = query["WorkItem"] as WorkItem;
            WorkItem = workItem;
            InitFields();
        }

        #region Overrides
        public async Task InitializeAsync()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
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
        #endregion

        #region Commands
        [RelayCommand]
        private void Back()
        {
            FirstScreenVisible = true;
            SecondScreenVisible = false;
            SaveVisible = false;
        }

        [RelayCommand]
        private void NextScreen()
        {
            if (IsInProgress) return;
            IsInProgress = true;
            if (string.IsNullOrWhiteSpace(Title))
            {
                IsInProgress = false;
                IsErrorMsgVisible = true;
                ErrorMessage = AppResources.Goal_Title_Required;
                return;
            } else if (Title != null && Title.Split().Length < 2)
            {
                IsInProgress = false;
                IsErrorMsgVisible = true;
                ErrorMessage = AppResources.GoalTitleShouldMinimumTwoWords;
                return;
            }else if(OriginalEndDate == null)
            {
                IsInProgress = false;
                IsErrorMsgVisible = true;
                ErrorMessage = AppResources.Target_end_date_is_required;
                return;
            }
            else
            {
                IsErrorMsgVisible = false;
            }
            FirstScreenVisible = false;
            SecondScreenVisible = true;
            SaveVisible = true;
            IsInProgress = false;
        }

        [RelayCommand]
        private void SelectPriority(string priority)
        {
            if (SelectedPriority == priority)
            {
                SelectedPriority = "Notset"; // Clear the selection
            }
            else
            {
                SelectedPriority = priority;
            }
        }

        [RelayCommand]
        private void SelectComplexity(string complexity)
        {
            if (SelectedComplexity == complexity)
            {
                SelectedComplexity = "Notset"; // Clear the selection
            }
            else
            {
                SelectedComplexity = complexity;
            }
        }

        [RelayCommand]
        private void SelectWantToDo(string wanttodo)
        {
            if (SelectedIWantToDoValue == wanttodo)
            {
                SelectedIWantToDoValue = "Notset"; // Clear the selection
            }
            else
            {
                SelectedIWantToDoValue = wanttodo;
            }
        }

        [RelayCommand]
        private void SelectHaveToDo(string havetodo)
        {
            if (SelectedIHaveToDoValue == havetodo)
            {
                SelectedIHaveToDoValue = "Notset"; // Clear the selection
            }
            else
            {
                SelectedIHaveToDoValue = havetodo;
            }
        }

        [RelayCommand]
        private async Task SaveGoal()
        {
            try
            {
                if (IsInProgress) return;
                IsInProgress = true;

                if (string.IsNullOrWhiteSpace(Title))
                {
                    IsInProgress = false;
                    IsErrorMsgVisible = true;
                    ErrorMessage = AppResources.Goal_Title_Required;
                    return;
                }

                if (Title != null && Title.Split().Length < 2)
                {
                    IsInProgress = false;
                    IsErrorMsgVisible = true;
                    ErrorMessage = AppResources.GoalTitleShouldMinimumTwoWords;
                    return;
                }
                Priority parsedPriority = ParseEnum(SelectedPriority, Model.Priority.Notset);
                Complexity parsedComplexity = ParseEnum(SelectedComplexity, Model.Complexity.Notset);
                Importance parsedImportance = ParseEnum(SelectedImportantValue, Importance.NotSpecified);
                Urgency parsedUrgency = ParseEnum(SelectedUrgentValue, Urgency.NoSpecificDeadline);
                YesNo parsedIHaveToDoValue = ParseEnum(SelectedIHaveToDoValue, Model.YesNo.Notset);
                YesNo parsedIWantToDoValue = ParseEnum(SelectedIWantToDoValue, Model.YesNo.Notset);

                if (WorkItem != null)
                {
                    if (WorkItem.ParentId != null)
                    {
                        if (WorkItem.Id == Guid.Empty)
                        {
                            var result = await _appEnvironmentService.GoalsService.AddGoal(new WorkItem
                            {
                                Id = Guid.NewGuid(),
                                Title = Title,
                                Description = Description,
                                OriginalPlannedEndDateTime = OriginalEndDate,
                                Priority = parsedPriority,
                                Important = parsedImportance,
                                Urgent = parsedUrgency,
                                Complexity = parsedComplexity,
                                ParentId = WorkItem.ParentId,
                                IHaveToDo = parsedIHaveToDoValue,
                                IWantToDo = parsedIWantToDoValue,
                                UserId = await _appEnvironmentService.SettingsService.GetUserIDAsync(),
                                TenantId = _appEnvironmentService.SettingsService.TenantID
                            });
                            if (result != null)
                            {
                                IsInProgress = false;
                                FirstScreenVisible = true;
                                SecondScreenVisible = false;
                                await Application.Current.MainPage.DisplaySnackbar(AppResources.Goal_Added_successfully, visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                                InitFields();
                                await Shell.Current.GoToAsync($"//GoalsPage");
                            }
                        }
                        else
                        {
                            var result = await _appEnvironmentService.GoalsService.UpdateGoal(new WorkItem
                            {
                                Id = WorkItem.Id,
                                Title = Title,
                                Description = Description,
                                OriginalPlannedEndDateTime = OriginalEndDate,
                                Priority = parsedPriority,
                                Complexity = parsedComplexity,
                                Important = parsedImportance,
                                Urgent = parsedUrgency,
                                ParentId = WorkItem.ParentId,
                                IHaveToDo = parsedIHaveToDoValue,
                                IWantToDo = parsedIWantToDoValue,
                                UserId = await _appEnvironmentService.SettingsService.GetUserIDAsync(),
                                TenantId = _appEnvironmentService.SettingsService.TenantID
                            });
                            if (result != null)
                            {
                                IsInProgress = false;
                                FirstScreenVisible = true;
                                SecondScreenVisible = false;
                                await Application.Current.MainPage.DisplaySnackbar(AppResources.Goal_Updated_successfully, visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                                InitFields();
                                await Shell.Current.GoToAsync($"//GoalsPage");
                            }
                        }

                    }
                    else
                    {

                        var result = await _appEnvironmentService.GoalsService.UpdateGoal(new WorkItem
                        {
                            Id = WorkItem.Id,
                            Title = Title,
                            Description = Description,
                            OriginalPlannedEndDateTime = OriginalEndDate,
                            Priority = parsedPriority,
                            Complexity = parsedComplexity,
                            Important = parsedImportance,
                            Urgent = parsedUrgency,
                            ParentId = null,
                            IHaveToDo = parsedIHaveToDoValue,
                            IWantToDo = parsedIWantToDoValue,
                            UserId = await _appEnvironmentService.SettingsService.GetUserIDAsync(),
                            TenantId = _appEnvironmentService.SettingsService.TenantID
                        });
                        if (result != null)
                        {
                            IsInProgress = false;
                            FirstScreenVisible = true;
                            SecondScreenVisible = false;
                            await Application.Current.MainPage.DisplaySnackbar(AppResources.Goal_Updated_successfully, visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                            InitFields();
                            await Shell.Current.GoToAsync($"//GoalsPage");
                        }
                    }

                }
                else
                {
                    var result = await _appEnvironmentService.GoalsService.AddGoal(new WorkItem
                    {
                        Id = Guid.NewGuid(),
                        Title = Title,
                        Description = Description,
                        OriginalPlannedEndDateTime = OriginalEndDate,
                        Priority = parsedPriority,
                        Complexity = parsedComplexity,
                        Important = parsedImportance,
                        Urgent = parsedUrgency,
                        ParentId = null,
                        IHaveToDo = parsedIHaveToDoValue,
                        IWantToDo = parsedIWantToDoValue,
                        UserId = await _appEnvironmentService.SettingsService.GetUserIDAsync(),
                        TenantId = _appEnvironmentService.SettingsService.TenantID
                    });
                    if (result != null)
                    {
                        IsInProgress = false;
                        SecondScreenVisible = false;
                        SaveVisible = false;
                        FirstScreenVisible = true;
                        await Application.Current.MainPage.DisplaySnackbar(AppResources.Goal_Added_successfully, visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                        InitFields();
                        await Shell.Current.GoToAsync($"//GoalsPage");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplaySnackbar(AppResources.Use_Less_words_in_Title_or_Description, visualOptions: _appEnvironmentService.SettingsService.SnackbarOptions);
                IsInProgress = false;
            }
        }

        [RelayCommand]
        private void OnRemove(string tagToRemove)
        {
            if (tagToRemove != null)
            {
                Tags.Remove(tagToRemove);
            }
        }
        #endregion

        #region Private Methods
        public static T ParseEnum<T>(string value, T defaultValue) where T : struct
        {
            var newvalue = value.Replace(" ", "");
            if (Enum.TryParse(newvalue, true, out T parsedValue))
            {
                return parsedValue;
            }
            else
            {
                return defaultValue;
            }
        }
        private void InitFields()
        {

            if (WorkItem != null)
            {

                if (WorkItem.Id != Guid.Empty && WorkItem.ParentId == null) //Edit goal
                {
                    // The work item has an Id and doesn't have a parent Id
                    IsGoalTitleVisible = false;
                    SaveVisible = false;
                    GoalTitle = string.Empty;
                    Title = WorkItem.Title;
                    Description = WorkItem.Description;
                    SelectedPriority = WorkItem.Priority.ToString();
                    OriginalEndDate = WorkItem.OriginalPlannedEndDateTime;

                    SelectedImportantValue = Enum.GetName(typeof(Importance), WorkItem.Important).Replace("_", " ");
                    SelectedComplexity = WorkItem.Complexity.ToString();
                    SelectedUrgentValue = Enum.GetName(typeof(Urgency), WorkItem.Urgent).Replace("_", " ");
                    SelectedIHaveToDoValue = WorkItem.IHaveToDo.ToString();
                    SelectedIWantToDoValue = WorkItem.IWantToDo.ToString();
                    ImportantValue = (int)WorkItem.Important;
                    UrgentValue = (int)WorkItem.Urgent;

                    IsErrorMsgVisible = false;
                }
                else if (WorkItem.ParentId != null && WorkItem.Id == Guid.Empty) //Add task
                {
                    // The work item doesn't have an Id but has a parent Id
                    IsGoalTitleVisible = true;
                    SaveVisible = false;
                    GoalTitle = WorkItem.Parent.Title;
                    Title = string.Empty;
                    Description = string.Empty;
                    SelectedPriority = string.Empty;
                    OriginalEndDate = null;
                    SelectedImportantValue = string.Empty;
                    SelectedComplexity = string.Empty;
                    SelectedUrgentValue = string.Empty;
                    SelectedIHaveToDoValue = string.Empty;
                    SelectedIWantToDoValue = string.Empty;
                    ImportantValue = 0;
                    UrgentValue = 0;
                    IsErrorMsgVisible = false;
                }
                else
                {// Edit task
                    IsGoalTitleVisible = true;
                    SaveVisible = false;
                    Tags = new ObservableCollection<string>();
                    SelectedComplexity = null;
                    Title = WorkItem.Title;
                    GoalTitle = WorkItem.Parent.Title;
                    Description = WorkItem.Description;
                    SelectedPriority = WorkItem.Priority.ToString();
                    OriginalEndDate = WorkItem.OriginalPlannedEndDateTime;
                    SelectedImportantValue = Enum.GetName(typeof(Importance), WorkItem.Important).Replace("_", " ");
                    SelectedComplexity = WorkItem.Complexity.ToString();
                    SelectedUrgentValue = Enum.GetName(typeof(Urgency), WorkItem.Urgent).Replace("_", " ");
                    SelectedIHaveToDoValue = WorkItem.IHaveToDo.ToString();
                    SelectedIWantToDoValue = WorkItem.IWantToDo.ToString();
                    ImportantValue = (int)WorkItem.Important;
                    UrgentValue = (int)WorkItem.Urgent;
                }
            }
            else
            {   // add goal
                IsGoalTitleVisible = false;
                Tags = new ObservableCollection<string>();
                SelectedComplexity = string.Empty;
                SelectedIHaveToDoValue = string.Empty;
                SelectedImportantValue = string.Empty;
                SelectedIWantToDoValue = string.Empty;
                SelectedPriority = string.Empty;
                SelectedUrgentValue = string.Empty;
                Title = string.Empty;
                Description = string.Empty;
                OriginalEndDate = null;
                IsErrorMsgVisible = false;
                ImportantValue = 0;
                UrgentValue = 0;
            }
        }

        private void UpdateImportanceLabel()
        {
            int roundedValue = (int)Math.Round(ImportantValue);
            switch (roundedValue)
            {
                case 0:
                    SelectedImportantValue = AppResources.Not_Specified;
                    break;
                case 1:
                    SelectedImportantValue = AppResources.Not_Important;
                    break;
                case 2:
                    SelectedImportantValue = AppResources.Important;
                    break;
                default:
                    SelectedImportantValue = AppResources.Not_Specified;
                    break;
            }
        }

        private void UpdateUrgentLabel()
        {
            int roundedValue = (int)Math.Round(UrgentValue);
            switch (roundedValue)
            {
                case 0:
                    SelectedUrgentValue = AppResources.No_Specific_Deadline;
                    break;
                case 1:
                    SelectedUrgentValue = AppResources.This_Week;
                    break;
                case 2:
                    SelectedUrgentValue = AppResources.Later;
                    break;
                case 3:
                    SelectedUrgentValue = AppResources.Immediate;
                    break;
                default:
                    SelectedUrgentValue = AppResources.No_Specific_Deadline;
                    break;
            }
        }

        internal void OnBackButtonPressed()
        {
            if (FirstScreenVisible)
            {
                InitFields();

                if (WorkItem != null)
                {
                    if (WorkItem.Id != Guid.Empty && WorkItem.ParentId == null) //Edit goal
                    {
                        Shell.Current.GoToAsync(nameof(GoalDetailsView));
                    }
                    else if (WorkItem.ParentId != null && WorkItem.Id == Guid.Empty) //Add task
                    {
                        Shell.Current.GoToAsync(nameof(GoalDetailsView));
                    }
                    else // Edit task
                    {
                        Shell.Current.GoToAsync(nameof(TaskDetailsView));
                    }
                }
                else // Add goal
                {
                    Shell.Current.GoToAsync($"//GoalsPage");
                    return;
                }
            }
            else
            {
                FirstScreenVisible = true;
                SecondScreenVisible = false;
                SaveVisible = false;
            }
        }
        #endregion
    }
}