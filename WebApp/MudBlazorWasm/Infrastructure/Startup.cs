using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.ApiClient;
using WUW.MicroManagerApp.Common.Shared.Authorization;
using System.Globalization;
using Blazored.LocalStorage;
using MudBlazor.Services;
using MudBlazor;
using WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WUW.MicroManagerWasm.MudBlazorApp.Infrastructure;

public static class Startup
{
    private const string ClientName = "MicroManager.API";

    public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration config) =>
        services
            //.AddLocalization(options => options.ResourcesPath = "Resources")
            .AddBlazoredLocalStorage()
            .AddMudServices(configuration =>
            {
                configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                configuration.SnackbarConfiguration.HideTransitionDuration = 500;
                configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                configuration.SnackbarConfiguration.ShowCloseIcon = false;
            })
            //.AddScoped<IClientPreferenceManager, ClientPreferenceManager>()
            //.AutoRegisterInterfaces<IAppService>()
            .AutoRegisterInterfaces<IApiService>()
            //.AddNotifications()
            .AddAuthentication(config)
            .AddAuthorizationCore(RegisterPermissionClaims)

            // Add Api Http Client.
            .AddHttpClient(ClientName, client =>
            {
                client.DefaultRequestHeaders.AcceptLanguage.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                client.BaseAddress = new Uri(config["ApiBaseUrl"]);
            })
                .AddAuthenticationHandler(config)
                .Services
            .AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(ClientName));

    private static void RegisterPermissionClaims(AuthorizationOptions options)
    {
        foreach (var permission in ApiPermissions.All)
        {
            options.AddPolicy(permission.Name, policy => policy.RequireClaim(ApiClaims.Permission, permission.Name));
        }
    }

    public static IServiceCollection AutoRegisterInterfaces<T>(this IServiceCollection services)
    {
        var @interface = typeof(T);

        var types = @interface
            .Assembly
            .GetExportedTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Service = t.GetInterface($"I{t.Name}"),
                Implementation = t
            })
            .Where(t => t.Service != null);

        foreach (var type in types)
        {
            if (@interface.IsAssignableFrom(type.Service))
            {
                services.AddTransient(type.Service, type.Implementation);
            }
        }

        return services;
    }
}
