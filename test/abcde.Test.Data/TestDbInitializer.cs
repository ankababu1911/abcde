using abcde.Data;
using abcde.Model;
using abcde.Model.Identity;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static abcde.Test.Data.WorkItemData;

namespace abcde.Test.Data
{
    /// <summary>
    /// In Test.Data so we can use this for other types of tests
    /// </summary>
    public static class TestDbInitializer
    {
        public static async Task Seed(DataContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            try
            {
                //var connString = context.Database.GetConnectionString();
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //// Default Roles
                //await AddDefaultRoles(roleManager);

                //// Default Users
                //await AddDefaultUsers(userManager);

                //// Tenants
                //var tenants = TenantData.GetEnumerable().ToList();
                //await AddTenents(context, tenants, userManager, roleManager);
                //var translation = LocalisationData.GetTranslations();
                //foreach (var item in translation)
                //{
                //    context.Translations.Add(item);
                //}
                ////seeding Mock Goal and Learn German Data
                //var goals = WorkItemData.GetEnumerable().ToList();

                //await SeedGoals(context, goals);

                //await SeedDomains(context);

                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add default roles
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task AddDefaultRoles(RoleManager<ApplicationRole> roleManager)
        {
            await roleManager.CreateAsync(new ApplicationRole(Roles.SystemAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Roles.Admin.ToString()));
        }

        /// <summary>
        /// Add default users
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public static async Task AddDefaultUsers(UserManager<ApplicationUser> userManager)
        {
            await AddSystemAdminUserAsync(userManager);
        }

        /// <summary>
        /// Add system admin user
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public static async Task AddSystemAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            var newUser = new ApplicationUser
            {
                Email = "systemadmin@abcde.com",
                UserName = "systemadmin@abcde.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                HasChangedPassword = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // create an instance of PasswordHasher
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            // hash a password
            newUser.PasswordHash = passwordHasher.HashPassword(newUser, "!+Password1");

            await AddUserToRole(userManager, newUser, Roles.SystemAdmin, null);
        }

        public static async Task AddTenents(DataContext context, List<Tenant> tenants, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            foreach (var tenant in tenants)
            {
                tenant.Id = new Guid(tenant.TenantId);
                context.Tenants.Add(tenant);
                var tenantSettings = new Faker<TenantSettings>()
                    .RuleFor(u => u.Id, f => f.Random.Guid())
                    .RuleFor(u => u.TenantId, f => tenant.Id.ToString())
                    .RuleFor(u => u.LicenseValidFrom, DateTime.UtcNow)
                    .RuleFor(u => u.LicenseValidTo, DateTime.UtcNow.AddDays(30))
                    .RuleFor(u => u.LicenseCount, f => 10)
                    .RuleFor(u => u.Timezone, f => "Europe/London")
                    .RuleFor(u => u.TenantCulture, f => "en-GB")
                    .RuleFor(u => u.ContactName, f => f.Person.FullName)
                    .RuleFor(u => u.ContactEmail, f => f.Person.Email)
                    .RuleFor(u => u.PhoneNumber, f => f.Person.Phone)
                    .RuleFor(u => u.Address, f => f.Address.FullAddress()).Generate();
                context.TenantSettings.Add(tenantSettings);
                context.SaveChanges();

                var applicationUsers = new Faker<ApplicationUser>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.UserName, f => f.Person.UserName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.EmailConfirmed, f => true)
                .RuleFor(u => u.PhoneNumber, f => f.Person.Phone)
                .RuleFor(u => u.PhoneNumberConfirmed, f => f.Random.Bool())
                .RuleFor(u => u.TwoFactorEnabled, f => false)
                .RuleFor(u => u.LockoutEnd, f => null)
                .RuleFor(u => u.LockoutEnabled, f => false)
                .RuleFor(u => u.AccessFailedCount, f => 0)
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.IsActive, f => true)
                .RuleFor(u => u.HasChangedPassword, f => true)
                .Generate(2);
                foreach (var user in applicationUsers)
                {
                    //user.TenantId = tenant.Id;
                    user.UserName = user.Email;
                    var result = await userManager.CreateAsync(user, "Abcd@1234");
                    if (result.Succeeded)
                    {
                        var roleResult = await userManager.AddToRoleAsync(user, $"{Roles.Admin.ToString().ToUpper()}");
                    }
                }
            }
        }

