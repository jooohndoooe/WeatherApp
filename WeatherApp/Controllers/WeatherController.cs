using Microsoft.AspNetCore.Mvc;
using WeatherApp.Core;
using WeatherApp.Core.CityWeatherService;

namespace WeatherApp.Controllers
{
    [ApiController, Route("api")]
    public class WeatherController : ControllerBase
    {
        private ICityWeatherService cityWeatherService;

        public WeatherController(ICityWeatherService cityWeatherService)
        {
            this.cityWeatherService = cityWeatherService;
        }

        [HttpGet, Route("weather")]
        public async Task<IActionResult> GetWeather()
        {
            string[] cities = ["A", "B"];

            List<CityWeather> cityWeathers = await cityWeatherService.GetWeather();

            return Ok(cityWeathers);
        }

        [HttpPost, Route("city")]
        public async Task<IActionResult> AddCity([FromBody] AddCityRequest request)
        {
            await cityWeatherService.AddCity(request.City);
            return Ok();
        }

        [HttpDelete, Route("city/{locationKey}")]
        public async Task<IActionResult> DeleteCity([FromRoute] string locationKey)
        {
            await cityWeatherService.RemoveCity(locationKey);
            return NoContent();
        }
    }

    public class AddCityRequest
    {
        public string City { get; set; }
    }
}

