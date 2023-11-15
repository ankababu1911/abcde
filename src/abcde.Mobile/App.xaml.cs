#if __ANDROID__

using Microsoft.Maui.Controls.Compatibility.Platform.Android;

#endif

using abcde.Mobile.Controls;
using abcde.Mobile.Helpers;
using abcde.Mobile.ViewModels;
using abcde.Mobile.Views;
using abcde.Model.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleToolkit.SimpleShell;
using Microsoft.Maui.Controls.Platform;

namespace abcde.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(InitialSetUp), typeof(InitialSetUp));
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(EmailVerificationView), typeof(EmailVerificationView));
            Routing.RegisterRoute(nameof(ForgotPasswordView), typeof(ForgotPasswordView));
            Routing.RegisterRoute(nameof(ChangePasswordView), typeof(ChangePasswordView));
            Routing.RegisterRoute(nameof(GoalDetailsView), typeof(GoalDetailsView));
            Routing.RegisterRoute(nameof(PlanMyDayView), typeof(PlanMyDayView));
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderLessEntry), (handler, view) =>
            {
#if __ANDROID__
                handler.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.SetNavigationBarColor(Color.FromHex("#024556").ToAndroid());
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.SetStatusBarColor(Color.FromHex("#024556").ToAndroid());
#elif __IOS__
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(BorderLessEditor), (handler, view) =>
            {
#if __ANDROID__
                handler.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.SetNavigationBarColor(Color.FromHex("#024556").ToAndroid());
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.SetStatusBarColor(Color.FromHex("#024556").ToAndroid());
#elif __IOS__
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
               // handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif

            });
            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(BorderLessDatePicker), (handler, view) =>
            {
#if __ANDROID__
                handler.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.SetNavigationBarColor(Color.FromHex("#024556").ToAndroid());
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Window.SetStatusBarColor(Color.FromHex("#024556").ToAndroid());
#elif __IOS__
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
               // handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            if (this.MainPage == null)
            {
                this.MainPage = new GoalsView(null);
            }

            return base.CreateWindow(activationState);
        }

        protected override async void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            var result = await LocalStorage.Get<LoginResult>(Constants.LoginResult);
            MainPage = new AppShell();
            if (Handler is not null)
            {
                //In some scenarios if we want different database for an organisation we would need to
                //change the connection string in API. That is achieved by adding a code to the header.
                //This code is stored in the secure storage and is as long as app is running.
                //Also we need to ask again if for some reason local data is removed. This won't be removed even after logout
                var orgCode = await SecureStorage.GetAsync(Constants.OrganisationConnectionStringCode);
                if (orgCode == null)
                {
                    await Shell.Current.GoToAsync(nameof(InitialSetUp));
                }
                else if (result == null || !result.HasChangedPassword)
                {
                    await Shell.Current.GoToAsync(nameof(LoginView));
                }
                else
                {
                    if (result.Token != null)
                    {
                        await Shell.Current.GoToAsync($"//GoalsPage");
                    }
                }
            }
        }
    }
}