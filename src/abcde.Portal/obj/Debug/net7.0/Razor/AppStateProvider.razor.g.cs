#pragma checksum "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\AppStateProvider.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7e701c9ad1bcb6cdcc83dd90e5688c52b31f93e25b656ff28ffaded970ab0fd4"
// <auto-generated/>
#pragma warning disable 1591
namespace abcde.Portal
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
    public partial class AppStateProvider : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            global::__Blazor.abcde.Portal.AppStateProvider.TypeInference.CreateCascadingValue_0(__builder, 0, 1, 
#nullable restore
#line 1 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\AppStateProvider.razor"
                        this

#line default
#line hidden
#nullable disable
            , 2, (__builder2) => {
#nullable restore
#line (2,6)-(2,18) 25 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\AppStateProvider.razor"
__builder2.AddContent(3, ChildContent);

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
    }
}
namespace __Blazor.abcde.Portal.AppStateProvider
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateCascadingValue_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, TValue __arg0, int __seq1, global::Microsoft.AspNetCore.Components.RenderFragment __arg1)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.CascadingValue<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Value", (object)__arg0);
        __builder.AddAttribute(__seq1, "ChildContent", (object)__arg1);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
