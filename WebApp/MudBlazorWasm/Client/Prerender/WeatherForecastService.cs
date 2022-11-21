using System.Net.Http.Json;
using WUW.MicroManagerWasm.MudBlazorApp.Shared;
using WUW.MicroManagerWasm.MudBlazorApp.Shared.Prerender;

namespace WUW.MicroManagerWasm.MudBlazorApp.Client.Prerender;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(HttpClient httpclient)
    {
        _httpClient = httpclient;
    }

    public async Task<WeatherForecast[]> GetForecastAsync()
    {
        return await _httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }
}