using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Metadata;

public class AddGenreCommand : IRequest<ZachZoneCommandResponse>
{
    public required string GenreName { get; set; }
}

public class AddGenreCommandHandler(IDatabaseService dbContext)
    : IRequestHandler<AddGenreCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        if (dbContext.Set<Genre>().Any(x => x.Name.ToLower() == request.GenreName.ToLower()))
        {
            return ZachZoneCommandResponse.Failure("GenreName", "Requested genre already exists.");
        }

        await dbContext.Add(new Genre()
        {
            Name = request.GenreName
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        var record = await dbContext.Set<Genre>().SingleAsync(x => x.Name == request.GenreName, cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}