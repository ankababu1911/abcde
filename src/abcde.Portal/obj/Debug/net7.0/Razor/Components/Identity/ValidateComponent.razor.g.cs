#pragma checksum "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4b544b85fddff48a3ecb720a7bee697142966a2a28f6d653fa39415ae618b3e3"
// <auto-generated/>
#pragma warning disable 1591
namespace abcde.Portal.Components.Identity
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
    public partial class ValidateComponent : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "row mt-5");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "col-md-6 col-md-offset-2");
            __builder.OpenElement(4, "section");
            __builder.OpenElement(5, "h2");
#nullable restore
#line (6,18)-(6,59) 24 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
__builder.AddContent(6, BaseLocale["WelcomeTaskManagement"].Value);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(7, "\r\n            <hr>\r\n            ");
            __builder.AddMarkupContent(8, @"<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam nec diam metus. Fusce tristique semper nisl rutrum pulvinar. Nullam at congue orci, nec malesuada ante. Proin et cursus ligula, vel facilisis sapien. Fusce et ornare lacus. Duis vulputate lectus nulla, id gravida quam bibendum mattis. Proin diam turpis, mollis eu sem posuere, pulvinar rhoncus turpis.</p>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n    ");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", "col-md-4");
            __builder.OpenElement(12, "section");
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.EditForm>(13);
            __builder.AddAttribute(14, "EditContext", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.Forms.EditContext>(
#nullable restore
#line 13 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                    editContext

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(15, "OnValidSubmit", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.AspNetCore.Components.Forms.EditContext>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 13 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                  () => OnVerify.InvokeAsync(model)

#line default
#line hidden
#nullable disable
            ))));
            __builder.AddAttribute(16, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(17);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(18, "\r\n                ");
                __builder2.OpenElement(19, "h2");
#nullable restore
#line (15,22)-(15,54) 26 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
__builder2.AddContent(20, BaseLocale["ValidateUser"].Value);

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
                __builder2.AddMarkupContent(21, "\r\n                <hr>                \r\n                ");
                __builder2.OpenComponent<global::abcde.ComponentLibrary.Shared.InputComponents.InputFormFloatingComponent>(22);
                __builder2.AddAttribute(23, "Label", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 17 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                    BaseLocale["Email"].Value

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(24, "ValidationFor", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Linq.Expressions.Expression<System.Func<System.String>>>(
#nullable restore
#line 17 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                                                                             () => model.EmailId

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(25, "Value", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 17 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                                             model.EmailId

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(26, "ValueChanged", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::System.String>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::System.String>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => model.EmailId = __value, model.EmailId)))));
                __builder2.AddAttribute(27, "ValueExpression", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Linq.Expressions.Expression<System.Func<System.String>>>(() => model.EmailId)));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(28, "\r\n                ");
                __builder2.OpenComponent<global::abcde.ComponentLibrary.Shared.InputComponents.InputFormFloatingComponent>(29);
                __builder2.AddAttribute(30, "InputType", (object)("password"));
                __builder2.AddAttribute(31, "Label", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 18 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                         BaseLocale["Password"].Value

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(32, "ValidationFor", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Linq.Expressions.Expression<System.Func<System.String>>>(
#nullable restore
#line 18 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                                                                                                      () => model.Password

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(33, "Value", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 18 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                                                                     model.Password

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(34, "ValueChanged", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::System.String>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::System.String>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => model.Password = __value, model.Password)))));
                __builder2.AddAttribute(35, "ValueExpression", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Linq.Expressions.Expression<System.Func<System.String>>>(() => model.Password)));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(36, "\r\n                ");
                __builder2.OpenElement(37, "button");
                __builder2.AddAttribute(38, "id", "login-submit");
                __builder2.AddAttribute(39, "type", "submit");
                __builder2.AddAttribute(40, "class", "w-100 btn btn-lg btn-primary");
                __builder2.AddAttribute(41, "disabled", 
#nullable restore
#line 19 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                                                         DisabledButton

#line default
#line hidden
#nullable disable
                );
                __builder2.OpenElement(42, "span");
                __builder2.AddAttribute(43, "class", "spinner-border spinner-border-sm spinner");
                __builder2.AddAttribute(44, "style", "display:" + " " + (
#nullable restore
#line 20 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                                             SpinnerDisplay ?? "none"

#line default
#line hidden
#nullable disable
                ));
                __builder2.CloseElement();
                __builder2.AddContent(45, " ");
#nullable restore
#line (20,130)-(20,158) 26 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
__builder2.AddContent(46, BaseLocale["Validate"].Value);

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
                __builder2.AddMarkupContent(47, "\r\n                ");
                __builder2.OpenComponent<global::abcde.ComponentLibrary.Shared.ErrorMessageDisplayComponent>(48);
                __builder2.AddAttribute(49, "ErrorMessage", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 22 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
                                                                                           ErrorMessage

#line default
#line hidden
#nullable disable
                )));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 28 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Components\Identity\ValidateComponent.razor"
       

    [Parameter]
    public string EncryptedOrganisationId { get; set; }

    [Parameter]
    public string ErrorMessage { get; set; }

    [Parameter]
    public EventCallback<VerifyOrganisation> OnVerify { get; set; }
    [Parameter]
    public string SpinnerDisplay { get; set; }
    [Parameter]
    public bool DisabledButton { get; set; }

    private VerifyOrganisation model;
    private EditContext editContext;

    /// <summary>
    /// Set up editcontext
    /// </summary>
    override protected void OnInitialized()
    {
        model = new VerifyOrganisation() { EncryptedOrganisationId = EncryptedOrganisationId };
        editContext = new EditContext(model);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.Extensions.Localization.IStringLocalizer<App> BaseLocale { get; set; }
    }
}
#pragma warning restore 1591
