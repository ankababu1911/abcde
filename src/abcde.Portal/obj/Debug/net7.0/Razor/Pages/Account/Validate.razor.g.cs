#pragma checksum "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4954489b54aab97f750e504f2dbf264713f0ed94350473b988a462517a9ed571"
// <auto-generated/>
#pragma warning disable 1591
namespace abcde.Portal.Pages.Account
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
#nullable restore
#line 2 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
using abcde.Model.Constants;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
using abcde.Portal.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
using abcde.Portal.Services;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.LayoutAttribute(typeof(AuthLayout))]
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/account/validate")]
    public partial class Validate : Microsoft.AspNetCore.Components.OwningComponentBase<abcde.Client.APIGateway>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Web.PageTitle>(0);
            __builder.AddAttribute(1, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddContent(2, "Validate User");
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 14 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
 if (!string.IsNullOrEmpty(OrganisationId))
{

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<global::abcde.Portal.Components.Identity.ValidateComponent>(3);
            __builder.AddAttribute(4, "EncryptedOrganisationId", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 16 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
                                                 OrganisationId

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(5, "OnVerify", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::abcde.Model.Identity.VerifyOrganisation>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::abcde.Model.Identity.VerifyOrganisation>(this, 
#nullable restore
#line 16 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
                                                                           VerifyOrganisation

#line default
#line hidden
#nullable disable
            ))));
            __builder.AddAttribute(6, "ErrorMessage", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 16 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
                                                                                                              errorMessage

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(7, "DisabledButton", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Boolean>(
#nullable restore
#line 16 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
                                                                                                                                              Disabled

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(8, "SpinnerDisplay", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 16 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
                                                                                                                                                                         Spinner

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
#nullable restore
#line 17 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(9, "p");
#nullable restore
#line (20,9)-(20,66) 25 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
__builder.AddContent(10, BaseLocale["UnableToVerifyWithoutOrganisationCode"].Value);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 21 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 23 "C:\Babu\Templates\New folder\src 2 new\src\abcde.Portal\Pages\Account\Validate.razor"
       
    //set cookie value in after render event
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (!string.IsNullOrEmpty(cc))
            {
                var decryptedConnectionStringCode = _encryptionHelper.DecryptAES(cc);
                await CookieService.SetCookie(CommonConstants.ConnectionStringCodeCookieName, decryptedConnectionStringCode, 300); // Cookie will expire in 300 days
            }
        }
    }




    [SupplyParameterFromQuery]
    [Parameter]
    public string? OrganisationId { get; set; }
    [SupplyParameterFromQuery]
    [Parameter]
    public string? cc { get; set; }
    private string errorMessage;
    private string Spinner { get; set; }
    private bool Disabled { get; set; }

    /// <summary>
    /// Validate, register and login user
    /// </summary>
    /// <returns></returns>
    private async Task VerifyOrganisation(VerifyOrganisation model)
    {
        try
        {
            Disabled = true;
            Spinner = "inline-block";
            model.EncryptedOrganisationId = OrganisationId;
            string decryptedConnectionStringCode = string.Empty;
            if (!string.IsNullOrEmpty(cc))
            {
                //now decryot the string
                decryptedConnectionStringCode = _encryptionHelper.DecryptAES(cc);
                Service.SetConnectionStringCode(decryptedConnectionStringCode);
                Service.SetHeaders();
            }
            var verifyResult = await Service.IdentityService.VerifyOrganisation(model);

            if (verifyResult != null)
            {
                Service.TenantId = verifyResult.TenantId.ToString();
                Service.SetHeaders();
                var result = await Service.IdentityService.Login(new LoginModel() { Email = model.EmailId, Password = model.Password });

                if (result.UserId!=Guid.Empty)
                {
                    string returnUrl = $"account/changePassword?cc={cc}";
                    NM.NavigateTo($"Authentication/Login?token={result.Token}&redirectUrl={returnUrl}&cc={cc}", true);
                }
            }
            else
            {
                errorMessage = BaseLocale["UnableToVerify"].Value;
                Disabled = false;
                Spinner = "none";
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Message : {ex.Message}");

            errorMessage = BaseLocale["UnableToVerify"].Value;
            Disabled = false;
            Spinner = "none";
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private CookieService CookieService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private EncryptionHelper _encryptionHelper { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IStringLocalizer<Resource> BaseLocale { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NM { get; set; }
    }
}
#pragma warning restore 1591