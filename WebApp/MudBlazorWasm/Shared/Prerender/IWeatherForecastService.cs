namespace WUW.MicroManagerWasm.MudBlazorApp.Shared.Prerender;

public interface IWeatherForecastService
{
    Task<WeatherForecast[]> GetForecastAsync();
}