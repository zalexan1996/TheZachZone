using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks;

public class RemoveSocialLinkCommand : IRequest<ZachZoneCommandResponse>
{
    public int Id { get; set; }
}

public class RemoveSocialLinkCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<RemoveSocialLinkCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveSocialLinkCommand request, CancellationToken cancellationToken)
    {
        var record = await dbContext.Set<SocialLink>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (record is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(RemoveSocialLinkCommand.Id), "No SocialLink exists with the specified Id.");
        }

        dbContext.Remove(record);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}