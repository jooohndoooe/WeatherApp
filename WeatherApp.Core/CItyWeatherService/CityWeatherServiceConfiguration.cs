using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Core.CityWeatherService
{
    public static class CityWeatherServiceConfiguration
    {
        public static void AddCityWeatherService(this IServiceCollection services)
        {
            services.AddScoped<ICityWeatherService, CityWeatherService>();
        }
    }
}
