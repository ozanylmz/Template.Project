using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Template.Project.Domain.Interfaces;
using Template.Project.Infrastructure.DbConfigurations.MongoDB;
using Template.Project.Infrastructure.DbConfigurations.Redis;
using Template.Project.Infrastructure.Repositories.MongoDB;
using Template.Project.Infrastructure.Repositories.Redis;

namespace Template.Project.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoSettings) + ":" + MongoSettings.ConnectionStringValue).Value;
                options.DatabaseName = configuration
                    .GetSection(nameof(MongoSettings) + ":" + MongoSettings.DatabaseValue).Value;
            });

            var multiplexer = ConnectionMultiplexer.Connect(configuration
                    .GetSection(nameof(RedisSettings) + ":" + RedisSettings.ConnectionStringValue).Value);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}