using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public class SubmitReviewCommand : IRequest<ZachZoneCommandResponse>
{
    public int GameId { get; set; }
    public required string Content { get; set; }
}

public class SubmitReviewCommandHandler(IDatabaseService dbContext, ICurrentUserService user)
    : IRequestHandler<SubmitReviewCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(SubmitReviewCommand request, CancellationToken cancellationToken)
    {
        if (!user.UserId.HasValue)
        {
            return ZachZoneCommandResponse.Failure("UserId", "You do not have access to this functionality.");
        }

        if (!await dbContext.Set<Game>().AnyAsync(x => x.Id == request.GameId))
        {
            return ZachZoneCommandResponse.Failure("GameId", "Requested game does not exist.");
        }

        await dbContext.Add(new Review()
        {
            Author = await dbContext.Set<User>().SingleAsync(x => x.Id == user.UserId, cancellationToken),
            AuthorId = user.UserId.Value,
            Content = request.Content,
            CreatedOn = DateTime.Now,
            Game = await dbContext.Set<Game>().SingleAsync(x => x.Id == request.GameId, cancellationToken),
            GameId = request.GameId,
            
        }, cancellationToken);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}