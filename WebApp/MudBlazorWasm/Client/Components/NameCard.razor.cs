using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WUW.MicroManagerApp.Common.Infrastructure.Authorization;
namespace WUW.MicroManagerWasm.MudBlazorApp.Client.Components;

public partial class NameCard
{
    [Parameter]
    public string? Class { get; set; }
    [Parameter]
    public string? Style { get; set; }

    private NameCardAvatar? _nameCardAvatar { get; set; }

    private string? EmailFromAvatar { get; set; }
    private string? FullNameFromAvatar { get; set; }

    private void GoToAccount()
    {
        Navigation.NavigateTo("/account");
    }
}