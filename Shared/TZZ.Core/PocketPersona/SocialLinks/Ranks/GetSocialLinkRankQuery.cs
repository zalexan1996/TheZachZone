using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;

namespace TZZ.Core.PocketPersona.SocialLinkRanks;

public class SocialLinkRankDto
{
    public int SocialLinkRankId { get; set; }
    public int SocialLinkId { get; set; }
    public int Rank { get; set; }
    public string? Requirement { get; set; }
}

public class GetSocialLinkRankQuery : IRequest<ZachZoneCommandResponse<List<SocialLinkRankDto>>>
{
    public int? SocialLinkRankId { get; set; }
    public int? SocialLinkId { get; set; }
    public int? Rank { get; set; }
}

public class GetSocialLinkRankQueryHandler(IDatabaseService dbContext)
    : IRequestHandler<GetSocialLinkRankQuery, ZachZoneCommandResponse<List<SocialLinkRankDto>>>
{
    public async Task<ZachZoneCommandResponse<List<SocialLinkRankDto>>> Handle(GetSocialLinkRankQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Set<Domain.Entities.PocketPersona.SocialLinkRank>();

        if (request.SocialLinkRankId.HasValue)
        {
            query.Where(x => x.Id == request.SocialLinkRankId.Value);
        }
        if (request.SocialLinkId.HasValue)
        {
            query.Where(x => x.SocialLinkId == request.SocialLinkId.Value);
        }
        if (request.Rank.HasValue)
        {
            query.Where(x => x.Rank == request.Rank.Value);
        }

        var records = await query.Select(x => new SocialLinkRankDto()
        {
            SocialLinkRankId = x.Id,
            Rank = x.Rank,
            Requirement = x.Requirement,
            SocialLinkId = x.SocialLinkId,
        }).ToListAsync(cancellationToken);

        return ZachZoneCommandResponse.Success(records);
    }
}
