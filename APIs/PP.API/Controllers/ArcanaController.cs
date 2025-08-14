using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.Arcanas;

namespace PP.API.Controllers;

[ApiController]
[Route("arcana")]
sealed class ArcanaController(ISender sender) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ArcanaDto>>> GetArcanas()
  {
    var result = await sender.Send(new GetArcanaQuery());

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok(result.Result);
  }

  [HttpPost]
  public async Task<ActionResult> AddArcana(AddArcanaCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> RemoveArcana(int id)
  {
    var result = await sender.Send(new RemoveArcanaCommand()
    {
      Id = id
    });

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> UpdateArcana(UpdateArcanaCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }
}
