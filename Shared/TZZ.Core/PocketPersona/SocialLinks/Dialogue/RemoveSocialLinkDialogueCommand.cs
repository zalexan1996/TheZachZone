using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Ranks;

public class RemoveSocialLinkDialogueCommand : IRequest<ZachZoneCommandResponse>
{
    public int SocialLinkDialogueId { get; set; }
}

public class RemoveSocialLinkDialogueCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<RemoveSocialLinkDialogueCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveSocialLinkDialogueCommand request, CancellationToken cancellationToken)
    {
        var rank = await dbContext.Set<SocialLinkDialogue>().SingleOrDefaultAsync(x => x.Id == request.SocialLinkDialogueId, cancellationToken);

        if (rank is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(request.SocialLinkDialogueId), "The requested SocialLinkDialogueId does not exist.");
        }

        dbContext.Remove(rank);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}
