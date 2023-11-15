using abcde.Client;
using abcde.Client.Interfaces;
using abcde.Portal.Interop;
using abcde.Portal.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using System.Globalization;
using abcde.Portal.Middleware;
using abcde.Portal.Helpers;
using abcde.Portal.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs\\log-portal.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//ConfigureWebHostDefaults(webBuilder =>
// {
//     //TODO:Read from config
//     webBuilder.UseUrls("https://localhost:7127", "https://*.localhost:7127");
// });

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

// HttpClient Services
builder.Services.AddAPIGatewayServices(builder.Configuration);

// Services
builder.Services.AddScoped<IAPIGateway, APIGateway>();
builder.Services.AddTransient<IJSInteropService, JSInteropService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddBlazorBootstrap();

builder.Services.Configure<EncryptionSettings>(builder.Configuration.GetSection("EncryptionSettings"));
// Register the EncryptionHelper class as a singleton, so it's reused across requests
builder.Services.AddSingleton<EncryptionHelper>();

// Cookie Auth Code
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

// Configure supported cultures
var supportedCultures = new[] { "en-GB", "de-CH" };

builder.Services.AddScoped<MultiTenantServiceMiddleware>();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-GB");
    options.SupportedCultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList();
    options.SupportedUICultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList();
});
builder.Services.AddLocalization();

var app = builder.Build();

// Configure the request localization middleware
app.UseRequestLocalization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<MultiTenantServiceMiddleware>();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();