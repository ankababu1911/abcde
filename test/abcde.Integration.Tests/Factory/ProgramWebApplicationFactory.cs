using Microsoft.AspNetCore.Hosting;
using abcde.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Identity;
using abcde.Model.Identity;
using Serilog;
using abcde.Test.Data;
using abcde.vAPI.Clients.TWPortal;
using StubTWPortalClient = abcde.Integration.Tests.Stub.StubTWPortalClient;

namespace abcde.Integration.Tests.Factory
{
    public class ProgramWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly string _connectionString1 = $"DataSource={Guid.NewGuid()}.db";

        /// <summary>
        /// Configure WebHost
        /// </summary>
        /// <remarks>
        /// Remove DataContext setup in the program and add new Sqllite Db for testing
        /// </remarks>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace TWPortal client with stub
                var twPortalService = services.SingleOrDefault(d => d.ServiceType == typeof(ITWPortalService));

                if (twPortalService != null)
                    services.Remove(twPortalService);

                services.AddScoped(typeof(ITWPortalService), typeof(StubTWPortalClient));

                // Replace DataContext with SQLite
                var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                if (dbContext != null)
                    services.Remove(dbContext);

                var serviceProvider = new ServiceCollection().AddEntityFrameworkSqlite().BuildServiceProvider();

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlite(_connectionString1);
                    options.UseInternalServiceProvider(serviceProvider);
                });
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                try
                {
                    TestDbInitializer.Seed(context, userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);

                    throw;
                }
            });
        }
    }
}
