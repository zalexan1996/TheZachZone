using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks;

public class UpdateSocialLinkCommand : IRequest<ZachZoneCommandResponse>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int ArcanaId { get; set; }
    public required int CharacterId { get; set; }
}

public class UpdateSocialLinkCommandHandler(IDatabaseService dbContext) : IRequestHandler<UpdateSocialLinkCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(UpdateSocialLinkCommand request, CancellationToken cancellationToken)
    {
        // Query
        var record = await dbContext.Set<SocialLink>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var arcana = await dbContext.Set<Arcana>().SingleOrDefaultAsync(x => x.Id == request.ArcanaId, cancellationToken);
        var character = await dbContext.Set<Character>().SingleOrDefaultAsync(x => x.Id == request.CharacterId, cancellationToken);

        // Assertions
        if (record is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(UpdateSocialLinkCommand.Id), "No SocialLink exists with the specified Id.");
        }

        if (arcana is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(UpdateSocialLinkCommand.ArcanaId), "The specified arcana does not exist.");
        }

        if (character is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(UpdateSocialLinkCommand.CharacterId), "The specified character does not exist.");
        }

        // Update
        record.Name = request.Name;
        record.ArcanaId = request.ArcanaId;
        record.CharacterId = request.CharacterId;
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}
