using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.TheGameZone.Statistics.Commands;

namespace TGZ.API.Controllers;

[ApiController]
[Route("[controller]")]
sealed class StatisticsController(ISender sender) : ControllerBase
{
  [HttpPost("[action]")]
  public async Task<ActionResult> AddGameplayTimestamp(AddGamePlayStatisticCommand command)
  {
    var result = await sender.Send(command);

    if (result.IsValid)
    {
      return Ok();
    }

    return BadRequest();
  }
}