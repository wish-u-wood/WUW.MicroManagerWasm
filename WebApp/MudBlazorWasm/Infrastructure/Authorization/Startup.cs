using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.Authorization;

internal static class Startup
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config) =>
            services
                .AddScoped<AuthenticationStateProvider, JwtAuthenticationService>()
                .AddScoped(sp => (IAuthenticationService)sp.GetRequiredService<AuthenticationStateProvider>())
                .AddScoped(sp => (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>())
                .AddScoped<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>()
                .AddScoped<JwtAuthenticationHeaderHandler>();

    public static IHttpClientBuilder AddAuthenticationHandler(this IHttpClientBuilder builder, IConfiguration config) =>
            // Jwt
            builder.AddHttpMessageHandler<JwtAuthenticationHeaderHandler>();
}
