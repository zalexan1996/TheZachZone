using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Games;

public class RemoveGameCommand : IRequest<ZachZoneCommandResponse>
{
    public int Id { get; set; }
}

public class RemoveGameCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<RemoveGameCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveGameCommand request, CancellationToken cancellationToken)
    {
        var record = await dbContext.Set<Game>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (record is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(RemoveGameCommand.Id), "No game exists with the specified Id.");
        }

        dbContext.Remove(record);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}