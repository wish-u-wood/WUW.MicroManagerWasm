using WUW.MicroManagerWasm.MudBlazorApp.Shared;
using WUW.MicroManagerWasm.MudBlazorApp.Shared.Prerender;

namespace WUW.MicroManagerWasm.MudBlazorApp.Server.Prerender;

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


    public async Task<WeatherForecast[]> GetForecastAsync()
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)],
        })
        .ToArray();
    }
}