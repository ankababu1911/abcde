using Microsoft.AspNetCore.Components;

namespace abcde.Portal.Authentication
{
    public class RedirectToLogin : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            NavigationManager.NavigateTo("login");
        }
    }
}
