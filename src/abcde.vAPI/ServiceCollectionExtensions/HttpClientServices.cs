using abcde.vAPI.Clients.TWPortal;

namespace abcde.vAPI.ApplicationBuilderCollectionExtensions
{
    public static class HttpClientServices
    {
        private const string TWPortalUri = "Settings:API:TWBaseUri";

        public static IServiceCollection AddTWPortalClient(this IServiceCollection services, IConfiguration configuration)
        {
            var x = new Uri(configuration[TWPortalUri]);

            services.AddHttpClient<TWPortalClient>(client =>
            {
                client.BaseAddress = new Uri(configuration[TWPortalUri]);
            });

            return services;
        }
    }
}
