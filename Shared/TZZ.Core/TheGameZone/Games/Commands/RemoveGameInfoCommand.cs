using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public record RemoveGameInfoCommand(int Id) : IRequest<ZachZoneCommandResponse>;

public class RemoveGameInfoCommandHandler(IDatabaseService dbContext) : IRequestHandler<RemoveGameInfoCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(RemoveGameInfoCommand request, CancellationToken cancellationToken)
    {
        var gameInfo = await dbContext.Set<Game>().SingleAsync(x => x.Id == request.Id, cancellationToken);

        dbContext.Remove(gameInfo);
        await dbContext.SaveChanges(cancellationToken);


        string root = Directory.GetCurrentDirectory();
        string uploadDirectory = Path.Combine(root, "wwwroot", "games", request.Id.ToString());
        if (Directory.Exists(uploadDirectory))
        {
            Directory.Delete(uploadDirectory, true);
        }
        return new ZachZoneCommandResponse();
    }
}