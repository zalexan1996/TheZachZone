using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record GameInfoDto(int Id, string Name, string Description, string[] Categories, DateOnly UploadDate);
public record GetGameInfoQuery(int? Id = null, string? Name = null, string? Category = null) : IRequest<ZachZoneCommand<List<GameInfoDto>>>;

public class GetGameInfoQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameInfoQuery, ZachZoneCommand<List<GameInfoDto>>>
{
    public async Task<ZachZoneCommand<List<GameInfoDto>>> Handle(GetGameInfoQuery request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Set<GameInfo>()
            .Where(x => request.Id == null || x.Id == request.Id)
            .Where(x => request.Name == null || x.Name == request.Name)
            .Where(x => request.Category == null || x.Categories.Any(c => request.Category == c))
            .OrderByDescending(x => x.UploadDate)
            .Select(x => new GameInfoDto(x.Id, x.Name, x.Description, x.Categories, x.UploadDate))
            .ToListAsync(cancellationToken);

        return ZachZoneCommand.Success(results);
    }
}