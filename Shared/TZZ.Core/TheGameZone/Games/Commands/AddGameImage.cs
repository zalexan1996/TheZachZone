using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TZZ.Common;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public class AddGameImageCommand : IRequest<ZachZoneCommand<int>>
{
    public int GameInfoId { get; set; }
    public required IFormFile File { get; set; }
}

public class AddGameImageCommandHandler(IDatabaseService dbContext) : IRequestHandler<AddGameImageCommand, ZachZoneCommand<int>>
{
    public async Task<ZachZoneCommand<int>> Handle(AddGameImageCommand request, CancellationToken cancellationToken)
    {
        var image = new GameImage()
        {
            UploadedOn = DateTime.Now,
            GameInfoId = request.GameInfoId,
            GameInfo = await dbContext.Set<GameInfo>().SingleAsync(x => x.Id == request.GameInfoId),
            Data = await request.File.ToByteArray()
        };

        await dbContext.Add(image, cancellationToken);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommand.Success(image.Id);
    }
}