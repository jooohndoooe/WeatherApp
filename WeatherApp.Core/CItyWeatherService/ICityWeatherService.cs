namespace WeatherApp.Core.CityWeatherService
{
    public interface ICityWeatherService
    {
        Task AddCity(string city);
        Task RemoveCity(string locationKey);
        Task<List<CityWeather>> GetWeather();
    }
}
