using WeatherApp.Data.Storage;
using WeatherApp.Data.WeatherApi;

namespace WeatherApp.Core.CityWeatherService
{
    public class CityWeatherService : ICityWeatherService
    {
        private ICityRepository databaseCityStorage;
        private IWeatherApiClient weatherApiClient;

        public CityWeatherService(ICityRepository databaseCityStorage, IWeatherApiClient weatherApiClient)
        {
            ArgumentNullException.ThrowIfNull(databaseCityStorage);
            ArgumentNullException.ThrowIfNull(weatherApiClient);

            this.databaseCityStorage = databaseCityStorage;
            this.weatherApiClient = weatherApiClient;
        }
        public async Task AddCity(string cityName)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cityName);
            cityName = cityName.Trim();
            var result = await weatherApiClient.GetLocationKey(cityName);
            var city = new City { Name = result.EnglishName, LocationKey = result.Key };
            await databaseCityStorage.Add(city);
        }

        public async Task<List<CityWeather>> GetWeather()
        {
            var cityWeathers = new List<CityWeather>();
            var citiesToUpdate = new List<CityWeather>();
            foreach (var city in databaseCityStorage.GetAll())
            {
                var result = await weatherApiClient.GetCityWeather(city.LocationKey);
                var precipitation = "none";
                var dailyForecasts = result.DailyForecasts[0];
                if (dailyForecasts.Day.HasPrecipitation)
                {
                    precipitation = result.DailyForecasts[0].Day.PrecipitationType;
                }
                var cityWeather = new CityWeather
                {
                    LocationKey = city.LocationKey,
                    CityName = city.Name,
                    Precipitation = precipitation,
                    HighestDailyTemprature = dailyForecasts.Temperature.Maximum.Value,
                    LowestDailyTemprature = dailyForecasts.Temperature.Minimum.Value,
                    ShowNotification = (DateTime.Now.Date != (city.LastNotification ?? DateTime.MinValue).Date ||
                                      DateTime.Now - (city.LastNotification ?? DateTime.MinValue) < TimeSpan.FromMinutes(10)) &&
                                      StringComparer.InvariantCultureIgnoreCase.Equals(precipitation, "Rain")
                };
                cityWeathers.Add(cityWeather);
                if (cityWeather.ShowNotification && DateTime.Now.Date != (city.LastNotification ?? DateTime.MinValue).Date)
                {
                    citiesToUpdate.Add(cityWeather);
                }
            }
            await databaseCityStorage.UpdateLastNotificationDate(citiesToUpdate.Select(e => e.LocationKey).ToArray());
            return cityWeathers;
        }

        public async Task RemoveCity(string locationKey)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(locationKey);
            await databaseCityStorage.Remove(locationKey);
        }
    }
}
