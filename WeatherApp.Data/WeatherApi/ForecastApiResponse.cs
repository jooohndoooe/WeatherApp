namespace WeatherApp.Data.WeatherApi
{
    public class ForecastApiResponse
    {
        public DailyForecasts[] DailyForecasts { get; set; }

    }

    public class DailyForecasts
    {
        public Temperature Temperature { get; set; }
        public Day Day { get; set; }
    }

    public class Temperature
    {
        public WithValue Minimum { get; set; }
        public WithValue Maximum { get; set; }
    }

    public class WithValue
    {
        public double Value { get; set; }
    }

    public class Day
    {
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
    }
}
