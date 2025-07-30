using MediatR;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class LogoutCommand : IRequest<ZachZoneCommandResponse>
{
}

public class LogoutCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService) : IRequestHandler<LogoutCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
  {
    if (currentUserService.IsAuthenticated)
    {
      await identityService.LogoutCurrentUser();
    }

    return ZachZoneCommandResponse.Success();
  }
}