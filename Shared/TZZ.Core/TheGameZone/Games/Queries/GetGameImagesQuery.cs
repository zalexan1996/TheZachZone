using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record GameImageDto(int ImageId, int GameInfoId, DateTime UploadedOn, byte[] Data);

public record GetGameImagesQuery(int GameInfoId) : IRequest<ZachZoneCommand<List<GameImageDto>>>;

public class GetGameImagesQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameImagesQuery, ZachZoneCommand<List<GameImageDto>>>
{
    public async Task<ZachZoneCommand<List<GameImageDto>>> Handle(GetGameImagesQuery request, CancellationToken cancellationToken)
    {
        var images = await dbContext.Set<GameImage>()
            .Where(i => i.GameInfoId == request.GameInfoId)
            .OrderBy(x => x.UploadedOn)
            .Select(x => new GameImageDto(x.Id, x.GameInfoId, x.UploadedOn, x.Data))
            .ToListAsync(cancellationToken);

        return ZachZoneCommand.Success(images);
    }
}
