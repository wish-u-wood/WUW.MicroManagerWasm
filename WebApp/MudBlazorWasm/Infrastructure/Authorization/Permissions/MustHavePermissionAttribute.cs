using Microsoft.AspNetCore.Authorization;
using WUW.MicroManagerApp.Common.Shared.Authorization;

namespace WUW.MicroManagerWasm.MudBlazorApp.Infrastructure.Authorization.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = ApiPermission.NameFor(action, resource);
}