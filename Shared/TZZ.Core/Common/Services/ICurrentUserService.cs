using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TZZ.Core.Common.Services;

public interface ICurrentUserService
{
  public HttpContext HttpContext { get; }
  public string Site { get; }

  public int? UserId
  {
    get
    {
      int userId;
      string? userIdString = Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
      return int.TryParse(userIdString, out userId) ? userId : null;
    }
  }

  public string? Email => Principal.Identity?.Name;
  public ClaimsPrincipal Principal => HttpContext.User;
  public bool IsAuthenticated => Principal.Identity?.IsAuthenticated ?? false;
  public string IpAddress => HttpContext.Connection.RemoteIpAddress.ToString();
}
