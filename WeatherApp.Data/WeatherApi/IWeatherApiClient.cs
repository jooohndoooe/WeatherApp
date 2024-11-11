namespace WeatherApp.Data.WeatherApi
{
    public interface IWeatherApiClient
    {
        Task<LocationApiResponse> GetLocationKey(string name);
        Task<ForecastApiResponse> GetCityWeather(string locationKey);
    }
}
