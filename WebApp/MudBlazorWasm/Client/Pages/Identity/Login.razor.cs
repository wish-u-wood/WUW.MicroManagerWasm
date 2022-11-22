using WUW.MicroManagerWasm.MudBlazorApp.Client.Components;
using WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.ApiClient;
using WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using WUW.MicroManagerApp.Common.Shared.Multitenancy;
using WUW.MicroManagerWasm.MudBlazorApp.Client.Shared;

namespace WUW.MicroManagerWasm.MudBlazorApp.Client.Pages.Identity;

public partial class Login
{
    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; } = default!;
    [Inject]
    public IAuthenticationService AuthService { get; set; } = default!;

    private CustomValidation? _customValidation;

    public bool BusySubmitting { get; set; }

   private readonly GetTokenRequest _tokenRequest = new();
    private string TenantId { get; set; } = string.Empty;
    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    protected override async Task OnInitializedAsync()
    {
        //if (AuthService.ProviderType == AuthProvider.AzureAd)
        //{
        //    AuthService.NavigateToExternalLogin(Navigation.Uri);
        //    return;
        //}

        var authState = await AuthState;
        if (authState.User.Identity?.IsAuthenticated is true)
        {
            Navigation.NavigateTo("/");
        }
    }

    private void TogglePasswordVisibility()
    {
        if (_passwordVisibility)
        {
            _passwordVisibility = false;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _passwordVisibility = true;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }

    private void FillAdministratorCredentials()
    {
        _tokenRequest.Email = RootTenant.AdminEmail;
        _tokenRequest.Password = MultitenancyConstants.DefaultPassword;
        TenantId = RootTenant.Id;
    }

    private async Task SubmitAsync()
    {
        BusySubmitting = true;

        if (await ApiHelper.ExecuteCallGuardedAsync(
            () => AuthService.LoginAsync(TenantId, _tokenRequest),
            Snackbar,
            _customValidation))
        {
            Snackbar.Add($"Logged in as {_tokenRequest.Email}", Severity.Info);
        }

        BusySubmitting = false;
    }
}