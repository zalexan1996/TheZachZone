using MediatR;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class DeleteUserCommand : IRequest<ZachZoneCommandResponse>
{
  public int UserId { get; set; }
}

public class DeleteUserCommandHandler(IIdentityService identity, ICurrentUserService currentUser)
  : IRequestHandler<DeleteUserCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
  {
    if (currentUser.UserId == request.UserId)
    {
      return ZachZoneCommandResponse.Failure("UserId", "You can't delete your account while logged in.");
    }

    return await identity.DeleteUser(request.UserId).ConfigureAwait(false);
  }
}