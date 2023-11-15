using abcde.Model;
using abcde.Model.Identity;
using abcde.Portal.Helpers;
using abcde.Portal.ViewModels;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Serilog;

namespace abcde.Portal.Pages
{
    public partial class Domains
    {
        private Modal popupmodal = default!;

        [BindProperty]
        public string SelectedUserId { get; set; }

        [BindProperty]
        public List<string> StudentsList { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        private async Task OnShowModalClick()
        {
            await popupmodal.ShowAsync();
           
        }

        private async Task OnHideModalClick()
        {
            await popupmodal.HideAsync();
            model = new Domain();
        }

        [Parameter]
        public string ErrorMessage { get; set; }

        private async Task SubmitForm()
        {
            try
            {
                if (Users.Any() && !string.IsNullOrEmpty(SelectedUserId))
                {
                    var user = Users.Find(x => x.Id.ToString() == SelectedUserId);
                    if (user != null)
                    {
                        model.DomainUsers = new List<DomainUser>
                        {
                            new DomainUser
                            {
                                Id = Guid.NewGuid(),
                                UserID = user.Id,
                                DomainID = model.Id,
                                IsActive = true,
                                IsDomainHead = true
                            }
                        };
                    }
                }

                if (StudentsList != null && StudentsList.Any())
                {
                    model.DomainUsers ??= new List<DomainUser>();
                    foreach (var student in StudentsList)
                    {
                        var user = Users.Find(x => x.Id.ToString() == student);
                        if (user != null)
                        {
                            model.DomainUsers.Add(new DomainUser
                            {
                                Id = Guid.NewGuid(),
                                UserID = user.Id,
                                DomainID = model.Id,
                                IsActive = true,
                                IsDomainHead = false
                            });
                        }
                    }
                }
                model.TenantId = Service.TenantId;
                var data = await Service.DomainService.SaveAsync(model);
                if (data == null)
                {
                    ShowMessage(ToastType.Danger, "Failed to create");
                }
                else
                {
                    ShowMessage(ToastType.Success, "Domain Added Successfully");
                    model = new Domain();
                    await Initialise();
                    await OnHideModalClick();
                    await grid.RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ToastType.Danger, ex.Message);
                Log.Error($"Message : {ex.Message}");
            }
        }

        private Grid<DomainViewModel> grid;
        private List<DomainViewModel> DomainList { get; set; }

        private Domain model;

        private async Task DeleteDomain(Guid id)
        {
            //try
            //{
            //    var data = await Service.DomainService.DeleteAsync(id);
            //    if (data == null)
            //    {
            //        ShowMessage(ToastType.Danger);
            //    }
            //    else
            //    {
            //        ShowMessage(ToastType.Success);
            //        DomainList = await Service.DomainService.GetAllAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"Message : {ex.Message}");
            //}
        }

        private async Task EditDomain(Guid id)
        {
            try
            {
                model = await Service.DomainService.GetAsync(id);
                if (model != null && model.DomainUsers != null)
                {
                    SelectedUserId = model.DomainUsers.FirstOrDefault().UserID.ToString();
                }
                await OnShowModalClick();
            }
            catch (Exception ex)
            {
                Log.Error($"Message : {ex.Message}");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            model = new Domain();
            Service.SetHeaders();
            await Initialise();
        }

        private List<ApplicationUser> Users = new List<ApplicationUser>();
               
        public List<string> Students = new List<string>();

        private List<ToastMessage> messages = new List<ToastMessage>();

        private void ShowMessage(ToastType toastType, string message) => messages.Add(CreateToastMessage(toastType, message));

        private ToastMessage CreateToastMessage(ToastType toastType, string message)
        => new ToastMessage
        {
            Type = toastType,
            Message = @BaseLocale[message].Value,
            AutoHide = true,
        };

        private async Task<GridDataProviderResult<DomainViewModel>> DomainDataProvider(GridDataProviderRequest<DomainViewModel> request)
        {
            await Initialise();
            return await Task.FromResult(request.ApplyTo(DomainList));
        }

        private async Task Initialise()
        {
            var domains = await Service.DomainService.GetAllAsync();
            Users = await Service.TenantService.GetUsersForTenant();
            DomainNames = new List<string>
            {
                @BaseLocale["Class 1"].Value,
                @BaseLocale["Class 2"].Value,
                @BaseLocale["Class 3"].Value,
                @BaseLocale["Class 4"].Value,
                @BaseLocale["Class 5"].Value,
                @BaseLocale["Class 6"].Value,
                @BaseLocale["Class 7"].Value,
                @BaseLocale["Class 8"].Value,
                @BaseLocale["Class 9"].Value,
                @BaseLocale["Class 10"].Value
            };
            DomainList = domains.Select(x => DomainHelper.GetDomainViewModel(x, Users)).OrderBy(x => x.Name).ToList();
            foreach(var domain in DomainList)
            {
                if(DomainNames.Contains(domain.Name))
                {
                    DomainNames.Remove(domain.Name);
                }
            }
        }

        private List<string> DomainNames { get; set; }

        private List<string> filteredItems = new List<string>();
        private string displayDropdown = "none";

        private void Search(ChangeEventArgs e)
        {
            var text = e.Value.ToString();
            if (text != "")
            {
                filteredItems = DomainNames
                .Where(item => item.ToLower().Contains(text.ToLower()))
                .ToList();
                displayDropdown = filteredItems.Any() ? "block" : "none";
            }
            else
            {
                HideDropdown();
            }
        }

        private void HideDropdown()
        {
            displayDropdown = displayDropdown == "none" && filteredItems.Any() ? "block" : "none";
        }

        private void HideDropdownOnUserClick()
        {
            displayDropdown = "none";
        }

        private void SelectItem(string item)
        {
            model.Name = item;
            displayDropdown = "none";
        }
    }
}