using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;

namespace TZZ.Core.PocketPersona.SocialLinks;

public class SocialLinkDto
{
  public int SocialLinkId { get; set; }
  public required string Name { get; set; }
  public int CharacterId { get; set; }
  public int ArcanaId { get; set; }
  public int GameId { get; set; }
}

public class GetSocialLinkQuery : IRequest<ZachZoneCommandResponse<List<SocialLinkDto>>>
{
  public int? Id { get; set; }
  public string? Name { get; set; }
}

public class GetSocialLinkQueryHandler(IDatabaseService dbContext)
  : IRequestHandler<GetSocialLinkQuery, ZachZoneCommandResponse<List<SocialLinkDto>>>
{
  public async Task<ZachZoneCommandResponse<List<SocialLinkDto>>> Handle(GetSocialLinkQuery request, CancellationToken cancellationToken)
  {
    var query = dbContext.Entity<Domain.Entities.PocketPersona.SocialLink>();

    if (request.Id.HasValue)
    {
      query = query.Where(x => x.Id == request.Id.Value);
    }
    if (!string.IsNullOrEmpty(request.Name))
    {
      query = query.Where(x => x.Name.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase));
    }

    var records = await query.Select(x => new SocialLinkDto()
    {
      SocialLinkId = x.Id,
      Name = x.Name,
      ArcanaId = x.ArcanaId,
      CharacterId = x.CharacterId,
      GameId = x.Character.GameId
    }).ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(records);
  }
}
