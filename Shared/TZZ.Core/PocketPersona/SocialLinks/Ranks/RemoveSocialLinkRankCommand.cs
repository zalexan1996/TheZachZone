using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Ranks;

public class RemoveSocialLinkRankCommand : IRequest<ZachZoneCommandResponse>
{
    public int SocialLinkRankId { get; set; }
}

public class RemoveSocialLinkRankCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<RemoveSocialLinkRankCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveSocialLinkRankCommand request, CancellationToken cancellationToken)
    {
        var rank = await dbContext.Set<SocialLinkRank>().SingleOrDefaultAsync(x => x.Id == request.SocialLinkRankId, cancellationToken);

        if (rank is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(request.SocialLinkRankId), "The requested SocialLinkRankId does not exist.");
        }

        dbContext.Remove(rank);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}
