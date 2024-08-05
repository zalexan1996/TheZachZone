using MediatR;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Statistics.Commands;

public record AddGamePlayStatisticCommand(int GameId) : IRequest<ZachZoneCommand>;

public class AddGamePlayStatisticCommandHandler(IDatabaseService dbContext) : IRequestHandler<AddGamePlayStatisticCommand, ZachZoneCommand>
{
    public async Task<ZachZoneCommand> Handle(AddGamePlayStatisticCommand request, CancellationToken cancellationToken)
    {
        var newStatistic = new GameStatistic()
        {
            GameInfoId = request.GameId,
            PlayedOn = DateTime.Now,
            GameInfo = dbContext.Set<GameInfo>().Single(x => x.Id == request.GameId)
        };

        await dbContext.Add(newStatistic, cancellationToken);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommand.Success();
    }
}