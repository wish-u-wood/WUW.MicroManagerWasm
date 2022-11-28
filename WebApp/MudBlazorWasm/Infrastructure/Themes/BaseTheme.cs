using MudBlazor;

namespace WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.Themes
{
    public class BaseTheme : MudTheme
    {
        public BaseTheme()
        {
            LayoutProperties = new LayoutProperties()
            {
                DefaultBorderRadius = "5px"
            };

            //Typography = CustomTypography.FSHTypography;
            Shadows = new Shadow();
            ZIndex = new ZIndex()
            {
                Drawer = 1100,
                AppBar = 1200,
                Dialog = 1300,
                Popover = 1400,
                Snackbar = 1500,
                Tooltip = 1600
            };
        }
    }
}
