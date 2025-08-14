using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.SocialLinks;
using TZZ.Core.PocketPersona.SocialLinks.Dialog;
using TZZ.Core.PocketPersona.SocialLinks.Gifts;

namespace PP.API.Controllers;

[ApiController]
[Route("socialLink")]
sealed class SocialLinkController(ISender sender) : ControllerBase
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


  #region Dialogue

  [HttpGet("[action]")]
  public async Task<ActionResult<List<SocialLinkDialogDto>>> GetDialog(int socialLinkId)
  {
    var result = await sender.Send(new GetSocialLinkDialogQuery(socialLinkId));

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok(result.Result);
  }

  [HttpPost("[action]")]
  public async Task<ActionResult> AddDialog(AddSocialLinkDialogCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpDelete("[action]")]
  public async Task<ActionResult> DeleteDialog(int socialLinkDialogId)
  {
    var result = await sender.Send(new DeleteSocialLinkDialogCommand(socialLinkDialogId));

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  #endregion Dialogue

  #region Gifts
  
  [HttpGet("[action]")]
  public async Task<ActionResult<List<SocialLinkGiftDto>>> GetGifts(int socialLinkId)
  {
    var result = await sender.Send(new GetSocialLinkGiftsQuery(socialLinkId));

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok(result.Result);
  }

  [HttpPost("[action]")]
  public async Task<ActionResult> AddGift(AddSocialLinkGiftCommand command)
  {
    var result = await sender.Send(command);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }

  [HttpDelete("[action]")]
  public async Task<ActionResult> DeleteGift(int socialLinkGiftId)
  {
    var result = await sender.Send(new DeleteSocialLinkGiftCommand(socialLinkGiftId));

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok();
  }
  #endregion Gifts
}
