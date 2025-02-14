using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TZZ.Common.Shared.Enums;
using TZZ.Core.TheGameZone.Games.Commands;
using TZZ.Core.TheGameZone.Games.Queries;

namespace TGZ.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(ISender sender) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<IList<GameDto>>> GetGames([FromQuery]GetGameQuery query)
    {
        var result = await sender.Send(query);
        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<byte[]>> LoadGame(int id)
    {
        var result = await sender.Send(new LoadGameQuery(id));
        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Default)]
    public async Task<ActionResult<int>> AddGame(AddGameCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Default)]
    public async Task<ActionResult> DeleteGame(int id)
    {
        var result = await sender.Send(new RemoveGameInfoCommand(id));

        if (result.IsValid)
        {
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IList<CommentDto>>> GetGameComments(int gameInfoId)
    {
        var result = await sender.Send(new GetGameCommentsQuery(gameInfoId));

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Default)]
    public async Task<ActionResult<int>> AddGameComment(AddGameCommentCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Default)]
    public async Task<ActionResult> DeleteGameComment(int commentId)
    {
        var result = await sender.Send(new DeleteGameCommentCommand(commentId));

        if (result.IsValid)
        {
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IList<GameImageDto>>> GetGameImages(int gameInfoId)
    {
        var result = await sender.Send(new GetGameImagesQuery(gameInfoId));
        
        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Default)]
    public async Task<ActionResult<int>> AddGameImage(AddGameImageCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Default)]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews([FromQuery]GetReviewsQuery query)
    {
        var result = await sender.Send(query);

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Default)]
    public async Task<ActionResult<int>> SubmitReview(SubmitReviewCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsValid)
        {
            return Ok(result);
        }

        return BadRequest(result.Errors);
    }
}