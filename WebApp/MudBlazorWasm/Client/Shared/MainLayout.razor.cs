using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WUW.MicroManagerWasm.MudBlazorApp.Client.Shared
{
    public partial class MainLayout
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; } = default!;

        private bool UserIsAuthenticated { get; set; } = false;


        bool _drawerOpen = true;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task IsAuthenticated()
        {
            var user = (await AuthState).User;
            UserIsAuthenticated = user.Identity?.IsAuthenticated == true;
        }
    }
}