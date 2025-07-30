using MediatR;
using TZZ.Common.Enums.Constants;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class RevokeAdminAccessCommand : IRequest<ZachZoneCommandResponse<int>>
{
  public int UserId { get; set; }
}

public class RevokeAdminAccessCommandHandler(IIdentityService identityService, ICurrentUserService currentUser)
  : IRequestHandler<RevokeAdminAccessCommand, ZachZoneCommandResponse<int>>
{
  public async Task<ZachZoneCommandResponse<int>> Handle(RevokeAdminAccessCommand request, CancellationToken cancellationToken)
  {
    if (currentUser.UserId == request.UserId)
    {
      return ZachZoneCommandResponse.Failure<int>("UserId", "You can't revoke your admin rights while logged in.");
    }

    var result = await identityService.RemoveClaim(request.UserId, ClaimTypes.Role, Roles.Admin).ConfigureAwait(false);

    return result ? ZachZoneCommandResponse.Success(request.UserId)
      : ZachZoneCommandResponse.Failure<int>(nameof(request.UserId), "Failed to remove a role to the requested user.");
  }
}