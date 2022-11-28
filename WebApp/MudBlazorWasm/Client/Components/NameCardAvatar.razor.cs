
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WUW.MicroManagerApp.Common.Infrastructure.Authorization;

namespace WUW.MicroManagerWasm.MudBlazorApp.Client.Components;

public partial class NameCardAvatar
{
    [CascadingParameter]
    protected Task<AuthenticationState> AuthState { get; set; } = default!;

    private string? UserId { get; set; }
    [Parameter] 
    public string? Email { get; set; }
    [Parameter]
    public EventCallback<string> EmailChanged { get; set; }
    [Parameter]
    public string? FullName { get; set; }
    [Parameter]
    public EventCallback<string> FullNameChanged { get; set; }
    private string? ImageUri { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadUserData();
        }
    }

    private async Task LoadUserData()
    {
        var user = (await AuthState).User;
        if (user.Identity?.IsAuthenticated == true)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                FullName = user.GetFullName();
                await FullNameChanged.InvokeAsync(FullName);

                Email = user.GetEmail();
                await EmailChanged.InvokeAsync(Email);

                UserId = user.GetUserId();
                ImageUri = string.IsNullOrEmpty(user?.GetImageUrl()) ? string.Empty : (Config["ApiBaseUrl"] + user?.GetImageUrl());
                StateHasChanged();
            }
        }
    }


}