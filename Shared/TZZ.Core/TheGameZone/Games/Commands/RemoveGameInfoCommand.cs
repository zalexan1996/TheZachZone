using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public record RemoveGameInfoCommand(int Id) : IRequest<ZachZoneCommand>;

public class RemoveGameInfoCommandHandler(IDatabaseService dbContext) : IRequestHandler<RemoveGameInfoCommand, ZachZoneCommand>
{
    public async Task<ZachZoneCommand> Handle(RemoveGameInfoCommand request, CancellationToken cancellationToken)
    {
        var gameInfo = await dbContext.Set<GameInfo>().SingleAsync(x => x.Id == request.Id, cancellationToken);

        dbContext.Remove(gameInfo);
        await dbContext.SaveChanges(cancellationToken);


        string root = Directory.GetCurrentDirectory();
        string uploadDirectory = Path.Combine(root, "wwwroot", "games", request.Id.ToString());
        if (Directory.Exists(uploadDirectory))
        {
            Directory.Delete(uploadDirectory, true);
        }
        return new ZachZoneCommand();
    }
}