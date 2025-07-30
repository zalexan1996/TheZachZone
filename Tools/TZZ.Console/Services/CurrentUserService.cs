using Microsoft.AspNetCore.Http;
using TZZ.Core.Common.Services;

namespace TZZ.Console.Services;


public class CurrentUserService : ICurrentUserService
{
  public HttpContext HttpContext => throw new NotSupportedException("The HTTP Context is not accessible to a console application.");
  public string Site => "TZZ.Console";
}
