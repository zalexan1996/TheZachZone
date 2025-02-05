using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;

namespace TZZ.Core.PocketPersona.Arcanas;

public class AddArcanaCommand : IRequest<ZachZoneCommandResponse>
{
    public required string Name { get; set; }
}

public class AddArcanaCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<AddArcanaCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddArcanaCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Set<TZZ.Domain.Entities.PocketPersona.Arcana>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
        {
            return ZachZoneCommandResponse.Failure(nameof(AddArcanaCommand.Name), "The specified Arcana already exists.");
        }

        await dbContext.Add(new TZZ.Domain.Entities.PocketPersona.Arcana()
        {
            Name = request.Name,
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();

    }
}