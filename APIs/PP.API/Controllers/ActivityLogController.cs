using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.ActivityLog;
using TZZ.Domain.Entities.PocketPersona;

namespace PP.API.Controllers;

[ApiController]
[Route("[controller]")]
sealed class ActivityLogController(ISender sender) : ControllerBase
{
  [HttpGet("[action]")]
  public async Task<ActionResult<List<ActivityLog>>> GetActivityLogs()
  {
    var result = await sender.Send(new GetActivityLogsQuery());

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok(result.Result);
  }
}
