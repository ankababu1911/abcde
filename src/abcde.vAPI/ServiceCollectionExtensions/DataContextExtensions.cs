using abcde.Data;
using abcde.Test.Data;
using abcde.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Options;

namespace abcde.vAPI.ApplicationBuilderCollectionExtensions
{
    public static class DataContextExtensions
    {
        public static async Task<IApplicationBuilder> ConfigureDataContext(this IApplicationBuilder app, List<string> connectionStrings)
        {
            try
            {
                using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

                var services = serviceScope.ServiceProvider;

                using (var scope = services.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetRequiredService<DataContext>())
                    {
                        try
                        {
                            //Seed Tenant connectionstrings
                            foreach (var connectionString in connectionStrings)
                            {
                                context.SetConnectionString(connectionString);
                                var oldRoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                                var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole, DataContext, Guid>(context),
                                    oldRoleManager.RoleValidators, oldRoleManager.KeyNormalizer, oldRoleManager.ErrorDescriber, null);

                                var oldUserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                                IOptions<IdentityOptions> options = new OptionsWrapper<IdentityOptions>(oldUserManager.Options);
                                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser, ApplicationRole, DataContext, Guid>(context),
                                                                   options, oldUserManager.PasswordHasher, oldUserManager.UserValidators, oldUserManager.PasswordValidators,
                                                                                                  oldUserManager.KeyNormalizer, oldUserManager.ErrorDescriber, services, null);
                                var connString = context.Database.GetConnectionString();
                                await TestDbInitializer.Seed(context, userManager, roleManager);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }

                return app;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}