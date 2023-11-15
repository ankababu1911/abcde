using abcde.Data;
using abcde.Data.Interfaces;
using abcde.Data.Repositories;
using abcde.Data.Services;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Identity;
using abcde.Model.Validation;
using abcde.vAPI.ActionFilters;
using abcde.vAPI.ApplicationBuilderCollectionExtensions;
using abcde.vAPI.Clients.TWPortal;
using abcde.vAPI.Middleware;
using abcde.vAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using abcde.Model.ViewModels;
using AutoMapper;
using abcde.vAPI.Mapping;
using Microsoft.AspNetCore.Identity;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs\\log-api.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApiVersioning();

builder.Services.AddScoped<ITenantHandlerService, TenantHandlerService>();
builder.Services.AddScoped<MultiTenantServiceMiddleware>();

// Action Filters
builder.Services.AddScoped<ValidateEntityExistsAttribute<BaseEntity>>();

// DataContext
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
DataContext.ConnectionString = connectionString;

//builder.Services.AddDbContextFactory<DataContext>(options =>
//{
//    options.UseSqlServer(connectionString,
//        sqlServerOptionsAction: sqlOptions =>
//        {
//            sqlOptions.CommandTimeout(900);
//            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
//        });
//}, ServiceLifetime.Scoped);

//Add SQL.
builder.Services.AddDbContext<DataContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, b =>
    b.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient
);

// Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.Stores.MaxLengthForKeys = 128)
               .AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//})
//    .AddRoles<ApplicationRole>() // Assuming ApplicationRole inherits from IdentityRole<Guid>
//    .AddEntityFrameworkStores<DataContext>();

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
//    .AddEntityFrameworkStores<DataContext>()
//    .AddDefaultTokenProviders();


//builder.Services.AddScoped<DataContext>();

builder.Services.AddScoped<IDbContextFactory<DataContext>, DbContextFactory>();
// Repositories
builder.Services.AddTransient<IAuditRepository, AuditRepository>();
builder.Services.AddTransient<ITenantRepository, TenantRepository>();
builder.Services.AddTransient<ITenantSettingsRepository, TenantSettingsRepository>();
builder.Services.AddTransient<ITenantAdminRepository, TenantAdminRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILoginAuditRepository, LoginAuditRepository>();
builder.Services.AddTransient<INoteRepository, NoteRepository>();
builder.Services.AddTransient<IWorkItemRepository, WorkItemRepository>();
builder.Services.AddTransient<IDomainRepository, DomainRepository>();
builder.Services.AddTransient<IDomainUserRepository, DomainUserRepository>();
builder.Services.AddTransient<IWorkItemProgressRepository, WorkItemProgressRepository>();

// Services
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IWorkItemService, WorkItemService>();
builder.Services.AddTransient<IDomainService, DomainService>();
builder.Services.AddTransient<ITenantService, TenantService>();
//Add Automapper
builder.Services.AddAutoMapper(typeof(Program));

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new UserProfile());
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

// ContextAccssor
builder.Services.AddHttpContextAccessor();

// Options
builder.Services.Configure<abcde.Model.Configuration.Options>(builder.Configuration);

// Validators
builder.Services.AddScoped<IValidator<Note>, NoteValidator>();
builder.Services.AddScoped<IValidator<WorkItemViewModel>, WorkItemValidator>();
builder.Services.AddScoped<IValidator<Domain>, DomainValidator>();

// Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["ApplicationSettings:JWTSettings:Audience"],
        ValidIssuer = builder.Configuration["ApplicationSettings:JWTSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ApplicationSettings:JWTSettings:Key"]))
    };
});

// TW Portal Client
builder.Services.AddTWPortalClient(builder.Configuration);
builder.Services.AddTransient<ITWPortalService, TWPortalClient>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

    ////Clear down and seed the database
    //await app.ConfigureDataContext(builder.Configuration.GetSection("ConnectionStrings").Get<List<string>>());
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<MultiTenantServiceMiddleware>();
app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program
{ }