using Ardalis.GuardClauses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class ResetPasswordCommand : IRequest<ZachZoneCommand>
{
    public required string ResetToken { get; set; }
    public required string NewPassword { get; set; }
    public required string ConfirmPassword { get; set; }
}

public class ResetPasswordCommandHandler(ICurrentUserService _currentUserService, IIdentityService _identityService)
    : IRequestHandler<ResetPasswordCommand, ZachZoneCommand>
{
    public async Task<ZachZoneCommand> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        Guard.Against.Null(userId, null, "User not found.");

        var result = await _identityService.ResetPassword(userId.Value, request.ResetToken, request.NewPassword);

        return result ? ZachZoneCommand.Success() : ZachZoneCommand.Failure("summary", "Failed to reset password.");
    }
}
