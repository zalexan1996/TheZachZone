using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.SocialLinks;

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

  [HttpPost("[action]")]
  public async Task<ActionResult> SetSocialLink(SetSocialLinkCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> DeleteSocialLink(int id)
  {
    var result = await sender.Send(new DeleteSocialLinkCommand()
    {
      Id = id
    });

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }
  #endregion SocialLink
}
