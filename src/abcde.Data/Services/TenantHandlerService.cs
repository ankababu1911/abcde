using abcde.Model;
using Microsoft.Extensions.Options;

namespace abcde.Data.Services
{
    public interface ITenantHandlerService
    {
        string Tenant { get; }

        /// <summary>
        /// Sets the tenant for the context
        /// </summary>
        /// <param name="tenant"></param>
        void SetTenant(string tenant);

        event TenantChangedEventHandler OnTenantChanged;

        /// <summary>
        /// This will set the connection string for the tenant while creating organisation.
        /// Let's say we get some value inside ConnectionStringCode from TW portal and we
        /// have a matching connection string in our appsettings then we create the tenant
        /// in that database rather than default database.
        /// </summary>
        /// <param name="organisation"></param>
        void VerifyAndSetConnectionString(Organisation organisation);

        /// <summary>
        /// This will set the connection string for the context
        /// </summary>
        /// <param name="connectionStringCode"></param>
        void VerifyAndSetConnectionString(string connectionStringCode);

        /// <summary>
        /// This will get the appropriate connection string for the site based on the connection string code
        /// </summary>
        /// <param name="connectionStringCode"></param>
        /// <returns></returns>
        string VerifyAndGetConnectionString(string connectionStringCode);
    }

    public delegate void TenantChangedEventHandler(object source, TenantChangedEventArgs args);

    public class TenantHandlerService : ITenantHandlerService
    {
        public TenantHandlerService()
        {
        }

        public TenantHandlerService(string tenant, IOptions<Model.Configuration.Options> appSettings, DataContext dataContext)
        {
            this.appSettings = appSettings;
            this.dataContext = dataContext;
            _tenant = tenant;
        }

        private string _tenant;
        private readonly IOptions<Model.Configuration.Options> appSettings;
        private readonly DataContext dataContext;

        public event TenantChangedEventHandler OnTenantChanged = null!;

        public string Tenant => _tenant;

        /// <see cref="ITenantHandlerService.OnTenantChanged"/>"
        public void SetTenant(string tenant)
        {
            if (tenant != _tenant)
            {
                var old = _tenant;
                _tenant = tenant;
                OnTenantChanged?.Invoke(this, new TenantChangedEventArgs(old, _tenant));
                dataContext.SetTenentId(_tenant);
            }
        }

        /// <see cref="ITenantHandlerService.VerifyAndSetConnectionString(Organisation)"/>"
        public void VerifyAndSetConnectionString(Organisation organisation)
        {
            if (organisation != null && !string.IsNullOrEmpty(organisation.ConnectionStringCode)
                && appSettings.Value.ConnectionStrings.ContainsKey($"{organisation.ConnectionStringCode}_DefaultConnection"))
            {
                var connectionString = appSettings.Value.ConnectionStrings[$"{organisation.ConnectionStringCode}_DefaultConnection"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    dataContext.SetConnectionString(connectionString);
                }
            }
            else
            {
                var connectionString = appSettings.Value.ConnectionStrings["DefaultConnection"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    dataContext.SetConnectionString(connectionString);
                }
            }
        }

        /// <see cref="ITenantHandlerService.VerifyAndSetConnectionString(Organisation)"/>"
        public void VerifyAndSetConnectionString(string connectionStringCode)
        {
            if (!string.IsNullOrEmpty(connectionStringCode)
                               && appSettings.Value.ConnectionStrings.ContainsKey($"{connectionStringCode}_DefaultConnection"))
            {
                var connectionString = appSettings.Value.ConnectionStrings[$"{connectionStringCode}_DefaultConnection"];
                if (!string.IsNullOrEmpty(connectionString))
                {
                    dataContext.SetConnectionString(connectionString);
                }
            }
            else
            {
                var connectionString = appSettings.Value.ConnectionStrings["DefaultConnection"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    dataContext.SetConnectionString(connectionString);
                }
            }
        }

        /// <see cref="ITenantHandlerService.VerifyAndSetConnectionString(string)"/>"
        public string VerifyAndGetConnectionString(string connectionStringCode)
        {
            if (!string.IsNullOrEmpty(connectionStringCode)
                               && appSettings.Value.ConnectionStrings.ContainsKey($"{connectionStringCode}_DefaultConnection"))
            {
                var connectionString = appSettings.Value.ConnectionStrings[$"{connectionStringCode}_DefaultConnection"];
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return connectionString;
                }
            }
            return string.Empty;
        }
    }
}