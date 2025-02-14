using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Ranks;

public class UpdateSocialLinkDialogueCommand : IRequest<ZachZoneCommandResponse>
{
    public required int SocialLinkDialogueId { get; set; }
    public required int SocialLinkRankId { get; set; }
    public required int Order { get; set; }
    public required string Text { get; set; }
    public string? Requirement { get; set; }
}

public class UpdateSocialLinkDialogueCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<UpdateSocialLinkDialogueCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(UpdateSocialLinkDialogueCommand request, CancellationToken cancellationToken)
    {
        var SocialLinkDialogue = await dbContext.Set<SocialLinkDialogue>().SingleOrDefaultAsync(x => x.Id == request.SocialLinkDialogueId);

        if (SocialLinkDialogue is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(request.SocialLinkDialogueId), "The specified SocialLinkDialogue does not exist.");
        }

        if (!await SocialLinkRankExists(request))
        {
            return ZachZoneCommandResponse.Failure(nameof(request.SocialLinkRankId), "The specified SocialLinkRankId doesn't exist.");
        }

        if (await OrderExists(request))
        {
            return ZachZoneCommandResponse.Failure(nameof(request.Order), "The specified Order already exists on the SocialLink.");
        }

        SocialLinkDialogue.SocialLinkRankId = request.SocialLinkRankId;
        SocialLinkDialogue.ExtraRequirement = request.Requirement;
        SocialLinkDialogue.Text = request.Text;
        SocialLinkDialogue.Order = request.Order;

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }

    private async Task<bool> SocialLinkRankExists(UpdateSocialLinkDialogueCommand request)
        => await dbContext.Set<SocialLink>().AnyAsync(x => x.Id == request.SocialLinkRankId);

    private async Task<bool> OrderExists(UpdateSocialLinkDialogueCommand request)
        => await dbContext.Set<SocialLinkDialogue>()
            .AnyAsync(x => x.Id != request.SocialLinkDialogueId && x.Order == request.Order && x.SocialLinkRankId == request.SocialLinkRankId);
}