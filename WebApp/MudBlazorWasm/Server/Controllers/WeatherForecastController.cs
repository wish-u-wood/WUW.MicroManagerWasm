using Microsoft.AspNetCore.Mvc;
using WUW.MicroManagerWasm.MudBlazorApp.Shared;
using WUW.MicroManagerWasm.MudBlazorApp.Shared.Prerender;

namespace WUW.MicroManagerWasm.MudBlazorApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _weatherForecastService.GetForecastAsync();
        }
    }
}