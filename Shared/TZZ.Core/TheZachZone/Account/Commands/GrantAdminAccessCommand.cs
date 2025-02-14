using MediatR;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using static TZZ.Common.Shared.Enums.ZachZoneConstants;

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
        if (await identityService.HasClaim(request.UserId, ClaimTypes.Role, Roles.Admin))
        {
            throw new ArgumentException("The requested user already has the requested role.", nameof(GrantAdminAccessCommand.UserId));
        }

        var result = await identityService.AddClaim(request.UserId, ClaimTypes.Role, Roles.Admin);
                
        return result ? ZachZoneCommandResponse.Success(request.UserId) 
            : ZachZoneCommandResponse.Failure<int>(nameof(request.UserId), "Failed to add a role to the requested user.");
    }
}