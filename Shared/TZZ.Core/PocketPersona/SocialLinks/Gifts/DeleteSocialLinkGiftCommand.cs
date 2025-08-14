using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Gifts;

public record DeleteSocialLinkGiftCommand(int SocialLinkGiftId) : IRequest<ZachZoneCommandResponse>;

public class DeleteSocialLinkGiftCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<DeleteSocialLinkGiftCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(DeleteSocialLinkGiftCommand request, CancellationToken cancellationToken)
  {
    var gift = await dbContext.Entity<SocialLinkGift>().SingleAsync(x => x.Id == request.SocialLinkGiftId, cancellationToken);

    dbContext.Remove(gift);
    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}