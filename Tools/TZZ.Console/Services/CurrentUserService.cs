using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Core.Shared.Services;

namespace TZZ.Console.Services;


public class CurrentUserService : ICurrentUserService
{
    public HttpContext HttpContext => throw new NotImplementedException();
    public string Site => "TZZ.Console";
}