        public static async Task SeedGoals(DataContext context, List<WorkItem> goals)
        {
            //var tenants = context.Tenants.ToList();
            //var goalList = new List<WorkItem>();
            //foreach (var item in tenants)
            //{
            //    var users = context.Users.Where(x => x.TenantId == item.Id).ToList();
            //    foreach (var user in users)
            //    {
            //        foreach (var goal in goals)
            //        {
            //            //create a new Goal object with all properties assigned using json serialiser
            //            var newGoal = JsonConvert.DeserializeObject<WorkItem>(JsonConvert.SerializeObject(goal));
            //            newGoal.TenantId = item.Id.ToString();
            //            newGoal.CreatedBy = user.Id.ToString();
            //            newGoal.UserId = user.Id;
            //            if (newGoal.Notes != null)
            //            {
            //                foreach (var note in newGoal.Notes)
            //                {
            //                    note.UserId = user.Id;
            //                    note.TenantId = item.Id.ToString();
            //                }
            //            }
            //            foreach (var child in newGoal.Children)
            //            {
            //                child.UserId = user.Id;
            //                child.TenantId = item.Id.ToString();
            //                child.CreatedBy = user.Id.ToString();
            //                if (child.Children != null)
            //                {
            //                    foreach (var task in child.Children)
            //                    {
            //                        task.UserId = user.Id;
            //                        task.TenantId = item.Id.ToString();
            //                        task.CreatedBy = user.Id.ToString();
            //                    }
            //                }
            //            }
            //            goalList.Add(newGoal);
            //        }
            //    }
            //}
            //context.WorkItems.AddRange(goalList);
            //await context.SaveChangesAsync();
            //goalList = new List<WorkItem>();
        }

        public static async Task SeedDomains(DataContext context)
        {
            //var tenants = context.Tenants.ToList();
            //var domainList = new List<Domain>();
            //var users = context.Users.Where(x => x.TenantId == tenants.First().Id).ToList();

            //foreach (var tenant in tenants)
            //{
            //    var rootDomain = new Faker<Domain>()
            //        .RuleFor(u => u.Id, f => f.Random.Guid())
            //        .RuleFor(u => u.Name, f => $"{tenant.Name}_root")
            //        .RuleFor(u => u.IsActive, f => true)
            //        .RuleFor(u => u.TenantId, tenant.Id.ToString())
            //        .RuleFor(
            //            u => u.DomainUsers,
            //            f => new List<DomainUser>()
            //            {
            //                new()
            //                {
            //                    UserID = users[0].Id,
            //                    IsActive = true,
            //                    IsDomainHead = true,
            //                    Id = Guid.NewGuid()
            //                }
            //            })
            //        .Generate();
            //    domainList.Add(rootDomain);

            //    var domain = new Faker<Domain>()
            //        .RuleFor(u => u.Id, f => f.Random.Guid())
            //        .RuleFor(u => u.Name, f => $"Class {f.Random.Int(1, 10)}")
            //        .RuleFor(u => u.IsActive, f => true)
            //        .RuleFor(
            //            u => u.DomainUsers,
            //            f => new List<DomainUser>()
            //            {
            //                new()
            //                {
            //                    UserID = users[0].Id,
            //                    IsActive = true,
            //                    IsDomainHead = true,
            //                    Id = Guid.NewGuid()
            //                }
            //            })
            //        .RuleFor(u => u.TenantId, tenant.Id.ToString())
            //        .RuleFor(u => u.ParentId, rootDomain.Id)
            //        .Generate(5);

            //    domainList.AddRange(domain);
            //}

            //context.Domains.AddRange(domainList);
            //await context.SaveChangesAsync();
        }

        /// <summary>
        /// Add user to role
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="newUser"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        private static async Task AddUserToRole(UserManager<ApplicationUser> userManager, ApplicationUser newUser, Roles roles, string tenantId)
        {
            if (userManager.Users.All(u => u.Id != newUser.Id))
            {
                var user = await userManager.FindByEmailAsync(newUser.Email);

                if (user == null)
                {
                    var result = await userManager.CreateAsync(newUser, "!+Password1");

                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(tenantId))
                        {
                            await userManager.AddToRoleAsync(newUser, $"{roles}");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(newUser, $"{roles}.{tenantId}");
                        }
                    }
                }
            }
        }

    }
}