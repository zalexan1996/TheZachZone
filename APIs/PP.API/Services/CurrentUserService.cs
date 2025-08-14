using TZZ.Core.Common.Services;

namespace PP.API.Services;

sealed class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
{
  public HttpContext HttpContext => _httpContextAccessor.HttpContext!;
  public string Site => "TheGameZone";
}
