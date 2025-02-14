using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks.Dialogue;

public class AddSocialLinkDialogueCommand : IRequest<ZachZoneCommandResponse>
{
    public required int SocialLinkRankId { get; set; }
    public required int Order { get; set; }
    public required string Text { get; set; }
    public string? Requirement { get; set; }
}

public class AddSocialLinkDialogueCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<AddSocialLinkDialogueCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddSocialLinkDialogueCommand request, CancellationToken cancellationToken)
    {
        var rank = await dbContext.Set<SocialLinkRank>()
            .SingleOrDefaultAsync(x => x.Id == request.SocialLinkRankId, cancellationToken);

        if (rank is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(request.SocialLinkRankId), "The specified SocialLinkRankId does not exist.");
        }

        if (await dbContext.Set<SocialLinkDialogue>().AnyAsync(x => x.SocialLinkRankId == rank.Id && x.Order == request.Order, cancellationToken))
        {
            return ZachZoneCommandResponse.Failure(nameof(request.Order), "The specified rank already has a dialogue at this Order level.");
        }

        await dbContext.Add(new SocialLinkDialogue()
        {
            Order = request.Order,
            Text = request.Text,
            ExtraRequirement = request.Requirement,
            SocialLinkRank = rank,
            SocialLinkRankId = rank.Id
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}