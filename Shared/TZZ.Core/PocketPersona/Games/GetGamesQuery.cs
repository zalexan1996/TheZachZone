using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Games;

public class GameDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
public class GetGamesQuery : IRequest<ZachZoneCommandResponse<IEnumerable<GameDto>>>
{
}

public class GetGamesQueryHandler(IDatabaseService dbContext)
    : IRequestHandler<GetGamesQuery, ZachZoneCommandResponse<IEnumerable<GameDto>>>
{
    public async Task<ZachZoneCommandResponse<IEnumerable<GameDto>>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Set<Game>().Select(x => new GameDto()
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync(cancellationToken);

        return ZachZoneCommandResponse.Success(results.AsEnumerable());
    }
}