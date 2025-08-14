using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.Common;
using TZZ.Core.TheGameZone.Metadata;

namespace TGZ.API.Controllers;

[Route("metadata")]
[ApiController]
sealed class MetadataController(ISender sender) : ControllerBase
{
  [HttpGet("genre")]
  public async Task<ActionResult<IEnumerable<string>>> GetGenres()
  {
    var result = await sender.Send(new GetGenresQuery());

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok(result.Result);
  }
  [HttpPost("genre")]
  public async Task<ActionResult<ZachZoneCommandResponse>> AddGenre(string genreName)
  {
    var result = await sender.Send(new AddGenreCommand()
    {
      GenreName = genreName
    });

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }
  [HttpDelete("genre")]
  public async Task<ActionResult<ZachZoneCommandResponse>> RemoveGenre(string genreName)
  {
    var result = await sender.Send(new RemoveGenreCommand()
    {
      Name = genreName
    });

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

}
