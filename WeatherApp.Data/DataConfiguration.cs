using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherApp.Data.Storage;
using WeatherApp.Data.WeatherApi;

namespace WeatherApp.Data
{
    public static class DataConfiguration
    {
        public static void AddDataConfiguration(this IServiceCollection services, string connectionString, string accuWeatherApiKey)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString);
            ArgumentNullException.ThrowIfNull(accuWeatherApiKey);


            services.AddScoped<ICityRepository, DatabaseCityStorage>();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            services.AddDbContextPool<WeatherDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableDetailedErrors()
            );
            services.AddHttpClient();
            services.AddSingleton(new WeatherApiClientSetting { ApiKey = accuWeatherApiKey });
            services.AddScoped<IWeatherApiClient, AccuWeatherApiClient>();
        }
    }
}
