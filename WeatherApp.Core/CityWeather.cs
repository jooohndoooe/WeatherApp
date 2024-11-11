namespace WeatherApp.Core
{
    public class CityWeather
    {
        public string LocationKey { get; set; }
        public string CityName { get; set; }
        public string Precipitation { get; set; }
        public double HighestDailyTemprature { get; set; }
        public double LowestDailyTemprature { get; set; }
        public bool ShowNotification { get; set; }
    }
}
