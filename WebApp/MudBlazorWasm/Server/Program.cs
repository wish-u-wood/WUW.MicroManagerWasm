using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using MudBlazor.Services;
using WUW.MicroManagerApp.Common.Shared.Authorization;
using WUW.MicroManagerWasm.MudBlazorApp.Infrastructure;
using WUW.MicroManagerWasm.MudBlazorApp.Server.Prerender;
using WUW.MicroManagerWasm.MudBlazorApp.Shared.Prerender;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddMudServices();

builder.Services.TryAddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddClientServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

//app.MapFallbackToFile("index.html");
// prerender
app.MapFallbackToPage("/_Host");

app.Run();