using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.Games;

namespace PP.API.Controllers;

[ApiController]
[Route("game")]
public class GameController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
    {
        var result = await sender.Send(new GetGamesQuery());

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<ActionResult> AddGame(AddGameCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoveGame(int id)
    {
        var result = await sender.Send(new RemoveGameCommand()
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
    public async Task<ActionResult> UpdateGame(UpdateGameCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}
