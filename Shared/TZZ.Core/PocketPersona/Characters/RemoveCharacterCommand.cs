using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Characters;

public class RemoveCharacterCommand : IRequest<ZachZoneCommandResponse>
{
    public int Id { get; set; }
}

public class RemoveCharacterCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<RemoveCharacterCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveCharacterCommand request, CancellationToken cancellationToken)
    {
        var record = await dbContext.Set<TZZ.Domain.Entities.PocketPersona.Character>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (record is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(RemoveCharacterCommand.Id), "No Character exists with the specified Id.");
        }

        dbContext.Remove(record);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}