using System.Security.Claims;
using TZZ.Core.Shared.Services;

namespace TGZ.API.Services;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
{
    public HttpContext HttpContext => _httpContextAccessor.HttpContext!;
    public string Site => "TheGameZone";
}
