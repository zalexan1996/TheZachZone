using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Ranks;

public class UpdateSocialLinkRankCommand : IRequest<ZachZoneCommandResponse>
{
    public int SocialLinkRankId { get; set; }
    public int SocialLinkId { get; set; }
    public int Rank { get; set; }
    public string? Requirement { get; set; }
}

public class UpdateSocialLinkRankCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<UpdateSocialLinkRankCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(UpdateSocialLinkRankCommand request, CancellationToken cancellationToken)
    {
        var socialLinkRank = await dbContext.Set<SocialLinkRank>().SingleOrDefaultAsync(x => x.Id == request.SocialLinkRankId);

        if (socialLinkRank is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(request.SocialLinkRankId), "The specified SocialLinkRank does not exist.");
        }

        if (!await SocialLinkExists(request))
        {
            return ZachZoneCommandResponse.Failure(nameof(request.Rank), "The specified SocialLink doesn't exist.");
        }
        
        if (await RankExists(request))
        {
            return ZachZoneCommandResponse.Failure(nameof(request.Rank), "The specified Rank already exists on the SocialLink.");
        }

        socialLinkRank.Rank = request.Rank;
        socialLinkRank.Requirement = request.Requirement;
        socialLinkRank.SocialLinkId = request.SocialLinkId;

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }

    private async Task<bool> SocialLinkExists(UpdateSocialLinkRankCommand request)
        => await dbContext.Set<SocialLink>().AnyAsync(x => x.Id == request.SocialLinkId);

    private async Task<bool> RankExists(UpdateSocialLinkRankCommand request) 
        => await dbContext.Set<SocialLinkRank>()
            .AnyAsync(x => x.Id != request.SocialLinkRankId && x.Rank == request.Rank && x.SocialLinkId == request.SocialLinkId);
}