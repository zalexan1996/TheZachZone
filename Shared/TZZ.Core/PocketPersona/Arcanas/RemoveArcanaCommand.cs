using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Arcanas;

public class RemoveArcanaCommand : IRequest<ZachZoneCommandResponse>
{
    public int Id { get; set; }
}

public class RemoveArcanaCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<RemoveArcanaCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveArcanaCommand request, CancellationToken cancellationToken)
    {
        var record = await dbContext.Set<TZZ.Domain.Entities.PocketPersona.Arcana>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (record is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(RemoveArcanaCommand.Id), "No Arcana exists with the specified Id.");
        }

        dbContext.Remove(record);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}