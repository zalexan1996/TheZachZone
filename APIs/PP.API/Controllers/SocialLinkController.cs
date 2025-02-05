using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.SocialLinkDialogues;
using TZZ.Core.PocketPersona.SocialLinkRanks;
using TZZ.Core.PocketPersona.SocialLinks;
using TZZ.Core.PocketPersona.SocialLinks.Dialogue;
using TZZ.Core.PocketPersona.SocialLinks.Ranks;

namespace PP.API.Controllers;

[ApiController]
[Route("socialLink")]
public class SocialLinkController(ISender sender) : ControllerBase
{
    #region SocialLink
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SocialLinkDto>>> GetSocialLinks()
    {
        var result = await sender.Send(new GetSocialLinkQuery());

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<ActionResult> AddSocialLink(AddSocialLinkCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoveSocialLink(int id)
    {
        var result = await sender.Send(new RemoveSocialLinkCommand()
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
    public async Task<ActionResult> UpdateSocialLink(UpdateSocialLinkCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
    #endregion SocialLink
    #region Ranks
    [HttpGet("rank")]
    public async Task<ActionResult<List<SocialLinkRankDto>>> GetSocialLinkRanks(GetSocialLinkRankQuery query)
    {
        var result = await sender.Send(query);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Result);
    }

    [HttpPost("rank")]
    public async Task<ActionResult> AddSocialLinkRank(AddSocialLinkRankCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPut("rank")]
    public async Task<ActionResult> UpdateSocialLinkRank(UpdateSocialLinkRankCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpDelete("rank")]
    public async Task<ActionResult> RemoveSocialLinkRank(int id)
    {
        var result = await sender.Send(new RemoveSocialLinkRankCommand()
        {
            SocialLinkRankId = id
        });

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
    #endregion Ranks
    #region Dialogue

    [HttpGet("dialogue")]
    public async Task<ActionResult<List<SocialLinkDialogueDto>>> GetSocialLinkDialogues(GetSocialLinkDialogueQuery query)
    {
        var result = await sender.Send(query);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Result);
    }

    [HttpPost("dialogue")]
    public async Task<ActionResult> AddSocialLinkDialogue(AddSocialLinkDialogueCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPut("dialogue")]
    public async Task<ActionResult> UpdateSocialLinkDialogue(UpdateSocialLinkDialogueCommand command)
    {
        var result = await sender.Send(command);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpDelete("dialogue")]
    public async Task<ActionResult> RemoveSocialLinkDialogue(int id)
    {
        var result = await sender.Send(new RemoveSocialLinkDialogueCommand()
        {
            SocialLinkDialogueId = id
        });

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
    #endregion Dialogue
}
