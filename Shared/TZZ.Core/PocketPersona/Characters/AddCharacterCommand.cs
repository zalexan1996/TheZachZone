using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Characters;

public class AddCharacterCommand : IRequest<ZachZoneCommandResponse>
{
    public required string Name { get; set; }
    public required int GameId { get; set; }
}

public class AddCharacterCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<AddCharacterCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddCharacterCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Set<TZZ.Domain.Entities.PocketPersona.Character>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
        {
            return ZachZoneCommandResponse.Failure(nameof(AddCharacterCommand.Name), "The specified Character already exists.");
        }

        var game = await dbContext.Set<Game>().SingleOrDefaultAsync(x => x.Id == request.GameId, cancellationToken);

        if (game is null)
        {
            return ZachZoneCommandResponse.Failure(nameof(AddCharacterCommand.GameId), "The specified game does not exist.");
        }

        await dbContext.Add(new TZZ.Domain.Entities.PocketPersona.Character()
        {
            Name = request.Name,
            GameId = request.GameId,
            Game = game
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}