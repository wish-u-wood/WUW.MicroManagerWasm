using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WUW.MicroManagerWasm.MudBlazorApp.Client.Prerender;
using WUW.MicroManagerWasm.MudBlazorApp.Infrastructure;
using WUW.MicroManagerWasm.MudBlazorApp.Shared.Prerender;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//prerender
//builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddClientServices(builder.Configuration);

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//prerender
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();