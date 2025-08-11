using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Gifts;

public record AddSocialLinkGiftCommand(int SocialLinkId, string GiftName, string AcquiredAd) : IRequest<ZachZoneCommandResponse>;

public class AddSocialLinkGiftCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<AddSocialLinkGiftCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(AddSocialLinkGiftCommand request, CancellationToken cancellationToken)
  {
    await dbContext.Add(new SocialLinkGift()
    {
      AcquiredAt = request.AcquiredAd,
      Name = request.GiftName,
      SocialLink = await dbContext.Entity<SocialLink>().FirstAsync(x => x.Id == request.SocialLinkId, cancellationToken),
      SocialLinkId = request.SocialLinkId,
    }, cancellationToken);

    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}