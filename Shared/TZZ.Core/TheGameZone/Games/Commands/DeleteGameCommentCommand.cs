using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public record DeleteGameCommentCommand(int CommentId) : IRequest<ZachZoneCommandResponse>;

public class DeleteGameCommentCommandHandler(IDatabaseService dbContext) : IRequestHandler<DeleteGameCommentCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(DeleteGameCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await dbContext.Set<Comment>().SingleAsync(x => x.Id == request.CommentId, cancellationToken);

        dbContext.Remove(comment);

        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}