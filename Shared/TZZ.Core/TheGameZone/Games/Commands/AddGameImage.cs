using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TZZ.Common;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public class AddGameImageCommand : IRequest<ZachZoneCommandResponse<int>>
{
  public int GameInfoId { get; set; }
  public required IFormFile File { get; set; }
}

public class AddGameImageCommandHandler(IDatabaseService dbContext) : IRequestHandler<AddGameImageCommand, ZachZoneCommandResponse<int>>
{
  public async Task<ZachZoneCommandResponse<int>> Handle(AddGameImageCommand request, CancellationToken cancellationToken)
  {
    var image = new GameImage()
    {
      UploadedOn = DateTime.Now,
      GameId = request.GameInfoId,
      Game = await dbContext.Entity<Game>().SingleAsync(x => x.Id == request.GameInfoId, cancellationToken),
      Data = await request.File.ToByteArray()
    };

    await dbContext.Add(image, cancellationToken);
    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success(image.Id);
  }
}