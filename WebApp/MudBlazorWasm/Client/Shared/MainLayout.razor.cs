using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using WUW.MicroManagerWasm.MudBlazorApp.Client.Components;
using static MudBlazor.CategoryTypes;

namespace WUW.MicroManagerWasm.MudBlazorApp.Client.Shared
{
    public partial class MainLayout
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;
        //[CascadingParameter]
        //protected Task<AuthenticationState> AuthState { get; set; } = default!;

        //private bool UserIsAuthenticated { get; set; } = false;

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        await IsAuthenticated();
        //    }
        //}

        bool _drawerOpen = true;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        //private async Task IsAuthenticated()
        //{
        //    var user = (await AuthState).User;
        //    UserIsAuthenticated = user.Identity?.IsAuthenticated == true;
        //}

        async Task Logout()
        {
            await AuthService.LogoutAsync();
            Snackbar.Add("Logged out", Severity.Info);
        }
    }
}