using MediatR;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using static TZZ.Common.Shared.Enums.ZachZoneConstants;

namespace TZZ.Core.TheZachZone.Account.Queries;

public record UserInfoDto(string Email, string Role);
public class GetUserInfoQuery : IRequest<ZachZoneCommand<UserInfoDto>>
{
}

public class GetUserInfoQueryHandler(ICurrentUserService _currentUserService, IIdentityService _identityService)
    : IRequestHandler<GetUserInfoQuery, ZachZoneCommand<UserInfoDto>>
{
    public async Task<ZachZoneCommand<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            return ZachZoneCommand.Failure<UserInfoDto>("summary", "User is not logged in.");
        }

        var user = await _identityService.GetUser(_currentUserService.Email!);
        var role = await _identityService.GetClaim(user.Id, ClaimTypes.Role);
        return ZachZoneCommand.Success(new UserInfoDto(user!.Email!, role ?? "N/A"));
    }
}
