using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record GameImageDto(int ImageId, int GameInfoId, DateTime UploadedOn, IList<byte> Data);

public record GetGameImagesQuery(int GameInfoId) : IRequest<ZachZoneCommandResponse<List<GameImageDto>>>;

public class GetGameImagesQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameImagesQuery, ZachZoneCommandResponse<List<GameImageDto>>>
{
  public async Task<ZachZoneCommandResponse<List<GameImageDto>>> Handle(GetGameImagesQuery request, CancellationToken cancellationToken)
  {
    var images = await dbContext.Entity<GameImage>()
      .Where(i => i.GameId == request.GameInfoId)
      .OrderBy(x => x.UploadedOn)
      .Select(x => new GameImageDto(x.Id, x.GameId, x.UploadedOn, x.Data.ToArray()))
      .ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(images);
  }
}
