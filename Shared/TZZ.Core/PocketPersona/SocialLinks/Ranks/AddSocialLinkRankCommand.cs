using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinkRanks;

public class AddSocialLinkRankCommand : IRequest<ZachZoneCommandResponse>
{
    public int SocialLinkId { get; set; }
    public int Rank { get; set; }
    public string? Requirement { get; set; }
}

public class AddSocialLinkRankCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<AddSocialLinkRankCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddSocialLinkRankCommand request, CancellationToken cancellationToken)
    {
        var socialLink = await dbContext.Set<SocialLink>().SingleOrDefaultAsync(x => x.Id == request.SocialLinkId, cancellationToken);

        if (socialLink is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(AddSocialLinkRankCommand.SocialLinkId), "The specified SocialLinkId does not exist.");
        }

        if (await dbContext.Set<SocialLinkRank>().AnyAsync(x => x.SocialLinkId == request.SocialLinkId && x.Rank == request.Rank, cancellationToken))
        {
            return ZachZoneCommandResponse.Failure(nameof(AddSocialLinkRankCommand.Rank), "The specified SocialLink already has the requested rank.");
        }

        await dbContext.Add(new SocialLinkRank()
        {
            Rank = request.Rank,
            SocialLink = socialLink,
            SocialLinkId = request.SocialLinkId,
            Requirement = request.Requirement,
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}