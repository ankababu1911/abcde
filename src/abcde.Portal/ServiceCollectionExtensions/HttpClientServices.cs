using abcde.Client;

namespace abcde.Portal.ServiceCollectionExtensions
{
    public static class HttpClientServices
    {
        public static IServiceCollection AddAPIGatewayServices(this IServiceCollection services, IConfiguration configuration)
        {
            var x = new Uri(configuration["Settings:API:BaseUri"]);

            services.AddHttpClient<APIGateway>(client =>
            {
                client.BaseAddress = new Uri(configuration["Settings:API:BaseUri"]);
            });

            return services;
        }
    }
}
