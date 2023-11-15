using abcde.Mobile.Views;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;

namespace abcde.Mobile
{
    public partial class AppShell : SimpleShell
    {
        public AppShell()
        {
            InitializeComponent();

            AddTab(typeof(GoalsView), PageType.GoalsPage, "Goals");
            AddTab(typeof(NotesView), PageType.ToDoPage, "To Do");            
            AddTab(typeof(ProfileView), PageType.ProfilePage, "Profile");

            Routing.RegisterRoute(nameof(AddGoalView), typeof(AddGoalView));
            Routing.RegisterRoute(nameof(GoalDetailsView), typeof(GoalDetailsView));
            Routing.RegisterRoute(nameof(TaskDetailsView), typeof(TaskDetailsView));
			Routing.RegisterRoute(nameof(PlanMyDayView), typeof(PlanMyDayView));
            Loaded += AppShellLoaded;
        }
        private static void AppShellLoaded(object sender, EventArgs e)
        {
            var shell = sender as AppShell;

            shell.Window.SubscribeToSafeAreaChanges(safeArea =>
            {
                shell.pageContainer.Margin = safeArea;
                shell.tabBarView.Margin = safeArea;
                shell.bottomBackgroundRectangle.IsVisible = safeArea.Bottom > 0;
                shell.bottomBackgroundRectangle.HeightRequest = safeArea.Bottom;
            });
        }
        private void AddTab(Type page, PageType pageEnum, string title)
        {
            Tab tab = new Tab { Route = pageEnum.ToString(), Title = pageEnum.ToString() };
            tab.Items.Add(new ShellContent { ContentTemplate = new DataTemplate(page), Title = title });

            tabBar.Items.Add(tab);
        }

        private void TabBarViewCurrentPageChanged(object sender, TabBarEventArgs e)
        {
         Shell.Current.GoToAsync("///" + e.CurrentPage.ToString());
        }
        public enum PageType
        {
            GoalsPage,ToDoPage,ProfilePage
        }
    }
}