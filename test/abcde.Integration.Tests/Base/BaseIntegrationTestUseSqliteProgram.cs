using abcde.Test.Data;
using abcde.Integration.Tests.Factory;
using abcde.Client;
using abcde.Model.Identity;

namespace abcde.Integration.Tests.Base
{
    public abstract class BaseIntegrationTestUseSqliteProgram : IClassFixture<ProgramWebApplicationFactory<Program>>
    {
        protected APIGateway APIGateway;
        protected HttpClient client;

        protected string tenant1Id = "a123456789abcdef";
        protected string tenant2Id = "a987654321";
        protected const string tenant1domain = "salamander";
        protected const string tenant2domain = "thebear";

        protected string defaultRequestTenantHeader = "TenantId";
        protected string UserId;

        #region ctor

        protected BaseIntegrationTestUseSqliteProgram()
        {
            var application = new ProgramWebApplicationFactory<Program>();

            client = application.CreateClient();
            client.BaseAddress = new Uri("https://localhost/api/v1/");
            client.DefaultRequestHeaders.Add(defaultRequestTenantHeader, tenant1Id);

            //var services = new ServiceCollection();
            //services.AddMemoryCache();
            //var serviceProvider = services.BuildServiceProvider();
            //var memoryCache = serviceProvider.GetService<IMemoryCache>();

            APIGateway = new APIGateway(client);

            RegisterUserAssignToken().GetAwaiter().GetResult();
        }

        #endregion ctor

        private async Task RegisterUserAssignToken()
        {
            var registerModel = IdentityData.GetRegisterModel(tenant1Id);
            var regResult = await APIGateway.IdentityService.Register(registerModel);

            if (regResult.Successful)
            {
                var loggedInUser = await APIGateway.IdentityService.Login(new LoginModel()
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                });

                UserId = loggedInUser.UserId.ToString();
                APIGateway.Token = loggedInUser.Token;
            }
        }

        protected async Task SwitchUser()
        {
            // Arrange
            var registerModel = IdentityData.GetRegisterModel(tenant2Id);
            var regResult = await APIGateway.IdentityService.Register(registerModel);

            if (regResult.Successful)
            {
                var loggedInUser = await APIGateway.IdentityService.Login(new LoginModel()
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                });

                UserId = loggedInUser.UserId.ToString();
                APIGateway.Token = loggedInUser.Token;
            }
        }

        protected static DateTime GetLastWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek - 7) % 7;

            return start.AddDays(daysToAdd);
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;

            return start.AddDays(daysToAdd);
        }

        public static DateTime GetNextWeekdayFromToday(DayOfWeek day)
        {
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 30, 0);

            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;

            return start.AddDays(daysToAdd);
        }

        public static DateTime GetToday1230()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 30, 0);
        }

        public static string GetToday1230String()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 30, 0).ToString("O");
        }
    }
}