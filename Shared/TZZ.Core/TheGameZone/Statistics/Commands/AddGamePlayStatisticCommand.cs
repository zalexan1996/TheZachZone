using MediatR;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Statistics.Commands;

public record AddGamePlayStatisticCommand(int GameId) : IRequest<ZachZoneCommandResponse>;

public class AddGamePlayStatisticCommandHandler(IDatabaseService dbContext) : IRequestHandler<AddGamePlayStatisticCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddGamePlayStatisticCommand request, CancellationToken cancellationToken)
    {
        var newStatistic = new GameStatistic()
        {
            GameInfoId = request.GameId,
            PlayedOn = DateTime.Now,
            GameInfo = dbContext.Set<GameInfo>().Single(x => x.Id == request.GameId)
        };

        await dbContext.Add(newStatistic, cancellationToken);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}