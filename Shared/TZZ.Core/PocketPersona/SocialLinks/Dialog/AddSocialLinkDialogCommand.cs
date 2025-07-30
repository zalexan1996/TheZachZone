using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Dialog;

public class AddSocialLinkDialogCommand : IRequest<ZachZoneCommandResponse>
{
  public int SocialLinkId { get; set; }
  public int Rank { get; set; }
  public int Order { get; set; }
  public string? OptionalRequirement { get; set; }
  public required string Text { get; set; }
}

public class AddSocialLinkDialogCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<AddSocialLinkDialogCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(AddSocialLinkDialogCommand request, CancellationToken cancellationToken)
  {
    var socialLink = await dbContext.Entity<SocialLink>()
      .SingleAsync(x => x.Id == request.SocialLinkId, cancellationToken);

    var dialogue = new SocialLinkDialogue()
    {
      Order = request.Order,
      Rank = request.Rank,
      Text = request.Text,
      ExtraRequirement = request.OptionalRequirement,
      SocialLinkId = request.SocialLinkId,
      SocialLink = socialLink
    };


    await dbContext.Add(dialogue, cancellationToken);
    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}