using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Games;

public class AddGameCommand : IRequest<ZachZoneCommandResponse>
{
    public required string Name { get; set; }
}

public class AddGameCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<AddGameCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Set<Game>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
        {
            return ZachZoneCommandResponse.Failure(nameof(AddGameCommand.Name), "The specified game already exists.");
        }

        await dbContext.Add(new Game()
        {
            Name = request.Name,
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();

    }
}