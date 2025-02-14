using MediatR;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using static TZZ.Common.Shared.Enums.ZachZoneConstants;

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

        if (!await identityService.HasClaim(request.UserId, ClaimTypes.Role, Roles.Admin))
        {
            throw new ArgumentException("The requested user isn't an admin.", nameof(RevokeAdminAccessCommand.UserId));
        }

        var result = await identityService.RemoveClaim(request.UserId, ClaimTypes.Role, Roles.Admin);

        return result ? ZachZoneCommandResponse.Success(request.UserId)
            : ZachZoneCommandResponse.Failure<int>(nameof(request.UserId), "Failed to remove a role to the requested user.");
    }
}