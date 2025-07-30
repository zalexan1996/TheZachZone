using MediatR;
using TZZ.Common.Enums.Constants;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class GrantAdminAccessCommand : IRequest<ZachZoneCommandResponse<int>>
{
  public int UserId { get; set; }
}

public class GrantAdminAccessCommandHandler(IIdentityService identityService)
  : IRequestHandler<GrantAdminAccessCommand, ZachZoneCommandResponse<int>>
{
  public async Task<ZachZoneCommandResponse<int>> Handle(GrantAdminAccessCommand request, CancellationToken cancellationToken)
  {
    var result = await identityService.AddClaim(request.UserId, ClaimTypes.Role, Roles.Admin).ConfigureAwait(false);

    return result ? ZachZoneCommandResponse.Success(request.UserId)
      : ZachZoneCommandResponse.Failure<int>(nameof(request.UserId), "Failed to add a role to the requested user.");
  }
}