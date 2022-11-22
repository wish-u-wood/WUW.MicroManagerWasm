using WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.ApiClient;

namespace WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.Authorization;

public interface IAuthenticationService
{
    AuthProvider ProviderType { get; }

    //void NavigateToExternalLogin(string returnUrl);

    Task<bool> LoginAsync(string tenantId, GetTokenRequest request);

    Task LogoutAsync();

    Task ReLoginAsync(string returnUrl);
}
