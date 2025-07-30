using MediatR;
using TZZ.Common.Enums.Constants;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Queries;

public class UserInfoDto
{
  public string Email { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
}

public class GetUserInfoQuery : IRequest<ZachZoneCommandResponse<UserInfoDto>>
{
}

public class GetUserInfoQueryHandler(ICurrentUserService currentUserService, IIdentityService identityService)
  : IRequestHandler<GetUserInfoQuery, ZachZoneCommandResponse<UserInfoDto>>
{
  public async Task<ZachZoneCommandResponse<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
  {
    if (!currentUserService.IsAuthenticated)
    {
      return ZachZoneCommandResponse.Failure<UserInfoDto>("summary", "User is not logged in.");
    }

    var user = await identityService.GetUser(currentUserService.Email!).ConfigureAwait(false);
    if (user is null)
    {
      return ZachZoneCommandResponse.Failure<UserInfoDto>("summary", "User is not logged in.");
    }

    var role = await identityService.GetClaim(user!.Id, ClaimTypes.Role).ConfigureAwait(false);
    return ZachZoneCommandResponse.Success(new UserInfoDto()
    {
      Email = user.Email!,
      Role = role ?? "N/A"
    });
  }
}
