using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TZZ.Common.Enums.Constants;
using TZZ.Infrastructure.Data;

namespace TZZ.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.Admin)]
public class AdminController(DataSeederService dataSeeder) : ControllerBase
{
  [HttpPost("[action]")]
  public async Task Seed()
  {
    await dataSeeder.Seed(CancellationToken.None);
  }
}
