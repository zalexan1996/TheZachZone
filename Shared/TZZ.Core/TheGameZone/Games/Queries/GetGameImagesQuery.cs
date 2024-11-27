using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record GameImageDto(int ImageId, int GameInfoId, DateTime UploadedOn, byte[] Data);

public record GetGameImagesQuery(int GameInfoId) : IRequest<ZachZoneCommandResponse<List<GameImageDto>>>;

public class GetGameImagesQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameImagesQuery, ZachZoneCommandResponse<List<GameImageDto>>>
{
    public async Task<ZachZoneCommandResponse<List<GameImageDto>>> Handle(GetGameImagesQuery request, CancellationToken cancellationToken)
    {
        var images = await dbContext.Set<GameImage>()
            .Where(i => i.GameId == request.GameInfoId)
            .OrderBy(x => x.UploadedOn)
            .Select(x => new GameImageDto(x.Id, x.GameId, x.UploadedOn, x.Data))
            .ToListAsync(cancellationToken);

        return ZachZoneCommandResponse.Success(images);
    }
}
