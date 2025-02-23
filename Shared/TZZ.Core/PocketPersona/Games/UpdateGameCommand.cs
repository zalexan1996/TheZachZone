﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Games;

public class UpdateGameCommand : IRequest<ZachZoneCommandResponse>
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class UpdateGameCommandHandler(IDatabaseService dbContext) : IRequestHandler<UpdateGameCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var record = await dbContext.Set<Game>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (record is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(UpdateGameCommand.Id), "No game exists with the specified Id.");
        }

        record.Name = request.Name;

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}
