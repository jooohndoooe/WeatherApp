using System.Text.Json;
using System.Xml.Linq;
using System.Collections.Concurrent;

namespace WeatherApp.Data.WeatherApi
{
    public class AccuWeatherApiClient : IWeatherApiClient
    {
        private static ConcurrentDictionary<string, (DateTime, ForecastApiResponse)> CachedWeather = new ConcurrentDictionary<string, (DateTime, ForecastApiResponse)>();

        private readonly HttpClient httpClient;
        private readonly WeatherApiClientSetting settings;

        public AccuWeatherApiClient(IHttpClientFactory httpClientFactory, WeatherApiClientSetting weatherApiClientSetting)
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);
            ArgumentNullException.ThrowIfNull(weatherApiClientSetting);

            httpClient = httpClientFactory.CreateClient();
            settings = weatherApiClientSetting;
        }
        public async Task<ForecastApiResponse> GetCityWeather(string locationKey)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(locationKey);

            if (CachedWeather.TryGetValue(locationKey, out var cached))
            {
                if (DateTime.Now - cached.Item1 < TimeSpan.FromMinutes(20))
                {
                    return cached.Item2;
                }
            }
            var response = await httpClient.GetAsync($"http://dataservice.accuweather.com/forecasts/v1/daily/1day/{locationKey}?apikey={settings.ApiKey}&language=en-us&metric=true");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var locationApiResponse = JsonSerializer.Deserialize<ForecastApiResponse>(responseBody);
            CachedWeather.Remove(locationKey, out var _);
            CachedWeather.TryAdd(locationKey, (DateTime.Now, locationApiResponse));
            return locationApiResponse;
        }

        public async Task<LocationApiResponse> GetLocationKey(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            var response = await httpClient.GetAsync($"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={settings.ApiKey}&q={name}&language=en-us");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var locationApiResponse = JsonSerializer.Deserialize<LocationApiResponse[]>(responseBody);

            return locationApiResponse.FirstOrDefault();
        }
    }

    public class WeatherApiClientSetting
    {
        public string ApiKey { get; set; }
    }
}
