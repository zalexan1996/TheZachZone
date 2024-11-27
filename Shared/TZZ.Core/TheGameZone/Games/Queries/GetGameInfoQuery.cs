using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record GameInfoDto(int Id, string Name, string Description, IDictionary<int, string> Genres, DateOnly UploadDate);
public record GetGameInfoQuery(int? Id = null, string? Name = null, int? Genre = null) : IRequest<ZachZoneCommandResponse<List<GameInfoDto>>>;

public class GetGameInfoQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameInfoQuery, ZachZoneCommandResponse<List<GameInfoDto>>>
{
    public async Task<ZachZoneCommandResponse<List<GameInfoDto>>> Handle(GetGameInfoQuery request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Set<Game>()
            .Where(x => request.Id == null || x.Id == request.Id)
            .Where(x => request.Name == null || x.Name == request.Name)
            .Where(x => request.Genre == null || x.Genres.Any(c => request.Genre == c.GenreId))
            .OrderByDescending(x => x.UploadDate)
            .Select(x => new GameInfoDto(x.Id, x.Name, x.Description, x.Genres.ToDictionary(x => x.GenreId, x => x.Name), x.UploadDate))
            .ToListAsync(cancellationToken);

        return ZachZoneCommandResponse.Success(results);
    }
}