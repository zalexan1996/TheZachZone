using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Dialog;

public class SocialLinkDialogDto
{
  public SocialLinkDialogDto(SocialLinkDialogue d)
  {
    SocialLinkDialogId = d.Id;
    SocialLinkId = d.SocialLinkId;
    Rank = d.Rank;
    Order = d.Order;
    Text = d.Text;
    OptionalRequirement = d.ExtraRequirement;
  }

  public int SocialLinkDialogId { get; init; }
  public int SocialLinkId { get; init; }
  public int Rank { get; init; }
  public int Order { get; init; }
  public string Text { get; init; }
  public string? OptionalRequirement { get; init; }
}

public record GetSocialLinkDialogQuery(int SocialLinkId) : IRequest<ZachZoneCommandResponse<List<SocialLinkDialogDto>>>;

public class GetSocialLinkDialogQueryHandler(IDatabaseService dbContext)
  : IRequestHandler<GetSocialLinkDialogQuery, ZachZoneCommandResponse<List<SocialLinkDialogDto>>>
{
  public async Task<ZachZoneCommandResponse<List<SocialLinkDialogDto>>> Handle(GetSocialLinkDialogQuery request, CancellationToken cancellationToken)
  {
    var dialogs = await dbContext.Entity<SocialLinkDialogue>()
      .Where(x => x.SocialLinkId == request.SocialLinkId)
      .Select(x => new SocialLinkDialogDto(x))
      .ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(dialogs);
  }
}