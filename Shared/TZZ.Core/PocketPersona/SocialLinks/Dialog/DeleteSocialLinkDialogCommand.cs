using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Dialog;

public record DeleteSocialLinkDialogCommand(int SocialLinkDialogId) : IRequest<ZachZoneCommandResponse>;

public class DeleteSocialLinkDialogCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<DeleteSocialLinkDialogCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(DeleteSocialLinkDialogCommand request, CancellationToken cancellationToken)
  {
    var dialogue = await dbContext.Entity<SocialLinkDialogue>()
      .SingleAsync(x => x.Id == request.SocialLinkDialogId, cancellationToken);

    var othersInRank = await dbContext.Entity<SocialLinkDialogue>()
      .Where(x => x.Id != dialogue.Id && x.SocialLinkId == dialogue.SocialLinkId && x.Rank == dialogue.Rank)
      .OrderBy(x => x.Order)
      .ToListAsync(cancellationToken);

    dbContext.Remove(dialogue);

    othersInRank = [.. othersInRank.Select((x, i) =>
    {
      x.Order = i + 1;
      return x;
    })];

    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();



  }
}