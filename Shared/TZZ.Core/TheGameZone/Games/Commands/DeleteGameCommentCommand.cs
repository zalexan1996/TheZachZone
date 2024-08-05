using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public record DeleteGameCommentCommand(int CommentId) : IRequest<ZachZoneCommand>;

public class DeleteGameCommentCommandHandler(IDatabaseService dbContext) : IRequestHandler<DeleteGameCommentCommand, ZachZoneCommand>
{
    public async Task<ZachZoneCommand> Handle(DeleteGameCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await dbContext.Set<GameComment>().SingleAsync(x => x.Id == request.CommentId, cancellationToken);

        dbContext.Remove(comment);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommand.Success();
    }
}