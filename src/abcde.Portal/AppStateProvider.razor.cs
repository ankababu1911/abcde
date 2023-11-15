using Microsoft.AspNetCore.Components;

namespace abcde.Portal
{
    public partial class AppStateProvider
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
