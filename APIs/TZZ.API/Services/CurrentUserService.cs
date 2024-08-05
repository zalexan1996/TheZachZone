using System.Security.Claims;
using TZZ.Core.Shared.Services;

namespace TZZ.API.Services;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
{
    public HttpContext HttpContext => _httpContextAccessor.HttpContext!;
    public string Site => "TheZachZone";
}
