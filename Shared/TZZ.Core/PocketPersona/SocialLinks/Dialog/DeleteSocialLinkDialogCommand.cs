using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Dialog;

public record DeleteSocialLinkDialogCommand(int socialLinkDialogId) : IRequest<ZachZoneCommandResponse>;

public class DeleteSocialLinkDialogCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<DeleteSocialLinkCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(DeleteSocialLinkCommand request, CancellationToken cancellationToken)
  {
    var dialogue = await dbContext.Entity<SocialLinkDialogue>()
      .SingleAsync(x => x.Id == request.Id, cancellationToken);

    dbContext.Remove(dialogue);
    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}