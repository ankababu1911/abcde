// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace abcde.Portal.Shared
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Portal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Portal.Components.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Portal.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Portal.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Portal.Components.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Model.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Portal.Shared.Resources;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.ComponentLibrary.User;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.ComponentLibrary.Shared.InputComponents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using BlazorBootstrap;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using abcde.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\_Imports.razor"
using Serilog;

#line default
#line hidden
#nullable disable
    public partial class NavMenu : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 31 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Shared\NavMenu.razor"
       
    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IStringLocalizer<Resource> Localisation { get; set; }
    }
}
#pragma warning restore 1591
