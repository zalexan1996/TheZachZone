using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Characters;

public class UpdateCharacterCommand : IRequest<ZachZoneCommandResponse>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int GameId { get; set; }
}

public class UpdateCharacterCommandHandler(IDatabaseService dbContext) : IRequestHandler<UpdateCharacterCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
    {
        var record = await dbContext.Set<Domain.Entities.PocketPersona.Character>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (record is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(UpdateCharacterCommand.Id), "No Character exists with the specified Id.");
        }

        var game = await dbContext.Set<Game>().SingleOrDefaultAsync(x => x.Id == record.Id, cancellationToken);

        if (game is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(UpdateCharacterCommand.GameId), "No Game exists with the specified Id.");
        }

        record.Name = request.Name;
        record.GameId = request.GameId;

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}
