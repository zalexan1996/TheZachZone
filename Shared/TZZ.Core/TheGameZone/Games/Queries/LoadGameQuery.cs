using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record LoadGameQuery(int Id) : IRequest<ZachZoneCommand<byte[]>>;

public class LoadGameQueryHandler(IDatabaseService dbContext) : IRequestHandler<LoadGameQuery, ZachZoneCommand<byte[]>>
{
    public async Task<ZachZoneCommand<byte[]>> Handle(LoadGameQuery request, CancellationToken cancellationToken)
    {
        var gameInfo = await dbContext.Set<GameInfo>().SingleAsync(x => x.Id == request.Id, cancellationToken);

        return ZachZoneCommand.Success<byte[]>([]);
    }
}