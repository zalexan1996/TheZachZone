using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;

namespace TZZ.Core.PocketPersona.SocialLinkDialogues;

public class SocialLinkDialogueDto
{
    public int SocialLinkDialogueId { get; set; }
    public required string Text { get; set; }
    public int Order { get; set; }
    public string? Requirement { get; set; }
    public int SocialLinkId { get; set; }
    public int SocialLinkRankId { get; set; }
}

public class GetSocialLinkDialogueQuery : IRequest<ZachZoneCommandResponse<List<SocialLinkDialogueDto>>>
{
    public int? SocialLinkDialogueId { get; set; }
    public int? SocialLinkRankId { get; set; }
    public int? SocialLinkId { get; set; }
}

public class GetSocialLinkDialogueQueryHandler(IDatabaseService dbContext)
    : IRequestHandler<GetSocialLinkDialogueQuery, ZachZoneCommandResponse<List<SocialLinkDialogueDto>>>
{
    public async Task<ZachZoneCommandResponse<List<SocialLinkDialogueDto>>> Handle(GetSocialLinkDialogueQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Set<Domain.Entities.PocketPersona.SocialLinkDialogue>();

        if (request.SocialLinkDialogueId.HasValue)
        {
            query.Where(x => x.Id == request.SocialLinkDialogueId.Value);
        }

        if (request.SocialLinkRankId.HasValue)
        {
            query.Where(x => x.SocialLinkRankId == request.SocialLinkRankId.Value);
        }
        if (request.SocialLinkId.HasValue)
        {
            query.Where(x => x.SocialLinkRank.SocialLinkId == request.SocialLinkId.Value);
        }

        var records = await query.Select(x => new SocialLinkDialogueDto()
        {
            Order = x.Order,
            Requirement = x.ExtraRequirement,
            Text = x.Text,
            SocialLinkRankId = x.SocialLinkRankId,
            SocialLinkId = x.SocialLinkRank.SocialLinkId,
            SocialLinkDialogueId = x.Id
        }).ToListAsync(cancellationToken);

        return ZachZoneCommandResponse.Success(records);
    }
}
