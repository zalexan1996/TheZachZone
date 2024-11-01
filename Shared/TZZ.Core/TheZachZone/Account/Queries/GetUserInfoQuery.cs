using MediatR;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using static TZZ.Common.Shared.Enums.ZachZoneConstants;

namespace TZZ.Core.TheZachZone.Account.Queries;

public class UserInfoDto
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class GetUserInfoQuery : IRequest<ZachZoneCommandResponse<UserInfoDto>>
{
}

public class GetUserInfoQueryHandler(ICurrentUserService _currentUserService, IIdentityService _identityService)
    : IRequestHandler<GetUserInfoQuery, ZachZoneCommandResponse<UserInfoDto>>
{
    public async Task<ZachZoneCommandResponse<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            return ZachZoneCommandResponse.Failure<UserInfoDto>("summary", "User is not logged in.");
        }

        var user = await _identityService.GetUser(_currentUserService.Email!);
        if (user is null)
        {
            return ZachZoneCommandResponse.Failure<UserInfoDto>("summary", "User is not logged in.");
        }

        var role = await _identityService.GetClaim(user!.Id, ClaimTypes.Role);
        return ZachZoneCommandResponse.Success(new UserInfoDto()
        {
            Email = user.Email!,
            Role = role ?? "N/A"
        });
    }
}
