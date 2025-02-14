using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Metadata;

public class RemoveGenreCommand : IRequest<ZachZoneCommandResponse>
{
    public required string Name { get; set; }
}

public class RemoveGenreCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<RemoveGenreCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await dbContext.Set<Genre>().SingleOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        if (genre is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(RemoveGenreCommand.Name), "The specified genre does not exist.");
        }

        var impact = await dbContext.Set<Game>().CountAsync(x => x.Genres.Any(g => g.Name == request.Name), cancellationToken);

        dbContext.Remove(genre);
        await dbContext.SaveChanges(cancellationToken);

        return new ZachZoneCommandResponse()
        {
            Infos =
            {
                {
                    "Summary", $"Genre '{request.Name}' was removed."
                },
                {
                    "Impact", $"{impact} games were affected."
                }
            }
        };
    }
}
