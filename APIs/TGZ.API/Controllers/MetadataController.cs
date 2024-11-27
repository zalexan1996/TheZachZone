﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.Shared;
using TZZ.Core.TheGameZone.Metadata;

namespace TGZ.API.Controllers;

[Route("metadata")]
[ApiController]
public class MetadataController(ISender sender) : ControllerBase
{
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
}
