using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.Characters;

namespace PP.API.Controllers;

[ApiController]
[Route("character")]
sealed class CharacterController(ISender sender) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
  {
    var result = await sender.Send(new GetCharacterQuery());

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok(result.Result);
  }

  [HttpPost]
  public async Task<ActionResult> AddCharacter(AddCharacterCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> RemoveCharacter(int id)
  {
    var result = await sender.Send(new RemoveCharacterCommand()
    {
      Id = id
    });

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpPost("[action]")]
  public async Task<ActionResult> UpdateCharacter(UpdateCharacterCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpPost("[action]")]
  public async Task<ActionResult> SetCharacterIcon(SetCharacterIconCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }
}
