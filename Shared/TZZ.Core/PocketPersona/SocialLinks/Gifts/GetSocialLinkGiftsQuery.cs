using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Gifts;

public record SocialLinkGiftDto(int SocialLinkGiftId, int SocialLinkId, string GiftName, string GiftAcquiredAt);

public record GetSocialLinkGiftsQuery(int SocialLinkId) : IRequest<ZachZoneCommandResponse<List<SocialLinkGiftDto>>>;


public class GetSocialLinkGiftsQueryHandler(IDatabaseService dbContext) 
  : IRequestHandler<GetSocialLinkGiftsQuery, ZachZoneCommandResponse<List<SocialLinkGiftDto>>>
{
  public async Task<ZachZoneCommandResponse<List<SocialLinkGiftDto>>> Handle(GetSocialLinkGiftsQuery request, CancellationToken cancellationToken)
  {
    var gifts = await dbContext.Entity<SocialLinkGift>()
      .Where(x => x.SocialLinkId == request.SocialLinkId)
      .Select(x => new SocialLinkGiftDto(x.SocialLinkGiftId, x.SocialLinkId, x.Name, x.AcquiredAt))
      .ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(gifts);
  }
}
