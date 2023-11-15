using abcde.Client;
using abcde.Client.Interfaces;
using abcde.Mobile.Helpers;
using abcde.Mobile.Services.AppEnvironment;
using abcde.Mobile.Services.Auth;
using abcde.Mobile.Services.Goals;
using abcde.Mobile.Services.Navigation;
using abcde.Mobile.Services.Notes;
using abcde.Mobile.Services.Settings;
using abcde.Mobile.ViewModels;
using abcde.Mobile.Views;
using Akavache;
using CommunityToolkit.Maui;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;
using LogLevel = Microsoft.AppCenter.LogLevel;

namespace abcde.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterViewModels()
                .RegisterMAUIViews()
                .RegisterServices()
                .UseMauiApp<App>()
                .UseSimpleToolkit()
                .UseSimpleShell();

            Registrations.Start("Dicipli");

            AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start($"android={EnvironmentSettings.AppCenterDiscipliAndroidAppId};" +
                            $"ios={EnvironmentSettings.AppCenterDiscipliiOSAppId};", typeof(Analytics), typeof(Crashes));

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Editor), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.TryAddScoped<InitialSetUpViewModel>();
            builder.Services.TryAddSingleton<LoginViewModel>();
            builder.Services.TryAddSingleton<RegisterViewModel>();
            builder.Services.TryAddSingleton<NotesViewModel>();
            builder.Services.TryAddSingleton<ForgotPasswordViewModel>();
            builder.Services.TryAddSingleton<EmailVerificationViewModel>();
            builder.Services.TryAddSingleton<GoalsViewModel>();
            builder.Services.TryAddSingleton<AddGoalViewModel>();
            builder.Services.TryAddSingleton<GoalDetailViewModel>();
            builder.Services.TryAddSingleton<TaskDetailViewModel>();
			builder.Services.TryAddSingleton<PlanMyDayViewModel>();
            return builder;
        }

        private static MauiAppBuilder RegisterMAUIViews(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<InitialSetUp>();
            builder.Services.AddScoped<LoginView>();
            builder.Services.AddScoped<RegisterView>();
            builder.Services.AddScoped<NotesView>();
            builder.Services.AddScoped<ForgotPasswordView>();
            builder.Services.AddScoped<EmailVerificationView>();
            builder.Services.AddScoped<GoalsView>();
            builder.Services.AddScoped<AddGoalView>();
            builder.Services.AddScoped<GoalDetailsView>();
            builder.Services.AddScoped<TaskDetailsView>();
			builder.Services.AddScoped<PlanMyDayView>();
            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IAPIGateway, APIGateway>(
                serviceProvider =>
                {
                    var settingsProvider = serviceProvider.GetService<ISettingsService>();
                    var httpClient = new HttpClient
                    {
                        BaseAddress = new Uri("https://dicipli-api.azurewebsites.net/api/v1/")
                        // BaseAddress = new Uri("https://localhost:7053/api/v1/")
                        //BaseAddress= new Uri("https://7kg6xs0w-7147.eun1.devtunnels.ms/api/v1/")
                    };
                    httpClient.DefaultRequestHeaders.Add("TenantId", settingsProvider.TenantID);
                    var gateWay = new APIGateway(httpClient);
                    return gateWay;
                });
            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddSingleton<IRegisterService, RegisterService>();
            builder.Services.AddSingleton<INotesService, NotesService>();
            builder.Services.AddSingleton<IGoalsService, GoalsService>();
            builder.Services.AddSingleton<INavigation, NavigationServices>();
            builder.Services.AddSingleton<ISettingsService, SettingsService>();

            builder.Services.AddSingleton<IAppEnvironmentService, AppEnvironmentService>(
                serviceProvider =>
                {
                    var loginProvider = serviceProvider.GetService<ILoginService>();
                    var registerProvider = serviceProvider.GetService<IRegisterService>();
                    var noteProvider = serviceProvider.GetService<INotesService>();
                    var goalProvider = serviceProvider.GetService<IGoalsService>();
                    var navigationProvider = serviceProvider.GetService<INavigation>();
                    var settingsProvider = serviceProvider.GetService<ISettingsService>();

                    var aes =
                    new AppEnvironmentService(loginProvider, registerProvider, noteProvider, goalProvider, settingsProvider, navigationProvider);

                    aes.UpdateDependencies(true);
                    return aes;
                });
            return builder;
        }
        public static T GetService<T>()
        {
            return Application.Current.MainPage.Handler.MauiContext.Services.GetService<T>();
        }
    }
}