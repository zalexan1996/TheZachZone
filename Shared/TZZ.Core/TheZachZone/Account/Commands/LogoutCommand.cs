using MediatR;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class LogoutCommand : IRequest<ZachZoneCommandResponse>
{
}

public class LogoutCommandHandler(IIdentityService _identityService, ICurrentUserService _currentUserService) : IRequestHandler<LogoutCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (_currentUserService.IsAuthenticated)
        {
            await _identityService.LogoutCurrentUser();
        }

        return ZachZoneCommandResponse.Success();
    }
}