using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks;

public class AddSocialLinkCommand : IRequest<ZachZoneCommandResponse>
{
    public required string Name { get; set; }
    public required int ArcanaId { get; set; }
    public required int CharacterId { get; set; }
}

public class AddSocialLinkCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<AddSocialLinkCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddSocialLinkCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Set<SocialLink>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
        {
            return ZachZoneCommandResponse.Failure(nameof(AddSocialLinkCommand.Name), "The specified SocialLink already exists.");
        }

        var arcana = await dbContext.Set<Arcana>().SingleOrDefaultAsync(x => x.Id == request.ArcanaId, cancellationToken);

        if (arcana is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(AddSocialLinkCommand.ArcanaId), "The specified arcana does not exist.");
        }

        var character = await dbContext.Set<Character>().SingleOrDefaultAsync(x => x.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(AddSocialLinkCommand.CharacterId), "The specified character does not exist.");
        }

        await dbContext.Add(new SocialLink()
        {
            Name = request.Name,
            ArcanaId = request.ArcanaId,
            CharacterId = request.CharacterId,
            Arcana = arcana,
            Character = character,
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}